/// <summary>
/// DeplacementsFourmisScript.cs
/// Script pour gérer les déplacements des fourmis sur le terrain avec entre autres
/// 	- le déplacement à proprement parler
/// 	- la connaissance du terrain de la fourmis et de la place qu'elle a
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 2.4.0
/// </remarks>

using UnityEngine;
using System.Collections;

/// <summary>
/// Script pour gérer le déplacement d'une fourmis
/// </summary>
/// <see cref="TerrainManagerScript"/>
/// <see cref="HexagonesVueScript"/>
/// <see cref="HexagoneInfo"/> 
public class DeplacementsFourmisScript : MonoBehaviour {


	/* ********* *
	 * Attributs *
	 * ********* */

#region Attributs privés
	/// <summary>
	/// Flag indiquant que l'objet est en mouvement ou non
	/// Flag mis à jour régulièrement.
	/// </summary>
	private bool enMouvement;
	
	/// <summary>
	/// Flag indiquant que l'objectif, i.e. la position de la case à atteindre
	/// a été atteint.
	/// Flag mis à jour régulièrement
	/// </summary>
	private bool objectifAtteint;

	/// <summary>
	/// La position que doit atteindre l'objet, i.e. le centre d'une case.
	/// Position mise à jour régulièrement.
	/// </summary>
	private Vector3 positionAatteindre;
	
	/// <summary>
	/// L'orientation courante de l'objet
	/// </summary>
	// FIXME pas très propre
	private TypesRotations orientationCourante;

	/// <summary>
	/// Flag indiquant que la fourmis doit se stopper à cause d'une collision
	/// </summary>
	private bool stopCollision;
#endregion

#region Attributs publics
	/// <summary>
	/// La vitesse maximale de déplacement.
	/// 2 ou 3 pour une fourmis peut convenir.
	/// </summary>
	public float vitesseMax;

	/// <summary>
	/// La distance pour aller du centre d'une case à un autre
	/// </summary>
	public const int DISTANCE_CASE = 5;

	// FIXME Mettre ça dans les scripts modéliser les fourmis (vie, attaque, pods, PTAC, ...)
	/// <summary>
	/// La visée maximale d'une ouvrière en termes de cases
	/// </summary>
	public const int VISEE_MAX_OUVRIERE = 2;
	/// <summary>
	/// La visée maximale d'une contremaitre en termes de cases
	/// </summary>
	public const int VISEE_MAX_CONTREMAITRESSE = 4;
	/// <summary>
	/// La visée maximale d'une soldate en termes de cases
	/// </summary>
	public const int VISEE_MAX_SOLDATE = 2;
	/// <summary>
	/// La visée maximale d'une générale en termes de cases
	/// </summary>
	public const int VISEE_MAX_GENERALE = 4;
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes privées
	/// <summary>
	/// Récupère le morceau de terrain, i.e. le bloc, sur lequel est posée la fourmis,
	/// c'est à dire le bloc ayant un pool d'hexagones de meme texture.
	/// </summary>
	/// <returns>Le bloc de terrain en tant que GameObject, null si aucun objet en dessous</returns>
	private GameObject GetBlocCourantAsGO(){
		// Lancement d'un rayon vers la case sous la fourmis
		RaycastHit hit;
		if ( Physics.Raycast(transform.position, -Vector3.up, out hit, 100.0F) ){
			// Récupération du morceau de terrain contenant plusieurs hexagones
			return hit.transform.gameObject;
		} else {
			return null;
		}
	}

	/// <summary>
	/// Récupère le morceau de terrain, i.e. le bloc, sur lequel est posée la fourmis,
	/// c'est à dire le bloc ayant un pool d'hexagones de meme texture.
	/// </summary>
	/// <returns>Le bloc de terrain en tant que string, au format JSON</returns>
	private string GetBlocCourantAsString(){
		GameObject goBlocTerrain = GetBlocCourantAsGO();
		return JSONUtils.parseBlocTerrain(goBlocTerrain);
	}

	/// <summary>
	/// Retourne la position courante de l'objet
	/// </summary>
	private void AfficherPositionCourante(){
		Debug.Log("BlocTerrain :" + GetBlocCourantAsGO());
		Debug.Log("BlocTerrain :" + GetBlocCourantAsString());
		Debug.Log("Infos :" + Get3dInfos());
	}

	/// <summary>
	/// Idéalement, dès qu'un objet apparait sur le terrain, il faut le recentrer sur un hexagone.
	/// Pour cela, la classe utilitaire TerrainUtils permet de trouver l'hexagone le plus proche.
	/// Une fois cet hexagone trouvé, il faut recentrer l'objet courant dessus une bonne fois pour toute.
	/// Par la suite, il suffirait de déplacer l'objet courant avec une distance correspondant à
	/// un multiple de la distance entre deux centres d'hexagones afin d'avoir un objet toujours centré.
	/// </summary>
	/// <remarks>
	/// Il vaut mieu appeler cette fonction un minimum de fois car l'opéraiton est gourmande.
	/// </remarks>
	private void Recentrer(){
		HexagoneInfo hexPlusProche = TerrainUtils.HexagonePlusProche(transform.localPosition);
		//Debug.Log("Recentrage de l="+transform.localPosition+" vers "+hexPlusProche.positionGlobale);
		transform.localPosition = hexPlusProche.positionLocaleSurTerrain;
	}

	/// <summary>
	/// Effectue une rotation de la fourmis.
	/// Les valeurs utilisées pour la rotation sont des valeurs euleriennes,
	/// elles peuvent etre récupérées par transform.rotation.eulerAngles.
	/// </summary>
	/// <param name="rotation">Le sens de rotation</param>
	private void FaireRotation( TypesRotations rotation ){
		TypesRotations r;
		if (rotation == TypesRotations.RANDOM){
			// Définition de la rotation : plage de valeur définie dans[-135;+135]
			// avec un pas de +45. L'idée est de tirer un nombre qui servira de coefficient
			// multiplicateur pour 45
			int typeRotation = Random.Range(-3, +3);
			int angle = (+45)*typeRotation;
			// Problem si on tire un angle de 0 ou de 180
			if ( angle == 0 ) angle = 90; 
			if ( angle == 180 ) angle = -90; 
			r = (TypesRotations)angle;
		} else {
			r = rotation;
		}
		orientationCourante = r;
		transform.rotation = Quaternion.identity;
		switch (r){
			case TypesRotations.NORD:
				transform.Rotate(0,-90,0);
				break;
			case TypesRotations.NORD_OUEST:
				transform.Rotate(0,-135,0);
				break;
			case TypesRotations.SUD_OUEST:
				transform.Rotate(0,+135,0);
				break;
			case TypesRotations.SUD:
				transform.Rotate(0,+90,0);
				break;
			case TypesRotations.SUD_EST:
				transform.Rotate(0,+45,0);
				break;
			case TypesRotations.NORD_EST:
				transform.Rotate(0,-45,0);
				break;
			case TypesRotations.AUCUN:
				break;
			default:
				Debug.LogError("ERREUR: FaireRotation() : Rotation non gérée :"+r);
				break;
		}
		//Debug.Log("Rotation actuelle en Y : " + r);
	}

	/// <summary>
	/// Routine appelée par la routine Unity Update().
	/// Fait déambuler la fourmis.
	/// Pour ce faire, va faire une rotation ou non, et va faire avancer la fourmis
	/// d'un certain nombre de case.
	/// </summary>
	private void Deambuler(){

		stopCollision = false;

		transform.position = Vector3.Lerp(transform.position, positionAatteindre, 0.03f);

		if ( Vector3.Distance(transform.position, positionAatteindre) <= 1 ){
			enMouvement = false;
			objectifAtteint = true;
		}
		
/*
/////////////////////////
// CODE OK, merci de ne pas supprimer pour le moment (Pierre-Yves)

		/*
		 * Déplacement de la fourmis
		 * http://answers.unity3d.com/questions/195698/stopping-a-rigidbody-at-target.html
		 * /

		float velocityFinale = 2.5f;	// Pour convertir la distance restant pour avoir la velocité finale (= façon de stopper)
		float forceMax = 40.0f;
		float gain = 5f; 				// Gain par rapport à la position finale à atteindre

		//Debug.Log ("Je suis en :" + transform.position + ", et dois aller en :" + positionAatteindre);
		Vector3 distance = positionAatteindre - transform.position;
		distance.y = 0;
		// Calcul de la vitesse à atteindre proportionnellement à la distance, bornée par vietsseMax
		Vector3 vitesseVoulue = Vector3.ClampMagnitude(velocityFinale * distance, vitesseMax);
		// Calcul de l'erreur que l'on a avec la vitesse
		Vector3 correctionVitesse = vitesseVoulue - rigidbody.velocity;
		// Calcul d'une force proporitonnele à l'erreur, bornée par maxForce
		Vector3 force = Vector3.ClampMagnitude(gain * correctionVitesse, forceMax);
		rigidbody.AddForce(force);

		if (System.Math.Round(Vector3.Distance(transform.position,positionAatteindre)) == 0){
			//Debug.Log ("Objectif atteint");
			enMouvement = false;
			objectifAtteint = true;
			Recentrer();
		}/* else {
			Debug.Log ("Objectif pas encore atteint : "+Vector3.Distance(transform.position,positionAatteindre));
		}* /

		//Recentrer();
		
/////////////////////////
*/

	}
#endregion

#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity au démarrage du script
	/// </summary>
	void Awake(){
		enMouvement = false;
		objectifAtteint = false;
		stopCollision = false;
	}

	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame
	/// </summary>
	void Update(){
		HexagoneCourant();
		// A chaque frame, continuer la déambulation selon les flags
		if (enMouvement && !objectifAtteint){
			Debug.Log("Déambulation");
			Deambuler();
		// Ou en relancer une nouvelle (si la fourmis ne bouge plus)
		} else {
			if ( ! stopCollision ){
				// FIXME Coupler ça avec l'IA
				Debug.Log("Plus de déambulation, redémarrage");
				int nombreDesCases = Random.Range(1,VISEE_MAX_OUVRIERE);
				FaireRotation(TypesRotations.RANDOM);
				Avancer(nombreDesCases);
				//Recentrer();
				//Stopper();
			}
		}
	}
#endregion

#region Méthodes publiques
	/// <summary>
	/// Fait avancer la fourmis de nbCases cases.
	/// Si nbCases est à <= 0, l'objet ne bouge plus
	/// </summary>
	/// <param name="nbCases">Le nombre de cases à avancer</param>
	public void Avancer( int nbCases ){

		// Arret de l'objet
		if  (nbCases <= 0 ){
			Debug.Log("STOP !");
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			enMouvement = false;
			objectifAtteint = true;
			return;
		}

		//Debug.Log("Dois avancer de "+nbCases+" cases");
		positionAatteindre = transform.position;

		switch (orientationCourante){
			case TypesRotations.NORD:
				positionAatteindre.x += (-1) * nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				break;
			case TypesRotations.NORD_EST:
				positionAatteindre.x += (-1) * nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				positionAatteindre.z += nbCases*DISTANCE_CASE;
				break;
			case TypesRotations.NORD_OUEST:
				positionAatteindre.x += (-1) * nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				positionAatteindre.z += (-1) * nbCases*DISTANCE_CASE;
				break;
			case TypesRotations.SUD:
				positionAatteindre.x += nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				break;
			case TypesRotations.SUD_EST:
				positionAatteindre.x += nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				positionAatteindre.z += nbCases*DISTANCE_CASE;
				break;
			case TypesRotations.SUD_OUEST:
				positionAatteindre.x += nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				positionAatteindre.z += (-1) * nbCases*DISTANCE_CASE;
				break;
			case TypesRotations.AUCUN:
				// TODO
				break;
			default:
				Debug.LogError("ERREUR: Valeur pas gérée dans switch : "+orientationCourante);
				break;
		}

		enMouvement = true;
		objectifAtteint = false;
		rigidbody.isKinematic = false;
		//Debug.Log("Je suis en " + transform.position + ", je dois aller en " + positionAatteindre);

	}

	/// <summary>
	/// Stoppe l'objet en mouvement
	/// </summary>
	public void Stopper(){
		Avancer(-1);
	}

	/// <summary>
	/// Stoppe l'objet en mouvement à cause d'une collision
	/// </summary>
	/// <param name="causeCollision">
	/// L'objet qui a causé la collision. En fonction de cet objet, l'objet courant
	/// bouge différement (ex: ne pas s'acharner à aller sur un coté du bac à sable)
	/// </param>
	public void StopperParCollision( TypesObjetsRencontres causeCollision ){
		stopCollision = true;
		// Arret de l'objet
		Avancer(-1);
		// Changement de direction su on tape dans un coté du bac à sable
		switch (causeCollision){
			case TypesObjetsRencontres.COTE_BAC_1:
				FaireRotation(TypesRotations.SUD_EST);
				Avancer(Random.Range(1,VISEE_MAX_OUVRIERE));
				break;
			case TypesObjetsRencontres.COTE_BAC_2:
				FaireRotation(TypesRotations.SUD_OUEST);
				Avancer(Random.Range(1,VISEE_MAX_OUVRIERE));
				break;
			case TypesObjetsRencontres.COTE_BAC_3:
				FaireRotation(TypesRotations.SUD);
				Avancer(Random.Range(1,VISEE_MAX_OUVRIERE));
				break;
			case TypesObjetsRencontres.COTE_BAC_4:
				FaireRotation(TypesRotations.NORD);
				Avancer(Random.Range(1,VISEE_MAX_OUVRIERE));
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// Retourne l'hexagone sur lequel est la fourmis
	/// </summary>
	/// <returns>L'hexagone sur lequel est la fourmis</returns>
	public HexagoneInfo HexagoneCourant(){
		HexagoneInfo hexagoneCourant = TerrainUtils.HexagonePlusProche(transform.localPosition);
		//Debug.Log("Hexagone courant : pos=" + hexagoneCourant.positionLocaleSurTerrain + "/ texture=" + hexagoneCourant.GetTypeTerrain());
		return hexagoneCourant;
	}

	/// <summary>
	/// Retourne les infos 3D de la fourmis à savoir sa rotation en (x,y,z) et sa position en (x,y,z).
	/// String de  la forme :
	/// 
	/// 	{position:{x:XXX,y:YYY,z:ZZZ},rotation:{x:UUU,y:VVV,z:WWW}}
	/// 
	/// </summary>
	/// <returns>Un string au format JSON</returns>
	public string Get3dInfos(){
		return JSONUtils.parseInfos3D(gameObject.transform.position, gameObject.transform.rotation);
	}
#endregion

}

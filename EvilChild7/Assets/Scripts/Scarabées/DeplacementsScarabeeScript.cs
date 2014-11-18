/// <summary>
/// DeplacementsScarabeeScript.cs
/// Script pour gérer les déplacements des scarabée sur le terrain avec entre autres
/// 	- le déplacement à proprement parler
/// 	- la connaissance du terrain de la fourmis et de la place qu'elle a
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;
using System.Collections;

#region DeplacementsScarabeeScript
/// <summary>
/// Script pour gérer le déplacement d'un scarabée
/// </summary>
public class DeplacementsScarabeeScript : MonoBehaviour {


	/* ********* *
	 * Attributs *
	 * ********* */

#region Attributs privés
	/// <summary>
	/// La position que doit atteindre l'objet, i.e. le centre d'une case.
	/// Position mise à jour régulièrement.
	/// </summary>
	private Vector3 positionAatteindre;

	/// <summary>
	/// Distance de la translation à effectuer de la positon courante de départ
    /// à la positon finale à atteindre
	/// </summary>
	private float distanceTranslation;

	/// <summary>
	/// Le début au début de la translation
	/// </summary>
	private float tempsDebutTranslation;
#endregion

#region Constantes privées
	/// <summary>
	/// La distance pour aller du centre d'une case à un autre, égale à 5
	/// </summary>
	public static /*const*/ int DISTANCE_CASE = 5;

	/// <summary>
	/// Le nombre de case maximum que peut parcourir la fourmi en une traite
	/// </summary>
	private const int AVANCEMENT_CASE = 1;

	/// <summary>
	/// Un coefficient pour la vitesse de déplacement l'objet
	/// </summary>
	private const float COEFF_VITESSE = 0.5f;

	/// <summary>
	/// La vitesse du scarabée
	/// </summary>
	private const int VITESSE = 2;
#endregion

#region Attributs publics
	/// <summary>
	/// Flag indiquant que l'objet est en mouvement ou non
	/// Flag mis à jour régulièrement.
	/// </summary>
	[HideInInspector]
	public bool enMouvement;
	
	/// <summary>
	/// Flag indiquant que l'objectif, i.e. la position de la case à atteindre
	/// a été atteint.
	/// Flag mis à jour régulièrement
	/// </summary>
	[HideInInspector]
	public bool objectifAtteint;
	
	/// <summary>
	/// L'orientation courante de l'objet
	/// </summary>
	[HideInInspector]
	public TypesRotations orientationCourante;
#endregion

	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes privées
	/// <summary>
	/// Récupère le morceau de terrain, i.e. le bloc, sur lequel est posée le scarabée,
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
	/// Récupère le morceau de terrain, i.e. le bloc, sur lequel est posée le scarabée,
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
	/// Il vaut mieux appeler cette fonction un minimum de fois car l'opéraiton est gourmande.
	/// </remarks>
	private void Recentrer(){
		HexagoneInfo hexPlusProche = TerrainUtils.HexagonePlusProche(transform.localPosition);
		//Debug.Log("Recentrage de l="+transform.localPosition+" vers "+hexPlusProche.positionGlobale);
		transform.localPosition = hexPlusProche.positionLocaleSurTerrain;
	}

	/// <summary>
	/// Effectue une rotation du scarabée.
	/// Les valeurs utilisées pour la rotation sont des valeurs euleriennes,
	/// elles peuvent etre récupérées par transform.rotation.eulerAngles.
	/// </summary>
	/// <param name="rotation">Le sens de rotation</param>
	//private void FaireRotation( TypesRotations rotation ){
	public void FaireRotation( TypesRotations rotation ){
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
				//Debug.LogError("Aucune rotation ?");
				break;
			default:
				Debug.LogError("ERREUR: FaireRotation() : Rotation non gérée :"+r);
				break;
		}
		//Debug.Log("Rotation actuelle en Y : " + r);
	}

	/// <summary>
	/// Routine appelée par la routine Unity Update().
	/// Fait déambuler le scarabée.
	/// Pour ce faire, va faire une rotation ou non, et va faire avancer le scarabée
	/// d'un certain nombre de case.
	/// </summary>
	//private void Deambuler(){
	public void Deambuler(){
		if (enMouvement && !objectifAtteint) {

			//transform.position = Vector3.Lerp (transform.position, positionAatteindre, COEFF_VITESSE * VITESSE);
			float distCovered = (Time.time - tempsDebutTranslation) * COEFF_VITESSE * VITESSE;/*speed*/;
			float fracJourney = distCovered / distanceTranslation;
			transform.position = Vector3.Lerp(transform.position, positionAatteindre, fracJourney);

			//Debug.Log("Déambuler");
			//Debug.Log("Je suis en "+transform.position+" et dois aller en "+ positionAatteindre);
			//Debug.Log("Distance :"+Vector3.Distance (transform.position, positionAatteindre));

			if ( Mathf.Abs(transform.position.x-positionAatteindre.x) < 1
			    && Mathf.Abs(transform.position.z-positionAatteindre.z) < 1 ){
				enMouvement = false;
				objectifAtteint = true;
				Avancer(-1);
			}

		}
	}
#endregion

#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity au démarrage du script
	/// </summary>
	void Awake(){
		enMouvement = false;
		objectifAtteint = true;
	}

	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame
	/// </summary>
	void FixedUpdate(){
		Deambuler();
	}
#endregion

#region Méthodes publiques
	/// <summary>
	/// Fait avancer le scarabée de nbCases cases.
	/// Si nbCases est à <= 0, l'objet ne bouge plus
	/// </summary>
	/// <param name="nbCases">Le nombre de cases à avancer</param>
	public void Avancer( int nbCases ){

		// Arret de l'objet
		if  (nbCases <= 0 ){
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
				//Debug.LogError("Aucune rotation ?");
				//FIXME : Cas ne devant jamais apapraitre normalement
				positionAatteindre.x += (-1) * nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				positionAatteindre.z += (-1) * nbCases*DISTANCE_CASE;
				break;
			default:
				Debug.LogError("ERREUR: Valeur non gérée dans switch : "+orientationCourante);
				break;
		}

		enMouvement = true;
		objectifAtteint = false;
		rigidbody.isKinematic = false;

		distanceTranslation = Vector3.Distance(transform.position, positionAatteindre);
		tempsDebutTranslation = Time.time;
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
		//Debug.Log("Stop par collision");
		// Arret de l'objet
		Avancer(-1);
		// Changement de direction su on tape dans un coté du bac à sable
		switch (causeCollision){
			case TypesObjetsRencontres.COTE_BAC_1:
				FaireRotation(TypesRotations.SUD_EST);
				Avancer(AVANCEMENT_CASE);
				break;
			case TypesObjetsRencontres.COTE_BAC_2:
				FaireRotation(TypesRotations.SUD_OUEST);
				Avancer(AVANCEMENT_CASE);
				break;
			case TypesObjetsRencontres.COTE_BAC_3:
				FaireRotation(TypesRotations.SUD);
				Avancer(AVANCEMENT_CASE);
				break;
			case TypesObjetsRencontres.COTE_BAC_4:
				FaireRotation(TypesRotations.NORD);
				Avancer(AVANCEMENT_CASE);
				break;
			case TypesObjetsRencontres.PETIT_CAILLOU:
			case TypesObjetsRencontres.CAILLOU:
			case TypesObjetsRencontres.TRES_GROS_CAILLOUX:
			case TypesObjetsRencontres.EAU:
			case TypesObjetsRencontres.EAU3D:
				FaireRotation(TypesRotations.RANDOM);
				Avancer(AVANCEMENT_CASE);
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// Retourne l'hexagone sur lequel est le scarabée
	/// </summary>
	/// <returns>L'hexagone sur lequel est le scarabée</returns>
	public HexagoneInfo HexagoneCourant(){
		HexagoneInfo hexagoneCourant = TerrainUtils.HexagonePlusProche(transform.localPosition);
		//Debug.Log("Hexagone courant : pos=" + hexagoneCourant.positionLocaleSurTerrain + "/ texture=" + hexagoneCourant.GetTypeTerrain());
		return hexagoneCourant;
	}

	/// <summary>
	/// Retourne les infos 3D du scarabée à savoir sa rotation en (x,y,z) et sa position en (x,y,z).
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
#endregion 

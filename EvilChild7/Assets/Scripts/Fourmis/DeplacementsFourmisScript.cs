/// <summary>
/// DeplacementsFourmisScript.cs
/// Script pour gérer les déplacements des fourmis sur le terrain avec entre autres
/// 	- le déplacement à proprement parler
/// 	- la connaissance du terrain de la fourmis et de la place qu'elle a
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 2.1.0
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
	/// Flag pour indiquer si l'objet a été rencentré ou pas.
	/// </summary>
	private bool recentrageFait;

	/// <summary>
	/// Flag indiquant que l'objet est en mouvement ou non
	/// </summary>
	private bool enMouvement;
	
	/// <summary>
	/// Flag indiquant que l'objectif, i.e. la position de la case à atteindre
	/// a été atteint
	/// </summary>
	private bool objectifAtteint;

	/// <summary>
	/// La position que doit atteindre l'objet, i.e. le centre d'une case
	/// </summary>
	private Vector3 positionAatteindre;
#endregion

#region Attributs publics
	/// <summary>
	/// La vitesse de déplacement
	/// </summary>
	public float deplacementVitesse;

	/// <summary>
	/// La distance pour aller du centre d'une case à un autre
	/// </summary>
	public const int DISTANCE_CASE = 5;
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes privées
	/// <summary>
	/// Récupère le morceau de terrain, i.e. le bloc, sur lequel est posée la fourmis,
	/// c'est à dire le bloc ayant un pool d'hexagones de meme texture.
	/// </summary>
	/// <returns>Le bloc de terrain en tant que GameObject</returns>
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
		HexagoneInfo hexPlusProche = TerrainUtils.hexagonePlusProche(transform.position);
		transform.position = hexPlusProche.positionGobale;
	}

	/// <summary>
	/// Effectue une rotation de la fourmis
	/// </summary>
	/// <param name="rotation">Le sens de rotation</param>
	private void FaireRotation( Rotation rotation ){
		Rotation r;
		if (rotation == Rotation.RANDOM) {
			int typeRotation = Random.Range(1, 6);
			r = (Rotation)typeRotation;
		} else {
			r = rotation;
		}
		switch (r){
			case Rotation.NORD:
				//transform.rotation = Quaternion.Euler(0,-90,0);
				transform.Rotate(0,-90,0);
				break;
			case Rotation.NORD_OUEST:
				//transform.rotation = Quaternion.Euler(0,-135,0);				
				transform.Rotate(0,-135,0);
				break;
			case Rotation.SUD_OUEST:
				//transform.rotation = Quaternion.Euler(0,+135,0);
				transform.Rotate(0,+135,0);
				break;
			case Rotation.SUD:
				//transform.rotation = Quaternion.Euler(0,+90,0);
				transform.Rotate(0,+90,0);
				break;
			case Rotation.SUD_EST:
				//transform.rotation = Quaternion.Euler(0,+45,0);
				transform.Rotate(0,+45,0);
				break;
			case Rotation.NORD_EST:
				//transform.rotation = Quaternion.Euler(0,-45,0);
				transform.Rotate(0,-45,0);
				break;
			default:
				Debug.Log("ERREUR: FaireRotation() : Rotation non gérée :"+r);
				break;
		}
	}

	/// <summary>
	/// Routine appelée par la routine Unity Update().
	/// Fait déambuler la fourmis.
	/// Pour ce faire, va faire une rotation ou non, et va faire avancer la fourmis
	/// d'un certain nombre de case.
	/// </summary>
	private void Deambuler(){

		// FIXME
		/*
		 * Réalisation d'une rotation (random)
		 */
		//Rotation rotation = Rotation.AUCUN;
		//FaireRotation(rotation);

		/*
		 * Déplacement de la fourmis
		 * http://answers.unity3d.com/questions/195698/stopping-a-rigidbody-at-target.html
		 */

		float velocityFinale = 2.5f;	// Pour convertir la distance restant pour avoir la velocité finale (= façon de stopper)
		float vitesseMax = 15.0f;
		float forceMax = 40.0f;
		float gain = 5f; 				// Gain par rapport à la position finale à atteindre

		//Debug.Log ("Je suis en :" + transform.position + ", et dois aller en :" + positionAatteindre);
		Vector3 distance = positionAatteindre - transform.position;
		distance.y = 0; // ignore height differences
		// Calcul de la vitesse à atteindre proportionnellement à la distance, bornée par vietsseMax
		Vector3 vitesseVoulue = Vector3.ClampMagnitude(velocityFinale * distance, vitesseMax);
		// Calcul de l'erreur que l'on a avec la vitesse
		Vector3 correctionVitesse = vitesseVoulue - rigidbody.velocity;
		// Calcul d'une force proporitonnele à l'erreur, bornée par maxForce
		Vector3 force = Vector3.ClampMagnitude(gain * correctionVitesse, forceMax);
		rigidbody.AddForce(force);

		Debug.Log("Je déambule de "+transform.position+" vers "+positionAatteindre );
		if (Vector3.Distance(transform.position,positionAatteindre) <= 0.02){
			//Debug.Log ("Objectif atteint");
			enMouvement = false;
			objectifAtteint = true;
		}/* else {
			Debug.Log ("Objectif pas encore atteint : "+Vector3.Distance(transform.position,positionAatteindre));
		}*/

	}
#endregion

#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity au démarrage du script
	/// </summary>
	void Awake(){
		recentrageFait = false;
		enMouvement = false;
		objectifAtteint = false;
		Avancer(5);// DEBUG
	}

	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame
	/// </summary>
	void Update(){
		if (! recentrageFait){
			Recentrer();
			recentrageFait = true;
		}
		// A chaque frame, continuer la déambulation selon les flags
		if (enMouvement && !objectifAtteint){
			Deambuler();
		// Ou en relancer une nouvelle (si la fourmis ne bouge plus)
		} else {
			Debug.Log("Plus de déambulation, redémarrage");
			int nombreDesCases = Random.Range(1,6);
			Avancer(nombreDesCases);
			FaireRotation(Rotation.RANDOM);
			Recentrer();
			//Stopper();
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
		if  (nbCases <= 0 ){
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			rigidbody.isKinematic = true;
			enMouvement = false;
			objectifAtteint = true;
			return;
		}
		Debug.Log ("Dois avancer de "+nbCases+" cases");
		// Changer le X et le Z par rapport à l'angle
		positionAatteindre = transform.position;
		Rotation r = (Rotation)transform.rotation.y;
		switch (r){
			case Rotation.NORD:
				positionAatteindre.x += (-1) * nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				break;
			case Rotation.NORD_EST:
				positionAatteindre.x += (-1) * nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				positionAatteindre.z += nbCases*DISTANCE_CASE;
				break;
			case Rotation.NORD_OUEST:
				positionAatteindre.x += (-1) * nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				positionAatteindre.z += (-1) * nbCases*DISTANCE_CASE;
				break;
			case Rotation.SUD:
				positionAatteindre.x += nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				break;
			case Rotation.SUD_EST:
				positionAatteindre.x += nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				positionAatteindre.z += nbCases*DISTANCE_CASE;
				break;
			case Rotation.SUD_OUEST:
				positionAatteindre.x += nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				positionAatteindre.z += (-1) * nbCases*DISTANCE_CASE;
				break;
			default:
				Debug.Log("ERREUR: BIG JERK, valeur pas gérée dans switch:"+transform.rotation.y);
				break;
		}
		enMouvement = true;
		objectifAtteint = false;
		rigidbody.isKinematic = false;
		Debug.Log("Je suis en " + transform.position + ", je dois aller en " + positionAatteindre);
	}

	/// <summary>
	/// Stoppe l'objet en mouvement
	/// </summary>
	public void Stopper(){
		Avancer(-1);
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

#region Rotation
/// <summary>
/// Les types de rotations possibles
/// </summary>
public enum Rotation : int { 
	/// <summary>
	/// Rotation aléatoire
	/// </summary>
	RANDOM = -1,
	/// <summary>
	/// Pas de rotation
	/// </summary>
	AUCUN = 0,
	/// <summary>
	/// Nord
	/// Typiquement, tete de la fourmis vers la direction -X, de bas en haut
	/// </summary>
	NORD = 1, 
	/// <summary>
	/// Nord Est
	/// </summary>
	NORD_EST = 2,
	/// <summary>
	/// Sud Est
	/// </summary>
	SUD_EST = 3,
	/// <summary>
	/// Sud : direction X, tete de la fourmis vers X, de haut en bas
	/// </summary>
	SUD = 4,
	/// <summary>
	/// Nord Ouest
	/// </summary>
	NORD_OUEST = 5,
	/// <summary>
	/// Sud Ouest
	/// </summary>
	SUD_OUEST = 6
}
#endregion

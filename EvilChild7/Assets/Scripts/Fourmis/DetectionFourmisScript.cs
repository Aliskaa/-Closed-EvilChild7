/// <summary>
/// DetectionFourmisScript.cs
/// Script pour gérer les détections d'objets avec l'odorat et la vue ains que les collisions des fourmis
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 3.1.0
/// </remarks>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Classe pour gérer les détections d'objets avec l'odorat et la vue ains que les collisions des fourmis
/// </summary>
public class DetectionFourmisScript : MonoBehaviour {


	/* ********* *
	 * Attributs *
	 * ********* */

#region Attributs privés
	/// <summary>
	/// Référence vers le script de déplacement de l'objet
	/// </summary>
	private DeplacementsFourmisScript scriptDeplacement;

	/// <summary>
	/// Une lite d'objets qui ont été vus et qui n'ont pas encore été signalé à "l'IA"
	/// </summary>
	private List<Cible> objetsDetectes;
#endregion


#region Constantes publiques
	/// <summary>
	/// La longueur de visée pour une ouvrière
	/// </summary>
	public const float LONGUEUR_VISEE_OUVRIERE 
		= DeplacementsFourmisScript.VISEE_MAX_OUVRIERE*DeplacementsFourmisScript.DISTANCE_CASE;
	/// <summary>
	/// La longueur de visée pour une contremaitresse
	/// </summary>
	public const float LONGUEUR_VISEE_CONTREMAITRESSE 
		= DeplacementsFourmisScript.VISEE_MAX_CONTREMAITRESSE*DeplacementsFourmisScript.DISTANCE_CASE;
	/// <summary>
	/// La longueur de visée pour une soldate
	/// </summary>
	public const float LONGUEUR_VISEE_SOLDATE 
		= DeplacementsFourmisScript.VISEE_MAX_SOLDATE*DeplacementsFourmisScript.DISTANCE_CASE;
	/// <summary>
	/// La longueur de visée pour une générale
	/// </summary>
	public const float LONGUEUR_VISEE_GENERALE 
		= DeplacementsFourmisScript.VISEE_MAX_GENERALE*DeplacementsFourmisScript.DISTANCE_CASE;
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */
	
#region Méthodes privées
	/// <summary>
	/// Méthode pour détecter des objets sur un axe donné.
	/// Equivaut à la vue et à l'odorat
	/// </summary>
	/// <param name="direction">La direction de détection.</param>
	/// <param name="visee">La longueur du raydon de visée</param>
	private void VoirEtSentir( TypesAxes direction, float visee ){

		// Convertion direction <-> vecteur
		Color debugRayColor = Color.black;
		Vector3 directionV;
		switch ( direction ){
			case TypesAxes.DEVANT:
				debugRayColor = Color.blue;
				directionV = Vector3.forward;
				break;
			case TypesAxes.DERRIERE:
				debugRayColor = Color.green;
				directionV = Vector3.back;
				break;
			case TypesAxes.DERRIERE_DROITE:
				debugRayColor = Color.red;
				directionV = Vector3.back + Vector3.right;
				break;
			case TypesAxes.DERRIERE_GAUCHE:
				debugRayColor = Color.yellow;
				directionV = Vector3.back + Vector3.left;
				break;
			case TypesAxes.DEVANT_DROITE:
				debugRayColor = Color.white;
				directionV = Vector3.forward + Vector3.right;
				break;
			case TypesAxes.DEVANT_GAUCHE:
				debugRayColor = Color.magenta;
				directionV = Vector3.forward + Vector3.left;
				break;
			case TypesAxes.GAUCHE:
				directionV = Vector3.left;
				break;
			case TypesAxes.DROITE:
				directionV = Vector3.right;
				break;
			case TypesAxes.DESSUS:
				directionV = Vector3.up;
				break;
			case TypesAxes.DESSOUS:
				directionV = Vector3.down;
				break;
			default:
				directionV = Vector3.forward;
				break;
		}

		// Création du rayon qui va partir dans le bonne direction
		Vector3 positionRayon = transform.position;
		positionRayon.y += 1f;

		Vector3 directionRayon = transform.TransformDirection(directionV);
		directionRayon.Normalize();

		Ray charles = new Ray(positionRayon, directionRayon);
		RaycastHit hit;

		//if ( Physics.Raycast(charles.origin, charles.direction, out hit, visee*30) ){ // DEBUG
		if ( Physics.Raycast(charles.origin, charles.direction, out hit, visee) ){
			string nomObjetProche = hit.transform.gameObject.name;
			TypesObjetsRencontres objetSurChemin = GameObjectUtils.parseToType(nomObjetProche);
			//Debug.Log("Détecté : "+objetSurChemin);
			Debug.DrawLine(charles.origin, hit.point, debugRayColor);
			Cible objetRepere = new Cible(objetSurChemin, hit.distance, direction);
			objetsDetectes.Add(objetRepere);
		}/* else {
			Debug.Log("Rien de détecté");
		}*/

	}

	/// <summary>
	/// Signale les objets repérés à l'IA.
	/// Vide la liste stockant ces objets une fois l'IA avertie.
	/// </summary>
	private void SignalerDetections(){
		/*
		 * TODO
		 * Envoyer tous les objets vus/sentis à l'IA
		 * et vider la liste
		 */
		if (objetsDetectes.Count > 0) {
			System.Text.StringBuilder sb = new System.Text.StringBuilder ();
			sb.Append ("Objets detectés :\n");
			foreach ( Cible c in objetsDetectes ){
				sb.Append("\t").Append(c).Append("\n");
			}
			//Debug.Log(sb.ToString());
			// TODO Pousser la liste à l'IA afin qu'elle soit avertie
			objetsDetectes = new List<Cible>();
		}
	}
#endregion


#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity
	/// lorsqu'il y a une collision, i.e. lorsque des rigidbodys avec des colliders
	/// entrent en contact entre eux.
	/// Si celà arrive, arret du mouvement pour l'objet.
	/// Celui-ci sera remis en mouvement via le script de déplacement
	/// </summary>
	/// <param name="coll">Le collider de l'objet entrain en collision avec soit</param>
	//void OnTriggerEnter( Collider coll ){
	void OnCollisionEnter( Collision coll ){
		string nomObjetTouche = coll.gameObject.name;
		TypesObjetsRencontres objetTouche = GameObjectUtils.parseToType(nomObjetTouche);
		if (objetTouche != TypesObjetsRencontres.BLOC_TERRAIN) {
						Debug.Log ("Collision OnCollisionEnter avec : " + objetTouche + " / " + nomObjetTouche);
						scriptDeplacement.StopperParCollision (objetTouche);
		}
	}

	/// <summary>
	/// Routine appellée automatiquement par Unity au lancement du script
	/// </summary>
	void Awake(){
		scriptDeplacement = (DeplacementsFourmisScript) gameObject.GetComponent(typeof(DeplacementsFourmisScript));
		objetsDetectes = new List<Cible>();
	}

	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame.
	/// Va vérifier ce qu'il y a autour de la fourmis dans les 6 sens possibles/
	/// </summary>
	void Update(){
		VoirEtSentir(TypesAxes.DEVANT, LONGUEUR_VISEE_OUVRIERE);
		VoirEtSentir(TypesAxes.DEVANT_DROITE, LONGUEUR_VISEE_OUVRIERE);
		VoirEtSentir(TypesAxes.DEVANT_GAUCHE, LONGUEUR_VISEE_OUVRIERE);
		VoirEtSentir(TypesAxes.DERRIERE, LONGUEUR_VISEE_OUVRIERE);
		VoirEtSentir(TypesAxes.DERRIERE_DROITE, LONGUEUR_VISEE_OUVRIERE);
		VoirEtSentir(TypesAxes.DERRIERE_GAUCHE, LONGUEUR_VISEE_OUVRIERE);
		SignalerDetections();
	}
#endregion

}

/// <summary>
/// CollisionsFourmisScript.cs
/// Script pour gérer les collisions que peuvent avoir les fourmis
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 2.4.0
/// </remarks>

using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

/// <summary>
/// Script pour gérer les collisions que peut avoir une fourmis
/// </summary>
public class CollisionsFourmisScript : MonoBehaviour {


	/* ********* *
	 * Attributs *
	 * ********* */

#region Attributs privés
	/// <summary>
	/// Référence vers le script de déplacement de l'objet
	/// </summary>
	private DeplacementsFourmisScript scriptDeplacement;
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
	/// Méthode pour détecter des obstacles sur un axe donné
	/// </summary>
	/// <param name="direction">La direction de détection.</param>
	/// <param name="visee">La longueur du raydon de visée</param>
	private void DetecterObstacle( TypeAxes direction, float visee ){

		// Convertion direction <-> vecteur
		Color debugRayColor = Color.black;
		Vector3 directionV;
		switch ( direction ){
			case TypeAxes.DEVANT:
				debugRayColor = Color.blue;
				directionV = Vector3.forward;
				break;
			case TypeAxes.DERRIERE:
				debugRayColor = Color.green;
				directionV = Vector3.back;
				break;
			case TypeAxes.DERRIERE_DROITE:
				debugRayColor = Color.red;
				directionV = Vector3.back + Vector3.right;
				break;
			case TypeAxes.DERRIERE_GAUCHE:
				debugRayColor = Color.yellow;
				directionV = Vector3.back + Vector3.left;
				break;
			case TypeAxes.DEVANT_DROITE:
				debugRayColor = Color.white;
				directionV = Vector3.forward + Vector3.right;
				break;
			case TypeAxes.DEVANT_GAUCHE:
				debugRayColor = Color.magenta;
				directionV = Vector3.forward + Vector3.left;
				break;
			case TypeAxes.GAUCHE:
				directionV = Vector3.left;
				break;
			case TypeAxes.DROITE:
				directionV = Vector3.right;
				break;
			case TypeAxes.DESSUS:
				directionV = Vector3.up;
				break;
			case TypeAxes.DESSOUS:
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

		//if ( Physics.Raycast(charles.origin, charles.direction, out hit, visee*30) ){
		if ( Physics.Raycast(charles.origin, charles.direction, out hit, visee) ){
			string nomObjetProche = hit.transform.gameObject.name;
			TypesObjetsRencontres objetSurChemin = GameObjectUtils.parseToType(nomObjetProche);
			Debug.Log("Détecté : "+objetSurChemin);
			Debug.DrawLine(charles.origin, hit.point, debugRayColor);
		}/* else {
			Debug.Log("Rien de détecté");
		}*/

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
		Debug.Log("Collision OnCollisionEnter avec : "+objetTouche+" / "+nomObjetTouche);
		scriptDeplacement.StopperParCollision(objetTouche);
	}

	/// <summary>
	/// Routine appellée automatiquement par Unity au lancement du script
	/// </summary>
	void Awake(){
		scriptDeplacement = (DeplacementsFourmisScript) gameObject.GetComponent(typeof(DeplacementsFourmisScript));
	}

	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame.
	/// Va checker les collisions qui peuvent apparaitre.
	/// </summary>
	void Update(){
		DetecterObstacle(TypeAxes.DEVANT, LONGUEUR_VISEE_OUVRIERE);
		DetecterObstacle(TypeAxes.DEVANT_DROITE, LONGUEUR_VISEE_OUVRIERE);
		DetecterObstacle(TypeAxes.DEVANT_GAUCHE, LONGUEUR_VISEE_OUVRIERE);
		DetecterObstacle(TypeAxes.DERRIERE, LONGUEUR_VISEE_OUVRIERE);
		DetecterObstacle(TypeAxes.DERRIERE_DROITE, LONGUEUR_VISEE_OUVRIERE);
		DetecterObstacle(TypeAxes.DERRIERE_GAUCHE, LONGUEUR_VISEE_OUVRIERE);
	}
#endregion

}

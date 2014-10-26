/// <summary>
/// CollisionsFourmisScript.cs
/// Script pour gérer les collisions que peuvent avoir les fourmis
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 2.0.0
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


#region Atributs publics
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
		Vector3 directionV;
		switch (direction) {
			case TypeAxes.DEVANT:
				directionV = Vector3.forward;
				break;
			case TypeAxes.DERRIERE:
				directionV = Vector3.back;
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
		Ray charles = new Ray(transform.position, transform.TransformDirection(directionV));
		Debug.DrawLine(charles.origin, charles.origin + charles.direction * visee, Color.blue);

		RaycastHit hit;
		if ( Physics.Raycast(charles.origin, charles.origin + charles.direction, out hit, visee) ){
			//string nomObjetProche = hit.transform.gameObject.name;
			//TypeCollision objetSurChemin = GameObjectUtils.parseToTypeCollision(nomObjetProche);
			//Debug.Log("Détecté : "+objetSurChemin);
		} else {
			//Debug.Log("Rien de détecté");
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
	void OnCollisionEnter( Collision coll ){
		string nomObjetTouche = coll.gameObject.name;
		TypesObjetsRencontres objetTouche = GameObjectUtils.parseToTypeCollision(nomObjetTouche);
		//Debug.Log("Collision OnCollisionEnter avec : "+objetTouche);
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
	}
#endregion

}

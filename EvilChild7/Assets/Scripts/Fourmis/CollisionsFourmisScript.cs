/// <summary>
/// CollisionsFourmisScript.cs
/// Script pour gérer les collisions que peuvent avoir les fourmis
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

#region CollisionsFourmisScript
/// <summary>
/// Script pour gérer les collisions que peut avoir une fourmis
/// </summary>
public class CollisionsFourmisScript : MonoBehaviour {


	/* ********* *
	 * Attributs *
	 * ********* */

	
#region Attributs privés
	/// <summary>
	/// L'objet/dernier objet avec lequel on est entré en collision
	/// </summary>
	private TypeCollision objetTouche;

	/// <summary>
	/// L'objet avec lequel on va entrer en collision
	/// </summary>
	private TypeCollision objetSurChemin;
#endregion


#region Atributs publics
	/// <summary>
	/// La longueur de visée
	/// </summary>
	public float longueurVisee;
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
			string nomObjetProche = hit.transform.gameObject.name;
			objetSurChemin = GameObjectUtils.parseToTypeCollision(nomObjetProche);
			Debug.Log("Détecté : "+objetSurChemin);
		} else {
			objetTouche = TypeCollision.AUCUN;
			objetSurChemin = TypeCollision.AUCUN;
			//Debug.Log("Rien de détecté");
		}

	}
#endregion


#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity
	/// lorsqu'il y a une collision, i.e. lorsque des rigidbodys avec des colliders
	/// entrent en contact
	/// </summary>
	/// <param name="coll">Le collider de 'lobjet entrain en collision avec soit</param>
	//void OnCollisionEnter( Collision coll )
	void OnTriggerEnter( Collider coll ){
		string nomObjetTouche = coll.gameObject.name;
		objetTouche = GameObjectUtils.parseToTypeCollision(nomObjetTouche);
		Debug.Log ("Collision OnTriggerEnter avec : "+objetTouche);
	}

	void OnCollisionEnter( Collision coll ){
		string nomObjetTouche = coll.gameObject.name;
		objetTouche = GameObjectUtils.parseToTypeCollision(nomObjetTouche);
		Debug.Log ("Collision OnCollisionEnter avec : "+objetTouche);
	}

	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame.
	/// Va checker les collisions qui peuvent apparaitre.
	/// </summary>
	void Update(){
		DetecterObstacle(TypeAxes.DEVANT, longueurVisee);
	}
#endregion

}
#endregion


#region TypeCollision
/// <summary>
/// Les différentes collisions qui peuvent arriver.
/// Ces valeurs sont à utiliser lordque la fourmis (ou autre game object cible)
/// est SUR LE MEME CASE (meme position) qu'autre chose.
/// 
/// [0;9]   : par rapport au terrain
/// [10;19] : par rapport aux obstacles inertes
/// [20;29] : par rapport aux unités alliées
/// [30;39] : par rapport aux unités ennemies
/// [40;49] : par rapport à d'autres menaces
/// [50;59] : par rapport à la nourriture
/// </summary>
public enum TypeCollision : int {
	/// <summary>
	/// Objet en collision inconnu
	/// </summary>
	INCONNU = -3,
	/// <summary>
	/// Aucun objet en collision
	/// </summary>
	AUCUN = -2,
	/// <summary>
	/// Ca peut arriver...
	/// </summary>
	SOIT_MEME = -1,
	/// <summary>
	/// Collision avec un coté du bac
	/// </summary>
	COTE_BAC = 0,
	/// <summary>
	/// Collision avec un petit caillou
	/// </summary>
	PETIT_CAILLOU = 10,
	/// <summary>
	/// Collision avec un gros caillou
	/// </summary>
	GROS_CAILLOUX =11,
	/// <summary>
	/// Collision avec un bout de bois
	/// </summary>
	BOUT_DE_BOIS = 12,
	/// <summary>
	/// Une ouvrière amie
	/// </summary>
	OUVRIERE_AMIE = 20,
	/// <summary>
	/// Une contre-maitrsse alliée
	/// </summary>
	CONTREMAITRE_AMIE = 21,
	/// <summary>
	/// Une combattante alliée
	/// </summary>
	COMBATTANTE_AMIE = 22,
	/// <summary>
	/// Une générale alliée
	/// </summary>
	GENERALE_AMIE = 23,
	/// <summary>
	/// La reine de la fourmillière
	/// </summary>
	REINE_AMIE = 24,
	/// <summary>
	/// Des phéromones produites par une contre-maitre amie
	/// </summary>
	PHEROMONES_CM_AMIE = 25,
	/// <summary>
	/// Un oeuf pondu par la reine du camp
	/// </summary>
	OEUF_AMI = 26,
	/// <summary>
	/// Une ouvrière ennemie
	/// </summary>
	OUVRIERE_ENNEMIE = 30,
	/// <summary>
	/// Une contre-maitrsse ennemie
	/// </summary>
	CONTREMAITRE_ENNEMIE = 31,
	/// <summary>
	/// Une combattante ennemie
	/// </summary>
	COMBATTANTE_ENNEMIE = 32,
	/// <summary>
	/// Une générale ennemie
	/// </summary>
	GENERALE_ENNEMIE = 33,
	/// <summary>
	/// La reine du camp adverse
	/// </summary>
	REINE_ENNEMIE = 34,
	/// <summary>
	/// Un oeuf pondu par la reine du camp adverse
	/// </summary>
	OEUF_ENNEMIE = 35,
	/// <summary>
	/// Un scarabée	
	/// </summary>
	SCARABEE = 40	
}
#endregion


#region Axes de détections
/// <summary>
/// Les différents axes pour détecter un obstacle.
/// </summary>
public enum TypeAxes {
	/// <summary>
	/// Détection par devant
	/// </summary>
	DEVANT,
	/// <summary>
	/// Détection par derrière
	/// </summary>
	DERRIERE,
	/// <summary>
	/// Détection à gauche
	/// </summary>
	GAUCHE,
	/// <summary>
	/// Détection à droite
	/// </summary>
	DROITE,
	/// <summary>
	/// Détection au dessus
	/// </summary>
	DESSUS,
	/// <summary>
	/// Détection en dessous
	/// </summary>
	DESSOUS,
}
#endregion


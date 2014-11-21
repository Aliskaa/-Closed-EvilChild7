using UnityEngine;
using System.Collections;

public class CaillouxScript : MonoBehaviour {
	//Valeurs arbitraires, à définir avec le groupe
	private const int DEGAT_PETIT_CAILLOU = 2;
	private const int DEGAT_MOYEN_CAILLOU = 5;
	private const int DEGAT_GROS_CAILLOU = 10;
	private const int DISTANCE_CASE = 1;
	private Vector3 nouvellePosition;
	//private bool rebond = false;
	private int direction;

	void Start(){
		nouvellePosition = new Vector3(-1, -1, -1);
		direction = 0;
	}
	/*
	void Update(){
		if (rebond){
			HexagoneInfo hexaNouvelle = TerrainUtils.HexagonePlusProche (nouvellePosition);
			HexagoneInfo hexaCourante = TerrainUtils.HexagonePlusProche (transform.localPosition);

			transform.localPosition = Vector3.Lerp(transform.localPosition, nouvellePosition, 0.1f);

			if ( hexaCourante != hexaNouvelle) {
				transform.Rotate(8, 10, 8);
			} else {
				transform.rigidbody.isKinematic = true;
				transform.Rotate(0, 0, 0);
				transform.collider.isTrigger = false;
			}
		}
	}

	// TODO il faut que l'on gère s'il y a déjà un élément sur la case suite au rebond.
	void OnTriggerEnter(Collider c){
		string objetTouche = c.gameObject.name;
		TypesObjetsRencontres tor = GameObjectUtils.parseToType (objetTouche);
		switch (tor) {
		case TypesObjetsRencontres.COMBATTANTE_BLANCHE :// va falloir rajouter pour tous les éléments du terrain :)
			Debug.Log("Rencontre d'une fourmi");
			break;
		case TypesObjetsRencontres.SCARABEE:
			//pareil que pour la fourmis
			Debug.Log ("rencontre d'un scarabée");
			break;
		case TypesObjetsRencontres.CAILLOU:
			deplacerGameObject(c.gameObject, direction, 1);
			Debug.Log("CAILLOU - Collision avec un caillou" + direction);
			break;
		case TypesObjetsRencontres.SABLE:
			Debug.Log("Rencontre sur le sol");
			break;
		case TypesObjetsRencontres.EAU:
			Debug.Log("CAILLOU - Collision avec de l'eau");
			//transform.gameObject.
			// TODO rajouter des petites particules pour faire comme une gerbe d'eau
			Destroy(transform.gameObject);
			break;
		default:
			break;
		}

		if (!rebond) {
			//rebondir();
			rebond = true;
		}
	}

	/*
	 *<summary>
	 *Fonction permettant de calculer le rebond d'un caillou sur le terrain.
	 *</summary>
	 * /
	private void rebondir(){
		direction = Random.Range (0, 8);
		int distance = Random.Range (5, 10);
		deplacerGameObject (transform.gameObject, direction, distance);
	}
*/
	private void deplacerGameObject(GameObject go, int direction, int distance){
		Vector3 positionLocale = go.transform.localPosition;
		HexagoneInfo hexaCorrespondant;
		switch (direction){ // On se place par rapport au cailloux
		case 0: // Nord
			nouvellePosition.x = positionLocale.x + distance * DISTANCE_CASE;
			nouvellePosition.z = positionLocale.z;
			nouvellePosition.y = positionLocale.y;
			break;
		case 1: // Nord-Est
			nouvellePosition.x = positionLocale.x + distance * DISTANCE_CASE;
			nouvellePosition.z = positionLocale.z + distance * DISTANCE_CASE;
			nouvellePosition.y = positionLocale.y;
			break;
		case 2: // Est
			nouvellePosition.x = positionLocale.x;
			nouvellePosition.z = positionLocale.z + distance * DISTANCE_CASE;
			nouvellePosition.y = positionLocale.y;
			break;
		case 3: // Sud-Est
			nouvellePosition.x = positionLocale.x - distance * DISTANCE_CASE;
			nouvellePosition.z = positionLocale.z + distance * DISTANCE_CASE;
			nouvellePosition.y = positionLocale.y;
			break;
		case 4: // Sud
			nouvellePosition.x = positionLocale.x - distance * DISTANCE_CASE;
			nouvellePosition.z = positionLocale.z;
			nouvellePosition.y = positionLocale.y;
			break;
		case 5: // Sud-Ouest
			nouvellePosition.x = positionLocale.x - distance * DISTANCE_CASE;
			nouvellePosition.z = positionLocale.z - distance * DISTANCE_CASE;
			nouvellePosition.y = positionLocale.y;
			break;
		case 6: // Ouest
			nouvellePosition.x = positionLocale.x;
			nouvellePosition.z = positionLocale.z - distance * DISTANCE_CASE;
			nouvellePosition.y = positionLocale.y;
			break;
		case 7: // Nord-Ouest
			nouvellePosition.x = positionLocale.x + distance * DISTANCE_CASE;
			nouvellePosition.z = positionLocale.z - distance * DISTANCE_CASE;
			nouvellePosition.y = positionLocale.y;
			break;
		default:
			Debug.Log("Caillou : Position inconnue pour le rebond.");
			break;
		}
		//On centre la nouvelle position sur un hexagone existant
		hexaCorrespondant = TerrainUtils.HexagonePlusProche (nouvellePosition);
		nouvellePosition = hexaCorrespondant.positionLocaleSurTerrain;
	}
}

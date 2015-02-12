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
	
	void Update(){
		/*if (rebond){
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
		}*/
	}
	
	void OnTriggerEnter(Collider c){
		string objetTouche = c.gameObject.name;
		TypesObjetsRencontres tor = GameObjectUtils.parseToType (objetTouche);
		
		/** IF MEGA BADASS **/
		if (tor.Equals(TypesObjetsRencontres.COMBATTANTE_BLANCHE) 
		    || tor.Equals(TypesObjetsRencontres.COMBATTANTE_NOIRE) 
		    || tor.Equals(TypesObjetsRencontres.OUVRIERE_BLANCHE)
		    || tor.Equals(TypesObjetsRencontres.OUVRIERE_NOIRE)
		    || tor.Equals(TypesObjetsRencontres.REINE_BLANCHE)
		    || tor.Equals(TypesObjetsRencontres.REINE_NOIRE)
		    || tor.Equals(TypesObjetsRencontres.CONTREMAITRE_BLANCHE)
		    || tor.Equals(TypesObjetsRencontres.CONTREMAITRE_NOIRE)
		    || tor.Equals(TypesObjetsRencontres.GENERALE_NOIRE)
		    || tor.Equals(TypesObjetsRencontres.GENERALE_BLANCHE)){

			IAabstraite ia = null;
			// On a une fourmi normale
			if ( c.gameObject.GetComponent<FourmiScript>() != null ){
				ia = (IAabstraite)c.gameObject.GetComponent<FourmiScript>().iaBestiole;
			// On a une reine
			} else {
				ia = (IAabstraite)c.gameObject.GetComponent<ReineScript>().iaReine;
			}

			int pvRestants = ia.getModele().enleverPV(DEGAT_MOYEN_CAILLOU);
			
			if (pvRestants <= 0) {
				ia.mort ();
			}
			
		} else if (tor.Equals(TypesObjetsRencontres.CAILLOU)){
			deplacerGameObject(c.gameObject, direction, 1);
			
		} else if (tor.Equals(TypesObjetsRencontres.EAU)){
			Destroy(transform.gameObject);
			
		} else if (tor.Equals(TypesObjetsRencontres.SCARABEE)){ 
			IAabstraite ia = (IAabstraite)c.gameObject.GetComponent<ScarabeeScript>().iaBestiole;
			int pvRestants = ia.getModele().enleverPV(DEGAT_MOYEN_CAILLOU);
			
			if (pvRestants <= 0) {
				ia.mort ();
			}
		}
		
		//transform.rigidbody.isKinematic = true;
		//transform.rigidbody.constraints = RigidbodyConstraints.FreezePosition;
		transform.collider.isTrigger = false;
		/*
		if (!rebond) {
			//rebondir();
			rebond = true;
		}*/
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

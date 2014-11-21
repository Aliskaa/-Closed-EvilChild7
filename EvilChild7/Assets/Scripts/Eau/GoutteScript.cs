/// <summary>
/// GoutteScript
/// Fichier associé aux gouttes d'eau crées par la bombe à eau
/// </summary>
/// <remarks>
/// G POLLET 
/// </remarks>
/// 
using UnityEngine;
using System.Collections;

public class GoutteScript : MonoBehaviour
{
	private GameObject bas;
	private const int distance = 2;

	void Start(){
		bas = GameObject.FindGameObjectWithTag ("BAC_A_SABLE");
	}

	void OnTriggerEnter(Collider c){
		HexagoneInfo hi;
		GameObject go = c.gameObject;
		string name = go.name;
		TypesObjetsRencontres tor = GameObjectUtils.parseToType (name);
		TerrainManagerScript tms = bas.GetComponent<TerrainManagerScript> ();
		Vector3 colliderPosition = c.transform.localPosition;
		
		if (tor.Equals(TypesObjetsRencontres.INCONNU) || tor.Equals(TypesObjetsRencontres.EAU)){
			Destroy (transform.gameObject);
			//Debug.Log("Inconu ou eau");
			return;
		}

		if (tor.Equals(TypesObjetsRencontres.BLOC_TERRAIN)){
			//Debug.Log("Terrain");
			tms.ConvertirCaseEau (transform.localPosition);
			Destroy (transform.gameObject);
			return;
		}

		string tag = transform.gameObject.tag;

		switch(tag){
		case "nord":
			//Debug.Log("Nord");
			colliderPosition.x += distance;
			break;
		case "sud":
			//Debug.Log("Sud");
			colliderPosition.x -= distance;
			break;
		case "ne":
			//Debug.Log("NE");
			colliderPosition.x += distance;
			colliderPosition.z += distance;
			break;
		case "no":
			//Debug.Log("NO");
			colliderPosition.x += distance;
			colliderPosition.z -= distance;
			break;
		case "so":
			//Debug.Log("SO");
			colliderPosition.x -= distance;
			colliderPosition.z -= distance;
			break;
		case "se":
			//Debug.Log("SE");
			colliderPosition.x -= distance;
			colliderPosition.z += distance;
			break;
		default:
			//Debug.Log("Je ne sais pas quoi faire !!");
			Destroy(c.gameObject);
			break;
		}

		hi = TerrainUtils.HexagonePlusProche (colliderPosition);

		c.transform.localPosition = hi.positionLocaleSurTerrain;
		tms.ConvertirCaseEau (transform.localPosition);
		Destroy (transform.gameObject);
	}
}


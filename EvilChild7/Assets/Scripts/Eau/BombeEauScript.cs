/// <summary>
/// BombeEauScript
/// Fichier associé à la bombe à eau lancée
/// </summary>
/// 
/// <remarks>
/// G POLLET 
/// </remarks>
using UnityEngine;
using System.Collections;
using System;

public class BombeEauScript : MonoBehaviour {
	
	private bool collisionPassee = false;
	private const int DISTANCE = 5;
	private InvocateurObjetsScript invoc;
	
	void Start(){
		GameObject bacASable = GameObject.FindGameObjectWithTag ("BAC_A_SABLE");
		invoc = bacASable.GetComponent<InvocateurObjetsScript> ();
	}
	
	void OnTriggerEnter(Collider c){
		//Debug.Log ("Collision bombe");
		//Debug.Log ( collisionPassee);
		if (collisionPassee == false){
			GameObject g;
			GameObject terrain = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
			TerrainManagerScript tms = terrain.GetComponent<TerrainManagerScript>();
			string col = c.gameObject.name;
			TypesObjetsRencontres tor = GameObjectUtils.parseToType (col);
			
			switch (tor){
			case TypesObjetsRencontres.SCARABEE:
				collisionPassee = true;
				tms.ConvertirCaseEau(transform.localPosition);
				Destroy (transform.gameObject, 10);
				break;
			case TypesObjetsRencontres.EAU:
				collisionPassee = true;
				tms.ConvertirCaseEau(transform.localPosition);
				Destroy (transform.gameObject, 10);
				break;
			case TypesObjetsRencontres.COMBATTANTE_BLANCHE:
				break;
			case TypesObjetsRencontres.CONTREMAITRE_BLANCHE:
				collisionPassee = true;
				tms.ConvertirCaseEau(transform.localPosition);
				Destroy (transform.gameObject, 10);
				break;
			case TypesObjetsRencontres.OUVRIERE_BLANCHE:
				collisionPassee = true;
				tms.ConvertirCaseEau(transform.localPosition);
				Destroy (transform.gameObject, 10);
				break;
			case TypesObjetsRencontres.GENERALE_BLANCHE:
				collisionPassee = true;
				tms.ConvertirCaseEau(transform.localPosition);
				Destroy (transform.gameObject, 10);
				break;
			case TypesObjetsRencontres.COMBATTANTE_NOIRE:
				collisionPassee = true;
				tms.ConvertirCaseEau(transform.localPosition);
				Destroy (transform.gameObject, 10);
				break;
			case TypesObjetsRencontres.CONTREMAITRE_NOIRE:
				collisionPassee = true;
				tms.ConvertirCaseEau(transform.localPosition);
				Destroy (transform.gameObject, 10);
				break;
			case TypesObjetsRencontres.OUVRIERE_NOIRE:
				collisionPassee = true;
				tms.ConvertirCaseEau(transform.localPosition);
				Destroy (transform.gameObject, 10);
				break;
			case TypesObjetsRencontres.GENERALE_NOIRE:
				collisionPassee = true;
				tms.ConvertirCaseEau(transform.localPosition);
				Destroy (transform.gameObject, 10);
				break;
			case TypesObjetsRencontres.BLOC_TERRAIN:
				collisionPassee = true;
				tms.ConvertirCaseEau(transform.localPosition);
				Destroy (transform.gameObject, 10);
				
				HexagoneInfo hexaCorrespondant;
				
				Vector3 positionCourante = transform.localPosition;
				//Nord
				Vector3 nord = new Vector3(positionCourante.x + DISTANCE, 0.1f, positionCourante.z);
				//Sud
				Vector3 sud = new Vector3(positionCourante.x - DISTANCE, 0.1f, positionCourante.z);
				//Nord ouest
				Vector3 no = new Vector3(positionCourante.x + DISTANCE, 0.1f, positionCourante.z - DISTANCE+2);
				// Nord est
				Vector3 ne = new Vector3(positionCourante.x + DISTANCE, 0.1f, positionCourante.z + DISTANCE);
				//Sud Est
				Vector3 se = new Vector3(positionCourante.x, 0.1f, positionCourante.z + DISTANCE);
				//Sud Ouest
				Vector3 so = new Vector3(positionCourante.x, 0.1f, positionCourante.z - DISTANCE+2);
				
				
				hexaCorrespondant = TerrainUtils.HexagonePlusProche(nord);
				nord = hexaCorrespondant.positionLocaleSurTerrain;
				g = invoc.InvoquerObjet(Invocations.GOUTTE, nord);
				g.tag = "nord";
				
				hexaCorrespondant = TerrainUtils.HexagonePlusProche (sud);
				sud = hexaCorrespondant.positionLocaleSurTerrain;
				g = invoc.InvoquerObjet(Invocations.GOUTTE, sud);
				g.tag = "sud";
				
				hexaCorrespondant = TerrainUtils.HexagonePlusProche (no);
				no = hexaCorrespondant.positionLocaleSurTerrain;
				invoc.InvoquerObjet(Invocations.GOUTTE, no);
				g = invoc.InvoquerObjet(Invocations.GOUTTE, no);
				g.tag = "no";
				
				hexaCorrespondant = TerrainUtils.HexagonePlusProche (ne);
				ne = hexaCorrespondant.positionLocaleSurTerrain;
				g = invoc.InvoquerObjet(Invocations.GOUTTE, ne);
				g.tag = "ne";
				
				hexaCorrespondant = TerrainUtils.HexagonePlusProche (se);
				se = hexaCorrespondant.positionLocaleSurTerrain;
				g = invoc.InvoquerObjet(Invocations.GOUTTE, se);
				g.tag = "se";
				
				hexaCorrespondant = TerrainUtils.HexagonePlusProche (so);
				so = hexaCorrespondant.positionLocaleSurTerrain;
				g = invoc.InvoquerObjet(Invocations.GOUTTE, so);
				g.tag = "so";
				
				break;
			default:
				break;
			}
			
			
			
		}
	}
}

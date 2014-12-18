/// <summary>
/// Missile eau
/// Script qui crée un pistolet à eau et lance sur le terrain un missile d'eau pour créer une case d'eau
/// </summary>
/// 
/// <remarks>
/// S Rethore - version 10000
/// </remarks>
using UnityEngine;
using System.Collections;

public class MissileEau : MonoBehaviour {
	
	private bool collisionPassee = false;
	private const int DISTANCE = 5;

	public Transform creation;

	//GameObjects
	private GameObject obj_pistolet;
	private GameObject eau;
	private GameObject eau3D;

	//Correspondance au Terrain
	private TerrainManagerScript tms;
	private InvocateurObjetsScript invoc;

	void Start () 
	{
		//invocation de l'objet pistolet
		GameObject terrainGo = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		tms = terrainGo.GetComponent<TerrainManagerScript>();
		invoc = terrainGo.GetComponent<InvocateurObjetsScript> ();
		float distance = 0.5f;
		Vector3 cameraPosition = Camera.main.transform.position + Camera.main.transform.forward * distance;
		obj_pistolet = invoc.InvoquerDevantCamera( Invocations.PISTOLET, cameraPosition);

		Destroy (obj_pistolet, 5);

	}
	

	/// <summary>
	/// Raises the trigger enter event. gestion de collision
	/// </summary>
	/// <param name="c">C.</param>
	void OnTriggerEnter(Collider c){
				if (!collisionPassee) {
						
						GameObject terrain = GameObject.FindGameObjectWithTag ("BAC_A_SABLE");
						TerrainManagerScript tms = terrain.GetComponent<TerrainManagerScript> ();
						string col = c.gameObject.name;
						TypesObjetsRencontres tor = GameObjectUtils.parseToType (col);
			
					
						switch (tor) {
						case TypesObjetsRencontres.SCARABEE:
								break;
						case TypesObjetsRencontres.EAU:
								break;
						case TypesObjetsRencontres.COMBATTANTE_BLANCHE:
								break;
						case TypesObjetsRencontres.CONTREMAITRE_BLANCHE:
								break;
						case TypesObjetsRencontres.OUVRIERE_BLANCHE:
								break;
						case TypesObjetsRencontres.GENERALE_BLANCHE:
								break;
						case TypesObjetsRencontres.COMBATTANTE_NOIRE:
								break;
						case TypesObjetsRencontres.CONTREMAITRE_NOIRE:
								break;
						case TypesObjetsRencontres.OUVRIERE_NOIRE:
								break;
						case TypesObjetsRencontres.GENERALE_NOIRE:
								break;
						default:
								break;
						}
			
						collisionPassee = true;
						
						// Creation de la case d'eau manuellement 
						HexagoneInfo hexagoneClick = TerrainUtils.HexagonePlusProche(transform.localPosition);
						eau = invoc.InvoquerObjet(Invocations.EAU, hexagoneClick.positionLocaleSurTerrain);
						eau3D =invoc.InvoquerObjet(Invocations.EAU3D, hexagoneClick.positionLocaleSurTerrain);

						// destruction de la case d'eau au bout de 60s
						// cette valeur peut etre modifiée
						Destroy (eau, 60);
						Destroy (eau3D, 60);
				}
		}

}

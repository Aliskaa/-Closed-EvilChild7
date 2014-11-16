/// <summary>
/// DebugScript.cs
/// Script pour faire du débogage, c'est à dire profiter
/// des routines telles que Awake(), Update() ou autres piur faire des tests
/// sans squatter des scripts qui n'ont rien à voir.
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.2.1
/// </remarks>

using UnityEngine;

/// <summary>
/// Script de debug à coller n'importe où et à complèter avec n'importe quoi.
/// </summary>
public class DebugScript : MonoBehaviour {


	/* ********* *
	 * Attributs *
	 * ********* */

#region Attributs
	/// <summary>
	/// Flag indiquant si le terrain est rempli, i.e. si tous les hexagones ont été mis en places
	/// </summary>
	private bool terrainRempli;

	/// <summary>
	/// 
	/// </summary>
	private bool flagOKPop;
#endregion

	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes package
	/// <summary>
	/// 
	/// </summary>
	void Awake(){
		terrainRempli = false;
		flagOKPop = false;
	}

	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame
	/// </summary>
	void Update(){

		/*
		 * Il faut s'assurer que le terrain soit créé avant de placer les objets
		 */
		if ( !flagOKPop && (terrainRempli || VerifierRemplissageTerrain()) ){

			terrainRempli = true;
			flagOKPop = true;
			GameObject bacAsable = GameObject.Find("Bac à sable");
			InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();

			#region Debogage IA
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_COMBATTANTE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_OUVRIERE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_CONTREMAITRE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_GENERALE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_REINE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_COMBATTANTE, new Vector3(85f, 0.1f, 95f));
			scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_CONTREMAITRE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_OUVRIERE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_GENERALE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_REINE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.OEUF_FOURMI, click);
			//scriptInvoc.InvoquerObjet(Invocations.SCARABEE, new Vector3(85f, 0.1f, 95f));
			#endregion

		}


		/*
		 * Debogage click / convertion coordonnées
		 */
		if ( Input.GetMouseButtonDown(1/*CLIC_DROIT_SOURIS*/) ){
			Ray rayon = Camera.main.ScreenPointToRay(Input.mousePosition); 
			float distance;
			Plane planDuSol = new Plane(Vector3.up, transform.position);
			if ( planDuSol.Raycast(rayon, out distance) ){ 
				Vector3 pointImpact = rayon.GetPoint(distance);
				GameObject terrainGo = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
				TerrainManagerScript tms = terrainGo.GetComponent<TerrainManagerScript>();
				Vector3 click = tms.ConvertirCoordonnes(pointImpact);
				//tms.ConvertirCaseEau(click);

				#region Debogage IA
				InvocateurObjetsScript ios = terrainGo.GetComponent<InvocateurObjetsScript>();
				//ios.InvoquerObjet(Invocations.TRES_GROS_CAILLOU, click);
				//ios.InvoquerObjet(Invocations.PETIT_CAILLOU, click);
				//ios.InvoquerObjet(Invocations.CAILLOU, click);
				//ios.InvoquerObjet(Invocations.BONBON_MURE, click);
				//ios.InvoquerObjet(Invocations.BONBON_ANGLAIS_BLEU, click);
				//ios.InvoquerObjet(Invocations.BONBON_ANGLAIS_ROSE, click);
				//ios.InvoquerObjet(Invocations.BONBON_ORANGE, click);
				//ios.InvoquerObjet(Invocations.BONBON_VERT, click);
				//ios.InvoquerObjet(Invocations.BONBON_ROSE, click);
				//ios.InvoquerObjet(Invocations.OEUF_FOURMI, click);
				//ios.InvoquerObjet(Invocations.BOUT_DE_BOIS, click);
				//ios.InvoquerObjet(Invocations.FOURMI_NOIRE_COMBATTANTE, click);
				//ios.InvoquerObjet(Invocations.FOURMI_NOIRE_CONTREMAITRE, click);
				//ios.InvoquerObjet(Invocations.FOURMI_NOIRE_GENERALE, click);
				ios.InvoquerObjet(Invocations.FOURMI_NOIRE_OUVRIERE, click);
				//ios.InvoquerObjet(Invocations.FOURMI_BLANCHE_GENERALE, click);
				//ios.InvoquerObjet(Invocations.FOURMI_BLANCHE_CONTREMAITRE, click);
				//ios.InvoquerObjet(Invocations.FOURMI_BLANCHE_OUVRIERE, click);
				//ios.InvoquerObjet(Invocations.FOURMI_BLANCHE_COMBATTANTE, click);
				//ios.InvoquerObjet(Invocations.SCARABEE, click);
				//ios.InvoquerObjet(Invocations.PHEROMONES_CONTREMAITRE_BLANCHE, click);
				//GameObject bite =  ios.InvoquerObjet(Invocations.PHEROMONES_OUVRIERE_BLANCHE, click);
				//GameObject bite =  ios.InvoquerObjet(Invocations.PHEROMONES_CONTREMAITRE_BLANCHE, click);
				//GameObject bite =  ios.InvoquerObjet(Invocations.PHEROMONES_OUVRIERE_NOIRE, click);
				//GameObject bite =  ios.InvoquerObjet(Invocations.PHEROMONES_CONTREMAITRE_NOIRE, click);
				//bite.GetComponent<PheromonesScript>().direction = TypesAxes.DEVANT;
				#endregion 


				//Debug.Log("Coordonnées converties : "+tms.ConvertirCoordonnes(pointImpact));
			}
		}

	}
#endregion

#region Méthodes privées
	/// <summary>
	/// Vérifie si le terrain est rempli ou non
	/// </summary>
	/// <returns><c>true</c>, si le terrain est rempli, <c>false</c> sinon.</returns>
	private bool VerifierRemplissageTerrain(){
		GameObject terrain = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		TerrainManagerScript tms = terrain.GetComponent<TerrainManagerScript>();
		return ( tms != null ? tms.VerifierRemplissageTerrain() : false );
	}
#endregion

}

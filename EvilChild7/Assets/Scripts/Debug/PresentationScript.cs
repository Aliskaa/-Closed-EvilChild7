/// <summary>
/// PresentationScript.cs
/// Script pour faire une présentation aux clients
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 2.0.0
/// </remarks>

using UnityEngine;

/// <summary>
/// Script de présentation
/// </summary>
public class PresentationScript : MonoBehaviour {


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

#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity au réveil du script
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


			scriptInvoc.InvoquerObjet(Invocations.BONBON_ANGLAIS_BLEU, new Vector3(70f, 0.1f, 75f));
			scriptInvoc.InvoquerObjet(Invocations.BONBON_ANGLAIS_ROSE, new Vector3(70f, 0.1f, 80f));
			scriptInvoc.InvoquerObjet(Invocations.BONBON_MURE, 		   new Vector3(70f, 0.1f, 105f));
			scriptInvoc.InvoquerObjet(Invocations.BONBON_ORANGE, 	   new Vector3(70f, 0.1f, 90f));
			scriptInvoc.InvoquerObjet(Invocations.BONBON_ROSE,		   new Vector3(70f, 0.1f, 95f));
			scriptInvoc.InvoquerObjet(Invocations.BONBON_VERT, 		   new Vector3(70f, 0.1f, 100f));


			scriptInvoc.InvoquerObjet(Invocations.BOUT_DE_BOIS, 	   new Vector3(85f, 0.1f, 75f));
			scriptInvoc.InvoquerObjet(Invocations.PETIT_CAILLOU, 	   new Vector3(85f, 0.1f, 85f));
			scriptInvoc.InvoquerObjet(Invocations.CAILLOU, 	   		   new Vector3(85f, 0.1f, 95f));
			scriptInvoc.InvoquerObjet(Invocations.TRES_GROS_CAILLOU,   new Vector3(85f, 0.1f, 100f));


			scriptInvoc.InvoquerObjet(Invocations.OEUF_FOURMI,   	   new Vector3(100f, 0.1f, 75f));
			scriptInvoc.InvoquerObjet(Invocations.SCARABEE,   	  	   new Vector3(100f, 0.1f, 80f));
			scriptInvoc.InvoquerObjet(Invocations.PHEROMONES_CONTREMAITRE_BLANCHE,
			                          new Vector3(100f, 0.1f, 95f));
			scriptInvoc.InvoquerObjet(Invocations.PHEROMONES_CONTREMAITRE_NOIRE,
			                          new Vector3(100f, 0.1f, 100f));
			scriptInvoc.InvoquerObjet(Invocations.PHEROMONES_OUVRIERE_NOIRE,
			                          new Vector3(100f, 0.1f, 120f));
			scriptInvoc.InvoquerObjet(Invocations.PHEROMONES_OUVRIERE_BLANCHE,
			                          new Vector3(100f, 0.1f, 110f));


			scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_OUVRIERE,
			                          new Vector3(115f, 0.5f, 75f));
			scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_COMBATTANTE,
			                          new Vector3(115f, 0.5f, 85f));
			scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_CONTREMAITRE,
			                          new Vector3(115f, 0.5f, 95f));
			scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_GENERALE,
			                          new Vector3(115f, 0.5f, 105f));
			scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_REINE,
			                          new Vector3(115f, 0.5f, 115f));

			scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_OUVRIERE,
			                          new Vector3(130f, 0.5f, 75f));
			scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_COMBATTANTE,
			                          new Vector3(130f, 0.5f, 85f));
			scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_CONTREMAITRE,
			                          new Vector3(130f, 0.5f, 95f));
			scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_GENERALE,
			                          new Vector3(130f, 0.5f, 105f));
			scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_REINE,
			                          new Vector3(130f, 0.5f, 115f));

		}

	}
#endregion

}

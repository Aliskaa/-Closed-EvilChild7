/// <summary>
/// DebugScript.cs
/// Script pour faire du débogage, c'est à dire profiter
/// des routines telles que Awake(), Update() ou autres piur faire des tests
/// sans squatter des scripts qui n'ont rien à voir.
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
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
			scriptInvoc.InvoquerObjet(Invocations.TRES_GROS_CAILLOU,
		                              new Vector3(85f, 0.1f, 90f));
			scriptInvoc.InvoquerObjet(Invocations.DEBUG_FOURMIS,
			                          new Vector3(65f, 0.1f, 75f));
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

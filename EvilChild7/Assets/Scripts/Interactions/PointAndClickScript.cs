/// <summary>
/// PointAndClickScript.cs
/// Script pour gérer le clic de la souris et convertir ces cordonnées
/// 2D en coordonnées 3D locales au terrain
/// </summary>
/// <remarks>
/// http://mteys.com/detection-des-clics-sur-les-objets-unity/
/// </remarks>
/// <remarks>
/// PY Lapersonne - Version 1.1.0
/// </remarks>

using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

/// <summary>
/// Classe pour gérer le clic de la souris et convertir ces cordonnées
/// 2D en coordonnées 3D locales au terrain
/// </summary>
public class PointAndClickScript : MonoBehaviour {


	/* ********* *
	 * Attributs *
	 * ********* */

#region Attributs
	/// <summary>
	/// Flag mettant un hexagone dans une couleur différente
	/// s'il est clické
	/// </summary>
	public bool modeCouleurClick;

	/// <summary>
	/// Flag mettant un hexagone dans une couleur différente
	/// s'il est survolé
	/// </summary>
	public bool modeCouleurSurvol;

	/// <summary>
	/// Flag indiquant que l'on gère le survol de la souris
	/// </summary>
	public bool modeSurvol;

	/// <summary>
	/// Flag indiquant que l'on gère el click de la souris
	/// </summary>
	public bool modeClick;

	/// <summary>
	/// La texture à appliquer en cas de survol d'un hexagone
	/// </summary>
	public Texture textureSurvol;

	/// <summary>
	/// La texture à appliquer en cas de click sur un hexagone
	/// </summary>
	public Texture textureClikc;
#endregion

#region Constantes
	/// <summary>
	/// Clic gauche de la souris
	/// </summary>
	public const int CLIC_GAUCHE_SOURIS = 0;
	/// <summary>
	/// Clic droit de la souris
	/// </summary>
	public const int CLIC_DROIT_SOURIS = 1;
	/// <summary>
	/// Clic millieu de la souris
	/// </summary>
	public const int CLIC_CENTRE_SOURIS = 2;
#endregion

	/* ******** *
	 * Méthodes *
	 * ******** */
	
#region Méthodes privées
	/// <summary>
	/// Méthode pour détecter le click de l'utilisateur et pour convertir ces coordonnées
	/// x/y en coordonnées x/y/z locales au terrain.
	/// Pour cela, va récupérer la position de la souris, puis va récupérer le plan du sol.
	/// Puis va lancer un rayon avec ce sol.
	/// </summary>
	private void DetecterClick(){
		if ( Input.GetMouseButtonDown(CLIC_DROIT_SOURIS) ){
			Ray rayon = Camera.main.ScreenPointToRay(Input.mousePosition); 
			float distance;
			Plane planDuSol = new Plane(Vector3.up, transform.position);
			if ( planDuSol.Raycast(rayon, out distance) ){ 
				Vector3 pointImpact = rayon.GetPoint(distance);
				//Debug.Log("Coordonnées de la souris sur le plan  = " + pointImpact );
				Vector3 coordLocales = transform.worldToLocalMatrix.MultiplyPoint(pointImpact);
				//Debug.Log("Coordonnées locales p/r au terrain = " + coordLocales );
				HexagoneInfo hexagoneClick = TerrainUtils.HexagonePlusProche(coordLocales);
				Debug.Log("Hexagone le plus proche du clic = " + hexagoneClick.positionLocaleSurTerrain );
				/*
				if ( modeCouleurClick ){
					// FIXME Prendre un truc moins dégueulasse
					GameObject bacAsable = GameObject.Find("Bac à sable");
					if ( bacAsable == null ) Debug.LogError("BOUM");
					InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
					scriptInvoc.InvoquerObjet(Invocations.DEBUG_OBJECT, hexagoneClick.positionLocaleSurTerrain);
				}
				*/
			}
		}
	}

	/// <summary>
	/// Méthode pour détecter le survol de la souris pour convertir ces coordonnées
	/// x/y en coordonnées x/y/z locales au terrain.
	/// Pour cela, va récupérer la position de la souris, puis va récupérer le plan du sol.
	/// Puis va lancer un rayon avec ce sol.
	/// </summary>
	private void DetecterSurvol(){
		Ray rayon = Camera.main.ScreenPointToRay(Input.mousePosition); 
		float distance;
		Plane planDuSol = new Plane(Vector3.up, transform.position);
		if ( planDuSol.Raycast(rayon, out distance) ){ 
			Vector3 pointImpact = rayon.GetPoint(distance);
			//Debug.Log("Coordonnées de la souris sur le plan  = " + pointImpact );
			Vector3 coordLocales = transform.worldToLocalMatrix.MultiplyPoint(pointImpact);
			//Debug.Log("Coordonnées locales p/r au terrain = " + coordLocales );
			HexagoneInfo hexagoneClick = TerrainUtils.HexagonePlusProche(coordLocales);
			Debug.Log("Hexagone le plus proche du clic = " + hexagoneClick.positionLocaleSurTerrain );
			/*
			if ( modeCouleurSurvol ){
				// FIXME Prendre un truc moins dégueulasse
				GameObject bacAsable = GameObject.Find("Bac à sable");
				if ( bacAsable == null ) Debug.LogError("BOUM");
				InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
				scriptInvoc.InvoquerObjet(Invocations.DEBUG_OBJECT, hexagoneClick.positionLocaleSurTerrain);
			}
			*/
		}
	}
#endregion


#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity au lancement du script
	/// </summary>
	void Update(){
		if ( modeClick ) DetecterClick();
		if ( modeSurvol ) DetecterSurvol();
	}
#endregion

}

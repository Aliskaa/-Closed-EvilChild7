/// <summary>
/// PointAndClickScript.cs
/// Script pour gérer le clic de la souris et convertir ces cordonnées
/// 2D en coordonnées 3D locales au terrain
/// </summary>
/// <remarks>
/// http://mteys.com/detection-des-clics-sur-les-objets-unity/
/// </remarks>
/// <remarks>
/// PY Lapersonne - Version 1.1.3
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

#region Attributs privés
	/// <summary>
	/// Le dernier hexagone pointé/selectionné (qui peut toujours l'etre)
	/// </summary>
	private HexagoneInfo dernierHexagone;
#endregion

#region Attributs publics
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
	/// Flag indiquant que l'on gère le click de la souris
	/// </summary>
	public bool modeClick;

	/// <summary>
	/// La texture à appliquer en cas de survol d'un hexagone
	/// </summary>
	public Texture textureSurvol;

	/// <summary>
	/// La texture à appliquer en cas de click sur un hexagone
	/// </summary>
	public Texture textureClick;
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
				/*HexagoneInfo hexagoneClick = */TerrainUtils.HexagonePlusProche(coordLocales);
				//Debug.Log("Clic sur hexagone : " + hexagoneClick.positionLocaleSurTerrain );
			}
		}
	}

	/// <summary>
	/// Méthode pour détecter le survol de la souris pour convertir ces coordonnées
	/// x/y en coordonnées x/y/z locales au terrain.
	/// Pour cela, va récupérer la position de la souris, puis va récupérer le plan du sol.
	/// Puis va lancer un rayon avec ce sol.
	/// Là où est la souris, un game object nommé "Selection case" de tag POINTEUR sera créé.
	/// </summary>
	private void DetecterSurvol(){
		Ray rayon = Camera.main.ScreenPointToRay(Input.mousePosition); 
		float distance;
		Plane planDuSol = new Plane(Vector3.up, transform.position);
		if ( planDuSol.Raycast(rayon, out distance) ){ 
			Vector3 pointImpact = rayon.GetPoint(distance);
			// Récupération de l'hexagone si on a la souris sur le terrain :
			// pour ce faire vérifier la position de la souris par rapport aux coord globales
			if ( IsSourisSurTerrain(pointImpact) ){
				//Debug.Log("Coordonnées de la souris sur le plan  = " + pointImpact );
				Vector3 coordLocales = transform.worldToLocalMatrix.MultiplyPoint(pointImpact);
				//Debug.Log("Coordonnées locales p/r au terrain = " + coordLocales );
				HexagoneInfo hexagoneClick = TerrainUtils.HexagonePlusProche(coordLocales);
				//Debug.Log("Hexagone le plus proche du clic = " + hexagoneClick.positionLocaleSurTerrain );
				if ( modeCouleurSurvol ){
					GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
					InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
					SupprimerPointeurs( hexagoneClick );
					// Création d'un nouveau gameobject
					scriptInvoc.InvoquerObjet(Invocations.SELECTION_CASE, hexagoneClick.positionLocaleSurTerrain);
				}
			} else {
				SupprimerPointeurs( null );
				dernierHexagone = null;
			}
		}
	}

	/// <summary>
	/// Indique si la souris pointe sur le terrain
	/// </summary>
	/// <remarks>
	/// L'angle -x/-z du terrain : x=-98 et z=-94.
	/// L'angle +x/+z du terrain : x=0 et z=-8
	/// </remarks>
	/// <returns><c>true</c> Si la souris pointe vers le terrain, <c>false</c> sinon</returns>
	/// <param name="position">La position de la souris</param>
	private bool IsSourisSurTerrain( Vector3 position ){
		if (position.x < -98) return false;
		if (position.x > 0) return false;
		if (position.z < -94) return false;
		if (position.z > -8) return false;
		return true;
	}

	/// <summary>
	/// Supprime les game objects ayant servi en tant que pointeurs
	/// </summary>
	/// <param name="h">Hexagone où est la souris, null si la souris n'est plus sur le terrain</param>
	private void SupprimerPointeurs( HexagoneInfo h ){
		GameObject[] anciens = GameObject.FindGameObjectsWithTag("POINTEUR");
		// Cas où la souris n'est plus sur le terrain
		if ( h == null ){
			foreach ( GameObject go in anciens ) if ( go != null ) Destroy(go);
			return;
		}
		// Supprimer le game object de selection si un autre hexagone est visé
		if ( h != dernierHexagone ){
			if ( anciens.Length > 1 ){
				for ( int i = 0; i < anciens.Length; i++ ){
					if ( anciens[i] != null ) Destroy(anciens[i]);
				}
			}
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

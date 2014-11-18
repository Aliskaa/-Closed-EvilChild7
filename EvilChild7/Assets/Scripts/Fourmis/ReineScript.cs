/// <summary>
/// ReineScript.cs
/// Script pour gérer les phéromones
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 2.1.0
/// </remarks>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Classe pour gérer les phéromones
/// </summary>
public class ReineScript : MonoBehaviour, IAreaction {

	/* ********* *
	 * Attributs *
	 * ********* */
	
#region Attributs publics
	/// <summary>
	/// Une référence vers l'IA de la reine
	/// </summary>
	[HideInInspector]
	public IAappel iaReine;

	/// <summary>
	/// Le camp de la reine (blanc ou noir)
	/// </summary>
	public TypesCamps camp;
#endregion

	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes privées
	/// <summary>
	/// Mort "visible" de l'objet. Mort de la reine !
	/// </summary>
	public void Mourrir(){
		Vector3 position = transform.localPosition;
		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		scriptInvoc.InvoquerObjet(Invocations.PARTICULES_MORT_REINE, position);
		//MeshRenderer meshRender = gameObject.GetComponent<MeshRenderer>();
		//meshRender.enabled = false;
		Destroy(gameObject);
	}
#endregion

#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity au lancement du script
	/// </summary>
	void Awake(){
		switch (camp) {
			case TypesCamps.BLANC:
				iaReine = new IAreine( TypesObjetsRencontres.REINE_BLANCHE, (IAreaction) this );
				break;
			case TypesCamps.NOIR:
				iaReine = new IAreine( TypesObjetsRencontres.REINE_NOIRE, (IAreaction) this );
				break;
			default:
				Debug.LogError("Une reine ne peut avoir aucun camp");
				break;
		}
	}

#endregion
	
#region Méthodes public : Héritage de IAreaction.
	/// <summary>
	/// Ne fait rien (!). Obligation d'implémentation d'après
	/// la conception de l'IA
	/// </summary>
	public void bouger(TypesAxes direction, int nbCases){
		return;		
	}
	
	/// <summary>
	/// Ne fait rien (!). Obligation d'implémentation d'après
	/// la conception de l'IA
	/// </summary>
	public TypesAxes deambuler(){
		return TypesAxes.AUCUN;
	}
	
	/// <summary>
	/// Provoque la mort de la reine
	/// </summary>
	public void mourir(){
		Mourrir();
	}
	
	/// <summary>
	/// Ne fait rien (!). Obligation d'implémentation d'après
	/// la conception de l'IA
	/// </summary>
	public TypesAxes rentrerBase(){
		return TypesAxes.AUCUN;
	}
	
	/// <summary>
	/// Ne fait rien (!). Obligation d'implémentation d'après
	/// la conception de l'IA
	/// </summary>
	public void poserPheromones( bool activation ){
		return;
	}
#endregion

}


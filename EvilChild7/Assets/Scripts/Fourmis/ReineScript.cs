/// <summary>
/// ReineScript.cs
/// Script pour gérer les phéromones
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 2.0.0
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
	///
	/// </summary>
	public IAappel iaReine;

	/// <summary>
	/// The camp.
	/// </summary>
	public TypesCamps camp;
#endregion

	/* ******** *
	 * Méthodes *
	 * ******** */
	
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
	
	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame.
	/// </summary>
	void Update(){

	}
#endregion


	/// <summary>
	/// Bouger the specified direction and nbCases.
	/// </summary>
	/// <param name="direction">Direction.</param>
	/// <param name="nbCases">Nb cases.</param>
	public void bouger(TypesAxes direction, int nbCases){
		return;		
	}
	
	/// <summary>
	/// Deambuler this instance.
	/// </summary>
	public TypesAxes deambuler(){
		return TypesAxes.AUCUN;
	}
	
	/// <summary>
	/// Mourir this instance.
	/// </summary>
	public void mourir(){

	//	Mourrir();
	}
	
	/// <summary>
	/// Rentrers the base.
	/// </summary>
	public TypesAxes rentrerBase(){
		return TypesAxes.AUCUN;
	}
	
	/// <summary>
	/// Posers the pheromones.
	/// </summary>
	public void poserPheromones( bool activation ){
		return;
	}
}


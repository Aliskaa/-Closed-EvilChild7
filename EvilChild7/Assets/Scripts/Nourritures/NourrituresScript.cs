/// <summary>
/// NourrituresScript.cs
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
/// Classe pour gérer les nourritures
/// </summary>
public class NourrituresScript : MonoBehaviour, IAreaction {

	/* ********* *
	 * Attributs *
	 * ********* */

#region Attributs publics
	/// <summary>
	///
	/// </summary>
	public IAobjet iaNourriture;
#endregion

	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity
	/// </summary>
	void Awake(){
		iaNourriture = new IAnourriture((IAreaction) this);
	}
#endregion

#region Méthodes publics
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
#endregion
}


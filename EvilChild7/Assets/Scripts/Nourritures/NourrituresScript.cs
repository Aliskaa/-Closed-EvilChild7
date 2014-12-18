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
	/// Une référence vers l'IA gérant la nourriture
	/// </summary>
	[HideInInspector]
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

#region Méthodes publics venant de IAreaction
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
	/// Fait disparaitre le bonbon
	/// </summary>
	public void mourir(){
		//	Disparition();
		return;
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


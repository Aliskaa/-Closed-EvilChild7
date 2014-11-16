/// <summary>
/// PheromonesScript.cs
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
public class PheromonesScript : MonoBehaviour {

	/* ********* *
	 * Attributs *
	 * ********* */
	
#region Attributs privés
	/// <summary>
	/// Le pseudo timer
	/// </summary>
	private float Timer;
#endregion
	
#region Constantes privées
	/// <summary>
	/// La duréer de vie en secondes
	/// </summary>
	private const int DUREE_DE_VIE = 25;
#endregion

#region Attributs publics
	/// <summary>
	/// The direction.
	/// </summary>
	public TypesAxes direction;
#endregion

	/* ******** *
	 * Méthodes *
	 * ******** */
	
#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity au lancement du script
	/// </summary>
	void Awake(){
		this.Timer = DUREE_DE_VIE;
		direction = TypesAxes.AUCUN;
	}
	
	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame.
	/// </summary>
	void Update(){
		this.Timer -= Time.deltaTime;
		if ( this.Timer <= 0 ){
			Destroy(gameObject);
		}
	}
#endregion

}


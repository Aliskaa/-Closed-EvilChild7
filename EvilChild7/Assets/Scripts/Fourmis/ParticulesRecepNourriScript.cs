/// <summary>
/// ParticulesRecepNourriScript.cs
/// Script pour gérer les phéromones
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Classe pour gérer les particules émises à la réception de 
/// la nourriture. Typiquement
/// </summary>
public class ParticulesRecepNourriScript : MonoBehaviour {

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
	/// Le temps d'attente avant disparition
	/// </summary>
	private const int TEMPS_DISP = 5;
#endregion

#region Attributs publics
	/// <summary>
	/// The direction.
	/// </summary>
	[HideInInspector]
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
		this.Timer = TEMPS_DISP;
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


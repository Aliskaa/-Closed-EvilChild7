/// <summary>
/// ParticulesOeufScript.cs
/// Script pour gérer les particules apparaissant à l'éclosion
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
/// Classe pour gérer les particules apparaissant à l'éclosion
/// </summary>
public class ParticulesOeufScript : MonoBehaviour {


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
	/// La duréer d'incubation avant éclosion
	/// </summary>
	private const int DUREE_ATTENTE_AVANT_SUPPR = 20+5;	// OeufScript.DUREE_INCUBATION + delai
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */
	
#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity au lancement du script
	/// </summary>
	void Awake(){
		this.Timer = DUREE_ATTENTE_AVANT_SUPPR;
	}
	
	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame.
	/// </summary>
	void Update(){
		this.Timer -= Time.deltaTime;
		if ( this.Timer <= 0){
			Destroy(gameObject);
		}
	}
#endregion

}

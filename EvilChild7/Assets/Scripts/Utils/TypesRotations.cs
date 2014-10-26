/// <summary>
/// TypesRotations.cs
/// Fichier pour gérer les différents types de rotation d'un objet
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;


/// <summary>
/// Enumeration pour lister les différents types de rotation
/// </summary>
public enum TypesRotations : int { 
	/// <summary>
	/// Rotation aléatoire
	/// </summary>
	RANDOM = -1,
	/// <summary>
	/// Pas de rotation
	/// </summary>
	AUCUN = 0,
	/// <summary>
	/// Nord
	/// Typiquement, tete de la fourmis vers la direction -X, de bas en haut
	/// </summary>
	NORD = -90, 
	/// <summary>
	/// Nord Est
	/// </summary>
	NORD_EST = -45,
	/// <summary>
	/// Sud Est
	/// </summary>
	SUD_EST = +45,
	/// <summary>
	/// Sud : direction X, tete de la fourmis vers X, de haut en bas
	/// </summary>
	SUD = +90,
	/// <summary>
	/// Nord Ouest
	/// </summary>
	NORD_OUEST = -135,
	/// <summary>
	/// Sud Ouest
	/// </summary>
	SUD_OUEST = +135
}

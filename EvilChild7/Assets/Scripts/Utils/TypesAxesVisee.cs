/// <summary>
/// TypesAxesVisee.cs
/// Fichier pour gérer les différents types d'axe de visée pour entre autres les lancés de rayons
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;


/// <summary>
/// Enumeration pour lister les différents types d'axe de visée pour par exemple
/// détecter des obstacles
/// </summary>
public enum TypeAxes {
	/// <summary>
	/// Détection par devant
	/// </summary>
	DEVANT,
	/// <summary>
	/// Détection par derrière
	/// </summary>
	DERRIERE,
	/// <summary>
	/// Détection à gauche
	/// </summary>
	GAUCHE,
	/// <summary>
	/// Détection à droite
	/// </summary>
	DROITE,
	/// <summary>
	/// Détection au dessus
	/// </summary>
	DESSUS,
	/// <summary>
	/// Détection en dessous
	/// </summary>
	DESSOUS
}

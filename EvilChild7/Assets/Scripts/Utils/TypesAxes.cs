/// <summary>
/// TypesAxesVisee.cs
/// Fichier pour gérer les différents types d'axe de visée pour entre autres les lancés de rayons
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.1.1
/// </remarks>

using UnityEngine;


/// <summary>
/// Enumeration pour lister les différents types d'axe de visée pour par exemple
/// détecter des obstacles
/// </summary>
public enum TypesAxes {
	/// <summary>
	/// Détection par devant
	/// </summary>
	DEVANT,
	/// <summary>
	/// Détection par derrière
	/// </summary>
	DERRIERE,
	/// <summary>
	/// Détection en haut à gauche.
	/// </summary>
	DEVANT_GAUCHE,
	/// <summary>
	/// Détection en bas à gauche.
	/// </summary>
	DERRIERE_GAUCHE,
	/// <summary>
	/// Détection en haut à droite.
	/// </summary>
	DEVANT_DROITE,
	/// <summary>
	/// Détection en bas à droite.
	/// </summary>
	DERRIERE_DROITE,
	/// <summary>
	/// Détection au dessus
	/// </summary>
	DESSUS,
	/// <summary>
	/// Détection en dessous
	/// </summary>
	DESSOUS,
	/// <summary>
	/// Détection à gauche. A éviter.
	/// </summary>
	GAUCHE,
	/// <summary>
	/// Détection à droite. A éviter.
	/// </summary>
	DROITE
}

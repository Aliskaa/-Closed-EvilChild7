/// <summary>
/// TypesAxesVisee.cs
/// Fichier pour gérer les différents types d'axe de visée pour entre autres les lancés de rayons
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 2.0.0
/// </remarks>

using UnityEngine;


/// <summary>
/// Enumeration pour lister les différents types d'axe de visée pour par exemple
/// détecter des obstacles
/// </summary>
public enum TypesAxes : int{
	/// <summary>
	/// Vaeur nulle
	/// </summary>
	AUCUN = 0,
	/// <summary>
	/// Détection par devant
	/// </summary>
	DEVANT = 1,
	/// <summary>
	/// Détection par derrière
	/// </summary>
	DERRIERE = 2,
	/// <summary>
	/// Détection en haut à gauche.
	/// </summary>
	DEVANT_GAUCHE = 3,
	/// <summary>
	/// Détection en bas à gauche.
	/// </summary>
	DERRIERE_GAUCHE = 4,
	/// <summary>
	/// Détection en haut à droite.
	/// </summary>
	DEVANT_DROITE = 5,
	/// <summary>
	/// Détection en bas à droite.
	/// </summary>
	DERRIERE_DROITE = 6,
	/// <summary>
	/// Détection au dessus
	/// </summary>
	DESSUS = 7,
	/// <summary>
	/// Détection en dessous
	/// </summary>
	DESSOUS = 8,
	/// <summary>
	/// Détection à gauche. A éviter.
	/// </summary>
	GAUCHE = 9,
	/// <summary>
	/// Détection à droite. A éviter.
	/// </summary>
	DROITE = 10
}

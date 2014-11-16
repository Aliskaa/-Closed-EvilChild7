/// <summary>
/// TypesFourmis.cs
/// Enumeration listant les types de fourmis
/// http://forum.unity3d.com/threads/procedural-hexagon-terrain-tutorial.233296/
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 2.0.0
/// </remarks>

using UnityEngine;

/// <summary>
/// Les différentes castes de fourmis que peut prendre en charge le script.
/// Enumération utilisée entre autres dans des variables publiques pour des scripts
/// affectés aux fourmis
/// </summary>
public enum TypesFourmis {
	/// <summary>
	/// Une fourmi noire avec une marque evrte
	/// </summary>
	OUVRIERE_NOIRE,
	/// <summary>
	/// Une fourmi noire avec une marque rouge et un anneau rouge sur la tete
	/// </summary>
	COMBATTANTE_NOIRE,
	/// <summary>
	/// Une fourmi noire avec une marque jaune et un casque jaune sur la tete
	/// </summary>
	CONTREMAITRE_NOIRE,
	/// <summary>
	/// Une fourmi noire avec une marque bleu et un képi bleu sur la tete
	/// </summary>
	GENERALE_NOIRE,
	/// <summary>
	/// Une fourmi blanche avec une marque evrte
	/// </summary>
	OUVRIERE_BLANCHE,
	/// <summary>
	/// Une fourmi blanche avec une marque rouge et un anneau rouge sur la tete
	/// </summary>
	COMBATTANTE_BLANCHE,
	/// <summary>
	/// Une fourmi blanche avec une marque jaune et un casque jaune sur la tete
	/// </summary>
	CONTREMAITRE_BLANCHE,
	/// <summary>
	/// Une fourmi blanche avec une marque bleu et un képi bleu sur la tete
	/// </summary>
	GENERALE_BLANCHE
}

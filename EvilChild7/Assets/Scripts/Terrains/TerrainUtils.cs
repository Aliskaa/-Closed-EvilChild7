/// <summary>
/// TerrainUtils.cs
/// Fichier utilitaire pour gérer certains aspects du terrain,
/// à savoir les hexgones et la position des objets sur ces hexagone.
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Classe utilitaire pour gérer le terrain et ses hexagones
/// </summary>
public static class TerrainUtils {

	/* ********* *
	 * Attributs *
	 * ********* */
#region Attributs
	/// <summary>
	/// La liste d'hexagones
	/// </summary>
	private static List<HexagoneInfo> hexagones;
#endregion


	/* ************ *
	 * Constructeur *
	 * ************ */
#region Constructeurs
	/// <summary>
	/// Initalise la classe <see cref="TerrainUtils"/>
	/// </summary>
	static TerrainUtils(){
		hexagones = new List<HexagoneInfo>();
	}
#endregion

	/* ******** *
	 * Méthodes *
	 * ******** */
#region Méthodes
	/// <summary>
	/// Retourne l'hexagone le plus proche par rapport à la position de l'objet.
	/// Pour ce faire, les hexagones de la map vont etre parcouruts et leurs coordonnées
	/// en X et en Z vont etre récupérées. L'hexagone qui aura le X et le Z les plus proches
	/// des X et Z de l'objet sera l'hexagone le plus proche.
	/// </summary>
	/// <remarks>
	/// Cette opération étant gourmande (parcourt de tous els hexagones avec plusieurs tests),
	/// il vaut mieux faire cette opération dès qu'un objet attérit sur le terrain.
	/// </remarks>
	/// <returns>L'hexagone le plus proche</returns>
	/// <param name="positionObjet">La position de l'objet</param>
	public static HexagoneInfo hexagonePlusProche( Vector3 positionObjet ){
		bool flagOxXZ = false;
		float minimumDifferenceX = float.MaxValue;
		float minimumDifferenceZ = float.MaxValue;
		HexagoneInfo hexagPlusProche = null;
		foreach ( HexagoneInfo h in hexagones ){
			Vector3 posH = h.positionGlobale;
			float dX = Math.Abs(positionObjet.x - posH.x);
			float dZ = Math.Abs(positionObjet.z - posH.z);
			if ( dX <= minimumDifferenceX && dZ <= minimumDifferenceZ  ){
				minimumDifferenceX = dX;
				minimumDifferenceZ = dZ;
				flagOxXZ = true;
			} else {
				flagOxXZ = false;
			}
			if ( flagOxXZ ){
				hexagPlusProche = h;
			}
		}
		return hexagPlusProche;
	}

	/// <summary>
	/// Retourne un vecteur à appliquer pour décaler un objet 
	/// pour qu'il soit bien positionné sur un hexagone
	/// </summary>
	/// <returns>Le vevteur de correction à appliquer</returns>
	/// <param name="positionObjet">La position de l'objet à changer</param>
	public static Vector3 decalageHexagone( Vector3 positionObjet ){
		HexagoneInfo cible = hexagonePlusProche(positionObjet);
		Vector3 decalage = new Vector3(
								cible.positionGlobale.x-positionObjet.x,
								cible.positionGlobale.y-positionObjet.y,
								cible.positionGlobale.z-positionObjet.z
							);
		return decalage;
	}

	/// <summary>
	/// Enregistre un hexagone
	/// </summary>
	/// <param name="hexagone">L'hexagone à enregistrer</param>
	public static void ajouterHexagone( HexagoneInfo hexagone ){
		hexagones.Add(hexagone);
	}

	/// <summary>
	/// Supprime un hexagone
	/// </summary>
	/// <param name="hexagone">L'hexagone à supprimer</param>
	public static void supprimerHexagone( HexagoneInfo hexagone ){
		hexagones.Remove(hexagone);
	}

	/// <summary>
	/// Supprime tous les hexagones
	/// </summary>
	public static void supprimerHexagones(){
		hexagones = new List<HexagoneInfo>();
	}
#endregion

}

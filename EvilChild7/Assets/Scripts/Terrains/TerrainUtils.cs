/// <summary>
/// TerrainUtils.cs
/// Fichier utilitaire pour gérer certains aspects du terrain,
/// à savoir les hexgones et la position des objets sur ces hexagone.
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 2.0.0
/// </remarks>

using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Classe utilitaire pour gérer le terrain et ses hexagones
/// </summary>

// FIXME Méthode statique... A changer !
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
	/// La position donnée en apramètre doit etre une position locale/relative au
	/// game object "Terrain"
	/// </summary>
	/// <remarks>
	/// Cette opération étant gourmande (parcourt de tous les hexagones avec plusieurs tests),
	/// il vaut mieux faire cette opération dès qu'un objet attérit sur le terrain.
	/// </remarks>
	/// <returns>L'hexagone le plus proche</returns>
	/// <param name="positionObjet">La position de l'objet</param>
	public static HexagoneInfo HexagonePlusProche( Vector3 positionObjet ){
		bool flagOxXZ = false;
		float minimumDifferenceX = float.MaxValue;
		float minimumDifferenceZ = float.MaxValue;
		HexagoneInfo hexagPlusProche = null;
		foreach ( HexagoneInfo h in hexagones ){
			Vector3 posH = h.positionLocaleSurTerrain;
			float dX = Math.Abs(positionObjet.x - posH.x);
			float dZ = Math.Abs(positionObjet.z - posH.z);
			// 1er cas : on a directement un hexagone
			if ( posH.x == positionObjet.x && posH.z == positionObjet.z ){
				flagOxXZ = true;
				return h;
			}
			// 2ème cas : il faut touver l'hexagone le plus approprié
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
		if ( hexagPlusProche == null ){
			//Debug.Log("Aucun hexagone proche n'a été trouvé. Les coordonées sont-elles locales au terrain ?");
		}
		return hexagPlusProche;
	}

	/// <summary>
	/// Retourne un vecteur à appliquer pour décaler un objet 
	/// pour qu'il soit bien positionné sur un hexagone
	/// </summary>
	/// <returns>Le vevteur de correction à appliquer</returns>
	/// <param name="positionObjet">La position de l'objet à changer</param>
	public static Vector3 DecalageHexagone( Vector3 positionObjet ){
		HexagoneInfo cible = HexagonePlusProche(positionObjet);
		Vector3 decalage = new Vector3(
			cible.positionLocaleSurTerrain.x-positionObjet.x,
			cible.positionLocaleSurTerrain.y-positionObjet.y,
			cible.positionLocaleSurTerrain.z-positionObjet.z
		);
		return decalage;
	}

	/// <summary>
	/// Enregistre un hexagone
	/// </summary>
	/// <param name="hexagone">L'hexagone à enregistrer</param>
	public static void AjouterHexagone( HexagoneInfo hexagone ){
		//Debug.Log("Hexagone ajouté : " + hexagone.positionLocaleSurTerrain);
		hexagones.Add(hexagone);
	}

	/// <summary>
	/// Supprime un hexagone
	/// </summary>
	/// <param name="hexagone">L'hexagone à supprimer</param>
	public static void SupprimerHexagone( HexagoneInfo hexagone ){
		hexagones.Remove(hexagone);
	}

	/// <summary>
	/// Supprime tous les hexagones
	/// </summary>
	public static void SupprimerHexagones(){
		hexagones = new List<HexagoneInfo>();
	}

	/// <summary>
	/// Retourne le ième hexagone, null si positon <= 0 ou  position > nombres total d'hexagones
	/// </summary>
	/// <returns>Le <see cref="HexagoneInfo"/>.</returns>
	/// <param name="position">Position : entre 0 et hexagones.Count()-1</param>
	public static HexagoneInfo GetHexagoneAt( int position ){
		int indice = position - 1;
		if (indice < 0 || indice >= hexagones.Count) return null;
		return hexagones[indice];
	}

	/// <summary>
	/// Retourne les hexagones ayant de l'eau
	/// </summary>
	/// <returns>Une liste d'hexagones d'eau</returns>
	public static List<HexagoneInfo> GetHexagonesEau(){
		List<HexagoneInfo> hexagonesEau = new List<HexagoneInfo>();
		foreach ( HexagoneInfo h in hexagones ){
			if ( h.TextureAppliquee == TypesTerrains.EAU ){
				hexagonesEau.Add(h);
			}
		}
		return hexagonesEau;
	}

	/// <summary>
	/// Retourne les hexagones ayant du sable
	/// </summary>
	/// <returns>Une liste d'hexagones de sable</returns>
	public static List<HexagoneInfo> GetHexagonesSable(){
		List<HexagoneInfo> hexagonesSable = new List<HexagoneInfo>();
		foreach ( HexagoneInfo h in hexagones ){
			if ( h.TextureAppliquee == TypesTerrains.SABLE ){
				hexagonesSable.Add(h);
			}
		}
		return hexagonesSable;
	}

	/// <summary>
	/// Renvoie tous les hexagones
	/// </summary>
	/// <returns>Une liste avec tous les hexagones</returns>
	public static List<HexagoneInfo> GetHexagones(){
		return hexagones;
	}

	/// <summary>
	/// Vider la liste, i.e. les hexagones créés.
	/// </summary>
	/// <remarks>
	/// Impératif si on relance le jeu !
	/// </remarks>
	public static void Flush(){
		hexagones = new List<HexagoneInfo>();
	}
#endregion

}

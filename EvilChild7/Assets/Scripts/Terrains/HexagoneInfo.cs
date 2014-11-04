/// <summary>
/// HexagoneInfoScript.cs
/// Fichier pour gérer les données des hexagones
/// http://forum.unity3d.com/threads/procedural-hexagon-terrain-tutorial.233296/
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 2.2.0
/// </remarks>

using UnityEngine;
using System.Collections;

/// <summary>
/// Classe permettant de gérer les infos sur les hexagones.
/// Ces objets sont répliqués dans un tableau et servent pour le maillage d'un HexagonesVueScript.
/// Ce composant est ensuite affecté sur un game object "BlocTerrain" qui est un morceau de terrain.
/// </summary>
public class HexagoneInfo {


	/* ********* *
	 * Attributs *
	 * ********* */

#region Attributs publics
	/// <summary>
	/// La position locale, utile pour des calculs de meshs.
	/// Dépréciée.
	/// </summary>
	// FIXME Verifier la relle utilité, supprimer au besoin
	[HideInInspector]
	public Vector3 positionLocale;

	/// <summary>
	/// La position globale, utile pour des calculs 3D.
	/// Dépréciée.
	/// </summary>
	// FIXME Verifier la relle utilité, supprimer au besoin
	[HideInInspector]
	public Vector3 positionGlobale;

	/// <summary>
	/// La position locale de l'objet par rapport
	/// au GameObject grand-parent "Terrain" et non pas par rapport
	/// au GameObject parent "BlocTerrain" : ceci permet de faire une corrélation
	/// entre un objet sur le terrain et un hexagone.
	/// Représente la position "utile" de l'hexagone.
	/// </summary>
	[HideInInspector]
	public Vector3 positionLocaleSurTerrain;

	/// <summary>
	/// Coordonnées 3D sur la grille
	/// </summary>
	[HideInInspector]
	public Vector3 positionGrille;

	/// <summary>
	///
	/// </summary>
	[HideInInspector]
	public Vector3 hexExt;

	/// <summary>
	/// Le centre de l'hexagone
	/// </summary>
	[HideInInspector]
	public Vector3 centreHexagone;

	/// <summary>
	/// L'objet embarquant cet hexagone
	/// </summary>
	[HideInInspector]
	public HexagonesVueScript parentChunk;

	/// <summary>
	/// Le mesh Uity local
	/// </summary>
	[HideInInspector]
	public Mesh meshLocal;
	
   	/// <summary>
   	/// Tableau de sommets
   	/// </summary>
	[HideInInspector]
	public Vector3[] sommets;

	/// <summary>
	/// Tableau pour ramener dans des coordonnes 2D
	/// </summary>
	[HideInInspector]
	public Vector2[] uv;

	/// <summary>
	/// Les triangles de l'hexagone
	/// </summary>
	[HideInInspector]
	public int[] triangles;
#endregion

#region Attributs privés
	/// <summary>
	/// Le type de terrain de l'hexagone
	/// </summary>
	private TypesTerrains typeTerrain;
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes publiques
	/// <summary>
	/// Retourne la position axiale sur la grille
	/// </summary>
	/// <value>La position axiale sur la grille</value>
    public Vector2 AxialGridPosition
    {
        get { return new Vector2(CubeGridPosition.x, CubeGridPosition.y); }
    }
	
	/// <summary>
	/// Getter et setter pour la position 3D sur la grille
	/// </summary>
	/// <value>La position 3D sur la grille</value>
    public Vector3 CubeGridPosition
    {
        get { return positionGrille; }
        set { positionGrille = value; }
    }

	/// <summary>
	/// Getter et setters pour la texture du terrain
	/// </summary>
	/// <value>The texture appliquee.</value>
	public TypesTerrains TextureAppliquee
	{
		get { return typeTerrain; }
		set { typeTerrain = value; }
	}

    /// <summary>
    /// Procédure appellée par HexagoneVueScript
	/// pour faire le mesh d'un hexagone
    /// </summary>
    public void Initialiser(){
        DefinirMesh();
    }
#endregion

#region Attributs privés
    /// <summary>
    /// Récupère les données sur les hexagones depuis le TerrainManager
	/// pour les mettre dans le mesh Unity local
    /// </summary>
    private void DefinirMesh(){
        meshLocal = new Mesh();
        meshLocal.vertices = parentChunk.worldManager.meshHexagonesFlat.vertices;
        meshLocal.triangles = parentChunk.worldManager.meshHexagonesFlat.triangles;
        meshLocal.uv = parentChunk.worldManager.meshHexagonesFlat.uv;
        meshLocal.RecalculateNormals();
		meshLocal.name = "Hexagone";
    }
#endregion

}

/// <summary>
/// HexagoneInfoScript.cs
/// Fichier pour gérer les données des hexagones
/// http://forum.unity3d.com/threads/procedural-hexagon-terrain-tutorial.233296/
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.1.0
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
	/// La position locale
	/// </summary>
    public Vector3 positionLocale;

	/// <summary>
	/// La position globale
	/// </summary>
    public Vector3 positionGobale;

	/// <summary>
	/// Coordonnées 3D sur la grille
	/// </summary>
	public Vector3 positionGrille;

	/// <summary>
	///
	/// </summary>
    public Vector3 hexExt;

	/// <summary>
	/// Le centre de l'hexagone
	/// </summary>
    public Vector3 centreHexagone;

	/// <summary>
	/// L'objet embarquant cet hexagone
	/// </summary>
    public HexagonesVueScript parentChunk;

	/// <summary>
	/// Le mesh Uity local
	/// </summary>
    public Mesh meshLocal;
	
   	/// <summary>
   	/// Tableau de sommets
   	/// </summary>
    public Vector3[] sommets;

	/// <summary>
	/// Tableau pour ramener dans des coordonnes 2D
	/// </summary>
    public Vector2[] uv;

	/// <summary>
	/// Les triangles de l'hexagone
	/// </summary>
    public int[] triangles;
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
	/// Getters et setters pour la position 3D sur la grille
	/// </summary>
	/// <value>La position 3D sur la grille</value>
    public Vector3 CubeGridPosition
    {
        get { return positionGrille; }
        set { positionGrille = value; }
    }

    /// <summary>
    /// This is the setup called from HexChunk when it's ready for us to generate our meshes
    /// </summary>
    public void Start(){
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
    }
	#endregion

}

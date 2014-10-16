using UnityEngine;
using System.Collections;

/// <summary>
/// Hexagon cell.
/// Modélise un objet de type Hexagone pour un terrain hexagonal.
/// http://forum.unity3d.com/threads/procedural-hexagon-terrain-tutorial.233296/
/// </summary>
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>
public class HexagonCell : MonoBehaviour {

	/* ********* *
	 * Attributs *
	 * ********* */

	#region Attributs
	/// <summary>
	/// Les sommets d'un hexagone
	/// </summary>
	public Vector3[] vertices;

	/// <summary>
	/// Tableau de vecteur 2D pour appliquer des coordonnées 3D (u,v,w)
	/// vers des coordonnées 2D (u,v)
	/// </summary>
	public Vector2[] uv;

	/// <summary>
	/// Les différents triangles
	/// </summary>
	public int[] triangles;

	/// <summary>
	/// La texture à appliquer
	/// </summary>
	public Texture texture;
	#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

	#region Méthodes
	/// <summary>
	/// Méthode d'initialisation de l'instance de HexagonCell
	/// </summary>
	void Start(){
		CreerMesh();
	}

	/// <summary>
	/// Création du mesh, i.e. du maillage créant l'objet
	/// </summary>
	void CreerMesh(){

		#region Les différents points
		float floorLevel = 0;
		vertices = new Vector3[]{
			new Vector3(-1f , floorLevel, -0.5f),
			new Vector3(-1f, floorLevel, 0.5f),
			new Vector3(0f, floorLevel, 1f),
			new Vector3(1f, floorLevel, 0.5f),
			new Vector3(1f, floorLevel, -0.5f),
			new Vector3(0f, floorLevel, -1f)
		};
		#endregion
		
		#region Les triangles
		triangles = new int[]{
			1,5,0,
			1,4,5,
			1,2,4,
			2,3,4
		};
		#endregion
		
		#region Le tableau pour le mapping coord. 3D -> coord. 2D
		uv = new Vector2[]{
			new Vector2(0,0.25f),
			new Vector2(0,0.75f),
			new Vector2(0.5f,1),
			new Vector2(1,0.75f),
			new Vector2(1,0.25f),
			new Vector2(0.5f,0),
		};
		#endregion
		
		#region Finalisation de la création des hexagones
		// Ajout d'un mesh filter
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		// Ajout d'un mesh renderer
		gameObject.AddComponent<MeshRenderer>();
		
		// Création d'un maillage
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uv;
		
		// Recalcul des normales pour la lumière
		mesh.RecalculateNormals();
		
		// Application du mesh au mesh filter
		meshFilter.mesh = mesh;

		renderer.material.mainTexture = texture;
		#endregion
		
	}
	#endregion

}

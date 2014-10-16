using UnityEngine;
using System.Collections;

#region GenerationTerrainScript
/// <summary>
/// Génération du terrain.
/// http://forum.unity3d.com/threads/procedural-hexagon-terrain-tutorial.233296/
/// </summary>
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>
public class GenerationTerrainScript : MonoBehaviour {

	/* ********* *
	 * Attributs *
	 * ********* */

	#region Attributs
	public Mesh flatHexagonSharedMesh;
	public float hexRadiusSize;
	public Vector3 hexExt;
	public Vector3 hexSize;
	public Vector3 hexCenter;

	public GameObject chunkHolder;
	public Texture2D terrainTexture;
	
	int xSectors;
	int zSectors;
	
	public HexChunk[,] hexChunks;
	
	public Vector2 mapSize;
	public int chunkSize;
	#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

	#region Méthodes
	/// <summary>
	/// Appelée quand le script est appélé, avant Start().
	/// Récupère les dimensions des hexagones.
	/// Génère le terrain.
	/// </summary>
	void Awake(){
		CalculerHexProp();
		ConstruireMap();
	}

	/// <summary>
	/// Génération de la map
	/// </summary>
	private void ConstruireMap(){

		// Nombre de pièces pour la map
		xSectors = Mathf.CeilToInt(mapSize.x / chunkSize);
		zSectors = Mathf.CeilToInt(mapSize.y / chunkSize);
		hexChunks = new HexChunk[xSectors, zSectors];

		// Création des pièces
		for ( int x = 0; x < xSectors; x++ ){
			for ( int z = 0; z < zSectors; z++ ){
				hexChunks[x, z] = CreerPiece(x, z);
				hexChunks[x, z].gameObject.transform.position = new Vector3(x * (chunkSize * hexSize.x), 0f, (z * (chunkSize * hexSize.z) * (.75f)));
				hexChunks[x, z].hexSize = hexSize;
				hexChunks[x, z].SetSize(chunkSize, chunkSize);
				hexChunks[x, z].xSector = x;
				hexChunks[x, z].ySector = z;
				hexChunks[x, z].worldManager = this;
			}
		}

		foreach ( HexChunk chunk in hexChunks ){
			chunk.Begin();
		}

	}

	/// <summary>
	/// Définit les propriétés pour les hexagones.
	/// </summary>
	private void CalculerHexProp(){

		// Création d'un mesh pour calculer les frontières
		GameObject frontieres = new GameObject("Bounds Set Up: Flat");
		frontieres.AddComponent<MeshFilter> ();
		frontieres.AddComponent<MeshRenderer> ();
		frontieres.AddComponent<MeshCollider> ();
		// Réinitiliaser la position et les rotations
		frontieres.transform.position = Vector3.zero;
		frontieres.transform.rotation = Quaternion.identity;

		Vector3[] vertices;
		Vector2[] uv;
		int[] triangles;
		float floorLevel = 0;

		// Position des sommets de l'hexagone pour créer une figure
		vertices = new Vector3[]{
			/*0*/new Vector3((hexRadiusSize * Mathf.Cos((float)(2*Mathf.PI*(3+0.5)/6))), floorLevel, (hexRadiusSize * Mathf.Sin((float)(2*Mathf.PI*(3+0.5)/6)))),
			/*1*/new Vector3((hexRadiusSize * Mathf.Cos((float)(2*Mathf.PI*(2+0.5)/6))), floorLevel, (hexRadiusSize * Mathf.Sin((float)(2*Mathf.PI*(2+0.5)/6)))),
			/*2*/new Vector3((hexRadiusSize * Mathf.Cos((float)(2*Mathf.PI*(1+0.5)/6))), floorLevel, (hexRadiusSize * Mathf.Sin((float)(2*Mathf.PI*(1+0.5)/6)))),
			/*3*/new Vector3((hexRadiusSize * Mathf.Cos((float)(2*Mathf.PI*(0+0.5)/6))), floorLevel, (hexRadiusSize * Mathf.Sin((float)(2*Mathf.PI*(0+0.5)/6)))),
			/*4*/new Vector3((hexRadiusSize * Mathf.Cos((float)(2*Mathf.PI*(5+0.5)/6))), floorLevel, (hexRadiusSize * Mathf.Sin((float)(2*Mathf.PI*(5+0.5)/6)))),
			/*5*/new Vector3((hexRadiusSize * Mathf.Cos((float)(2*Mathf.PI*(4+0.5)/6))), floorLevel, (hexRadiusSize * Mathf.Sin((float)(2*Mathf.PI*(4+0.5)/6))))
		};
		
		// Triangles pour connecter les sommets
		triangles = new int[]{1,5,0,1,4,5,1,2,4,2,3,4};

		// Mappage pour avoir des coordonnées uv au lieu de uvw
		uv = new Vector2[]{
			new Vector2(0,0.25f),new Vector2(0,0.75f),new Vector2(0.5f,1),
			new Vector2(1,0.75f),new Vector2(1,0.25f),new Vector2(0.5f,0),
		};

		// Création du mesh
		flatHexagonSharedMesh = new Mesh();
		flatHexagonSharedMesh.vertices = vertices;
		flatHexagonSharedMesh.triangles = triangles;
		flatHexagonSharedMesh.uv = uv;

		frontieres.GetComponent<MeshFilter>().mesh = flatHexagonSharedMesh;
		frontieres.GetComponent<MeshFilter>().mesh.RecalculateNormals();
		frontieres.GetComponent<MeshCollider>().sharedMesh = flatHexagonSharedMesh;

		hexExt = new Vector3(frontieres.gameObject.collider.bounds.extents.x, frontieres.gameObject.collider.bounds.extents.y, frontieres.gameObject.collider.bounds.extents.z);
		hexSize = new Vector3(frontieres.gameObject.collider.bounds.size.x, frontieres.gameObject.collider.bounds.size.y, frontieres.gameObject.collider.bounds.size.z);
		hexCenter = new Vector3(frontieres.gameObject.collider.bounds.center.x, frontieres.gameObject.collider.bounds.center.y, frontieres.gameObject.collider.bounds.center.z);
	
		// Détruire l'objet pour les frontières, libération de ressources
		Destroy(frontieres);

	}

	/// <summary>
	/// Créé une nouvelle pièce pour la map
	/// </summary>
	/// <param name="largeur">La largeur de l'interval entre les pièces</param>
	/// <param name="hauteur">La hauteur de l'intervalle entre les pièces</param>
	/// <returns>Un objet de type HexagonCellMaterial</returns>
	public HexagonCellMaterial CreerPiece( int largeur, int hauteur ){

		if ( largeur == 0 && hauteur == 0 ){
			chunkHolder = new GameObject("ChunkHolder");
		}

		GameObject chunkObj = new GameObject("Chunk[" + largeur + "," + hauteur + "]");
		chunkObj.AddComponent<HexagonCellMaterial>();
		chunkObj.GetComponent<HexagonCellMaterial>().AllocateHexArray();
		chunkObj.AddComponent<MeshRenderer>().material.mainTexture = terrainTexture;
		chunkObj.AddComponent<MeshFilter>();
		chunkObj.transform.parent = chunkHolder.transform;

		return chunkObj.GetComponent<HexagonCellMaterial>();

	}
	#endregion

}
#endregion

#region TypeCase
/// <summary>
/// Les différents types de case pour le terrain
/// </summary>
public enum TypeCase {

	/// <summary>
	/// Case de type sable (terrain de base)
	/// </summary>
	Sable,

	/// <summary>
	/// Case de type eau (flaque, marre, ...)
	/// </summary>
	Eau

}
#endregion


// TODO http://forum.unity3d.com/threads/procedural-hexagon-terrain-tutorial.233296/
//		Partie B.HexChunck


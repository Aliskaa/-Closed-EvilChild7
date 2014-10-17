/// <summary>
/// TerrainManagerScript.cs
/// Script pour gérer le terrain
/// http://forum.unity3d.com/threads/procedural-hexagon-terrain-tutorial.233296/
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.1.0
/// </remarks>

using UnityEngine;
using System.Collections;


/// <summary>
/// Master controller of the world; makes the chunks and handles world values
/// </summary>
public class TerrainManagerScript : MonoBehaviour {

	/* ********* *
	 * Attributs *
	 * ********* */

    #region Attributs publics

	/// <summary>
	/// Le mesh pour des hexagones à la flat design
	/// </summary>
    [HideInInspector]
    public Mesh meshHexagonesFlat;

	/// <summary>
	/// Le rayon
	/// </summary>
    public float hexRadiusSize;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public Vector3 hexExt;
	/// <summary>
	/// La taille de l'hexagone
	/// </summary>
    [HideInInspector]
    public Vector3 tailleHexagone;
	/// <summary>
	/// Le centre de l'hexagone
	/// </summary>
	[HideInInspector]
    public Vector3 centreHexagone;

	/// <summary>
	/// Le game object associé
	/// </summary>
    [HideInInspector]
    public GameObject chunkHolder;

	/// <summary>
	/// La texture du terrain de type sable
	/// </summary>
    public Texture2D textureTerrainSable;

	/// <summary>
	/// La texture du terrain de type eau
	/// </summary>
	public Texture2D textureTerrainEau;

	/// <summary>
	/// Les pièces
	/// </summary>
	public HexagonesVueScript[,] pieces;

	/// <summary>
	/// La taille de la map
	/// </summary>
	public Vector2 tailleMap;

	/// <summary>
	/// La taille d'une pièce
	/// </summary>
	public int taillePiece;

	/// <summary>
	/// Le game object parent qui va embarquer le terrain
	/// </summary>
	public GameObject parentTerrain;

	/// <summary>
	/// Le type de terrain à générer
	/// </summary>
	public TypeMap type;
	#endregion

	#region Attributs package
	int segmentsEnX;
    int segmentsEnZ;
	#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

	#region Méthodes publics
	/// <summary>
	/// 
	/// </summary>
    public void Awake(){
        GetHexagonesProp();
		CreerMap(type);
    }

	/// <summary>
	/// Créé une nouvelle pièce
	/// </summary>
	/// <param name="x">La largeur entre les morceaux</param>
	/// <param name="y">La hauteur entre les pièces</param>
	/// <param name="type">Le type de terrain</param>
	/// <returns>The new chunk's script</returns>
	public HexagonesVueScript CreerPiece( int x, int y, TypeTuile type ){

		// Création du GameObject et ajout au parent
		if ( x == 0 && y == 0 && chunkHolder == null ){
			chunkHolder = new GameObject("Terrain");
			chunkHolder.transform.parent = parentTerrain.transform;
		}

		GameObject chunkObj = new GameObject("MorceauTerrain[" + x + "," + y + "]");
		chunkObj.AddComponent<HexagonesVueScript>();
		Texture2D texture;
		switch (type) {
			case TypeTuile.SABLE:
				texture = textureTerrainSable;
				break;
			case TypeTuile.EAU:
				texture = textureTerrainEau;
				break;
			default:
				texture = textureTerrainSable;
				break;
		}
		chunkObj.AddComponent<MeshRenderer>().material.mainTexture = texture;
		chunkObj.AddComponent<MeshFilter>();
		chunkObj.transform.parent = chunkHolder.transform;

		return chunkObj.GetComponent<HexagonesVueScript>();

	}
	#endregion
	
	#region Méthodes privées
	/// <summary>
	/// Créé des meshes pour les hexagones 
	/// </summary>
	private void GetHexagonesProp(){

        GameObject inst = new GameObject("Bounds Set Up: Flat");
        inst.AddComponent<MeshFilter>();
        inst.AddComponent<MeshRenderer>();
        inst.AddComponent<MeshCollider>();
        inst.transform.position = Vector3.zero;
        inst.transform.rotation = Quaternion.identity;

        Vector3[] _sommets;
        int[] _triangles;
        Vector2[] _uv;

        #region Les sommets
        float floorLevel = 0;

        _sommets = new Vector3[]{
                /*0*/new Vector3((hexRadiusSize * Mathf.Cos((float)(2*Mathf.PI*(3+0.5)/6))), floorLevel, (hexRadiusSize * Mathf.Sin((float)(2*Mathf.PI*(3+0.5)/6)))),
                /*1*/new Vector3((hexRadiusSize * Mathf.Cos((float)(2*Mathf.PI*(2+0.5)/6))), floorLevel, (hexRadiusSize * Mathf.Sin((float)(2*Mathf.PI*(2+0.5)/6)))),
                /*2*/new Vector3((hexRadiusSize * Mathf.Cos((float)(2*Mathf.PI*(1+0.5)/6))), floorLevel, (hexRadiusSize * Mathf.Sin((float)(2*Mathf.PI*(1+0.5)/6)))),
                /*3*/new Vector3((hexRadiusSize * Mathf.Cos((float)(2*Mathf.PI*(0+0.5)/6))), floorLevel, (hexRadiusSize * Mathf.Sin((float)(2*Mathf.PI*(0+0.5)/6)))),
                /*4*/new Vector3((hexRadiusSize * Mathf.Cos((float)(2*Mathf.PI*(5+0.5)/6))), floorLevel, (hexRadiusSize * Mathf.Sin((float)(2*Mathf.PI*(5+0.5)/6)))),
                /*5*/new Vector3((hexRadiusSize * Mathf.Cos((float)(2*Mathf.PI*(4+0.5)/6))), floorLevel, (hexRadiusSize * Mathf.Sin((float)(2*Mathf.PI*(4+0.5)/6))))
        };
        #endregion

        #region Les triangles
        _triangles = new int[]{1,5,0,1,4,5,1,2,4,2,3,4};
        #endregion

        #region uv
        _uv = new Vector2[]{
                new Vector2(0,0.25f),
                new Vector2(0,0.75f),
                new Vector2(0.5f,1),
                new Vector2(1,0.75f),
                new Vector2(1,0.25f),
                new Vector2(0.5f,0),
            };
        #endregion

        meshHexagonesFlat = new Mesh();
        meshHexagonesFlat.vertices = _sommets;
        meshHexagonesFlat.triangles = _triangles;
        meshHexagonesFlat.uv = _uv;
        inst.GetComponent<MeshFilter>().mesh = meshHexagonesFlat;
        inst.GetComponent<MeshFilter>().mesh.RecalculateNormals();
        inst.GetComponent<MeshCollider>().sharedMesh = meshHexagonesFlat;

        hexExt = new Vector3(inst.gameObject.collider.bounds.extents.x, inst.gameObject.collider.bounds.extents.y, inst.gameObject.collider.bounds.extents.z);
        tailleHexagone = new Vector3(inst.gameObject.collider.bounds.size.x, inst.gameObject.collider.bounds.size.y, inst.gameObject.collider.bounds.size.z);
        centreHexagone = new Vector3(inst.gameObject.collider.bounds.center.x, inst.gameObject.collider.bounds.center.y, inst.gameObject.collider.bounds.center.z);

		Destroy(inst);

    }

	/// <summary>
	/// Créé le terrain en générant les pièces.
	/// Beaucoup de sable, partout, rien d'autres.
	/// </summary>
	private void CreerMapDesert(){
		
		// Détemrine le nombre de pièces
		segmentsEnX = Mathf.CeilToInt(tailleMap.x / taillePiece);
		segmentsEnZ = Mathf.CeilToInt(tailleMap.y / taillePiece);
		
		pieces = new HexagonesVueScript[segmentsEnX, segmentsEnZ];
		
		for ( int x = 0; x < segmentsEnX; x++ ){
			for ( int z = 0; z < segmentsEnZ; z++ ){
				pieces[x, z] = CreerPiece(x, z, TypeTuile.SABLE);
				pieces[x, z].gameObject.transform.position = new Vector3(x * (taillePiece * tailleHexagone.x), 0f, (z * (taillePiece * tailleHexagone.z) * (.75f)));
				pieces[x, z].dimensionHexagone = tailleHexagone;
				pieces[x, z].SetDimension(taillePiece, taillePiece);
				pieces[x, z].xSector = x;
				pieces[x, z].ySector = z;
				pieces[x, z].worldManager = this;
			}
		}
		
		foreach ( HexagonesVueScript chunk in pieces ){
			chunk.Demarrer();
		}
		
	}

	/// <summary>
	/// Créé le terrain en générant les pièces.
	/// De l'eau et du sable.
	/// </summary>
	private void CreerMapLac(){
		
		// Détemrine le nombre de pièces
		segmentsEnX = Mathf.CeilToInt(tailleMap.x / taillePiece);
		segmentsEnZ = Mathf.CeilToInt(tailleMap.y / taillePiece);
		
		pieces = new HexagonesVueScript[segmentsEnX, segmentsEnZ];
		
		for ( int x = 0; x < segmentsEnX; x++ ){
			for ( int z = 0; z < segmentsEnZ; z++ ){
				if ( x >= (segmentsEnX/3) && x <= (2*segmentsEnX/3)
				    && z >= (segmentsEnZ/3) && z <= (2*segmentsEnZ/3) ){
					pieces[x, z] = CreerPiece(x, z, TypeTuile.EAU);
				} else {
					pieces[x, z] = CreerPiece(x, z, TypeTuile.SABLE);
				}
				pieces[x, z].gameObject.transform.position = new Vector3(x * (taillePiece * tailleHexagone.x), 0f, (z * (taillePiece * tailleHexagone.z) * (.75f)));
				pieces[x, z].dimensionHexagone = tailleHexagone;
				pieces[x, z].SetDimension(taillePiece, taillePiece);
				pieces[x, z].xSector = x;
				pieces[x, z].ySector = z;
				pieces[x, z].worldManager = this;
			}
		}
		
		foreach ( HexagonesVueScript chunk in pieces ){
			chunk.Demarrer();
		}
		
	}

	/// <summary>
	/// Créé le terrain en générant les pièces.
	/// Enormément d'eau.
	/// </summary>
	private void CreerMapEtranglement(){
		
		// Détermine le nombre de pièces
		segmentsEnX = Mathf.CeilToInt(tailleMap.x / taillePiece);
		segmentsEnZ = Mathf.CeilToInt(tailleMap.y / taillePiece);
		
		pieces = new HexagonesVueScript[segmentsEnX, segmentsEnZ];

		// FIXME : Trop lourd
		for ( int x = 0; x < segmentsEnX; x++ ){
			for ( int z = 0; z < segmentsEnZ; z++ ){
				pieces[x, z] = CreerPiece(x, z, TypeTuile.EAU);
				if ( x >= segmentsEnX/4 && x <= segmentsEnX/3
				    && z >= segmentsEnZ/4 && z <= segmentsEnZ/3){
					pieces[x, z] = CreerPiece(x, z, TypeTuile.SABLE);
				}
				if ( x >= 2*segmentsEnX/3 && x <= 3*segmentsEnX/4
				    && z <= segmentsEnZ/3){
					pieces[x, z] = CreerPiece(x, z, TypeTuile.SABLE);
				}
				if ( x >= segmentsEnX/3 && x <= 2*segmentsEnX/3
				    && z >= segmentsEnZ/4 && z <= segmentsEnZ/3){
					pieces[x, z] = CreerPiece(x, z, TypeTuile.SABLE);
				}
				if ( x >= segmentsEnX/4 && x <= segmentsEnX/3
				    && z >= segmentsEnZ/3 && z <= 3*segmentsEnZ/4){
					pieces[x, z] = CreerPiece(x, z, TypeTuile.SABLE);
				}
				if ( x >= segmentsEnX/3 && x <= 3*segmentsEnX/4
				    && z >= 2*segmentsEnZ/3 && z <= 3*segmentsEnZ/4){
					pieces[x, z] = CreerPiece(x, z, TypeTuile.SABLE);
				}
				if ( x >= 2*segmentsEnX/3 && x <= 3*segmentsEnX/4
				    && z >= 3*segmentsEnZ/4){
					pieces[x, z] = CreerPiece(x, z, TypeTuile.SABLE);
				}
				pieces[x, z].gameObject.transform.position = new Vector3(x * (taillePiece * tailleHexagone.x), 0f, (z * (taillePiece * tailleHexagone.z) * (.75f)));
				pieces[x, z].dimensionHexagone = tailleHexagone;
				pieces[x, z].SetDimension(taillePiece, taillePiece);
				pieces[x, z].xSector = x;
				pieces[x, z].ySector = z;
				pieces[x, z].worldManager = this;
			}
		}
		
		foreach ( HexagonesVueScript chunk in pieces ){
			chunk.Demarrer();
		}
		
	}

	/// <summary>
	/// Créé le terrain en générant les pièces.
	/// Enormément d'eau avec quelques petits passages.
	/// </summary>
	private void CreerMapTraversees(){

		// Détermine le nombre de pièces
		segmentsEnX = Mathf.CeilToInt(tailleMap.x / taillePiece);
		segmentsEnZ = Mathf.CeilToInt(tailleMap.y / taillePiece);
		
		pieces = new HexagonesVueScript[segmentsEnX, segmentsEnZ];
		
		// FIXME : Trop lourd
		for ( int x = 0; x < segmentsEnX; x++ ){
			for ( int z = 0; z < segmentsEnZ; z++ ){
				pieces[x, z] = CreerPiece(x, z, TypeTuile.EAU);
				if ( x >= segmentsEnX/8 && x <= segmentsEnX/7 ){
					pieces[x, z] = CreerPiece(x, z, TypeTuile.SABLE);
				}
				if ( x >= 9*segmentsEnX/10 ){
					pieces[x, z] = CreerPiece(x, z, TypeTuile.SABLE);
				}
				if ( z >= 4*segmentsEnZ/9 && z <= 5*segmentsEnZ/9 ){
					pieces[x, z] = CreerPiece(x, z, TypeTuile.SABLE);
				}
				pieces[x, z].gameObject.transform.position = new Vector3(x * (taillePiece * tailleHexagone.x), 0f, (z * (taillePiece * tailleHexagone.z) * (.75f)));
				pieces[x, z].dimensionHexagone = tailleHexagone;
				pieces[x, z].SetDimension(taillePiece, taillePiece);
				pieces[x, z].xSector = x;
				pieces[x, z].ySector = z;
				pieces[x, z].worldManager = this;
			}
		}
		
		foreach ( HexagonesVueScript chunk in pieces ){
			chunk.Demarrer();
		}

	}

	/// <summary>
	/// Créé le terrain en générant les pièces.
	/// Tirage au hasard.
	/// </summary>
	private void CreerMapRandom(){

		// Détermine le nombre de pièces
		segmentsEnX = Mathf.CeilToInt(tailleMap.x / taillePiece);
		segmentsEnZ = Mathf.CeilToInt(tailleMap.y / taillePiece);
		
		pieces = new HexagonesVueScript[segmentsEnX, segmentsEnZ];

		for ( int x = 0; x < segmentsEnX; x++ ){
			for ( int z = 0; z < segmentsEnZ; z++ ){
				// 1 : Type sable, 2 : Type eau
				int numType = Random.Range(1,200);
				pieces[x, z] = CreerPiece(x, z, (TypeTuile)(numType%2+1));
				pieces[x, z].gameObject.transform.position = new Vector3(x * (taillePiece * tailleHexagone.x), 0f, (z * (taillePiece * tailleHexagone.z) * (.75f)));
				pieces[x, z].dimensionHexagone = tailleHexagone;
				pieces[x, z].SetDimension(taillePiece, taillePiece);
				pieces[x, z].xSector = x;
				pieces[x, z].ySector = z;
				pieces[x, z].worldManager = this;
			}
		}
		
		foreach ( HexagonesVueScript chunk in pieces ){
			chunk.Demarrer();
		}

	}
	#endregion

	#region Méthodes package
	/// <summary>
	/// Créé la map selon le type voulu
	/// </summary>
	/// <param name="typeMap">Le type de terrain</param>
	void CreerMap( TypeMap typeMap ){
		switch (typeMap){
			case TypeMap.NIVEAU_1:
				CreerMapDesert();
				break;
			case TypeMap.NIVEAU_2:
				CreerMapLac();
				break;
			case TypeMap.NIVEAU_3:
				CreerMapTraversees();
				break;
			case TypeMap.NIVEAU_4:
				CreerMapEtranglement();
				break;
			case TypeMap.RANDOM:
				CreerMapRandom();
				break;
		}
	}
	#endregion
	
}

#region TypeTuile
/// <summary>
/// Les types de tuiles
/// </summary>
public enum TypeTuile : int { 
	/// <summary>
	/// Du sable
	/// </summary>
	SABLE = 1, 
	/// <summary>
	/// De l'eau
	/// </summary>
	EAU = 2
}
#endregion

#region TypeMap
/// <summary>
/// Les types de map
/// </summary>
public enum TypeMap { 
	/// <summary>
	/// Que du sable, le désert
	/// </summary>
	NIVEAU_1, 
	/// <summary>
	/// De l'eau et du sable, un lac
	/// </summary>
	NIVEAU_2,
	/// <summary>
	/// Beaucoup d'eau, une grande traversée
	/// </summary>
	NIVEAU_3,
	/// <summary>
	/// Encore plus d'eau, un zig-zag
	/// </summary>
	NIVEAU_4,
	/// <summary>
	/// Génération aléatoire de la map
	/// </summary>
	RANDOM
}
#endregion

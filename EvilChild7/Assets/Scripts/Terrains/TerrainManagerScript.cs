/// <summary>
/// TerrainManagerScript.cs
/// Script pour gérer le terrain
/// http://forum.unity3d.com/threads/procedural-hexagon-terrain-tutorial.233296/
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;
using System;
using System.Collections.Generic;


#region TerrainManagerScript
/// <summary>
/// Master controller of the world; makes the chunks and handles world values
/// Gestionnaire de terrain : créé les blocs ("morceaux de terrains") et gère les valeurs du monde.
/// Le "bac à sable" contient un "terrain" qui contient plusieurs 'morceaux de terrains", ou blocs.
/// Ces blocs ont un maillage d'hexagones de meme texture.
/// </summary>
/// <see cref="HexagonesVueScript"/>
/// <see cref="HexagoneInfo"/> 
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
	/// La texture du terrain de type sable avec hexagones visibles
	/// </summary>
    public Texture2D textureTerrainSableHexagones;
	
	/// <summary>
	/// La texture du terrain de type sable sans hexagones visibles
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
	public TypesMaps type;

	/// <summary>
	/// Flag indiquant s'il faut afficher les hexagones ou non
	/// </summary>
	public bool afficherHexagones;
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
	/// Créé une nouvelle pièce
	/// </summary>
	/// <param name="x">La largeur entre les morceaux</param>
	/// <param name="y">La hauteur entre les pièces</param>
	/// <param name="type">Le type de terrain</param>
	/// <returns>The new chunk's script</returns>
	public HexagonesVueScript CreerPiece( int x, int y, TypesTerrains type ){

		// Création du GameObject et ajout au parent
		if ( x == 0 && y == 0 && chunkHolder == null ){
			chunkHolder = GameObject.Find("Terrain");
			chunkHolder.transform.parent = parentTerrain.transform;
		}

		GameObject chunkObj = new GameObject("BlocTerrain[" + x + "," + y + "]");
		chunkObj.AddComponent<HexagonesVueScript>();
		Texture2D texture;
		switch (type) {
			case TypesTerrains.SABLE:
				texture = (afficherHexagones ? textureTerrainSable : textureTerrainSableHexagones);
				break;
			case TypesTerrains.EAU:
				texture = textureTerrainEau;
				break;
			default:
				texture = textureTerrainSableHexagones;
				break;
		}
		chunkObj.AddComponent<MeshRenderer>().material.mainTexture = texture;
		chunkObj.AddComponent<MeshFilter>();
		chunkObj.transform.parent = chunkHolder.transform;

		return chunkObj.GetComponent<HexagonesVueScript>();

	}

	/// <summary>
	/// Vérifie si le terrain est rempli, i.e. si tous les hexagones sont créés
	/// </summary>
	/// <returns><c>true</c>, si le terrain est rempli, <c>false</c> sinon.</returns>
	public bool VerifierRemplissageTerrain(){
		float nombreHexagonesAttendus = tailleMap.x * tailleMap.y;
		return TerrainUtils.GetHexagones().Count == nombreHexagonesAttendus;
	}

	/// <summary>
	/// Va convertir des coordonées 3D GLOBALES (typqiuement venant d'un clic de souris ou autres)
	/// en coordonnées 3D LOCALES au terrain.
	/// </summary>
	/// <param name="coord">Des coordonnées globales</param>
	public Vector3 ConvertirCoordonnes( Vector3 coord ){
		chunkHolder = GameObject.Find("Terrain");
		Vector3 coordLocalesTerrain = chunkHolder.transform.worldToLocalMatrix.MultiplyPoint(coord);
		//Debug.Log("Coordonnées locales calculées : " + coordLocalesTerrain);
		return coordLocalesTerrain;
	}

	/// <summary>
	/// Converti une case de sable en une case d'eau
	/// </summary>
	/// <param name="coordonnees">Les coordonnées de l'hexagone ciblé (locales au terrain)</param>
	public void ConvertirCaseEau( Vector3 coordonnees ){
		HexagoneInfo hexagoneClick = TerrainUtils.HexagonePlusProche(coordonnees);
		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		scriptInvoc.InvoquerObjet(Invocations.EAU, hexagoneClick.positionLocaleSurTerrain);
		scriptInvoc.InvoquerObjet(Invocations.EAU3D, hexagoneClick.positionLocaleSurTerrain);
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
				pieces[x, z] = CreerPiece(x, z, TypesTerrains.SABLE);
				pieces[x, z].gameObject.transform.position = new Vector3(x * (taillePiece * tailleHexagone.x), 0f, (z * (taillePiece * tailleHexagone.z) * (.75f)));
				pieces[x, z].dimensionHexagone = tailleHexagone;
				pieces[x, z].SetDimension(taillePiece, taillePiece);
				pieces[x, z].xSector = x;
				pieces[x, z].ySector = z;
				pieces[x, z].worldManager = this;
				/*
				Debug.Log ("Positions bloc terrain : "
				           +"global:"+pieces[x, z].gameObject.transform.position
				           +" local:"+pieces[x, z].gameObject.transform.localPosition);
				*/
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
					pieces[x, z] = CreerPiece(x, z, TypesTerrains.EAU);
				} else {
					pieces[x, z] = CreerPiece(x, z, TypesTerrains.SABLE);
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
				pieces[x, z] = CreerPiece(x, z, TypesTerrains.EAU);
				if ( z >= segmentsEnZ/4 && z <= segmentsEnZ/3
				    && x >= segmentsEnX/4 && x <= segmentsEnX/3){
					pieces[x, z] = CreerPiece(x, z, TypesTerrains.SABLE);
				}
				if ( z >= 2*segmentsEnZ/3 && z <= 3*segmentsEnZ/4
				    && x <= segmentsEnX/3){
					pieces[x, z] = CreerPiece(x, z, TypesTerrains.SABLE);
				}
				if ( z >= segmentsEnZ/3 && z <= 2*segmentsEnZ/3
				    && x >= segmentsEnX/4 && x <= segmentsEnX/3){
					pieces[x, z] = CreerPiece(x, z, TypesTerrains.SABLE);
				}
				if ( z >= segmentsEnZ/4 && z <= segmentsEnZ/3
				    && x >= segmentsEnX/3 && x <= 3*segmentsEnX/4){
					pieces[x, z] = CreerPiece(x, z, TypesTerrains.SABLE);
				}
				if ( z >= segmentsEnZ/3 && z <= 3*segmentsEnZ/4
				    && x >= 2*segmentsEnX/3 && x <= 3*segmentsEnX/4){
					pieces[x, z] = CreerPiece(x, z, TypesTerrains.SABLE);
				}
				if ( z >= 2*segmentsEnZ/3 && z <= 3*segmentsEnZ/4
				    && x >= 3*segmentsEnX/4){
					pieces[x, z] = CreerPiece(x, z, TypesTerrains.SABLE);
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
				pieces[x, z] = CreerPiece(x, z, TypesTerrains.EAU);
				if ( x >= segmentsEnX/8 && x <= segmentsEnX/7 ){
					pieces[x, z] = CreerPiece(x, z, TypesTerrains.SABLE);
				}
				if ( x >= 9*segmentsEnX/10 ){
					pieces[x, z] = CreerPiece(x, z, TypesTerrains.SABLE);
				}
				if ( z >= 4*segmentsEnZ/9 && z <= 5*segmentsEnZ/9 ){
					pieces[x, z] = CreerPiece(x, z, TypesTerrains.SABLE);
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
				int numType = UnityEngine.Random.Range(1,200);
				pieces[x, z] = CreerPiece(x, z, (TypesTerrains)(numType%2+1));
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
	/// Créé la map selon le type voulu
	/// </summary>
	/// <param name="typeMap">Le type de terrain</param>
	private void CreerMap( TypesMaps typeMap ){
//		Debug.Log("Création du terrain : "+typeMap);
		switch (typeMap){
			case TypesMaps.NIVEAU_1:
				CreerMapDesert();
				break;
			case TypesMaps.NIVEAU_2:
				CreerMapLac();
				break;
			case TypesMaps.NIVEAU_3:
				CreerMapTraversees();
				break;
			case TypesMaps.NIVEAU_4:
				CreerMapEtranglement();
				break;
			case TypesMaps.RANDOM:
				CreerMapRandom();
				break;
		}
		// FIXME : Valeur en dur, Translate() non relatif, sale
		chunkHolder.transform.Translate(new Vector3 (-98.5f, 0.1f, -93));
	}

	/// <summary>
	/// Créé la map 3D, c'est à dire définit les gameobjets
	/// pour repérer "facilement" certains éléments comme l'eau.
	/// Pour ce faire, tous les hexagones vont etre parcourus et si un hexagone
	/// est une case d'eau, un game object sera mis dessus afin de repérer cette case 2D d'eau via un objet 3D
	/// </summary>
	private void CreerMap3D(){
		List<HexagoneInfo> hexagonesEau = TerrainUtils.GetHexagonesEau();
		GameObject bacAsable = GameObject.Find("Bac à sable");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		foreach ( HexagoneInfo h in hexagonesEau ){
			scriptInvoc.InvoquerObjet(Invocations.EAU3D, h.positionLocaleSurTerrain);
		}
	}

	/// <summary>
	/// Place un gameobject sur chaque hexagone pour bien vérifier les coordonnées associées
	/// </summary>
	private void DebugHexagones(){
		List<HexagoneInfo> hexags = TerrainUtils.GetHexagones();
		GameObject bacAsable = GameObject.Find("Bac à sable");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		foreach ( HexagoneInfo h in hexags ){
			scriptInvoc.InvoquerObjet(Invocations.DEBUG_OBJECT, h.positionLocaleSurTerrain);
		}
	}

	/// <summary>
	/// Va placer les reines sur le terrain
	/// </summary>
	private void PlacerReines(){
		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		HexagoneInfo h;
		switch (type){
			case TypesMaps.NIVEAU_1:
				// Reine blanche dans le 37ème hexagone (sur 400)
				h = TerrainUtils.GetHexagoneAt(37);
				scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_REINE, h.positionLocaleSurTerrain);
				// Reine noire dans le 360ème hexagone (sur 400)
				h = TerrainUtils.GetHexagoneAt(360);
				scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_REINE, h.positionLocaleSurTerrain);
				break;
			case TypesMaps.NIVEAU_2:
				// Reine blanche dans le 37ème hexagone (sur 400)
				h = TerrainUtils.GetHexagoneAt(37);
				scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_REINE, h.positionLocaleSurTerrain);
				// Reine noire dans le 360ème hexagone (sur 400)
				h = TerrainUtils.GetHexagoneAt(360);
				scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_REINE, h.positionLocaleSurTerrain);
				break;
			case TypesMaps.NIVEAU_3:
				// Reine blanche dans le 37ème hexagone (sur 400)
				h = TerrainUtils.GetHexagoneAt(37);
				scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_REINE, h.positionLocaleSurTerrain);
				// Reine noire dans le 360ème hexagone (sur 400)
				h = TerrainUtils.GetHexagoneAt(360);
				scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_REINE, h.positionLocaleSurTerrain);
				break;
			case TypesMaps.NIVEAU_4:
				// Reine blanche dans le 61ème hexagone (sur 400)
				h = TerrainUtils.GetHexagoneAt(61);
				scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_REINE, h.positionLocaleSurTerrain);
				// Reine noire dans le 384ème hexagone (sur 400)
				h = TerrainUtils.GetHexagoneAt(384);
				scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_REINE, h.positionLocaleSurTerrain);
				break;
			case TypesMaps.RANDOM:
				// Reine blanche en random
				int caseAuHasard = UnityEngine.Random.Range(0,TerrainUtils.GetHexagonesSable().Count);
				h = TerrainUtils.GetHexagonesSable()[caseAuHasard];
				scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_REINE, h.positionLocaleSurTerrain);
				// Reine noire en random
				caseAuHasard = UnityEngine.Random.Range(0,TerrainUtils.GetHexagonesSable().Count);
				h = TerrainUtils.GetHexagonesSable()[caseAuHasard];
				scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_REINE, h.positionLocaleSurTerrain);
				break;
		}
	}
#endregion

#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity au lancement du script
	/// </summary>
	void Awake(){
		GetHexagonesProp();
		//Debug.Log("Création du terrain : "+type);
		CreerMap(type);
		CreerMap3D();
		PlacerReines();
	}
#endregion
	
}
#endregion

#region TypeTuile
/// <summary>
/// Les types de terrains
/// </summary>
public enum TypesTerrains : int { 
	/// <summary>
	/// Aucun type de terrain
	/// </summary>
	AUCUN = 0,
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
public enum TypesMaps : int { 
	/// <summary>
	/// Que du sable, le désert
	/// </summary>
	NIVEAU_1 = 1, 
	/// <summary>
	/// De l'eau et du sable, un lac
	/// </summary>
	NIVEAU_2 = 2,
	/// <summary>
	/// Beaucoup d'eau, une grande traversée
	/// </summary>
	NIVEAU_3 = 3,
	/// <summary>
	/// Encore plus d'eau, un zig-zag
	/// </summary>
	NIVEAU_4 = 4,
	/// <summary>
	/// Génération aléatoire de la map
	/// </summary>
	RANDOM = 0
}
#endregion

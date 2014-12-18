/// <summary>
/// HexagoneVueScript.cs
/// Script permettant d'avoir des pièces d'hexagones
/// http://forum.unity3d.com/threads/procedural-hexagon-terrain-tutorial.233296/
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.4.0
/// </remarks>

using UnityEngine;
using System.Collections;

/// <summary>
/// Script permettant de crééer des hexagones.
/// Ce composant est affecté à un game object qui est un morceau de terrain, "BlocTerrain".
/// Un maillage d'hexagones est fait sur cette vue, et une texture y est appliquée.
/// Les infos des hexagones sont dans des objets HexagonesInfos.
/// </summary>
/// <see cref="MonoBehaviour"/>
/// <see cref="HexagoneInfo"/> 
public class HexagonesVueScript : MonoBehaviour {

	/* ********* *
	 * Attributs *
	 * ********* */

#region Attributs privés
	/// <summary>
	/// Le mesh filter Unity à utiliser
	/// </summary>
	private MeshFilter filter;

	/// <summary>
	/// Le box collider d'Unity à utiliser
	/// </summary>
	private new BoxCollider collider;
#endregion

#region Attributs publics
    /// <summary>
    /// Le tableau d'hexagones
    /// </summary>
	[HideInInspector]
    public HexagoneInfo[,] tabHexagones;

	/// <summary>
	/// Les dimensions de l'objet
	/// </summary>
	[HideInInspector]
    public Vector2 dimensionsPiece;

	/// <summary>
	/// Les dimensions de l'hexagone
	/// </summary>
	[HideInInspector]
	public Vector3 dimensionHexagone;

    /// <summary>
    /// 
    /// </summary>
	[HideInInspector]
	public int xSector;

	/// <summary>
	/// 
	/// </summary>
	[HideInInspector]
	public int ySector;

	/// <summary>
	/// Référece vers l'objet permettant de gérer le terrain
	/// </summary>
    public TerrainManagerScript worldManager;
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes privées
	/// <summary>
	/// Combine les différents hexagones du mesh de la pièce dans un seul mesh
	/// </summary>
	private void CombinerHexagones(){
		CombineInstance[,] combine = new CombineInstance[(int)dimensionsPiece.x,(int)dimensionsPiece.y];
		for( int x = 0; x < dimensionsPiece.x; x++ ){
			for ( int z = 0; z < dimensionsPiece.y; z++ ){
				combine[x, z].mesh = tabHexagones[x, z].meshLocal;
				Matrix4x4 matrix = new Matrix4x4();
				matrix.SetTRS(tabHexagones[x, z].positionLocale, Quaternion.identity, Vector3.one);
				combine[x, z].transform = matrix;
			}
		}
		filter = gameObject.GetComponent<MeshFilter>();
		filter.mesh = new Mesh();	
		CombineInstance[] final = null;
		CivGridUtility.ToSingleArray(combine, out final);	
		filter.mesh.CombineMeshes(final);
		filter.mesh.RecalculateNormals();
		CreerCollider();	
	}
	
	/// <summary>
	/// Génère la pièce, i.e. l'ensemble des hexagones
	/// </summary>
	/// <param name="texture">La texture à appliquer sur l'hexagone</param>
	private void GenererHexagones( Texture texture ){
		tabHexagones = new HexagoneInfo[(int)dimensionsPiece.x, (int)dimensionsPiece.y];
		bool notOdd;
		for ( int y = 0; y < dimensionsPiece.y; y++ ){
			notOdd = ((y % 2) == 0);
			if (notOdd){
				for ( int x = 0; x < dimensionsPiece.x; x++ ){
					CreerHexagone(x, y, texture);
				}
			} else {
				for ( int x = 0; x < dimensionsPiece.x; x++ ){
					CreerHexagoneDecale(x, y, texture);
				}
			}
		}
	}
	
	/// <summary>
	/// Créé un hexagone
	/// </summary>
	/// <param name="x">La coordonnée en X</param>
	/// <param name="y">La coordonnée en Y</param>
	/// <param name="texture">La texture qu'aura l'hexagone</param>
	private void CreerHexagone( int x, int y, Texture texture ){

		Vector2 posGlobale;
		tabHexagones[x, y] = new HexagoneInfo();

		// Définit la position globale for le positionnement des textures
		posGlobale.x = x + (dimensionsPiece.x * xSector);
		posGlobale.y = y + (dimensionsPiece.y * ySector);
		
		tabHexagones[x, y].CubeGridPosition = new Vector3(posGlobale.x - Mathf.Round((posGlobale.y / 2) + .1f), posGlobale.y, -(posGlobale.x - Mathf.Round((posGlobale.y / 2) + .1f) + posGlobale.y));
		tabHexagones[x, y].positionLocale = new Vector3((x * (worldManager.hexExt.x * 2)), 0, (y * worldManager.hexExt.z) * 1.5f);
		tabHexagones[x, y].positionGlobale = new Vector3(tabHexagones[x, y].positionLocale.x + (xSector * (dimensionsPiece.x * dimensionHexagone.x)),
		                                                 tabHexagones[x, y].positionLocale.y, 
		                                                 tabHexagones[x, y].positionLocale.z + ((ySector * (dimensionsPiece.y * dimensionHexagone.z)) * (.75f)));
		
		// Calcul de la position locale de l'hexagone par rapport au terrain
		// FIXME A améliorer
		Vector3 posLocaleSurTerrain = gameObject.transform.localPosition;
		posLocaleSurTerrain.x += (float)(x*1.75+1) * 3 + (3 / 2) + -4;
		posLocaleSurTerrain.z += (float)(y*1.5+1)*3 + -3;

		tabHexagones[x, y].positionLocaleSurTerrain = posLocaleSurTerrain;
		tabHexagones[x, y].hexExt = worldManager.hexExt;
		tabHexagones[x, y].centreHexagone = worldManager.centreHexagone;

		if ( GameObjectUtils.parseToType(texture.name) == TypesObjetsRencontres.SABLE ){
			tabHexagones[x,y].TextureAppliquee = TypesTerrains.SABLE;
		} else if ( GameObjectUtils.parseToType (texture.name) == TypesObjetsRencontres.EAU ){ 
			tabHexagones[x,y].TextureAppliquee = TypesTerrains.EAU;
		}

		TerrainUtils.AjouterHexagone(tabHexagones[x, y]);
		
	}
	
	/// <summary>
	/// Créé un hexagone décalé
	/// </summary>
	/// <param name="x">La position en X</param>
	/// <param name="y">La position en Y</param>
	/// <param name="texture">La texture qu'aura l'hexagone</param>
	private void CreerHexagoneDecale( int x, int y, Texture texture ){
		
		Vector2 positionGlobale;
		tabHexagones[x, y] = new HexagoneInfo();

		// Définit la position globale for le positionnement des textures
		positionGlobale.x = x + (dimensionsPiece.x * xSector);
		positionGlobale.y = y + (dimensionsPiece.y * ySector);
		
		tabHexagones[x, y].CubeGridPosition = new Vector3(positionGlobale.x - Mathf.Round((positionGlobale.y / 2) + .1f), positionGlobale.y, -(positionGlobale.x - Mathf.Round((positionGlobale.y / 2) + .1f) + positionGlobale.y));
		tabHexagones[x, y].positionLocale = new Vector3((x * (worldManager.hexExt.x * 2)) + worldManager.hexExt.x, 0, (y * worldManager.hexExt.z) * 1.5f);
		tabHexagones[x, y].positionGlobale = new Vector3(tabHexagones[x, y].positionLocale.x + (xSector * (dimensionsPiece.x * dimensionHexagone.x)), 
		                                                 tabHexagones[x, y].positionLocale.y, 
		                                                 tabHexagones[x, y].positionLocale.z + ((ySector * (dimensionsPiece.y * dimensionHexagone.z)) * (.75f)));
		
		// Calcul de la position locale de l'hexagone par rapport au terrain
		// FIXME A améliorer
		Vector3 posLocaleSurTerrain = gameObject.transform.localPosition;
		posLocaleSurTerrain.x += (float)(x*1.75+2) * 3 + (3 / 2) + -4;
		posLocaleSurTerrain.z += (float)(y*1.5+1)*3 + -3;

		tabHexagones[x, y].positionLocaleSurTerrain = posLocaleSurTerrain;
		tabHexagones[x, y].hexExt = worldManager.hexExt;
		tabHexagones[x, y].centreHexagone = worldManager.centreHexagone;

		if ( GameObjectUtils.parseToType (texture.name) == TypesObjetsRencontres.SABLE ){
			tabHexagones[x, y].TextureAppliquee = TypesTerrains.SABLE;
		} else if ( GameObjectUtils.parseToType (texture.name) == TypesObjetsRencontres.EAU ){
			tabHexagones[x, y].TextureAppliquee = TypesTerrains.EAU;
		} 
		
		TerrainUtils.AjouterHexagone(tabHexagones[x, y]);
		
	}
#endregion

#region Méthodes package
	/// <summary>
	/// Créé le collider
	/// </summary>
	void CreerCollider(){
		if( collider == null ){
			collider = gameObject.AddComponent<BoxCollider>();
		}
		// Lier le centre et la taille du colldier et du mesh
		collider.center = filter.mesh.bounds.center;
		collider.size = filter.mesh.bounds.size;
	}
	
	/// <summary>
	/// 
	/// </summary>
	void OnDestroy(){
		Destroy(renderer.material);
	}
#endregion

#region Méthodes publiques
	/// <summary>
	/// Définit la dimension d'une pièce
	/// </summary>
	/// <param name="x">Quantité d'hexagones sur l'axe x</param>
	/// <param name="y">Quantité d'hexagones sur l'axe y</param>
	public void SetDimension( int x, int y ){
		dimensionsPiece = new Vector2(x, y);
	}

    /// <summary>
    /// Démarre la création des vues en créant et assemblant des pièces (ensembles d'hexagones)
    /// </summary>
    public void Demarrer(){
		Texture texture = gameObject.GetComponent<MeshRenderer>().material.mainTexture;
        GenererHexagones( texture );
		for( int x = 0; x < dimensionsPiece.x; x++ ){
            for ( int z = 0; z < dimensionsPiece.y; z++ ){
                if ( tabHexagones[x, z] != null ){
                    tabHexagones[x, z].parentChunk = this;
                    tabHexagones[x, z].Initialiser();
                } else {
                   // Debug.LogError("Null hexagon found in memory: " + x + " " + z);
                }
            }
        }
        // Combiner tous les hexagones du mesh de la piece dans un seul mesh
        CombinerHexagones();
    }
#endregion
	
}

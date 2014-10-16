/// <summary>
/// CivGridUtils.cs
/// Fichier utilitaire pour gérer les terrains façon Civilization.
/// http://forum.unity3d.com/threads/procedural-hexagon-terrain-tutorial.233296/
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Classe utilitaire pour gérer les terrains façon Civilization
/// </summary>
public static class CivGridUtility {

    /// <summary>
    /// Convertit un tbaleau 2D en un tableau 1D
    /// </summary>
    /// <param name="tab2D">Le tableau 2D à convertir</param>
    /// <param name="tab1D">Le tableau 1D résultant</param>
    public static void ToSingleArray(CombineInstance[,] tab2D, out CombineInstance[] tab1D){
        List<CombineInstance> combineList = new List<CombineInstance>();
        foreach( CombineInstance ci in tab2D ){
            combineList.Add(ci);
        }
        tab1D = combineList.ToArray();
    }

}

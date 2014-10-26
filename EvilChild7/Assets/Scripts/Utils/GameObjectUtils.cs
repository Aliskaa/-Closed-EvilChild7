/// <summary>
/// GameObjectUtils.cs
/// Fichier utilitaire pour gérer des games objects
/// (parti exemple récupérer un type en fonction d'un nom de game object)
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.1.0
/// </remarks>

using UnityEngine;
using System;
using System.Text.RegularExpressions;

/// <summary>
/// Classe utilitaire pour gérer des games objects
/// </summary>
public class GameObjectUtils {

	/* ********* *
	 * Attributs *
	 * ********* */

#region Attributs privés
	/// <summary>
	/// Regex pour repérer si c'est un bloc de terrain
	/// </summary>
	//private static Regex regexBlocTerrain = new Regex(@"BlocTerrain");

	/// <summary>
	/// Regex pour repérer si c'est le coté 1 du bac à sable (en -Z)
	/// </summary>
	private static Regex regexCoteBac1 = new Regex(@"Coté 1");

	/// <summary>
	/// Regex pour repérer si c'est le coté 2 du bac à sable (en +Z)
	/// </summary>
	private static Regex regexCoteBac2 = new Regex(@"Coté 2");

	/// <summary>
	/// Regex pour repérer si c'est le coté 3 du bac à sable (en -X)
	/// </summary>
	private static Regex regexCoteBac3 = new Regex(@"Coté 3");

	/// <summary>
	/// Regex pour repérer si c'est le coté 4 du bac à sable (en +X)
	/// </summary>
	private static Regex regexCoteBac4 = new Regex(@"Coté 4");

	/// <summary>
	/// Regex pour voir si c'est soit
	/// </summary>
	private static Regex regexSoit = new Regex(@"Fourmis Noire");
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes
	/// <summary>
	/// Récupère le nom d'un game object
	/// et renvoit l'info sous forme de TypeCollision
	/// </summary>
	/// 
	/// <returns>Le game object en tant que TypeCollision</returns>
	/// <param name="nomGo">
	/// Le nom du Game Object que l'on veut connaitre
	/// </param>
	public static TypesObjetsRencontres parseToTypeCollision( string nomGo ){
		if (regexCoteBac1.IsMatch (nomGo)) return TypesObjetsRencontres.COTE_BAC_1;
		if (regexCoteBac2.IsMatch (nomGo)) return TypesObjetsRencontres.COTE_BAC_2;
		if (regexCoteBac3.IsMatch (nomGo)) return TypesObjetsRencontres.COTE_BAC_3;
		if (regexCoteBac4.IsMatch (nomGo)) return TypesObjetsRencontres.COTE_BAC_4;
		if (regexSoit.IsMatch (nomGo)) return TypesObjetsRencontres.SOIT_MEME;
		return TypesObjetsRencontres.INCONNU;
	}
#endregion

}

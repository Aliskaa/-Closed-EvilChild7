/// <summary>
/// GameObjectUtils.cs
/// Fichier utilitaire pour gérer des games objects
/// (parti exemple récupérer un type en fonction d'un nom de game object)
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.1
/// </remarks>

using UnityEngine;
using System;
using System.Text.RegularExpressions;

/// <summary>
/// Fichier utilitaire pour gérer des games objects
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
	/// Regex pour repérer si c'est un coté du bac à sable
	/// </summary>
	private static Regex regexCoteBac = new Regex(@"Coté");
	
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
	public static TypeCollision parseToTypeCollision( string nomGo ){
		TypeCollision objet = TypeCollision.INCONNU;
		if ( regexCoteBac.IsMatch(nomGo) )
			objet = TypeCollision.COTE_BAC;
		else if ( regexSoit.IsMatch(nomGo) )
			objet = TypeCollision.SOIT_MEME;
		return objet;
	}
#endregion

}

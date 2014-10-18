/// <summary>
/// JSONUtils.cs
/// Fichier utilitaire pour gérer les messages au format JSON
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;
using System;
using System.Text.RegularExpressions;

/// <summary>
/// Classe utilitaire pour traiter des messages JSON
/// </summary>
public class JSONUtils {

	/* ********* *
	 * Attributs *
	 * ********* */

	#region Attributs
	private static Regex regexBlocTerrain = new Regex (@"BlocTerrain");
	#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

	#region Méthodes
	/// <summary>
	/// Créé une version en string d'un game object de type BlocTerrain.
	/// String de la forme :
	/// 
	/// 	{x:ABS,y:ORD}
	/// 
	/// Où ABS et ORD sont les coordonnées en x et en y.
	/// Si la valeur est à -1, alors lobjet est null
	/// 
	/// </summary>
	/// <returns>Une description JSON du bloc terrain</returns>
	/// <param name="blocTerrain">
	/// Le GO à convertir. Ne doit pas etre null.
	/// Doit avoir un nom de la forme BlocTerrain[XXX,YYY]
	/// </param>
	public static string parseBlocTerrain( GameObject blocTerrain ){
		if (blocTerrain == null) return "";
		string nomBt = blocTerrain.name;
		if (!regexBlocTerrain.IsMatch(nomBt)) return "";
		string[] splits = nomBt.Split(new Char[]{','});
		string abs = splits[0].Split(new Char[]{'['})[1];
		if (abs == null || abs.Length <= 0) abs = "-1";
		string ord = splits[1].Split(new Char[]{']'})[0];
		if (ord == null || ord.Length <= 0) ord = "-1";
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		sb.Append("{x:").Append(abs).Append(",y:").Append(ord).Append("}");
		return sb.ToString();
	}

	/// <summary>
	/// Parse la position (x,y,z) 'un object en JSON
	/// String de la forme :
	/// 
	/// 	{x:ABS,y:ORD,z:QUOTE}
	/// 
	/// Où ABS, ORD et QUOTE els valeurs x, y et z dans le répère
	/// 
	/// </summary>
	/// <returns>La position en string</returns>
	/// <param name="position">Le vecteur de position, ne doit pas etre null</param>
	public static string parsePosition( Vector3 position ){
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		sb.Append("{x:").Append(position[0])
		  .Append(",y:").Append(position[1])
		  .Append(",z:").Append(position[2])
		  .Append("}");
		return sb.ToString();
	}

	/// <summary>
	/// Parse le vecteur de rotation (x,y,z) d'un object en JSON
	/// String de la forme :
	/// 
	/// 	{x:ABS,y:ORD,z:QUOTE}
	/// 
	/// Où ABS, ORD et QUOTE les valeurs de rotations x, y et z dans le répère
	/// 
	/// </summary>
	/// <returns>La rotation en string</returns>
	/// <param name="position">Le vecteur de rotation, ne doit pas etre null</param>
	public static string parseRotation( Vector3 rotation ){
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		sb.Append("{x:").Append(rotation[0])
		  .Append(",y:").Append(rotation[1])
		  .Append(",z:").Append(rotation[2])
		  .Append("}");
		return sb.ToString();
	}

	/// <summary>
	/// Parse les infos 3D, i.e. la position (x,y,z) et le vecteur de rotation (x,y,z) en JSON.
	/// String de la forme :
	/// 
	/// 	{position:{x:XXX,y:YYY,z:ZZZ},rotation:{x:UUU,y:VVV,z:WWW}}
	/// 
	/// </summary>
	/// <returns>Un messzge en JSON sur les infos 3D</returns>
	/// <param name="position">Le vecteur de position, ne doit pas etre null</param>
	/// <param name="rotation">Le vecteur de rotation, ne doit pas etre null</param>
	public static string parseInfos3D( Vector3 position, Quaternion rotation ){
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		sb.Append ("{position:");
		sb.Append("{x:").Append(position[0])
		  .Append(",y:").Append(position[1])
		  .Append(",z:").Append(position[2])
		  .Append("}");
		sb.Append(",rotation:");
		sb.Append("{x:").Append(rotation.x)
		  .Append(",y:").Append(rotation.y)
		  .Append(",z:").Append(rotation.z)
		  .Append("}}");
		return sb.ToString();
	}
	#endregion

}

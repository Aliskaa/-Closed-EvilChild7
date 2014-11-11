/// <summary>
/// Cible.cs
/// Fichier réprésentant un objet détecté par la vue ou l'odorat de la fourmis
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;
using System;
using System.Text.RegularExpressions;

/// <summary>
/// Classe réprésentant un objet détecté par la vue ou l'odorat de la fourmis
/// </summary>
public class Cible {

	/* ********* *
	 * Attributs *
	 * ********* */
	// Les attributs sont la plupart du temps publics
	// car les accès sont plus rapides, simplifiées et c'est la philosophie d'Unity

#region Attributs publics
	/// <summary>
	/// La distance à laquelle est l'objet detectés
	/// </summary>
	public float distance;

	/// <summary>
	/// La classe de l'objet récupéré, une référence vers l'objet avec les informations utiles
	/// comme la quantité de nourriture restante, les points de vie, etc.
	/// </summary>
	//public ScritpObjet objet;
	// TODO Voir avec la conception de l'IA pour l'objet à passer

	/// <summary>
	/// La direction à laquelle l'objet a été vue/senti
	/// </summary>
	public TypesAxes direction;

	/// <summary>
	/// Le type de l'objet qui a été repéré
	/// </summary>
	public TypesObjetsRencontres typeObjet;
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes
	/// <summary>
	/// Constructeur
	/// </summary>
	/// <param name="type">Le type d'objet qui a été repéré</param>
	/// <param name="distance">La distance à laquelle est l'objet</param>
	/// <param name="direction">La direction à laquelle est l'objet</param>
	public Cible( TypesObjetsRencontres type, float distance, /*ScriptObjet objet,*/ TypesAxes direction ){
		typeObjet = type;
		this.distance = distance;
		//this.objet = objet;
		this.direction = direction;
	}

	/// <summary>
	/// Returns a <see cref="System.String"/> that represents the current <see cref="Cible"/>.
	/// </summary>
	/// <returns>A <see cref="System.String"/> that represents the current <see cref="Cible"/>.</returns>
	override public string ToString(){
		System.Text.StringBuilder sb = new System.Text.StringBuilder ();
		sb.Append("Cible :\n");
		sb.Append("\t\t type = ").Append(typeObjet).Append("\n");
		sb.Append("\t\t direction = ").Append(direction).Append("\n");
		sb.Append("\t\t distance = ").Append(distance);
		return sb.ToString();
	}
	#endregion

}

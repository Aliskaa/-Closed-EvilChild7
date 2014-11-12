/// <summary>
/// GameObjectUtils.cs
/// Fichier utilitaire pour gérer des games objects
/// (parti exemple récupérer un type en fonction d'un nom de game object)
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.5.0
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
	/// Regex pour voir si c'est soi
	/// </summary>
	private static Regex regexSoi = new Regex(@"Fourmis Noire");

	/// <summary>
	/// Regex pour voir si on a du sable
	/// </summary>
	private static Regex regexSable = new Regex(@"sable");

	/// <summary>
	/// Regex pour voir si on a de l'eau (sol)
	/// </summary>
	private static Regex regexEauSol = new Regex(@"eau");

	/// <summary>
	/// Regex pour voir si on a de l'eau (game object)
	/// </summary>
	private static Regex regexEau3d = new Regex(@"Eau3D");

	/// <summary>
	/// Regex pour voir c'est du bois
	/// </summary>
	private static Regex regexBois = new Regex(@"bois");

	/// <summary>
	/// Regex pour voir c'est un caillou
	/// </summary>
	private static Regex regexCaillou = new Regex(@"caillou");

	/// <summary>
	/// Regex pour voir c'est un petit caillou
	/// </summary>
	private static Regex regexPetitCaillou = new Regex(@"petit_caillou");

	/// <summary>
	/// Regex pour voir c'est un tres gros caillou
	/// </summary>
	private static Regex regexTresGrosCaillou = new Regex(@"tres_gros_caillou");

	/// <summary>
	/// Regex pour voir c'est une fourmi noire combattante
	/// </summary>
	private static Regex regexFNCmb = new Regex(@"fourmi_noire_combattante");

	/// <summary>
	/// Regex pour voir c'est une fourmi noire contremaitre
	/// </summary>
	private static Regex regexFNCm = new Regex(@"fourmi_noire_contremaitre");

	/// <summary>
	/// Regex pour voir c'est une fourmi noire generale
	/// </summary>
	private static Regex regexFNGnl = new Regex(@"fourmi_noire_generale");	

	/// <summary>
	/// Regex pour voir c'est une fourmi noire ouvriere
	/// </summary>
	private static Regex regexFNO = new Regex(@"fourmi_noire_ouvriere");

	/// <summary>
	/// Regex pour voir c'est une fourmi noire reine
	/// </summary>
	private static Regex regexFNR = new Regex(@"fourmi_noire_reine");

	/// <summary>
	/// Regex pour voir c'est une fourmi blanche combattante
	/// </summary>
	private static Regex regexFBCmb = new Regex(@"fourmi_blanche_combattante");
	
	/// <summary>
	/// Regex pour voir c'est une fourmi blanche contremaitre
	/// </summary>
	private static Regex regexFBCm = new Regex(@"fourmi_blanche_contremaitre");
	
	/// <summary>
	/// Regex pour voir c'est une fourmi blanche generale
	/// </summary>
	private static Regex regexFBGnl = new Regex(@"fourmi_blanche_generale");	
	
	/// <summary>
	/// Regex pour voir c'est une fourmi blanche ouvriere
	/// </summary>
	private static Regex regexFBO = new Regex(@"fourmi_blanche_ouvriere");
	
	/// <summary>
	/// Regex pour voir c'est une fourmi blanche reine
	/// </summary>
	private static Regex regexFBR  = new Regex(@"fourmi_blanche_reine");

	/// <summary>
	/// Regex pour voir c'est un oeuf de fourmi
	/// </summary>
	private static Regex regexOeuf = new Regex(@"oeuf_fourmi");

	/// <summary>
	/// Regex pour voir c'est un bonbon anglais bleu
	/// </summary>
	private static Regex regexBAB = new Regex(@"bonbon_anglais_bleu");

	/// <summary>
	/// Regex pour voir c'est un bonbon anglais rose
	/// </summary>
	private static Regex regexBAR = new Regex(@"bonbon_anglais_rose");

	/// <summary>
	/// Regex pour voir c'est un bonbon mure
	/// </summary>
	private static Regex regexBM = new Regex(@"bonbon_mure");

	/// <summary>
	/// Regex pour voir c'est un bonbon orange
	/// </summary>
	private static Regex regexBO = new Regex(@"bonbon_orange");

	/// <summary>
	/// Regex pour voir c'est un bonbon rose
	/// </summary>
	private static Regex regexBR = new Regex(@"bonbon_rose");

	/// <summary>
	/// Regex pour voir c'est un bonbon mure
	/// </summary>
	private static Regex regexBV = new Regex(@"bonbon_vert");

	/// <summary>
	/// Regex pour voir c'est une pheromone d'ouvrière blanche
	/// </summary>
	private static Regex regexPOB = new Regex(@"pheromones_ouvriere_blanche");

	/// <summary>
	/// Regex pour voir c'est une pheromone d'ouvrière noire
	/// </summary>
	private static Regex regexPON = new Regex(@"pheromones_ouvriere_noire");

	/// <summary>
	/// Regex pour voir c'est une pheromone de contremaitre blanche
	/// </summary>
	private static Regex regexPCB = new Regex(@"pheromones_contremaitre_blanche");

	/// <summary>
	/// Regex pour voir c'est une pheromone de contremaitre noire
	/// </summary>
	private static Regex regexPCN = new Regex(@"pheromones_contremaitre_noire");

	/// <summary>
	/// Regex pour voir c'est un scarabée
	/// </summary>
	private static Regex regexScarabee = new Regex(@"scarabee");

	/// <summary>
	/// Regex pour voir c'est une aprtie du terrain
	/// </summary>
	private static Regex regexTerrain = new Regex(@"BlocTerrain");	
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes
	/// <summary>
	/// Récupère le nom d'un game object
	/// et renvoit l'info sous forme de TypesObjetsRencontres
	/// </summary>
	/// 
	/// <returns>Le game object en tant que TypesObjetsRencontres</returns>
	/// <param name="nomGo">
	/// Le nom du Game Object que l'on veut connaitre
	/// </param>
	public static TypesObjetsRencontres parseToType( string nomGo ){
		if (regexTerrain.IsMatch (nomGo))		return TypesObjetsRencontres.BLOC_TERRAIN;
		if (regexCoteBac1.IsMatch(nomGo)) 		return TypesObjetsRencontres.COTE_BAC_1;
		if (regexCoteBac2.IsMatch(nomGo))		return TypesObjetsRencontres.COTE_BAC_2;
		if (regexCoteBac3.IsMatch(nomGo)) 		return TypesObjetsRencontres.COTE_BAC_3;
		if (regexCoteBac4.IsMatch(nomGo))		return TypesObjetsRencontres.COTE_BAC_4;
		if (regexSoi.IsMatch(nomGo)) 			return TypesObjetsRencontres.SOIT_MEME;
		if (regexSable.IsMatch(nomGo)) 			return TypesObjetsRencontres.SABLE;
		if (regexEauSol.IsMatch(nomGo)) 		return TypesObjetsRencontres.EAU;
		if (regexEau3d.IsMatch(nomGo)) 			return TypesObjetsRencontres.EAU;
		if (regexBois.IsMatch(nomGo)) 			return TypesObjetsRencontres.BOUT_DE_BOIS;
		if (regexPetitCaillou.IsMatch(nomGo)) 	return TypesObjetsRencontres.PETIT_CAILLOU;
		if (regexCaillou.IsMatch(nomGo)) 		return TypesObjetsRencontres.CAILLOU;
		if (regexTresGrosCaillou.IsMatch(nomGo)) return TypesObjetsRencontres.TRES_GROS_CAILLOUX;
		if (regexFNO.IsMatch(nomGo)) 			return TypesObjetsRencontres.OUVRIERE_NOIRE;
		if (regexFNR.IsMatch(nomGo)) 			return TypesObjetsRencontres.REINE_NOIRE;
		if (regexFNGnl.IsMatch(nomGo)) 			return TypesObjetsRencontres.GENERALE_NOIRE;
		if (regexFNCm.IsMatch(nomGo)) 			return TypesObjetsRencontres.CONTREMAITRE_NOIRE;
		if (regexFNCmb.IsMatch(nomGo)) 			return TypesObjetsRencontres.COMBATTANTE_NOIRE;
		if (regexOeuf.IsMatch(nomGo)) 			return TypesObjetsRencontres.OEUF_NOIR;
		// TODO oeuf blanc
		if (regexPOB.IsMatch(nomGo)) 			return TypesObjetsRencontres.PHEROMONES_OUV_BLANCHE;
		if (regexPON.IsMatch(nomGo)) 			return TypesObjetsRencontres.PHEROMONES_OUV_NOIRE;
		if (regexPCB.IsMatch (nomGo)) 			return TypesObjetsRencontres.PHEROMONES_CM_BLANCHE;
		if (regexPCN.IsMatch (nomGo)) 			return TypesObjetsRencontres.PHEROMONES_CM_NOIRE;
		if (regexFBCm.IsMatch(nomGo)) 			return TypesObjetsRencontres.CONTREMAITRE_BLANCHE;
		if (regexFBCmb.IsMatch(nomGo)) 			return TypesObjetsRencontres.COMBATTANTE_BLANCHE;
		if (regexFBGnl.IsMatch(nomGo)) 			return TypesObjetsRencontres.GENERALE_BLANCHE;
		if (regexFBO.IsMatch(nomGo)) 			return TypesObjetsRencontres.OUVRIERE_BLANCHE;
		if (regexFBR.IsMatch(nomGo)) 			return TypesObjetsRencontres.REINE_BLANCHE;
		if (regexScarabee.IsMatch(nomGo)) 		return TypesObjetsRencontres.SCARABEE;
		if (regexBM.IsMatch(nomGo)) 			return TypesObjetsRencontres.BONBON_MURE;
		if (regexBO.IsMatch(nomGo))				return TypesObjetsRencontres.BONBON_ORANGE;
		if (regexBR.IsMatch(nomGo)) 			return TypesObjetsRencontres.BONBON_ROSE;
		if (regexBV.IsMatch(nomGo)) 			return TypesObjetsRencontres.BONBON_VERT;
		if (regexBAB.IsMatch (nomGo))			return TypesObjetsRencontres.BONBON_ANGLAIS_BLEU;
		if (regexBAR.IsMatch (nomGo))			return TypesObjetsRencontres.BONBON_ANGLAIS_ROSE;
		return TypesObjetsRencontres.INCONNU;
	}
#endregion

}

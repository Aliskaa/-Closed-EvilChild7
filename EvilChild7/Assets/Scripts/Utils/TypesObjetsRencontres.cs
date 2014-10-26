/// <summary>
/// TypesAxeVisee.cs
/// Fichier pour gérer les différents types d'axe de visée pour entre autres les lancés de rayons
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;

/// <summary>
/// Les différents objets avec lesquels les fourmis (ou autres objets) peuvent
/// entrer en collision ou peuvent détecter.
/// 
/// [0;9]   : par rapport au terrain
/// [10;19] : par rapport aux obstacles inertes
/// [20;29] : par rapport aux unités alliées
/// [30;39] : par rapport aux unités ennemies
/// [40;49] : par rapport à d'autres menaces
/// [50;59] : par rapport à la nourriture
/// </summary>
public enum TypesObjetsRencontres : int {
	/// <summary>
	/// Objet en collision inconnu
	/// </summary>
	INCONNU = -3,
	/// <summary>
	/// Aucun objet en collision
	/// </summary>
	AUCUN = -2,
	/// <summary>
	/// Ca peut arriver...
	/// </summary>
	SOIT_MEME = -1,
	/// <summary>
	/// Collision avec un coté du bac, celui en -Z
	/// </summary>
	COTE_BAC_1 = 0,
	/// <summary>
	/// Collision avec un coté du bac, celui en +Z
	/// </summary>
	COTE_BAC_2 = 1,
	/// <summary>
	/// Collision avec un coté du bac, celui en -X
	/// </summary>
	COTE_BAC_3 = 2,
	/// <summary>
	/// Collision avec un coté du bac, celui en +X
	/// </summary>
	COTE_BAC_4 = 3,
	/// <summary>
	/// Collision avec un petit caillou
	/// </summary>
	PETIT_CAILLOU = 10,
	/// <summary>
	/// Collision avec un gros caillou
	/// </summary>
	GROS_CAILLOUX =11,
	/// <summary>
	/// Collision avec un bout de bois
	/// </summary>
	BOUT_DE_BOIS = 12,
	/// <summary>
	/// Une ouvrière amie
	/// </summary>
	OUVRIERE_AMIE = 20,
	/// <summary>
	/// Une contre-maitrsse alliée
	/// </summary>
	CONTREMAITRE_AMIE = 21,
	/// <summary>
	/// Une combattante alliée
	/// </summary>
	COMBATTANTE_AMIE = 22,
	/// <summary>
	/// Une générale alliée
	/// </summary>
	GENERALE_AMIE = 23,
	/// <summary>
	/// La reine de la fourmillière
	/// </summary>
	REINE_AMIE = 24,
	/// <summary>
	/// Des phéromones produites par une contre-maitre amie
	/// </summary>
	PHEROMONES_CM_AMIE = 25,
	/// <summary>
	/// Un oeuf pondu par la reine du camp
	/// </summary>
	OEUF_AMI = 26,
	/// <summary>
	/// Une ouvrière ennemie
	/// </summary>
	OUVRIERE_ENNEMIE = 30,
	/// <summary>
	/// Une contre-maitrsse ennemie
	/// </summary>
	CONTREMAITRE_ENNEMIE = 31,
	/// <summary>
	/// Une combattante ennemie
	/// </summary>
	COMBATTANTE_ENNEMIE = 32,
	/// <summary>
	/// Une générale ennemie
	/// </summary>
	GENERALE_ENNEMIE = 33,
	/// <summary>
	/// La reine du camp adverse
	/// </summary>
	REINE_ENNEMIE = 34,
	/// <summary>
	/// Un oeuf pondu par la reine du camp adverse
	/// </summary>
	OEUF_ENNEMIE = 35,
	/// <summary>
	/// Un scarabée	
	/// </summary>
	SCARABEE = 40	
}


/// <summary>
/// TypesObjetsRencontres.cs
/// Fichier pour gérer les différents objets avec lesquels on peut entrer en collision
/// ou que l'on peut détecter
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.2.0
/// </remarks>

using UnityEngine;

/// <summary>
/// Les différents objets avec lesquels les fourmis (ou autres objets) peuvent
/// entrer en collision ou peuvent détecter.
/// 
/// [0;9]   : par rapport au terrain
/// [10;19] : par rapport aux obstacles inertes
/// [20;29] : par rapport aux unités noires
/// [30;39] : par rapport aux unités rouges
/// [40;49] : par rapport à d'autres menaces
/// [50;69] : par rapport à la nourriture
/// </summary>
public enum TypesObjetsRencontres : int {
	/// <summary>
	/// Objet en collision / détecté inconnu
	/// </summary>
	INCONNU = -3,
	/// <summary>
	/// Aucun objet en collision ou detecté
	/// </summary>
	AUCUN = -2,
	/// <summary>
	/// Ca peut arriver...
	/// </summary>
	SOIT_MEME = -1,
	/// <summary>
	/// Collision / détection avec un coté du bac, celui en -Z
	/// </summary>
	COTE_BAC_1 = 0,
	/// <summary>
	/// Collision / détection avec un coté du bac, celui en +Z
	/// </summary>
	COTE_BAC_2 = 1,
	/// <summary>
	/// Collision / détection avec un coté du bac, celui en -X
	/// </summary>
	COTE_BAC_3 = 2,
	/// <summary>
	/// Collision / détection avec un coté du bac, celui en +X
	/// </summary>
	COTE_BAC_4 = 3,
	/// <summary>
	/// Collision / détection avec du sable est sous l'objet
	/// </summary>
	SABLE = 4,
	/// <summary>
	/// Collision / détection de l'eau est sous l'objet
	/// </summary>
	EAU = 5,
	/// <summary>
	/// Collision / détection avec le game object pour l'eau
	/// </summary>
	EAU3D = 6,
	/// <summary>
	/// Collision / détection avec un petit caillou
	/// </summary>
	PETIT_CAILLOU = 10,
	/// <summary>
	/// Collision / détection avec un caillou
	/// </summary>
	CAILLOU = 11,
	/// <summary>
	/// Collision / détection avec un gros caillou
	/// </summary>
	TRES_GROS_CAILLOUX = 12,
	/// <summary>
	/// Collision / détection avec un bout de bois
	/// </summary>
	BOUT_DE_BOIS = 13,
	/// <summary>
	/// Collision / détection avec une ouvrière noire
	/// </summary>
	OUVRIERE_NOIRE = 20,
	/// <summary>
	//// Collision / détection avec une contre-maitresse noire
	/// </summary>
	CONTREMAITRE_NOIRE = 21,
	/// <summary>
	/// Collision / détection avec une combattante noire
	/// </summary>
	COMBATTANTE_NOIRE = 22,
	/// <summary>
	/// Collision / détection avec une générale noire
	/// </summary>
	GENERALE_NOIRE = 23,
	/// <summary>
	/// Collision / détection avec la reine de la fourmillière noire
	/// </summary>
	REINE_NOIRE = 24,
	/// <summary>
	/// Collision / détection avec des phéromones produites par une contre-maitre noire
	/// </summary>
	PHEROMONES_CM_NOIRE = 25,
	/// <summary>
	/// Collision / détection avec des phéromones produites par une ouvrière noire
	/// </summary>
	PHEROMONES_OUV_NOIRE = 26,
	/// <summary>
	/// Collision / détection avec un oeuf pondu par la reine du camp noir
	/// </summary>
	OEUF_NOIR = 27,
	/// <summary>
	/// Collision / détection avec une ouvrière rouge
	/// </summary>
	OUVRIERE_ROUGE = 30,
	/// <summary>
	/// Collision / détection avec une contre-maitrsse rouge
	/// </summary>
	CONTREMAITRE_ROUGE = 31,
	/// <summary>
	/// Collision / détection avec une combattante rouge
	/// </summary>
	COMBATTANTE_ROUGE = 32,
	/// <summary>
	/// Collision / détection avec une générale rouge
	/// </summary>
	GENERALE_ROUGE = 33,
	/// <summary>
	/// Collision / détection avec la reine du camp adverse
	/// </summary>
	REINE_ROUGE = 34,
	/// <summary>
	/// Collision / détection avec des phéromones produites par une contre-maitre rouge
	/// </summary>
	PHEROMONES_CM_ROUGE = 35,
	/// <summary>
	/// Collision / détection avec des phéromones produites par une ouvrière rouge
	/// </summary>
	PHEROMONES_OUV_ROUGE = 36,
	/// <summary>
	/// Collision / détection avec un oeuf pondu par la reine du camp rouge
	/// </summary>
	OEUF_ROUGE = 37,
	/// <summary>
	/// Collision / détection avec un scarabée	
	/// </summary>
	SCARABEE = 40,
	/// <summary>
	/// Collision / détection avec un bonbon anglais bleu
	/// </summary>
	BONBON_ANGLAIS_BLEU = 50,
	/// <summary>
	/// Collision / détection avec un bonbon anglais rose
	/// </summary>
	BONBON_ANGLAIS_ROSE = 51,
	/// <summary>
	/// Collision / détection avec un bonbon mure
	/// </summary>
	BONBON_MURE = 52,
	/// <summary>
	/// Collision / détection avec un bonbon orange
	/// </summary>
	BONBON_ORANGE = 53,
	/// <summary>
	/// Collision / détection avec un bonbon rose
	/// </summary>
	BONBON_ROSE = 54,
	/// <summary>
	/// Collision / détection avec un bonbon vert
	/// </summary>
	BONBON_VERT = 55
}


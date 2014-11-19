/// <summary>
/// Invocations.cs
/// Les types d'objets que l'on peut créer
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.4.0
/// </remarks>

using UnityEngine;


/// <summary>
/// Les types d'objets que l'on peut créer
/// [0;09]	: usages internes
/// [10;19] : par rapport aux obstacles inertes
/// [20;29] : par rapport aux unités noires
/// [30;39] : par rapport aux unités blanches
/// [40;49] : par rapport à d'autres menaces
/// [50;69] : par rapport à la nourriture
/// [70;...] : libre
/// </summary>
public enum Invocations : int {
	RIEN = 0,
	/// <summary>
	/// Un bout de bois
	/// </summary>
	BOUT_DE_BOIS = 10,
	/// <summary>
	/// Un caillou
	/// </summary>
	CAILLOU = 11,
	/// <summary>
	/// Un petit caillou
	/// </summary>
	PETIT_CAILLOU = 12,
	/// <summary>
	/// Un très gros caillou
	/// </summary>
	TRES_GROS_CAILLOU = 13,
	/// <summary>
	/// Une fourmi noire tete nue
	/// </summary>
	FOURMI_NOIRE_OUVRIERE = 20,
	/// <summary>
	/// Une fourmi noire avec un chapeau bleu
	/// </summary>
	FOURMI_NOIRE_COMBATTANTE = 21,
	/// <summary>
	/// Une fourmi noire avec un casque jaune
	/// </summary>
	FOURMI_NOIRE_CONTREMAITRE = 22,
	/// <summary>
	/// Une fourmi noire avec un képi bleu
	/// </summary>
	FOURMI_NOIRE_GENERALE = 23,
	/// <summary>
	/// Une fourmi noire avec une couronne
	/// </summary>
	FOURMI_NOIRE_REINE = 24,
	/// <summary>
	/// Un petit bloc de particules vert foncé
	/// </summary>
	PHEROMONES_OUVRIERE_NOIRE = 25,
	/// <summary>
	/// Un petit bloc de particules jaune foncé
	/// </summary>
	PHEROMONES_CONTREMAITRE_NOIRE = 26,
	/// <summary>
	/// Une fourmi blanche
	/// </summary>
	FOURMI_BLANCHE_OUVRIERE = 30,
	/// <summary>
	/// Une fourmi blanche avec un chapeau bleu
	/// </summary>
	FOURMI_BLANCHE_COMBATTANTE = 31,
	/// <summary>
	/// Une fourmi blanche avec un casque jaune
	/// </summary>
	FOURMI_BLANCHE_CONTREMAITRE = 32,
	/// <summary>
	/// Une fourmi blanche avec un képi bleu
	/// </summary>
	FOURMI_BLANCHE_GENERALE = 33,
	/// <summary>
	/// Une fourmi blanche avec une couronne
	/// </summary>
	FOURMI_BLANCHE_REINE = 34,
	/// <summary>
	/// Un petit bloc de particules vert pale
	/// </summary>
	PHEROMONES_OUVRIERE_BLANCHE = 35,
	/// <summary>
	/// Un petit bloc de particules jaune pale
	/// </summary>
	PHEROMONES_CONTREMAITRE_BLANCHE = 36,
	/// <summary>
	/// Un oeuf tout vert
	/// </summary>
	OEUF_FOURMI = 39,
	/// <summary>
	/// Un scarabee tout vert affamé
	/// </summary>
	SCARABEE = 40,
	/// <summary>
	/// Un bonbon cylindrique bleu d'extérieur et noir dedans
	/// </summary>
	BONBON_ANGLAIS_BLEU = 50,
	/// <summary>
	/// Un bon cylindrique rose d'extérieur et noir dedans
	/// </summary>
	BONBON_ANGLAIS_ROSE = 51,
	/// <summary>
	/// Un bonbon rouge en forme de mure
	/// </summary>
	BONBON_MURE = 52,
	/// <summary>
	/// Un bonbon rayé blanc et rouge
	/// </summary>
	BONBON_ORANGE = 53,
	/// <summary>
	/// Un bonbon rayé blanc et rose
	/// </summary>
	BONBON_ROSE = 54,
	/// <summary>
	/// Un bonbon rayé blanc et vert
	/// </summary>
	BONBON_VERT = 55,
	/// <summary>
	/// Une lumière dans un anneau pour montrer la case visée
	/// </summary>
	SELECTION_CASE = 71,
	/// <summary>
	/// Une game object invisible à placer sur les cases d'eau
	/// </summary>
	EAU3D = 72,
	/// <summary>
	/// Une game object "case d'eau" plat
	/// </summary>
	EAU = 73,
	/// <summary>
	/// Des particules d'éclosions
	/// </summary>
	PARTICULES_ECLOSION = 74,
	/// <summary>
	/// Des particules de mort pour les bestioles non fourmi
	/// </summary>
	PARTICULES_MORT_BESTIOLE = 75,
	/// <summary>
	/// Des particules de mort pour les fourmis noires
	/// </summary>
	PARTICULES_MORT_FOURMI_NOIRE = 76,
	/// <summary>
	/// Des particules de mort pour les fourmis blanches
	/// </summary>
	PARTICULES_MORT_FOURMI_BLANCHE = 77,
	/// <summary>
	/// Des particules de mort pour les reines blanches
	/// </summary>
	PARTICULES_MORT_REINE_BLANCHE = 78,
	/// <summary>
	/// Des particules de mort pour les reines noire
	/// </summary>
	PARTICULES_MORT_REINE_NOIRE = 79,
	/// <summary>
	/// Des particules de mort pour des bestioles. Sanglant.
	/// </summary>
	PARTICULES_MORT_BESTIOLE_TRASH = 80,
	/// <summary>
	/// Des particules de mort pour des reines. Sanglant.
	/// </summary>
	PARTICULES_MORT_REINE_TRASH = 81,
	/// <summary>
	/// Un objet de debug
	/// </summary>
	DEBUG_OBJECT = 173,
	/// <summary>
	/// Une fourmis pour les debugs
	/// </summary>
	DEBUG_FOURMIS = 174
}


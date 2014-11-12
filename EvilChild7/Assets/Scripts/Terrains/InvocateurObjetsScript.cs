/// <summary>
/// InvocateurObjetsScript
/// Script pour invoquer/crééer/isntancier des objets sur le terrain
/// </summary>
/// 
/// Exemple d'utilisation :
/// <code>
/// 		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
/// 		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
///			scriptInvoc.InvoquerObjet(Invocations.TRES_GROS_CAILLOU, new Vector3(141.5f, 0.1f, 128.4f));
/// </code>
/// <remarks>
/// PY Lapersonne - Version 2.0.0
/// </remarks>

using UnityEngine;
using System.Collections;

#region InvocateurObjetsScript
/// <summary>
/// Classe pour crééer de nouveaux objets via les prefabs.
/// </summary>
public class InvocateurObjetsScript : MonoBehaviour {

	/* ********* *
	 * Attributs *
	 * ********* */

#region Constantes

	#region Packages
	private const string packageBois 			= "Assets/Prefabs/Bois/";
	private const string packageCailloux 		= "Assets/Prefabs/Cailloux/";
	private const string packageFourmis			= "Assets/Prefabs/Fourmis/";
	private const string packageFourmisNoires	= "Assets/Prefabs/Fourmis/noires/";
	private const string packageFourmisBlanches	= "Assets/Prefabs/Fourmis/blanches/";
	private const string packageNourritures 	= "Assets/Prefabs/Nourritures/";
	private const string packagePheromones 		= "Assets/Prefabs/Pheromones/";
	private const string packageScarabees 		= "Assets/Prefabs/Scarabees/";
	private const string packageEau				= "Assets/Prefabs/Eau/";
	private const string packageTerrain 		= "Assets/Prefabs/Terrains/";
	private const string packageDebug	 		= "Assets/Prefabs/Debug/";
	#endregion

	#region Cailloux, eau, bois
	private const string fichierBoutDeBois 		= "bois.prefab";
	private const string fichierPetitCaillou 	= "petit_caillou.prefab";
	private const string fichierCaillou 		= "caillou.prefab";
	private const string fichierTresGrosCaillou = "tres_gros_caillou.prefab";
	private const string fichierEau3D 			= "Eau3D.prefab";
	#endregion

	#region Bestioles
	private const string fichierFNCmb 			= "fourmi_noire_combattante.prefab";
	private const string fichierFNCm 			= "fourmi_noire_contremaitre.prefab";
	private const string fichierFNG 			= "fourmi_noire_generale.prefab";
	private const string fichierFNO 			= "fourmi_noire_ouvriere.prefab";
	private const string fichierFNR 			= "fourmi_noire_reine.prefab";
	private const string fichierFBCmb 			= "fourmi_blanche_combattante.prefab";
	private const string fichierFBCm			= "fourmi_blanche_contremaitre.prefab";
	private const string fichierFBG 			= "fourmi_blanche_generale.prefab";
	private const string fichierFBO 			= "fourmi_blanche_ouvriere.prefab";
	private const string fichierFBR 			= "fourmi_blanche_reine.prefab";
	private const string fichierOeuf 			= "oeuf_fourmi.prefab";
	private const string fichierPhero 			= "pheromone.prefab";
	private const string fichierScara 			= "scarabee.prefab";
	#endregion

	#region Bonbons
	private const string fichierBAB 			= "bonbon_anglais_bleu.prefab";
	private const string fichierBAR 			= "bonbon_anglais_rose.prefab";
	private const string fichierBM 				= "bonbon_mure.prefab";
	private const string fichierBO 				= "bonbon_orange.prefab";
	private const string fichierBR 				= "bonbon_rose.prefab";
	private const string fichierBV 				= "bonbon_vert.prefab";
	#endregion
	
	#region Debug
	private const string fichierDebugObject 	= "Debug_Object.prefab";
	private const string fichierDebugFourmis 	= "Debug_fourmi_blanche_generale.prefab";
	#endregion

	private const string fichierSelectionCase 	= "Selection case.prefab";

#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes public
	/// <summary>
	/// Invoque l'objet demandé
	/// </summary>
	/// <returns>Le game object rattaché au terrain</returns>
	/// <param name="objet">Le type d'objet à créer</param>
	public GameObject InvoquerObjet(Invocations objet){
		string cheminPackage = "";
		string nomFichier = "";
		switch ( objet ){
			case Invocations.BONBON_ANGLAIS_BLEU:
				cheminPackage = packageNourritures;
				nomFichier = fichierBAB;
				break;
			case Invocations.BONBON_ANGLAIS_ROSE:
				cheminPackage = packageNourritures;
				nomFichier = fichierBAR;
				break;
			case Invocations.BONBON_MURE:
				cheminPackage = packageNourritures;
				nomFichier = fichierBM;
				break;
			case Invocations.BONBON_ORANGE:
				cheminPackage = packageNourritures;
				nomFichier = fichierBO;
				break;
			case Invocations.BONBON_ROSE:
				cheminPackage = packageNourritures;
				nomFichier = fichierBR;
				break;
			case Invocations.BONBON_VERT:
				cheminPackage = packageNourritures;
				nomFichier = fichierBV;
				break;
			case Invocations.BOUT_DE_BOIS:
				cheminPackage = packageBois;
				nomFichier = fichierBoutDeBois;
				break;
			case Invocations.CAILLOU:
				cheminPackage = packageCailloux;
				nomFichier = fichierCaillou;
				break;
			case Invocations.PETIT_CAILLOU:
				cheminPackage = packageCailloux;
				nomFichier = fichierPetitCaillou;
				break;
			case Invocations.TRES_GROS_CAILLOU:
				cheminPackage = packageCailloux;
				nomFichier = fichierTresGrosCaillou;
				break;
			case Invocations.FOURMI_NOIRE_COMBATTANTE:
				cheminPackage = packageFourmisNoires;
				nomFichier = fichierFNCmb;
				break;
			case Invocations.FOURMI_NOIRE_CONTREMAITRE:
				cheminPackage = packageFourmisNoires;
				nomFichier = fichierFNCm;
				break;
			case Invocations.FOURMI_NOIRE_GENERALE:
				cheminPackage = packageFourmisNoires;
				nomFichier = fichierFNG;
				break;
			case Invocations.FOURMI_NOIRE_OUVRIERE:
				cheminPackage = packageFourmisNoires;
				nomFichier = fichierFNO;
				break;
			case Invocations.FOURMI_NOIRE_REINE:
				cheminPackage = packageFourmisNoires;
				nomFichier = fichierFNR;
				break;
			case Invocations.FOURMI_BLANCHE_COMBATTANTE:
				cheminPackage = packageFourmisBlanches;
				nomFichier = fichierFBCmb;
				break;
			case Invocations.FOURMI_BLANCHE_CONTREMAITRE:
				cheminPackage = packageFourmisBlanches;
				nomFichier = fichierFBCm;
				break;
			case Invocations.FOURMI_BLANCHE_GENERALE:
				cheminPackage = packageFourmisBlanches;
				nomFichier = fichierFBG;
				break;
			case Invocations.FOURMI_ROUGE_OUVRIERE:
				cheminPackage = packageFourmisBlanches;
				nomFichier = fichierFBO;
				break;
			case Invocations.FOURMI_BLANCHE_REINE:
				cheminPackage = packageFourmisBlanches;
				nomFichier = fichierFBR;
				break;
			case Invocations.OEUF_FOURMI:
				cheminPackage = packageFourmis;
				nomFichier = fichierOeuf;
				break;
			case Invocations.PHEROMONE:
				cheminPackage = packagePheromones;
				nomFichier = fichierPhero;
				break;
			case Invocations.SCARABEE:
				cheminPackage = packageScarabees;
				nomFichier = fichierScara;
				break;
			case Invocations.EAU3D:
				cheminPackage = packageEau;
				nomFichier = fichierEau3D;
				break;
			case Invocations.SELECTION_CASE:
				cheminPackage = packageTerrain;
				nomFichier = fichierSelectionCase;
				break;
			case Invocations.DEBUG_OBJECT:
				cheminPackage = packageDebug;
				nomFichier = fichierDebugObject;
				break;
			case Invocations.DEBUG_FOURMIS:
				cheminPackage = packageDebug;
				nomFichier = fichierDebugFourmis;
				break;
			default:
				Debug.LogError("Impossible de créer l'objet :"+objet);
				return null;
		}
		string cheminComplet = cheminPackage + nomFichier;
		GameObject invoc = Resources.LoadAssetAtPath<GameObject>(cheminComplet);
		if (invoc == null) {
			Debug.LogError("Impossible de créer l'objet avec :"+cheminComplet);
			return null;
		} else {
			invoc.name = "Invoc:"+nomFichier.Split(new char[]{'.'})[0];
		}
		GameObject terrain = GameObject.Find("Terrain");
		if ( terrain == null ){
			Debug.LogError("Impossible de trouver l'objet \"Terrain\"");
			return null;
		}
		invoc = (GameObject)Instantiate(invoc);
		invoc.transform.parent = terrain.transform;
		return invoc;
	}

	/// <summary>
	/// Invoque l'objet demandé et le place sur l'hexagone
	/// le plus proche de la position indiquée
	/// </summary>
	/// <returns>Le game object, rattaché au terrain, sur l'hexagone le plus proche
	/// de la position indiquée</returns>
	/// <param name="objet">Le type d'objet à créer</param>
	/// <param name="positionApprox">La position approximative, non définitive de l'objet.</param>
	public GameObject InvoquerObjet(Invocations objet, Vector3 positionApprox){
		GameObject invoc = InvoquerObjet(objet);
		HexagoneInfo hexPlusProche = TerrainUtils.HexagonePlusProche(positionApprox);
		if (hexPlusProche == null) {
			Debug.LogError ("Aucun hexagone n'a été trouvé. Le terrain est-il initialisé ?");
		} else {
			invoc.transform.localPosition = hexPlusProche.positionLocaleSurTerrain;
		}
		return invoc;
	}

	/// <summary>
	/// Invoque l'objet demandé et le place sur l'hexagone
	/// le plus proche de la position indiquée en ajoutant en plus un décalage dans les
	/// coordonnées (typiquement : invoquer un objet sur une case mais le emttre très haut en Y pour qu'il tombe bien)
	/// </summary>
	/// <returns>Le game object, rattaché au terrain, sur l'hexagone le plus proche
	/// de la position indiquée, avec l'offset appliqué</returns>
	/// <param name="objet">Le type d'objet à créer</param>
	/// <param name="positionApprox">La position approximative, non définitive de l'objet.</param>
	public GameObject InvoquerObjetAvecOffset(Invocations objet, Vector3 positionApprox, Vector3 offset){
		GameObject invoc = InvoquerObjet(objet);
		HexagoneInfo hexPlusProche = TerrainUtils.HexagonePlusProche(positionApprox);
		if (hexPlusProche == null) {
			Debug.LogError ("Aucun hexagone n'a été trouvé. Le terrain est-il initialisé ?");
		} else {
			invoc.transform.localPosition = hexPlusProche.positionLocaleSurTerrain;
			invoc.transform.Translate(offset);
		}
		return invoc;
	}

	/// <summary>
	/// Invoque l'objet demandé et le recentre automatiquement
	/// </summary>
	/// <remarks>Il faut que le gameobject "Terrain" soit initlaisé avec tous ses game objets
	/// "Bloc Terrain" contenant les hexagones</remarks>
	/// <returns>Le game object, rattaché au terrain, sur l'hexagone le plus proche</returns>
	/// <param name="objet">Le type d'objet à créer</param>
	public GameObject InvoquerObjetRecentre(Invocations objet){
		GameObject invoc = InvoquerObjet(objet);
		HexagoneInfo hexPlusProche = TerrainUtils.HexagonePlusProche(invoc.transform.localPosition);
		//Debug.Log("Ma position :" + invoc.transform.position + ", pour " + hexPlusProche.positionGlobale);
		if (hexPlusProche == null) {
			Debug.LogError("Aucun hexagone n'a été trouvé. Le terrain est-il initialisé ?");
		} else {
			invoc.transform.localPosition = hexPlusProche.positionLocaleSurTerrain;
		}
		//Debug.Log("Ma nouvelle position :" + invoc.transform.position);
		return invoc;
	}
#endregion

}
#endregion

#region Invocation
/// <summary>
/// Les types d'objets que l'on peut créer
/// [10;19] : par rapport aux obstacles inertes
/// [20;29] : par rapport aux unités noires
/// [30;39] : par rapport aux unités rouges
/// [40;49] : par rapport à d'autres menaces
/// [50;69] : par rapport à la nourriture
/// </summary>
public enum Invocations : int {
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
	/// Une fourmi blanche
	/// </summary>
	FOURMI_ROUGE_OUVRIERE = 30,
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
	/// Un oeuf tout vert
	/// </summary>
	OEUF_FOURMI = 35,
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
	/// Un pale anneau vert
	/// </summary>
	PHEROMONE = 70,
	/// <summary>
	/// Une lumière dans un anneau pour montrer la case visée
	/// </summary>
	SELECTION_CASE = 71,
	/// <summary>
	/// Une game object invisible à placer sur les cases d'eau
	/// </summary>
	EAU3D = 72,
	/// <summary>
	/// Un objet de debug
	/// </summary>
	DEBUG_OBJECT = 173,
	/// <summary>
	/// Une fourmis pour les debugs
	/// </summary>
	DEBUG_FOURMIS = 174
}
#endregion

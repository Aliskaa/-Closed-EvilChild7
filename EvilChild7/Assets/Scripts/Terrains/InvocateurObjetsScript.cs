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
/// PY Lapersonne
/// Sophie Réthoré
/// Gwendal Pollet
/// 
/// Version 4.2.0
/// </remarks>

using UnityEngine;
using System.Collections;

/// <summary>
/// Classe pour crééer de nouveaux objets via les prefabs.
/// </summary>
public class InvocateurObjetsScript : MonoBehaviour {

	/* ********* *
	 * Attributs *
	 * ********* */

#region Constantes

	#region Packages
	private const string packageBois 			= "Assets/Bois/";
	private const string packageCailloux 		= "Assets/Resources/Cailloux/";
	private const string packageFourmis			= "Assets/Resources/Fourmis/";
	private const string packageFourmisNoires	= "Assets/Resources/Fourmis/noires/";
	private const string packageFourmisBlanches	= "Assets/Resources/Fourmis/blanches/";
	private const string packageNourritures 	= "Assets/Resources/Nourritures/";
	private const string packagePheromones 		= "Assets/Resources/Pheromones/";
	private const string packageScarabees 		= "Assets/Resources/Scarabees/";
	private const string packageEau				= "Assets/Resources/Eau/";
	private const string packagePistolet		= "Assets/Resources/Pistolet/";
	private const string packageTerrain 		= "Assets/Resources/Terrains/";
	private const string packageFleche 			= "Assets/Resources/FlechesDirections/";
	private const string packageDebug	 		= "Assets/Resources/Debug/";
	#endregion

	#region Cailloux, eau, bois
	private const string fichierBoutDeBois 		= "bois";
	private const string fichierPetitCaillou 	= "petit_caillou";
	private const string fichierCaillou 		= "caillou";
	private const string fichierTresGrosCaillou = "tres_gros_caillou";
	private const string fichierEau3D 			= "Eau3D";
	private const string fichierEau 			= "eau";
	private const string fichierPistolet	    = "flaregun";
	private const string fichierMissileEau	    = "missile_eau";
	private const string fichierGoutte 			= "goute";
	private const string fichierBombeEau 		= "BombeAeau";
	private const string fichierFleche2	 		= "fleche2";
	private const string fichierFleche	 		= "fleche";
	#endregion

	#region Bestioles
	private const string fichierFNCmb 			= "fourmi_noire_combattante";
	private const string fichierFNCm 			= "fourmi_noire_contremaitre";
	private const string fichierFNG 			= "fourmi_noire_generale";
	private const string fichierFNO 			= "fourmi_noire_ouvriere";
	private const string fichierFNR 			= "fourmi_noire_reine";
	private const string fichierFBCmb 			= "fourmi_blanche_combattante";
	private const string fichierFBCm			= "fourmi_blanche_contremaitre";
	private const string fichierFBG 			= "fourmi_blanche_generale";
	private const string fichierFBO 			= "fourmi_blanche_ouvriere";
	private const string fichierFBR 			= "fourmi_blanche_reine";
	private const string fichierOeuf 			= "oeuf_fourmi";
	private const string fichierPartEclosions	= "particules_eclosion";
	private const string fichierPON 			= "pheromones_ouvriere_noire";
	private const string fichierPOB 			= "pheromones_ouvriere_blanche";
	private const string fichierPCN 			= "pheromones_contremaitre_noire";
	private const string fichierPCB	 			= "pheromones_contremaitre_blanche";
	private const string fichierScara 			= "scarabee";
	private const string fichierMortBestiole	= "particules_mort_sans_camp";
	private const string fichierMortFNTrash		= "particules_mort_fourmi_trash";
	private const string fichierMortFOTrash		= "particules_mort_fourmi_trash";
	private const string fichierMortReineTrash	= "particules_mort_reine_trash";
	private const string fichierMortFN			= "particules_mort_fourmi_noire";
	private const string fichierMortFO			= "particules_mort_fourmi_blanche";
	private const string fichierMortReineNoire	= "particules_mort_reine_noire";
	private const string fichierMortReineBlanche= "particules_mort_reine_blanche";
	private const string fichierReceptionNourri = "particules_reception_nourriture";
	#endregion

	#region Bonbons
	private const string fichierBAB 			= "bonbon_anglais_bleu";
	private const string fichierBAR 			= "bonbon_anglais_rose";
	private const string fichierBM 				= "bonbon_mure";
	private const string fichierBO 				= "bonbon_orange";
	private const string fichierBR 				= "bonbon_rose";
	private const string fichierBV 				= "bonbon_vert";
	#endregion
	
	#region Debug
	private const string fichierDebugObject 	= "Debug_Object";
	private const string fichierDebugFourmis 	= "Debug_fourmi_blanche_generale";
	#endregion

	private const string fichierSelectionCase 	= "Selection case";

#endregion

#region Attributs publics
	/// <summary>
	/// YOLO
	/// </summary>
	public static bool MODE_TRASH = false;
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
		//string cheminPackage = "";
		string nomFichier = "";
		switch ( objet ){
			case Invocations.BONBON_ANGLAIS_BLEU:
				//cheminPackage = packageNourritures;
				nomFichier = fichierBAB;
				break;
			case Invocations.BONBON_ANGLAIS_ROSE:
				//cheminPackage = packageNourritures;
				nomFichier = fichierBAR;
				break;
			case Invocations.BONBON_MURE:
				//cheminPackage = packageNourritures;
				nomFichier = fichierBM;
				break;
			case Invocations.BONBON_ORANGE:
				//cheminPackage = packageNourritures;
				nomFichier = fichierBO;
				break;
			case Invocations.BONBON_ROSE:
				//cheminPackage = packageNourritures;
				nomFichier = fichierBR;
				break;
			case Invocations.BONBON_VERT:
				//cheminPackage = packageNourritures;
				nomFichier = fichierBV;
				break;
			case Invocations.BOUT_DE_BOIS:
				//cheminPackage = packageBois;
				nomFichier = fichierBoutDeBois;
				break;
			case Invocations.CAILLOU:
				//cheminPackage = packageCailloux;
				nomFichier = fichierCaillou;
				break;
			case Invocations.PETIT_CAILLOU:
				//cheminPackage = packageCailloux;
				nomFichier = fichierPetitCaillou;
				break;
			case Invocations.TRES_GROS_CAILLOU:
				//cheminPackage = packageCailloux;
				nomFichier = fichierTresGrosCaillou;
				break;
			case Invocations.FOURMI_NOIRE_COMBATTANTE:
				//cheminPackage = packageFourmisNoires;
				nomFichier = fichierFNCmb;
				break;
			case Invocations.FOURMI_NOIRE_CONTREMAITRE:
				//cheminPackage = packageFourmisNoires;
				nomFichier = fichierFNCm;
				break;
			case Invocations.FOURMI_NOIRE_GENERALE:
				//cheminPackage = packageFourmisNoires;
				nomFichier = fichierFNG;
				break;
			case Invocations.FOURMI_NOIRE_OUVRIERE:
				//cheminPackage = packageFourmisNoires;
				nomFichier = fichierFNO;
				break;
			case Invocations.FOURMI_NOIRE_REINE:
				//cheminPackage = packageFourmisNoires;
				nomFichier = fichierFNR;
				break;
			case Invocations.FOURMI_BLANCHE_COMBATTANTE:
				//cheminPackage = packageFourmisBlanches;
				nomFichier = fichierFBCmb;
				break;
			case Invocations.FOURMI_BLANCHE_CONTREMAITRE:
				//cheminPackage = packageFourmisBlanches;
				nomFichier = fichierFBCm;
				break;
			case Invocations.FOURMI_BLANCHE_GENERALE:
				//cheminPackage = packageFourmisBlanches;
				nomFichier = fichierFBG;
				break;
			case Invocations.FOURMI_BLANCHE_OUVRIERE:
				//cheminPackage = packageFourmisBlanches;
				nomFichier = fichierFBO;
				break;
			case Invocations.FOURMI_BLANCHE_REINE:
				//cheminPackage = packageFourmisBlanches;
				nomFichier = fichierFBR;
				break;
			case Invocations.OEUF_FOURMI:
				//cheminPackage = packageFourmis;
				nomFichier = fichierOeuf;
				break;
			case Invocations.PHEROMONES_CONTREMAITRE_BLANCHE:
				//cheminPackage = packagePheromones;
				nomFichier = fichierPCB;
				break;
			case Invocations.PHEROMONES_CONTREMAITRE_NOIRE:
				//cheminPackage = packagePheromones;
				nomFichier = fichierPCN;
				break;
			case Invocations.PHEROMONES_OUVRIERE_BLANCHE:
				//cheminPackage = packagePheromones;
				nomFichier = fichierPOB;
				break;
			case Invocations.PHEROMONES_OUVRIERE_NOIRE:
				//cheminPackage = packagePheromones;
				nomFichier = fichierPON;
				break;
			case Invocations.SCARABEE:
				//cheminPackage = packageScarabees;
				nomFichier = fichierScara;
				break;
			case Invocations.EAU:
				//cheminPackage = packageEau;
				nomFichier = fichierEau;
				break;
			case Invocations.EAU3D:
				//cheminPackage = packageEau;
				nomFichier = fichierEau3D;
				break;
			case Invocations.SELECTION_CASE:
				//cheminPackage = packageTerrain;
				nomFichier = fichierSelectionCase;
				break;
			case Invocations.DEBUG_OBJECT:
				//cheminPackage = packageDebug;
				nomFichier = fichierDebugObject;
				break;
			case Invocations.DEBUG_FOURMIS:
				//cheminPackage = packageDebug;
				nomFichier = fichierDebugFourmis;
				break;
			case Invocations.PARTICULES_ECLOSION:
				//cheminPackage = packageFourmis;
				nomFichier = fichierPartEclosions;
				break;
			case Invocations.PARTICULES_MORT_BESTIOLE:
				//cheminPackage = packageFourmis;
				nomFichier = fichierMortBestiole;
				break;
			case Invocations.PARTICULES_MORT_BESTIOLE_TRASH:
				//cheminPackage = packageFourmis;
				nomFichier = fichierMortFNTrash;
				break;
			case Invocations.PARTICULES_MORT_REINE_TRASH:
				//cheminPackage = packageFourmis;
				nomFichier = fichierMortReineTrash;
				break;
			case Invocations.PARTICULES_MORT_REINE_BLANCHE:
				//cheminPackage = packageFourmis;
				nomFichier = fichierMortReineBlanche;
				break;
			case Invocations.PARTICULES_MORT_REINE_NOIRE:
				//cheminPackage = packageFourmis;
				nomFichier = fichierMortReineNoire;
				break;
			case Invocations.PARTICULES_MORT_FOURMI_BLANCHE:
				//cheminPackage = packageFourmis;
				nomFichier = fichierMortFO;
				break;
			case Invocations.PARTICULES_MORT_FOURMI_NOIRE:
				//cheminPackage = packageFourmis;
				nomFichier = fichierMortFN;
				break;
			case Invocations.GOUTTE:
				//cheminPackage = packageEau;
				nomFichier = fichierGoutte;
				break;
			case Invocations.MISSILE_EAU:
				//cheminPackage = packageEau;
				nomFichier = fichierMissileEau;
				break; 
			case Invocations.BOMBE_EAU:
				//cheminPackage = packageEau;
				nomFichier = fichierBombeEau;
				break;
			case Invocations.PARTICULES_RECEP_NOURRI:
				//cheminPackage = packageFourmis;
				nomFichier = fichierReceptionNourri;
				break;
			case Invocations.FLECHE2:
				//cheminPackage = packageFleche;
				nomFichier = fichierFleche2;
				break;
			case Invocations.FLECHE:
				//cheminPackage = packageFleche;
				nomFichier = fichierFleche;
				break;
			default:
				//Debug.LogError("Impossible de créer l'objet :"+objet);
				return null;
		}
		//cheminPackage + 
		string cheminComplet = nomFichier;
		if(cheminComplet != fichierSelectionCase){
			//Debug.Log("Chemin complet du prefab : "+cheminComplet);
		}
		GameObject invoc = Resources.Load<GameObject>(cheminComplet);
		if ( invoc == null ){
			//Debug.LogError("Impossible de créer l'objet avec :"+cheminComplet);
			return null;
		} else {
			invoc.name = "Invoc:"+nomFichier.Split(new char[]{'.'})[0];
		}
		GameObject terrain = GameObject.Find("Terrain");
		if ( terrain == null ){
			//Debug.LogError("Impossible de trouver l'objet \"Terrain\"");
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
		if (objet == Invocations.RIEN) return null;
		GameObject invoc = InvoquerObjet(objet);
		HexagoneInfo hexPlusProche = TerrainUtils.HexagonePlusProche(positionApprox);
		if (hexPlusProche == null) {
			//Debug.LogError ("Aucun hexagone n'a été trouvé. Le terrain est-il initialisé ?");
		} else {
			invoc.transform.localPosition = hexPlusProche.positionLocaleSurTerrain;
		}
		return invoc;
	}

	/// <summary>
	/// Invoque l'objet demandé et le place sur l'hexagone donné
	/// </summary>
	/// <returns>Le game object, rattaché au terrain, sur l'hexagone le plus proche
	/// de la position indiquée</returns>
	/// <param name="objet">Le type d'objet à créer</param>
	/// <param name="positionApprox">La position de l'objet</param>
	public GameObject InvoquerObjetSansRecentrage(Invocations objet, Vector3 positionApprox){
		if (objet == Invocations.RIEN) return null;
		GameObject invoc = InvoquerObjet(objet);
		invoc.transform.localPosition = positionApprox;
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
			//Debug.LogError ("Aucun hexagone n'a été trouvé. Le terrain est-il initialisé ?");
		} else {
			Vector3 pos = hexPlusProche.positionLocaleSurTerrain;
			pos.x += offset.x;
			pos.y += offset.y;
			pos.z += offset.z;
			invoc.transform.localPosition = pos;
			//invoc.transform.Translate(offset);
		}
		return invoc;
	}

	/// <summary>
	/// Invoque l'objet demandé
	/// </summary>
	/// <returns>Le game object n'est pas rattaché au terrain</returns>
	/// <param name="objet">Le type d'objet à créer</param>
	public GameObject InvoquerObjetCamera(Invocations objet, Vector3 position){
		//string cheminPackage = "";
		string nomFichier = "";
		switch ( objet ){
		case Invocations.PISTOLET:
			//cheminPackage = packagePistolet;
			nomFichier = fichierPistolet;
			break;
		case Invocations.FLECHE2:
			//cheminPackage = packageFleche;
			nomFichier = fichierFleche;
			break;
		default:
			//Debug.LogError("Impossible de créer l'objet :"+objet);
			return null;
		}
		//cheminPackage + 
		string cheminComplet = nomFichier;
		GameObject invoc = Resources.LoadAssetAtPath<GameObject>(cheminComplet);
		if (invoc == null) {
			//Debug.LogError("Impossible de créer l'objet avec :"+cheminComplet);
			return null;
		} else {
			invoc.name = "Invoc:"+nomFichier.Split(new char[]{'.'})[0];
		}
		
		invoc = (GameObject)Instantiate(invoc,position,Quaternion.identity);
		
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
			//Debug.LogError("Aucun hexagone n'a été trouvé. Le terrain est-il initialisé ?");
		} else {
			invoc.transform.localPosition = hexPlusProche.positionLocaleSurTerrain;
		}
		//Debug.Log("Ma nouvelle position :" + invoc.transform.position);
		return invoc;
	}
	
	/// <summary>
	/// Invoque l'objet demandé devant la caméra de l'utilisateur pour simuler un lancé
	/// </summary>
	public GameObject InvoquerDevantCamera( Invocations objet, Vector3 position){	
		if (objet == Invocations.RIEN) return null;
		//GameObject terrainGo = GameObject.FindGameObjectWithTag ("BAC_A_SABLE");
		//TerrainManagerScript tms = terrainGo.GetComponent<TerrainManagerScript> ();
		//Vector3 p = tms.ConvertirCoordonnes (position);
		GameObject invoc = InvoquerObjetCamera(objet, position);
		return invoc;
	}
#endregion

}

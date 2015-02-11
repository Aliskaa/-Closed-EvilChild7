using UnityEngine;
using System.Collections.Generic;

public abstract class GroupeRessource:MonoBehaviour{
	
	//Variable image betises
	public Texture2D image_caillou;
	public Texture2D image_pistolet;
	public Texture2D image_bonbons;
	public Texture2D image_bout_de_bois;
	public Texture2D image_deux_fois;
	public Texture2D image_force;
	public Texture2D image_ventilateur;
	public Texture2D image_bombe_eau;
	public Texture2D image_scarabee;
	public Texture2D image_gelee;

	public Texture2D fond_sac;
	public Texture2D fond_betise;
	
	//Image pheromone et oeuf    
	public Texture2D image_pheromone_contremaitre;
	public Texture2D image_pheromone_ouvriere;
	public Texture2D image_oeuf;
	
	//Image vents
	public Texture2D vent_droit_faible;
	public Texture2D vent_droit_fort;
	public Texture2D vent_gauche_faible;
	public Texture2D vent_gauche_fort;
	public Texture2D vent_haut;
	public Texture2D vent_bas;
	
	//Image son et quitter
	public Texture2D image_quitter;
	public Texture2D onAir;
	public Texture2D mutted;
	public Texture2D textureBoutonMusique;
	
	//Variable style (css des messages)
	[HideInInspector]
	public GUIStyle smallFont;
	[HideInInspector]
	public GUIStyle largeFont;

	protected static int  NUMBER_BETISES = 3;

	protected static float  AFK_TIMER = 5f;
	protected static float NORMAL_TIMER = 45f;

	protected static float  AFF_INFOS_TIMER = 1.5f;
	protected static float  IA_SELECTION_BETISE_TIMER = 1.6f;

	protected float timerAffichageFinJeu = 0f;
	protected float timerAffichageDebutPartie = 0f;
	protected float timerAffichageDebutTour = 0f;
	protected float timerAffichageLancers = 0f;
	protected float timerSelectionBetiseIA = 0f;

	protected bool betisesChoisies = false;
	protected bool betisesGenerees = false;
	protected bool finDePartie = false;
	protected bool limitePtac = false;
	
	protected bool affichageFindePartieJoueur = false;
	protected bool affichageFindePartieIA = false;
	protected bool affichageDebutTourIA = false;
	protected bool affichageDebutTourJoueur = false;
	protected bool affichageBetiseAchoisir = false;
	protected bool affichageBetiseAlancer = false;
	protected bool affichageDebutPartie = false;
	protected bool choisirBetiseIA = false;
	protected bool stopTimer = false;

	// Variable progress bar
	public Texture2D barre_timer_pleine;
	public Texture2D barre_timer_vide;
	public Texture2D barre_vie_pleine;
	public Texture2D barre_vie_vide;
	public Texture2D icone_reine_blanche;
	public Texture2D icone_reine_noire;
	public Texture2D barre_ptac_pleine;
	public Texture2D barre_ptac_vide;

	protected void resetFlagsTour(){
		
		betisesChoisies = false;
		betisesGenerees = false;
		limitePtac = false;
		
		affichageFindePartieJoueur = false;
		affichageFindePartieIA = false;
		affichageDebutTourIA = false;
		affichageDebutTourJoueur = false;
		affichageBetiseAchoisir = false;
		affichageBetiseAlancer = false;
		affichageDebutPartie = false;

	}

	protected void afficherDebutPartie(){
		affichageDebutPartie = true;
		timerAffichageDebutPartie = AFF_INFOS_TIMER;
	}

	protected void afficherVictoireJoueur(){
		affichageFindePartieJoueur = true;
		timerAffichageFinJeu = AFF_INFOS_TIMER;
	}
	
	protected void afficherVictoireIA(){
		affichageFindePartieIA = true;
		timerAffichageFinJeu = AFF_INFOS_TIMER;
	}
	
	protected void afficherDebutTourJoueur(){
		affichageDebutTourJoueur = true;
		timerAffichageDebutTour = AFF_INFOS_TIMER;
	}
	
	protected void afficherDebutTourIA(){
		affichageDebutTourIA = true;
		timerAffichageDebutTour = AFF_INFOS_TIMER;
	}
	
	protected void afficherBetisesGenerees (){
		affichageBetiseAchoisir = true;
	}
	
	protected void afficherLancerBetises (){
		affichageBetiseAlancer = true;
		timerAffichageLancers = AFF_INFOS_TIMER;
	}

	protected Texture2D textureBetise(BetisesIndex indexBetise){
		switch (indexBetise) {
			
		case BetisesIndex.CAILLOU:
			return image_caillou;
		case BetisesIndex.PISTOLET:
			return image_pistolet;
		case BetisesIndex.BONBON:
			return image_bonbons;
		case BetisesIndex.BOUT_DE_BOIS:
			return image_bout_de_bois;
		case BetisesIndex.DOUBLE_LANCER:
			return image_deux_fois;
		case BetisesIndex.LANCER_GROS:
			return image_force;
		case BetisesIndex.VENTILATEUR:
			return image_ventilateur;
		case BetisesIndex.BOMBE_EAU:
			return image_bombe_eau;
		case BetisesIndex.SCARABEE:
			return image_scarabee;
		case BetisesIndex.GELEE:
			return image_gelee;
		default:
			return null;
		}
	}


}



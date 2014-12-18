using UnityEngine;
using System.Collections.Generic;

public class TourDeJeu{

	private static int BASE_PTAC = 2;
	private static int AUGMENT_PTAC = 1;
	private Joueur joueur;
	private int PTAC_TOTAL;
	private int PtacUtilise = 0;

	private int flagLancerPlusieursFois = 0;
	private Vent ventDuTour;
	private Level level = Level.DEBUT;
	private bool betisesChoisies = false;
	private List<BetisesTexturees> mesBetises = null;
	private bool isIA = false;


	public TourDeJeu(Joueur joueur,bool isIA){

		this.joueur = joueur;
		this.ventDuTour = new Vent ();
		this.PTAC_TOTAL = BASE_PTAC + joueur.getTour () * AUGMENT_PTAC;
		this.joueur.augmenterTour();
		this.joueur.getIaReine ().ratiosLineaires ();
		this.joueur.addOeufs (joueur.getIaReine().ponte());
		this.isIA = isIA;

	}

	public int getPTAC(){
		return this.PTAC_TOTAL;
	}

	public int getPTACutilise(){
		return this.PtacUtilise;
	}

	public List<Betise> genererBetise(){
		return new List<Betise> ();
	}
	
	public void faireDisparaitreBetise(BetisesTexturees text){
		if (flagLancerPlusieursFois > 0) {
			flagLancerPlusieursFois = flagLancerPlusieursFois - 1;
		} else {
			this.joueur.removeBetise(text);
		}

	}
	public void doublerPTAC(){

		PTAC_TOTAL = PTAC_TOTAL * 2;

	}

	public void augmenterPTACutilise(int quantite){
		this.PtacUtilise += quantite;
	}

	public void deuxLancer(){
		
		flagLancerPlusieursFois += 1;
		
	}
	public bool joueurIsIA(){
		return isIA;
	}
	public Vent getVent(){
		return this.ventDuTour;
	}

	public Level getLevel(){
		return level;
	}

	public void nextLevel(){
		level += 1;
	}

	public List<BetisesTexturees> getBetisesTour(){
		return this.mesBetises;
	}

	public void setBetisesTour(List<BetisesTexturees> maListe){
		this.mesBetises = maListe;
	}

	public bool getBetisesChoisies(){
		return this.betisesChoisies;
	}

	public void setBetisesChoisies(bool choix){
		this.betisesChoisies = choix;
	}


}
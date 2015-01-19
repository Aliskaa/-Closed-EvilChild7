using UnityEngine;
using System.Collections.Generic;

public class LogiqueDeJeu:GroupeRessource
{

	public GUISkin skinFontes;

	private GameObject bacASable;

	private IAreine iaReineBlanche = null;
	private IAreine iaReineNoire =  null;
	private List<Joueur> mesJoueurs;

	private Joueur monJoueur;
	private IAjoueur monIA;
	private Interface monInterface;

	TourDeJeu courant = null;

	float timerTour;
	private bool sound = true;

	public LogiqueDeJeu(){
	
		monInterface = new Interface (null, null,null);
	}
	void Awake(){
		bacASable = GameObject.FindGameObjectWithTag ("BAC_A_SABLE");
		monInterface.bacAsable = bacASable;
		
		GameObject gameObjectReineBlanche = GameObject.FindGameObjectWithTag ("REINE_BLANCHE");
		GameObject gameObjectReineNoire = GameObject.FindGameObjectWithTag ("REINE_NOIRE");
		
		if (gameObjectReineBlanche != null) {
			iaReineBlanche = (IAreine)((ReineScript)gameObjectReineBlanche.GetComponent<ReineScript> ()).iaReine;
		} else {
			Debug.Log("Reine blanche non trouvée"+iaReineBlanche);
		}
		
		if (gameObjectReineBlanche != null) {
			iaReineNoire = (IAreine)((ReineScript)gameObjectReineNoire.GetComponent<ReineScript> ()).iaReine;
		} else {
			Debug.Log("Reine noire non trouvée"+iaReineNoire);
		}
	}
	void Start(){

		smallFont = new GUIStyle();
		largeFont = new GUIStyle();
		
		smallFont.fontSize = 10;
		largeFont.fontSize = 32;

		monInterface.setLargefont (largeFont);
		monInterface.setSmallfont (smallFont);

	

		monJoueur = new Joueur (iaReineBlanche,true);
		monIA = new IAjoueur  (iaReineNoire,null,monInterface,false,bacASable);

		mesJoueurs = new List<Joueur> ();

		mesJoueurs.Add (monJoueur);
		mesJoueurs.Add (monIA);
		randomiserJoueurs ();

		afficherDebutPartie ();

		initTimerTour();
		if (mesJoueurs [0] == monIA) {
			courant = new TourDeJeu (getJoueurCourant (), true);
		} else {
			courant = new TourDeJeu (getJoueurCourant (), false);
		}
	}

	void Update(){
		//Debug.Log (getTourCourant ().getLevel ());
		updateTimers ();
		if(joueurCourantIsIA()){
			if(monIA.aFini()){
				finirTour();
				monIA.tourFini = false;
				monIA.actionLancees = false;
			}
		}
		if (iaReineBlanche.getModele ().getPointsDeVie () <= 0) {

			afficherVictoireJoueur ();

		} else if (iaReineNoire.getModele ().getPointsDeVie () <= 0) {

			afficherVictoireIA ();

		}
		if (!affichageDebutPartie) {
			if (courant.getLevel () == Level.DEBUT) {
					if (getJoueurCourant () == monIA) {
							afficherDebutTourIA ();
							courant.nextLevel();
					} else {
							afficherDebutTourJoueur ();
							courant.nextLevel();
					}
			}
			if (courant.getLevel () == Level.DEBUT_TOUR) {
					afficherBetisesGenerees ();
					
			}

			if (courant.getLevel () == Level.CHOISIR_BETISES) {
					afficherLancerBetises ();
					courant.nextLevel();
			}
		}
	}

	void OnGUI() 
	{	
		GUI.skin = skinFontes;

		if (getTourCourant ().getLevel () > Level.CHOISIR_BETISES) {
			bool tmp = monInterface.afficherBoutonFinTour(getTourCourant());
			if(tmp){
				finirTour();
			}
		}
		monInterface.afficherOeufs (image_oeuf, getJoueurCourant (),getTourCourant());
		monInterface.afficherNombreTours (getJoueurCourant ());
		monInterface.afficherSacAbetise (fond_sac,fond_betise,getJoueurCourant(),getTourCourant());
		gestionQuitter ();
		GestionSon ();
		monInterface.afficherBarreVieReineJoueur (getJoueurCourant());

		if(getJoueurCourant().absent()){
			monInterface.afficherTimerTour((int)timerTour,(int)AFK_TIMER);
		}else{
			monInterface.afficherTimerTour((int)timerTour,(int)NORMAL_TIMER);
		}

		monInterface.afficherPtac (getTourCourant ().getPTACutilise (),getTourCourant ().getPTAC ());

		if (affichageDebutPartie) {

			if(timerAffichageDebutPartie <=0){
				affichageDebutPartie = false;
			}else{
				monInterface.afficherDebut();
			}
			
		}

		if (affichageDebutTourIA) {

			if(timerAffichageDebutTour <=0){
				affichageDebutTourIA = false;
				//courant.nextLevel();
			}else{
				monInterface.afficherTourIA();
			}
			
		}

		if (affichageDebutTourJoueur) {
			
			if(timerAffichageDebutTour <=0){
				affichageDebutTourJoueur = false;
				//courant.nextLevel();
			}else{
				monInterface.afficherTourJoueur();
			}
			
		}
		
		if (affichageFindePartieIA) {
			
			if (timerAffichageFinJeu <= 0) {
					affichageFindePartieIA = false;
					Application.LoadLevel(0);
			} else {
					monInterface.afficherVictoireIA ();		
			}
		}

		if (affichageFindePartieJoueur) {
			
			if (timerAffichageFinJeu <= 0) {
				//affichageFindePartieJoueur = false;
				Application.LoadLevel(0);
			} else {
				monInterface.afficherVictoireJoueur();		
			}
		}
		if (!getTourCourant ().getBetisesChoisies ()) {

		
			if (affichageBetiseAchoisir) {
				if (getTourCourant ().getBetisesTour () == null) {
					genererBetisesAchoisir ();
					timerSelectionBetiseIA = IA_SELECTION_BETISE_TIMER;
				}
				if(joueurCourantIsIA()){
					if(timerSelectionBetiseIA<=0){
						monIA.selectionnerUneBetise(getTourCourant ().getBetisesTour ());
						courant.nextLevel();
						courant.setBetisesChoisies(true);
						affichageBetiseAchoisir = false;
					}
				}
				monInterface.afficherChoixBetise (getTourCourant ().getBetisesTour (), getTourCourant (), getJoueurCourant (),fond_sac);	
			}
		}

		if (affichageBetiseAlancer) {
			if (timerAffichageLancers <= 0) {
				if(joueurCourantIsIA()){
					monIA.deroulerTour();
				}
				affichageBetiseAlancer = false;
			} else {
				monInterface.afficherBetiseAlancer();
			}	
		}

	}	
	private void updateTimers(){

		if (affichageDebutPartie) {
			timerAffichageDebutPartie -= Time.deltaTime;
		}

		if (affichageDebutTourIA||affichageDebutTourJoueur) {
			timerAffichageDebutTour -= Time.deltaTime;
		}

		if (affichageFindePartieIA||affichageFindePartieJoueur) {
			timerAffichageFinJeu -= Time.deltaTime;
		}

		if (affichageBetiseAchoisir && joueurCourantIsIA ()) {
			timerSelectionBetiseIA -= Time.deltaTime;
		}

		if (affichageBetiseAlancer) {
			timerAffichageLancers -= Time.deltaTime;
		}
		if (!stopTimer) {
			timerTour -= Time.deltaTime;
			if (timerTour <= 0) {
				finirTour ();
			}
		}
	}

	private void randomiserJoueurs(){
		
		System.Random random = new System.Random();
		if(mesJoueurs.Count > 0){

			int index = random.Next(0,mesJoueurs.Count - 1);
			Joueur tmp = mesJoueurs[0];
			mesJoueurs[0] = mesJoueurs[index]; 
			mesJoueurs[index] = tmp;
			for(int i = 0; i<mesJoueurs.Count;i++){
				if(mesJoueurs[i].getNameJoueur() == ""){
					mesJoueurs[i].setNameJoueur("Joueur "+(i+1));
				}
			}
		}
		
	}
	
	private bool joueurCourantIsIA(){

		if(getJoueurCourant() == monIA){

			return true;

		}
		return false;
	}

	public Joueur getJoueurCourant(){
		
		return mesJoueurs[0];
		
	}
	private void afficherDebut (){
		if (joueurCourantIsIA ()) {
				afficherDebutTourIA ();
		} else {
				afficherDebutTourJoueur ();
		}
	}

	private void afficherFinTour (){
		if (joueurCourantIsIA ()) {
			afficherDebutTourJoueur ();
		} else {
			afficherDebutTourIA ();
		}
	}
	
	private Joueur passerProchainJoueur(){
		
		Joueur tmp = getJoueurCourant ();
		mesJoueurs.Remove (tmp);
		mesJoueurs.Add (tmp);
		return mesJoueurs[0];
		
	}

	private void initTimerTour(){
		if (getJoueurCourant ().absent ()) {
			timerTour = AFK_TIMER;
		} else {
			timerTour = NORMAL_TIMER;
		}
	}

	public TourDeJeu getTourCourant(){
		return this.courant;
	}

	
	void gestionQuitter(){
		if (GUI.Button(new Rect(1280, 20, 30, 30), image_quitter)){
			Application.LoadLevel(0);
		}
	}
	
	void GestionSon(){
		if (GUI.Button(new Rect(1250, 20, 30, 30), textureBoutonMusique)){
			lancerMusique();
		}
	}

	void lancerMusique(){
		
		if(sound == true){
			textureBoutonMusique = mutted;
			audio.Stop();
			sound = false;
		}else{
			audio.Play();
			textureBoutonMusique = onAir;
			sound = true;	
		}
	} 
	protected void genererBetisesAchoisir(){
		List<BetisesTexturees> maListe = new List<BetisesTexturees> ();
		for (int i = 0; i< NUMBER_BETISES; i++) {
			BetisesIndex indexBetise = (BetisesIndex)Random.Range((float)BetisesIndex.CAILLOU,(float)BetisesIndex.GELEE);
			Texture2D maTexture = textureBetise(indexBetise);
			maListe.Add(new BetisesTexturees(indexBetise,maTexture));
    	}
		getTourCourant ().setBetisesTour (maListe);
	}
	protected void finirTour(){
		afficherFinTour ();


		Joueur tmp = passerProchainJoueur ();
		if (mesJoueurs[0] == monIA) {
			courant = new TourDeJeu (tmp,true);
			monIA.setTour (courant);
		} else {
			courant = new TourDeJeu (tmp,false);
		}
		initTimerTour ();
		resetFlagsTour();
		monInterface.resetPrompts();
	}



}


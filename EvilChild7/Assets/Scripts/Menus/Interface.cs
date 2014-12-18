using UnityEngine;
using System.Collections.Generic;
public class Interface:ValeursInterfaces
{


	protected GUIStyle smallFont;
	protected GUIStyle largeFont;
	public GameObject bacAsable;
	public bool prompt = false;
	public bool promptLancer = false;
	public bool lancerEnCours = false;
	public bool promptOeuf = false;
	public bool promptFinTour = false;
	public bool finTour = false;
	public bool promptPoids = false;
	protected BetisesTexturees betiseChoisie = null;


	public Interface(GUIStyle smallFont,GUIStyle largeFont,GameObject bacAsable){
		this.smallFont = smallFont;
		this.largeFont = largeFont;
		this.bacAsable = bacAsable;
	}

	public void setSmallfont(GUIStyle smallFont){
		this.smallFont = smallFont;
	}

	public  void setLargefont(GUIStyle largeFont){
		this.largeFont = largeFont;
	}
	public void afficherDebut(){
		largeFont.normal.textColor = Color.white;
		GUI.Label (new Rect ((Screen.width*36)/100, (Screen.height*32)/100, (Screen.width*14)/100, (Screen.height*10)/100), STRING_DEBUT,largeFont);
	}
	
	public void afficherTourJoueur(){
		largeFont.normal.textColor = Color.white;
		GUI.Label (new Rect ((Screen.width*38)/100, (Screen.height*32)/100, (Screen.width*14)/100, (Screen.height*10)/100), "A toi de jouer",largeFont);

	}

	public void afficherTourIA(){
		largeFont.normal.textColor = Color.white;
		GUI.Label (new Rect ((Screen.width*38)/100, (Screen.height*32)/100, (Screen.width*14)/100, (Screen.height*10)/100), "Tour de l'IA",largeFont);
	}

	public void afficherNombreTours (Joueur joueur)
	{
		largeFont.normal.textColor = Color.white;
		string aff = "Tour "+joueur.getTour()+" "+joueur.getNameJoueur();
		if (joueur is IAjoueur) {
			aff = aff + " (IA) "; 
		}
		GUI.Label (new Rect (Screen.width*0.36f, 0, Screen.width*0.2f, Screen.height*0.1f), aff,largeFont);
	}
	public void afficherVictoireIA(){
		largeFont.normal.textColor = Color.red;
		GUI.Label (new Rect ((Screen.width*44)/100, (Screen.height*34)/100, (Screen.width*14)/100, (Screen.height*10)/100), "Defaite",largeFont);

	}
	
	public void afficherVictoireJoueur(){
		largeFont.normal.textColor = Color.green;
		GUI.Label (new Rect ((Screen.width*44)/100, (Screen.height*34)/100, (Screen.width*14)/100, (Screen.height*10)/100), "Victoire",largeFont);
	}

	public void afficherBarreVieReineJoueur(Joueur joueur){
		int vie  = joueur.getIaReine ().getModele ().getPointsDeVie ();
		int barVie = vie/ 100;
		GUI.HorizontalScrollbar(new Rect ((Screen.width*1)/100,(Screen.height*4)/100,(Screen.width*15)/100,(Screen.height*4)/100), 0, barVie,0, 100);
		GUI.Label(new Rect ((Screen.width*1)/100,(Screen.height*7)/100,(Screen.width*25)/100,(Screen.height*0.1f)), vie + "/" + "10000 PV Reine "+joueur.getNameJoueur());
	}

	public void afficherTimerTour(int temps,int tempsfin){
		GUI.Label(new Rect ((Screen.width*86)/100,(Screen.height*10)/100,(Screen.width*11)/100,(Screen.height*10)/100), "Temps Restant");
		GUI.Label(new Rect ((Screen.width*86)/100,(Screen.height*14)/100,(Screen.width*11)/100,(Screen.height*10)/100), (int) temps + "/" + tempsfin);
	}

	public void afficherChoixBetise(List<BetisesTexturees> betisesGenerees, TourDeJeu courant, Joueur joueurCourant,Texture2D fondChoix){
		if (!prompt) {
			GUI.BeginGroup (new Rect (Screen.width * RATIO_CHOIX_BETISE_X, Screen.height * RATIO_CHOIX_BETISE_Y, Screen.width * RATIO_CHOIX_BETISE_LARGEUR, Screen.height * RATIO_CHOIX_BETISE_HAUTEUR), "");
			GUI.Box (new Rect (0, 0, (Screen.width * 20) / 100, (Screen.height * 17) / 100), "Choix de betise ?");
			if (GUI.Button (new Rect ((Screen.width * 3) / 100, (Screen.height * 5) / 100, (Screen.width * 4) / 100, (Screen.height * 8) / 100), betisesGenerees [0].getTexture (), "box")) {
				if(!courant.joueurIsIA()){
					prompt = true;
					betiseChoisie = betisesGenerees [0];
				}
			} else if (GUI.Button (new Rect ((Screen.width * 8) / 100, (Screen.height * 5) / 100, (Screen.width * 4) / 100, (Screen.height * 8) / 100), betisesGenerees [1].getTexture (), "box")) {
				if(!courant.joueurIsIA()){
					prompt = true;
					betiseChoisie = betisesGenerees [1];
				}
			} else if (GUI.Button (new Rect ((Screen.width * 13) / 100, (Screen.height * 5) / 100, (Screen.width * 4) / 100, (Screen.height * 8) / 100), betisesGenerees [2].getTexture (), "box")) {
				if(!courant.joueurIsIA()){
					prompt = true;
					betiseChoisie = betisesGenerees [2];
				}

			}
			GUI.EndGroup ();
		} else {
			afficherConfirmationBetise(courant,joueurCourant);
		}
	}

	public void afficherPtac(int poid,int PTAC){
		GUI.Label(new Rect ((Screen.width*86)/100,(Screen.height*17)/100,(Screen.width*11)/100,(Screen.height*10)/100), "PTAC utilise");
		GUI.Label(new Rect ((Screen.width*86)/100,(Screen.height*20)/100,(Screen.width*11)/100,(Screen.height*10)/100), poid + "/" + PTAC);
	}

	public void afficherBetiseAlancer(){
		largeFont.normal.textColor = Color.white;
		GUI.Label (new Rect ((Screen.width*40)/100, (Screen.height*34)/100, (Screen.width*14)/100, (Screen.height*10)/100), "Lancez !",largeFont);
	}

	public void afficherConfirmationBetise(TourDeJeu courant,Joueur joueurCourant){
		GUI.Box ( new Rect ( Screen.width*0.28f, Screen.height*0.38f, Screen.width*0.34f,  Screen.height*0.16f), "Voulez vous choisir la betise "+betiseChoisie.ToString()+"?"); 
		if (GUI.Button (new Rect (Screen.width*0.35f, Screen.height*0.44f, Screen.width*.1f,Screen.height*.07f), "Oui")) {
			joueurCourant.addBetise(betiseChoisie);
			betiseChoisie = null;
			prompt = false;
			courant.nextLevel();
			courant.setBetisesChoisies(true);
		}else if(GUI.Button(new Rect (Screen.width*0.47f, Screen.height*0.44f, Screen.width*0.1f,Screen.height*0.07f), "Non")){
			prompt = false;
		} 
	}



	public void afficherSacAbetise(Texture2D fondSac,Texture2D fondBetise,Joueur joueurCourant,TourDeJeu tour){
		if (promptLancer) {
			afficherConfirmationLancerBetise(joueurCourant,tour);
		}
		if (promptPoids) {
			afficherPoidsLimite(tour);
		}
		GUI.BeginGroup(new Rect(Screen.width*RATIO_SAC_BETISE_X, Screen.height*RATIO_SAC_BETISE_Y, Screen.width*RATIO_SAC_BETISE_LARGEUR, Screen.height*RATIO_SAC_BETISE_HAUTEUR), "");
		GUI.Box(new Rect(0,0,Screen.width*RATIO_SAC_BETISE_LARGEUR,Screen.height*RATIO_SAC_BETISE_HAUTEUR), "Sac a Betises");
		if (GUI.Button (new Rect ((Screen.width * 1) / 100, (Screen.height * 7) / 100, (Screen.width * 4) / 100, (Screen.height * 8) / 100), getTextureAt (joueurCourant, 0))) {
						if (joueurCourant.nbBetise () > 0 && autorisationClic (tour)) {
				if (tour.getPTACutilise () + (int)PoidsObjetsAlancerM.indexToPoids (joueurCourant.getBetiseAt (0).getBetise ()) <= tour.getPTAC ()) {
										if (!promptLancer) {
												promptLancer = true;
												betiseChoisie = joueurCourant.getBetiseAt (0);
										}
								}else{
									promptOeuf = true;
								}
						}
				}
		if(GUI.Button(new Rect((Screen.width*6)/100, (Screen.height*7)/100, (Screen.width*4)/100, (Screen.height*8)/100),getTextureAt(joueurCourant,1))) {
			if (joueurCourant.nbBetise () > 1&&autorisationClic(tour)) {
				if (tour.getPTACutilise () + (int)PoidsObjetsAlancerM.indexToPoids (joueurCourant.getBetiseAt (1).getBetise ()) <= tour.getPTAC ()) {
					if (!promptLancer) {
						promptLancer = true;
						betiseChoisie = joueurCourant.getBetiseAt (1);
					}
				}else{
					promptPoids = true;
				}
			}
		}
		if(GUI.Button(new Rect((Screen.width*11)/100, (Screen.height*7)/100, (Screen.width*4)/100, (Screen.height*8)/100),getTextureAt(joueurCourant,2))) {
			if (joueurCourant.nbBetise () > 2&&autorisationClic(tour)) {
				if (tour.getPTACutilise () + (int)PoidsObjetsAlancerM.indexToPoids (joueurCourant.getBetiseAt (2).getBetise ()) <= tour.getPTAC ()) {
						if (!promptLancer) {
							promptLancer = true;
							betiseChoisie = joueurCourant.getBetiseAt (2);
						}
					}else{
						promptPoids = true;
					}
				}
			}
		if(GUI.Button(new Rect((Screen.width*1)/100, (Screen.height*17)/100, (Screen.width*4)/100, (Screen.height*8)/100),getTextureAt(joueurCourant,3)))  {
			if (joueurCourant.nbBetise () > 3&&autorisationClic(tour)){
				if (tour.getPTACutilise () + (int)PoidsObjetsAlancerM.indexToPoids (joueurCourant.getBetiseAt (3).getBetise ()) <= tour.getPTAC ()) {
					if (!promptLancer) {
						promptLancer = true;
						betiseChoisie = joueurCourant.getBetiseAt (3);
					}
				}else{
					promptPoids =true;
				}
			}
		}
			
		if(GUI.Button(new Rect((Screen.width*6)/100, (Screen.height*17)/100, (Screen.width*4)/100, (Screen.height*8)/100),getTextureAt(joueurCourant,4)))  {
			if (joueurCourant.nbBetise () > 4&&autorisationClic(tour)){
				if (tour.getPTACutilise () + (int)PoidsObjetsAlancerM.indexToPoids (joueurCourant.getBetiseAt (4).getBetise ()) <= tour.getPTAC ()) {
					if (!promptLancer) {
						promptLancer = true;
						betiseChoisie = joueurCourant.getBetiseAt (4);
					}
				}else{
					promptPoids =true;
				}
			}
		}
			
		if (GUI.Button (new Rect ((Screen.width * 11) / 100, (Screen.height * 17) / 100, (Screen.width * 4) / 100, (Screen.height * 8) / 100), getTextureAt (joueurCourant, 5))) {
			if (joueurCourant.nbBetise () > 5 && autorisationClic (tour)) {
				if (tour.getPTACutilise () + (int)PoidsObjetsAlancerM.indexToPoids (joueurCourant.getBetiseAt (5).getBetise ()) <= tour.getPTAC ()) {
					if (!promptLancer) {
						promptLancer = true;
						betiseChoisie = joueurCourant.getBetiseAt (5);
					}
				}else{
					promptPoids =true;
				}
			
			}
		}
			GUI.EndGroup();
			
		}

	private Texture2D getTextureAt(Joueur joueurCourant, int index){
	BetisesTexturees tmp = joueurCourant.getBetiseAt (index);

		if (tmp != null) {
		//Debug.Log ("texture", null);
			return tmp.getTexture();
		}
		return null;
	}
	public void afficherConfirmationLancerBetise(Joueur joueurCourant,TourDeJeu tour){
		GUI.Box ( new Rect ( Screen.width*0.28f, Screen.height*0.38f, Screen.width*0.34f,  Screen.height*0.16f), "Voulez vous lancer la betise "+betiseChoisie.ToString()+"?"); 
		if (GUI.Button (new Rect (Screen.width*.35f, Screen.height*.44f, Screen.width*.1f,Screen.height*.07f), "Oui")) {
			promptLancer = false;
			tour.faireDisparaitreBetise(betiseChoisie);
			lancerEnCours = true;
			lancerUneBetise(betiseChoisie.getBetise(),joueurCourant,tour);
			betiseChoisie = null;


		}else if(GUI.Button(new Rect (Screen.width*.47f, Screen.height*.44f, Screen.width*.1f,Screen.height*.07f), "Non")){
			promptLancer = false;
		} 
	}

	protected void lancerUneBetise(BetisesIndex index,Joueur joueurCourant,TourDeJeu tour){
		Betise maBetise;
		switch (index) {
			
		case BetisesIndex.CAILLOU:
			//Debug.Log("caillou selectionné");
			if(joueurCourant.getBonCote()){
				maBetise = bacAsable.AddComponent<Betises> ();
				((Betises)maBetise).nom_betise = Invocations.CAILLOU;
				((Betises)maBetise).monInterface = this;
			}else{
				maBetise = bacAsable.AddComponent<Betises2> ();
				((Betises2)maBetise).nom_betise = Invocations.CAILLOU;
				((Betises2)maBetise).monInterface = this;

			}

			tour.augmenterPTACutilise(maBetise.getPoids());
			break;

		case BetisesIndex.PISTOLET:
			//Debug.Log("pistolet selectionné");
			if(joueurCourant.getBonCote()){
				maBetise = bacAsable.AddComponent<Betises> ();
				((Betises)maBetise).nom_betise = Invocations.MISSILE_EAU;
				((Betises)maBetise).monInterface = this;
			}else{
				maBetise = bacAsable.AddComponent<Betises2> ();
				((Betises2)maBetise).nom_betise = Invocations.MISSILE_EAU;
				((Betises2)maBetise).monInterface = this;
			}
			tour.augmenterPTACutilise(maBetise.getPoids());
			break;

		case BetisesIndex.BONBON:
			//Debug.Log("bonbons selectionné");
			NourritureBonbons n = new NourritureBonbons ();
			Invocations nom_bonbon = n.ListeNourriture();

			if(joueurCourant.getBonCote()){
				maBetise = bacAsable.AddComponent<Betises> ();
				((Betises)maBetise).nom_betise = nom_bonbon;
				((Betises)maBetise).monInterface = this;
			}else{
				maBetise = bacAsable.AddComponent<Betises2> ();
				((Betises2)maBetise).nom_betise = nom_bonbon;
				((Betises2)maBetise).monInterface = this;
			}
			tour.augmenterPTACutilise(maBetise.getPoids());
			break;

		case BetisesIndex.BOUT_DE_BOIS:
			if(joueurCourant.getBonCote()){
				maBetise = bacAsable.AddComponent<Betises> ();
				((Betises)maBetise).nom_betise = Invocations.BOUT_DE_BOIS;
				((Betises)maBetise).monInterface = this;
			}else{
				maBetise = bacAsable.AddComponent<Betises2> ();
				((Betises2)maBetise).nom_betise = Invocations.BOUT_DE_BOIS;
				((Betises2)maBetise).monInterface = this;
			}
			tour.augmenterPTACutilise(maBetise.getPoids());
			break;

		case BetisesIndex.DOUBLE_LANCER:
			//Debug.Log("deux fois selectionné");
			LancerDeuxFois tmpDeuxFois = new LancerDeuxFois();
			tmpDeuxFois.lancerCeTour(tour);
			break;

		case BetisesIndex.LANCER_GROS:
			//Debug.Log("force selectionné");
			LancerGros tmpGros = new LancerGros();
			tmpGros.lancerCeTour(tour);
			break;

		case BetisesIndex.VENTILATEUR:
			//Debug.Log("ventilateur selectionné");
			Ventilateur monVentilateur = new Ventilateur();
			monVentilateur.lancerCeTour(tour);
			break;
			
		case BetisesIndex.BOMBE_EAU:
			if(joueurCourant.getBonCote()){
				maBetise = bacAsable.AddComponent<Betises> ();
				((Betises)maBetise).nom_betise = Invocations.BOMBE_EAU;
				((Betises)maBetise).monInterface = this;
			}else{
				maBetise = bacAsable.AddComponent<Betises2> ();
				((Betises2)maBetise).nom_betise = Invocations.BOMBE_EAU;
				((Betises2)maBetise).monInterface = this;
			}
			tour.augmenterPTACutilise(maBetise.getPoids());
			break;

		case BetisesIndex.SCARABEE:
			if(joueurCourant.getBonCote()){
				maBetise = bacAsable.AddComponent<Betises> ();
				((Betises)maBetise).nom_betise = Invocations.SCARABEE;
				((Betises)maBetise).monInterface = this;
			}else{
				maBetise = bacAsable.AddComponent<Betises2> ();
				((Betises2)maBetise).nom_betise = Invocations.SCARABEE;
				((Betises2)maBetise).monInterface = this;
			}
			tour.augmenterPTACutilise(maBetise.getPoids());
			break;

		case BetisesIndex.GELEE:
			//Debug.Log("Gelée royale selectionné");
			int tmp = Random.Range(0,1);
			Dopage monDopage =  new Dopage(joueurCourant.getIaReine(),(TypeDopage)tmp);
			monDopage.lancer();
			break;

		default:
			break;    
		}

	}
	public void afficherOeufs(Texture2D oeuf,Joueur joueurCourant,TourDeJeu tour){
		Texture2D text = null;
		if (joueurCourant.nbOeufs () > 0) {
			text = oeuf;		
		}
		GUI.Label (new Rect (Screen.width * 0.81f, Screen.height * 0.72f, Screen.width * 0.25f, Screen.height * 0.10f), "Nombre d'oeufs disponibles : "+joueurCourant.nbOeufs());
		if (GUI.Button (new Rect (Screen.width * 0.83f, Screen.height * 0.62f, Screen.width * 0.1f, Screen.height * 0.10f), oeuf)) {
			if (joueurCourant.nbOeufs () > 0&&autorisationClic(tour)) {
				if(!(tour.getPTACutilise() + (int)PoidsObjetsAlancer.POIDS_OEUFS <= tour.getPTAC())){
					promptPoids = true;
				}else{
					Oeuf tmpO = joueurCourant.getOeufAt(0);
					joueurCourant.removeOeuf(tmpO);
					tour.augmenterPTACutilise((int)PoidsObjetsAlancer.POIDS_OEUFS);
					Betise monOeuf;
					lancerEnCours = true;
					if(joueurCourant.getBonCote()){
						monOeuf = bacAsable.AddComponent<Betises> ();
						((Betises)monOeuf).nom_betise = Invocations.OEUF_FOURMI;
						((Betises)monOeuf).maFourmi = tmpO.getType();
						((Betises)monOeuf).monInterface = this;
					}else{
						monOeuf = bacAsable.AddComponent<Betises2> ();
						((Betises2)monOeuf).nom_betise = Invocations.OEUF_FOURMI;
						((Betises2)monOeuf).maFourmi = tmpO.getType();
						((Betises)monOeuf).monInterface = this;
					}
				}

			}
		}
	}

	public bool afficherBoutonFinTour(TourDeJeu tour){
		if (promptFinTour) {
			afficherConfirmationFinTour();
		}
		if (GUI.Button (new Rect (Screen.width * 0.76f, Screen.height * 0.85f, Screen.width * 0.2f, Screen.height * 0.10f), "Fin Du Tour")) {
			if(autorisationClic(tour)){
				promptFinTour = true;
			}
		}
		return finTour;
	}

	public void afficherConfirmationFinTour(){
		GUI.Box ( new Rect ( Screen.width*0.28f, Screen.height*0.38f, Screen.width*0.34f,  Screen.height*0.16f), "Voulez Finir le Tour ?"); 
		if (GUI.Button (new Rect (Screen.width*.35f, Screen.height*.44f, Screen.width*.1f,Screen.height*.07f), "Oui")) {
			promptFinTour = false;
			finTour = true;	
			
		}else if(GUI.Button(new Rect (Screen.width*.47f, Screen.height*.44f, Screen.width*.1f,Screen.height*.07f), "Non")){
			promptFinTour = false;
		} 
	}

	public void afficherPoidsLimite(TourDeJeu tour){
		GUI.Box (new Rect (Screen.width * 0.28f, Screen.height * 0.38f, Screen.width * 0.34f, Screen.height * 0.16f), "Impossible de depasser le PTAC autorise du tour");
		if (GUI.Button (new Rect (Screen.width * .40f, Screen.height * .44f, Screen.width * .1f, Screen.height * .07f), "Ok")) {
				promptPoids = false;

		}
	}

	private bool autorisationClic(TourDeJeu tour){
		if (lancerEnCours) {
			return false;
		}
		if (promptFinTour) {
			return false;
		}
		if (!tour.getBetisesChoisies()) {
			return false;
		}
		if (prompt) {
			return false;
		}

		if (promptLancer) {
			return false;
		}
		if (promptOeuf) {
			return false;
		}
		if(tour.joueurIsIA()){
			return false;
		}
		return true;
	}
	public void resetPrompts(){
		prompt = false;
		promptLancer = false;
		promptFinTour = false;
		promptOeuf = false;
		betiseChoisie = null;
		finTour = false;
	}

	}

	



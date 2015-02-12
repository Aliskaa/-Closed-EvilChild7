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
	
	public void afficherBarreVieReineJoueur(Joueur joueur1, Joueur joueur2, Texture2D barre_vide, Texture2D barre_pleine, Texture2D reine_noire, Texture2D reine_blanche){
		int vie  = joueur1.getIaReine ().getModele ().getPointsDeVie ();
		int barVie = vie/100;
		GUI.Box (new Rect ((Screen.width*0)/100,(Screen.height*1)/100,(Screen.width * 17) / 100,(Screen.height*26)/100),"");
		
		GUI.Label(new Rect ((Screen.width*1)/100,(Screen.height*4)/100,(Screen.width*25)/100,(Screen.height*0.1f)),joueur1.getNameJoueur() + " :");
		GUI.Label(new Rect ((Screen.width*1)/100,(Screen.height*7)/100,(Screen.width*25)/100,(Screen.height*0.1f)), vie + "/" + "10000 PV ");
		GUI.BeginGroup (new Rect ((Screen.width*1)/100,(Screen.height*10)/100,(Screen.width * 16) / 100,(Screen.height*5)/100));
		GUI.DrawTexture (new Rect (0,0, (Screen.width * 15) / 100,(Screen.height*4)/100), barre_vide);
		GUI.BeginGroup (new Rect (0, 0, (Screen.width * 15) / 100*barVie,(Screen.height*4)/100));
		GUI.DrawTexture(new Rect (0,0, (Screen.width * 15) / 100,(Screen.height*4)/100), barre_pleine);
		GUI.EndGroup ();
		GUI.EndGroup ();
		
		vie  = joueur2.getIaReine ().getModele ().getPointsDeVie ();
		barVie = vie/100;
		
		GUI.Label(new Rect ((Screen.width*1)/100,(Screen.height*15)/100,(Screen.width*25)/100,(Screen.height*0.1f)), joueur2.getNameJoueur() + " :");
		GUI.Label(new Rect ((Screen.width*1)/100,(Screen.height*18)/100,(Screen.width*25)/100,(Screen.height*0.1f)), vie + "/" + "10000 PV ");
		GUI.BeginGroup (new Rect ((Screen.width*1)/100,(Screen.height*21)/100,(Screen.width * 16) / 100,(Screen.height*5)/100));
		GUI.DrawTexture (new Rect (0,0, (Screen.width * 15) / 100,(Screen.height*4)/100), barre_vide);
		GUI.BeginGroup (new Rect (0, 0, (Screen.width * 15) / 100*barVie,(Screen.height*4)/100));
		GUI.DrawTexture(new Rect (0,0, (Screen.width * 15) / 100,(Screen.height*4)/100), barre_pleine);
		GUI.EndGroup ();
		GUI.EndGroup ();
	}
	
	public void afficherTimerTour(int temps,int tempsfin, Texture2D progressBackground, Texture2D progressForground){
		GUI.Label(new Rect ((Screen.width*86)/100,(Screen.height*9)/100,(Screen.width*11)/100,(Screen.height*10)/100), "Temps : " + temps + "/" + tempsfin);
		
		float avancement = (float)temps / (float)tempsfin;
		float width = (Screen.width * 11) / 100;
		float height = (Screen.height*4)/100;
		GUI.BeginGroup (new Rect ((Screen.width*86)/100,(Screen.height*12)/100,width,height));
		GUI.DrawTexture (new Rect (0,0, width,height), progressBackground);
		GUI.BeginGroup (new Rect (0, 0, width*avancement,height));
		GUI.DrawTexture(new Rect (0,0, width,height), progressForground);
		GUI.EndGroup ();
		GUI.EndGroup ();
	}
	
	public void afficherChoixBetise(List<BetisesTexturees> betisesGenerees, TourDeJeu courant, Joueur joueurCourant,Texture2D fondChoix){
		GUI.BeginGroup (new Rect (Screen.width * RATIO_CHOIX_BETISE_X, Screen.height * RATIO_CHOIX_BETISE_Y, Screen.width * RATIO_CHOIX_BETISE_LARGEUR, Screen.height * RATIO_CHOIX_BETISE_HAUTEUR), "");
		GUI.Box (new Rect (0, 0, (Screen.width * 20) / 100, (Screen.height * 17) / 100), "Choix de betise ?");
		if (GUI.Button (new Rect ((Screen.width * 3) / 100, (Screen.height * 5) / 100, (Screen.width * 4) / 100, (Screen.height * 8) / 100), betisesGenerees [0].getTexture (), "box")) {
			if(!courant.joueurIsIA()){
				joueurCourant.addBetise(betisesGenerees [0]);
				courant.nextLevel();
				courant.setBetisesChoisies(true);
			}
		} else if (GUI.Button (new Rect ((Screen.width * 8) / 100, (Screen.height * 5) / 100, (Screen.width * 4) / 100, (Screen.height * 8) / 100), betisesGenerees [1].getTexture (), "box")) {
			if(!courant.joueurIsIA()){
				joueurCourant.addBetise(betisesGenerees [1]);
				courant.nextLevel();
				courant.setBetisesChoisies(true);
			}
		} else if (GUI.Button (new Rect ((Screen.width * 13) / 100, (Screen.height * 5) / 100, (Screen.width * 4) / 100, (Screen.height * 8) / 100), betisesGenerees [2].getTexture (), "box")) {
			if(!courant.joueurIsIA()){
				joueurCourant.addBetise(betisesGenerees [2]);
				courant.nextLevel();
				courant.setBetisesChoisies(true);
			}
			
		}
		GUI.EndGroup ();
	}
	
	public void afficherPtac(int poids,int PTAC, Texture2D plein, Texture2D vide){
		GUI.Label(new Rect ((Screen.width*86)/100,(Screen.height*17)/100,(Screen.width*11)/100,(Screen.height*10)/100), "Poids utilise : " + poids + "/" + PTAC);
		
		float avancement = (float)poids / (float)PTAC;
		float width = (Screen.width * 11) / 100;
		float height = (Screen.height*4)/100;
		GUI.BeginGroup (new Rect ((Screen.width*86)/100,(Screen.height*20)/100,width,height));
		GUI.DrawTexture (new Rect (0,0, width,height), plein);
		GUI.BeginGroup (new Rect (0, 0, width*avancement,height));
		GUI.DrawTexture(new Rect (0,0, width,height), vide);
		GUI.EndGroup ();
		GUI.EndGroup ();
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
		}
	}
	
	
	
	public void afficherSacAbetise(Texture2D fondSac,Texture2D fondBetise,Joueur joueurCourant,TourDeJeu tour){
		GUI.BeginGroup(new Rect(Screen.width*RATIO_SAC_BETISE_X, Screen.height*RATIO_SAC_BETISE_Y, Screen.width*RATIO_SAC_BETISE_LARGEUR, Screen.height*RATIO_SAC_BETISE_HAUTEUR), "");
		GUI.Box(new Rect(0,0,Screen.width*RATIO_SAC_BETISE_LARGEUR,Screen.height*RATIO_SAC_BETISE_HAUTEUR), "Sac a Betises");
		if (GUI.Button (new Rect ((Screen.width * 1) / 100, (Screen.height * 7) / 100, (Screen.width * 4) / 100, (Screen.height * 8) / 100), getTextureAt (joueurCourant, 0))) {
			if (joueurCourant.nbBetise () > 0 && autorisationClic (tour)) {
				if (tour.getPTACutilise () + (int)PoidsObjetsAlancerM.indexToPoids (joueurCourant.getBetiseAt (0).getBetise ()) <= tour.getPTAC ()) {
					betiseChoisie = joueurCourant.getBetiseAt (0);
					tour.faireDisparaitreBetise(betiseChoisie);
					lancerEnCours = true;
					lancerUneBetise(betiseChoisie.getBetise(),joueurCourant,tour);
					betiseChoisie = null;
				}else{
					promptPoids = true;
				}
			}
		}
		if(GUI.Button(new Rect((Screen.width*6)/100, (Screen.height*7)/100, (Screen.width*4)/100, (Screen.height*8)/100),getTextureAt(joueurCourant,1))) {
			if (joueurCourant.nbBetise () > 1&&autorisationClic(tour)) {
				if (tour.getPTACutilise () + (int)PoidsObjetsAlancerM.indexToPoids (joueurCourant.getBetiseAt (1).getBetise ()) <= tour.getPTAC ()) {
					betiseChoisie = joueurCourant.getBetiseAt (1);
					tour.faireDisparaitreBetise(betiseChoisie);
					lancerEnCours = true;
					lancerUneBetise(betiseChoisie.getBetise(),joueurCourant,tour);
					betiseChoisie = null;
				}else{
					promptPoids = true;
				}
			}
		}
		if(GUI.Button(new Rect((Screen.width*11)/100, (Screen.height*7)/100, (Screen.width*4)/100, (Screen.height*8)/100),getTextureAt(joueurCourant,2))) {
			if (joueurCourant.nbBetise () > 2&&autorisationClic(tour)) {
				if (tour.getPTACutilise () + (int)PoidsObjetsAlancerM.indexToPoids (joueurCourant.getBetiseAt (2).getBetise ()) <= tour.getPTAC ()) {
					betiseChoisie = joueurCourant.getBetiseAt (2);
					tour.faireDisparaitreBetise(betiseChoisie);
					lancerEnCours = true;
					lancerUneBetise(betiseChoisie.getBetise(),joueurCourant,tour);
					betiseChoisie = null;
					
				}else{
					promptPoids = true;
				}
			}
		}
		if(GUI.Button(new Rect((Screen.width*1)/100, (Screen.height*17)/100, (Screen.width*4)/100, (Screen.height*8)/100),getTextureAt(joueurCourant,3)))  {
			if (joueurCourant.nbBetise () > 3&&autorisationClic(tour)){
				if (tour.getPTACutilise () + (int)PoidsObjetsAlancerM.indexToPoids (joueurCourant.getBetiseAt (3).getBetise ()) <= tour.getPTAC ()) {
					betiseChoisie = joueurCourant.getBetiseAt (3);
					tour.faireDisparaitreBetise(betiseChoisie);
					lancerEnCours = true;
					lancerUneBetise(betiseChoisie.getBetise(),joueurCourant,tour);
					betiseChoisie = null;
				}else{
					promptPoids =true;
				}
			}
		}
		
		if(GUI.Button(new Rect((Screen.width*6)/100, (Screen.height*17)/100, (Screen.width*4)/100, (Screen.height*8)/100),getTextureAt(joueurCourant,4)))  {
			if (joueurCourant.nbBetise () > 4&&autorisationClic(tour)){
				if (tour.getPTACutilise () + (int)PoidsObjetsAlancerM.indexToPoids (joueurCourant.getBetiseAt (4).getBetise ()) <= tour.getPTAC ()) {
					betiseChoisie = joueurCourant.getBetiseAt (4);
					tour.faireDisparaitreBetise(betiseChoisie);
					lancerEnCours = true;
					lancerUneBetise(betiseChoisie.getBetise(),joueurCourant,tour);
					betiseChoisie = null;
					
				}else{
					promptPoids =true;
				}
			}
		}
		
		if (GUI.Button (new Rect ((Screen.width * 11) / 100, (Screen.height * 17) / 100, (Screen.width * 4) / 100, (Screen.height * 8) / 100), getTextureAt (joueurCourant, 5))) {
			if (joueurCourant.nbBetise () > 5 && autorisationClic (tour)) {
				if (tour.getPTACutilise () + (int)PoidsObjetsAlancerM.indexToPoids (joueurCourant.getBetiseAt (5).getBetise ()) <= tour.getPTAC ()) {
					betiseChoisie = joueurCourant.getBetiseAt (5);
					tour.faireDisparaitreBetise(betiseChoisie);
					lancerEnCours = true;
					lancerUneBetise(betiseChoisie.getBetise(),joueurCourant,tour);
					betiseChoisie = null;
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
		}
	}
	
	protected void lancerUneBetise(BetisesIndex index,Joueur joueurCourant,TourDeJeu tour){
		Betise maBetise;
		switch (index) {
			
		case BetisesIndex.CAILLOU:
			//Debug.Log("caillou selectionnÃ©");
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
			//Debug.Log("pistolet selectionnÃ©");
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
			//Debug.Log("bonbons selectionnÃ©");
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
			//Debug.Log("deux fois selectionnÃ©");
			LancerDeuxFois tmpDeuxFois = new LancerDeuxFois();
			tmpDeuxFois.lancerCeTour(tour);
			break;
			
		case BetisesIndex.LANCER_GROS:
			//Debug.Log("force selectionnÃ©");
			LancerGros tmpGros = new LancerGros();
			tmpGros.lancerCeTour(tour);
			break;
			
		case BetisesIndex.VENTILATEUR:
			//Debug.Log("ventilateur selectionnÃ©");
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
			//Debug.Log("GelÃ©e royale selectionnÃ©");
			int tmp = Random.Range(0,1);
			Dopage monDopage =  new Dopage(joueurCourant.getIaReine(),(TypeDopage)tmp);
			monDopage.lancer();
			break;
			
		default:
			break;    
		}

		lancerEnCours = false;
		
	}
	public void afficherOeufs(Texture2D oeuf,Joueur joueurCourant,TourDeJeu tour){
		Texture2D text = null;
		if (joueurCourant.nbOeufs () > 0) {
			text = oeuf;		
		}
		GUI.Label (new Rect (Screen.width * 0.81f, Screen.height * 0.72f, Screen.width * 0.25f, Screen.height * 0.10f), "Oeufs restants : "+joueurCourant.nbOeufs());
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
		}
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

	public void gestionQuitter(Texture2D image_quitter){
		if (GUI.Button(new Rect(Screen.width*0.96f, Screen.height*0.03f, Screen.width*0.02f, Screen.height*0.05f), image_quitter)){
			TerrainUtils.Flush();
			Application.LoadLevel(0);
		}
	}
	
	public void GestionSon(Texture2D textureBoutonMusique,LogiqueDeJeu ldj){
		if (GUI.Button(new Rect(Screen.width*0.93f, Screen.height*0.03f, Screen.width*0.02f, Screen.height*0.05f), textureBoutonMusique)){
			ldj.lancerMusique();
		}
	}

}





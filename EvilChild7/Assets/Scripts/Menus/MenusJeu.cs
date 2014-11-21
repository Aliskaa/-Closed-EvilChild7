using UnityEngine;
using System.Collections;

public class MenusJeu : MonoBehaviour {
	
	//Variable Skin
	public GUISkin skin_test;
	public Rect windowRect = new Rect(0, 0, 200, 400);
	
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
	
	//Variable ramdom
	[HideInInspector]
	public int val1;
	[HideInInspector]
	public int val2;
	[HideInInspector]
	public int val3;
	
	//Varible sac à bétise
	[HideInInspector]
	public int[] listBetiseJoueur = {0,0,0,0,0,0,0};
	[HideInInspector]
	public int[] listBetiseIA = {0,0,0,0,0,0,0};
	
	//variable temps des joueurs
	public float tempsJoueur;
	public float tempsJoueurfin;
	
	public float tempsIA;
	public float TempsIAfin;
	
	public float tempsDebutPartie;
	
	//variable vie reine
	public int vieReineJoueur;
	public int vieReineIA;
	
	//variable PTAC
	public int joueurPTAC;
	public int joueurPTACfin;
	
	public int IAPTAC;
	public int IAPTACfin;
	
	//Flag des menus
	bool flagBarDeVieReineJoueur;
	bool flagChoixDeBetise;
	bool flagPTAC;
	bool flagSacABetise;
	bool flagGestionTour;
	bool flagtemps;
	bool flagDirectionVent;
	
	//Flag de jeu affichage
	bool flagMessageGagne;
	bool flagMessagePerdue;
	bool flagMessageDebutPartie;
	bool flagMessageDebutTour;
	
	//Flag boutons
	bool flagTourJoueur;
	bool flagTourIA;
	bool flagfaitChoixDeBetiseJoueur;
	
	//Flag autorisation de jouer
	bool flagAutorisationLancer;
	bool flagAutorisationFinDeTour;
	bool flagAutorisationOeufLancer;

	GameObject bacASable;
	Betises betise;

	private void Start()
	{

		bacASable = GameObject.FindGameObjectWithTag ("BAC_A_SABLE");

		//Initialisation variable temps
		tempsJoueur = 0;
		tempsJoueurfin = 45;
		
		tempsIA = 0;
		TempsIAfin = 45;
		
		//Initialisation PTAC
		joueurPTAC = 0;
		joueurPTACfin = 100;
		
		//temps debut partie
		tempsDebutPartie = 0;
		
		//Initialisation VieReine
		vieReineJoueur = 100;
		vieReineIA = 100;
		
		//Flag initialisation menu
		flagBarDeVieReineJoueur = false;
		flagChoixDeBetise = false;
		flagPTAC = false;
		flagSacABetise = false;
		flagGestionTour = false;
		flagtemps = false;
		
		//Flag initialisation affichage;
		flagMessageGagne = false;
		flagMessagePerdue = false;
		flagMessageDebutPartie = true;
		flagMessageDebutTour = false;
		
		//Flag initialisation bouton
		flagTourJoueur = true;
		flagTourIA = false;
		flagfaitChoixDeBetiseJoueur = false;
		
		//Flag autorisation de jeu
		flagAutorisationLancer = false;
		flagAutorisationFinDeTour = false;
		flagAutorisationOeufLancer = false;
		
		val1 = Random.Range (1, 10);
		val2 = Random.Range (1, 10);
		val3 = Random.Range (1, 10);
		
		smallFont = new GUIStyle();
		largeFont = new GUIStyle();
		
		smallFont.fontSize = 10;
		largeFont.fontSize = 32;
	}
	
	
	// Update is called once per frame
	void Update () {
		
		if(vieReineIA != 0 || vieReineJoueur != 0){
			
			if(tempsDebutPartie < 2){
				//On decompte le temps d affichage debut de jeu
				tempsDebutPartie = tempsDebutPartie + 1 * Time.deltaTime;
			}
			else{
				//On finie d'afficher debut jeu
				flagMessageDebutPartie = false;
				
				flagBarDeVieReineJoueur = true;
				flagPTAC = true;
				flagSacABetise = true;
				flagGestionTour = true;
				flagtemps = true;
				
				if(tempsJoueur < tempsJoueurfin && flagTourJoueur){
					
					tempsJoueur = tempsJoueur + 1 * Time.deltaTime;
					
					if(flagfaitChoixDeBetiseJoueur){
						flagChoixDeBetise = false;
						
						flagAutorisationFinDeTour = true;
						
						if(joueurPTAC < joueurPTACfin){
							flagAutorisationLancer = true;
							flagAutorisationOeufLancer = true;
						}
						else{
							flagAutorisationLancer = false;
							flagAutorisationOeufLancer = false;
						}
						
					}
					else{
						flagChoixDeBetise = true;
					}
				}
				else{
					flagTourIA = true;
					Debug.Log("temps ecoule joueur");
				}
				
				
				//Tour IA
				if(flagTourIA){
					tempsJoueur = 0;
					joueurPTAC = 0;
					flagAutorisationLancer = false;
					flagAutorisationFinDeTour = false;
					flagAutorisationOeufLancer = false;
					
					if (Input.GetKey("up")){
						flagTourIA = false;
						flagTourJoueur = true;
						flagfaitChoixDeBetiseJoueur = false;
						Debug.Log("up");
					}
					
					//Choisie betise
					//Suivant IAPTAC
					//mettre oeuf
					//mettre betise
					//Fin de tour
					
				}
				else{
					flagTourJoueur = true;
					Debug.Log("temps ecoule IA");
				}
			}
		}
		else{
			if (vieReineIA ==  0) {
				//Flag message perdue
				flagMessageGagne = true;
				
			} else {
				//Flag message gagne
				flagMessageGagne = true;
			}
		}
		
		
	}
	
	void OnGUI() 
	{
		
		GUI.skin = skin_test;
		//windowRect = GUI.Window (0, windowRect, DoMyWindow, "Menu de jeu");
		
		
		//Afiichage des menus
		if (flagBarDeVieReineJoueur) {
			//variable vie reine
			//this.BarVie(getJoueurCourant().getIaReine().getModele().getPointsDeVie());
			this.BarVie(55);
		}
		
		if (flagChoixDeBetise) {
			this.ChoixDeBetise();
		}
		
		if (flagPTAC) {
			this.PTACLabel(joueurPTAC,joueurPTACfin);
		}
		
		if(flagSacABetise){
			this.SacABetise(flagAutorisationLancer);
		}
		
		if (flagGestionTour) {
			this.GestionTour(flagAutorisationFinDeTour,flagAutorisationOeufLancer);
		}
		
		if (flagtemps) {
			this.tempsjeu(tempsJoueur,tempsJoueurfin);
		}
		
		if(flagDirectionVent){
			this.ChoixDirectionVent();
		}
		
		//Affichage des messages
		if (flagMessageGagne) {
			this.afficherVictoire();
		}
		
		if (flagMessagePerdue) {
			this.afficherDefait();
		}
		
		if (flagMessageDebutPartie) {
			this.afficherDebut();
		}
		
		if (flagMessageDebutTour) {
			this.afficherTonTurn();
		}
		
	}
	
	//Affichage des messages
	private void afficherDebut(){
		largeFont.normal.textColor = Color.blue;
		GUI.Label (new Rect (600, 200, 200, 60), "beginning part !",largeFont);
	}
	
	private void afficherTonTurn(){
		largeFont.normal.textColor = Color.blue;
		GUI.Label (new Rect (600, 200, 200, 60), "Your Turn!",largeFont);
	}
	
	private void afficherVictoire(){
		largeFont.normal.textColor = Color.green;
		GUI.Label (new Rect (600, 200, 200, 60), "Victory!",largeFont);
	}
	
	private void afficherDefait(){
		largeFont.normal.textColor = Color.red;
		GUI.Label (new Rect (600, 200, 200, 60), "Defeat!",largeFont);
	}
	
	
	//Affichage des menus 
	private void BarVie(int vie){
		GUI.HorizontalScrollbar(new Rect (20,20,200,20), 0, vie,0, 100);
		GUI.Label(new Rect (20,40,200,20), vie + "/" + "100");
	}
	
	private void PTACLabel(int poid,int PTAC){
		GUI.Label(new Rect (1180,100,150,60), "Your PTAC");
		GUI.Label(new Rect (1180,120,150,60), poid + "/" + PTAC);
	}
	
	private void tempsjeu(float temps,float tempsfin){
		GUI.Label(new Rect (1180,60,150,60), "Temps");
		GUI.Label(new Rect (1180,80,150,60), (int) temps + "/" + tempsfin);
	}
	
	public void ChoixDeBetise(){
		GUI.BeginGroup(new Rect(500, 250, 240, 100));
		GUI.Box(new Rect(0, 0, 240, 100), "Choice of foolishness ?");
		if (GUI.Button (new Rect (40, 40, 50, 50), betiseTexture (val1))) {
			EnvoieBetiseVersSac(listBetiseJoueur,val1);
			val1 = Random.Range (1, 10);
			val2 = Random.Range (1, 10);
			val3 = Random.Range (1, 10);
			flagfaitChoixDeBetiseJoueur = true;
		}
		else if(GUI.Button (new Rect (100, 40, 50, 50), betiseTexture (val2))){
			EnvoieBetiseVersSac(listBetiseJoueur,val2);
			val1 = Random.Range (1, 10);
			val2 = Random.Range (1, 10);
			val3 = Random.Range (1, 10);
			flagfaitChoixDeBetiseJoueur = true;
		}
		else if (GUI.Button (new Rect (160, 40, 50, 50), betiseTexture (val3))){
			EnvoieBetiseVersSac(listBetiseJoueur,val3);
			val1 = Random.Range (1, 10);
			val2 = Random.Range (1, 10);
			val3 = Random.Range (1, 10);
			flagfaitChoixDeBetiseJoueur = true;
		}
		GUI.EndGroup();
	}
	
	//Texture choix 
	private Texture2D betiseTexture(int val){
		switch (val) {
			
		case 1:
			return image_caillou;
		case 2:
			return image_pistolet;
		case 3:
			return image_bonbons;
		case 4:
			return image_bout_de_bois;
		case 5:
			return image_deux_fois;
		case 6:
			return image_force;
		case 7:
			return image_ventilateur;
		case 8:
			return image_bombe_eau;
		case 9:
			return image_scarabee;
		case 10:
			return image_gelee;
		default:
			return null;
		}
	}
	
	
	//afficher les directions de vents
	private void ChoixDirectionVent(){
		GUI.BeginGroup(new Rect(400, 200, 400, 100));
		GUI.Box(new Rect(0,0,400,100), "management vent");
		if(GUI.Button(new Rect(30, 30, 50, 50),vent_gauche_fort)) {
			flagDirectionVent = false;
		}
		else if(GUI.Button(new Rect(90, 30, 50, 50),vent_gauche_faible)){
			flagDirectionVent = false;
		}
		else if(GUI.Button(new Rect(150, 30, 50, 50),vent_haut)){
			flagDirectionVent = false;
		}
		else if(GUI.Button(new Rect(210, 30, 50, 50),vent_bas)){
			flagDirectionVent = false;
		}
		else if(GUI.Button(new Rect(270, 30, 50, 50),vent_droit_faible)){
			flagDirectionVent = false;
		}
		else if(GUI.Button(new Rect(330, 30, 50, 50),vent_droit_fort)){
			flagDirectionVent = false;
		}
		GUI.EndGroup();
	}
	
	//afficher gestion tour
	private void GestionTour(bool autorisationfin, bool autorisationOeuf){
		GUI.BeginGroup(new Rect(1150, 400, 150, 150));
		GUI.Box(new Rect(0,0,150,150), "management round");
		if(GUI.Button(new Rect(30, 30, 50, 50),image_oeuf)) {
			if(autorisationOeuf){
				joueurPTAC = joueurPTAC + 5;
				Debug.Log("Oeuf");
			}
		}
		else if(GUI.Button(new Rect(30, 100, 80, 20),"end turn")){
			if(autorisationfin){
				flagTourJoueur = false;
				Debug.Log("Fin de tour");
			}
		}
		
		GUI.EndGroup();
	}
	
	//Envoie betise vers sac
	private void EnvoieBetiseVersSac(int[] list,int val){
		bool plein = true;
		bool pasfait = true;
		for (int i = 1; i < list.Length; i++){ 
			if(list[i] == 0 && pasfait){
				list[i] = val;
				plein = false;
				pasfait = false;
			}
		}
		
		if (plein) {
			list[1] = val;
		}
	}
	
	//Supprimer betise dans sac
	private void SupprimeBetiseSac(int[] list, int val){
		list [val] = 0;
	}
	
	//afficher sac à betise
	void SacABetise(bool autorisation){
		GUI.BeginGroup(new Rect(1150, 200, 200, 190));
		GUI.Box(new Rect(0,0,200,190), "Choisir les betises");
		
		
		if(GUI.Button(new Rect(10, 40, 50, 50),betiseTexture(listBetiseJoueur[1]))) {
			if(autorisation){
				joueurPTAC = joueurPTAC + 10;
				BetiseUtilise(listBetiseJoueur[1]);
				Debug.Log("Betise 1");
				//Debug.Log(listBetiseJoueur[1]);
				SupprimeBetiseSac(listBetiseJoueur,1);
				betise = bacASable.AddComponent<Betises>();
				betise.nom_betise = Invocations.CAILLOU;
			}
		}
		if(GUI.Button(new Rect(70, 40, 50, 50),betiseTexture(listBetiseJoueur[2]))) {
			if(autorisation){
				joueurPTAC = joueurPTAC + 10;
				BetiseUtilise(listBetiseJoueur[2]);
				Debug.Log("Betise 2");
				//Debug.Log(listBetiseJoueur[2]);
				SupprimeBetiseSac(listBetiseJoueur,2);
				betise = bacASable.AddComponent<Betises>();
				betise.nom_betise = Invocations.MISSILE_EAU;
			}
		}
		if(GUI.Button(new Rect(130, 40, 50, 50),betiseTexture(listBetiseJoueur[3]))) {
			if(autorisation){
				joueurPTAC = joueurPTAC + 10;
				BetiseUtilise(listBetiseJoueur[3]);
				Debug.Log("Betise 3");
				//Debug.Log(listBetiseJoueur[3]);
				SupprimeBetiseSac(listBetiseJoueur,3);
				betise = bacASable.AddComponent<Betises>();
				NourritureBonbons n = new NourritureBonbons ();
				Invocations nom_bonbon = n.ListeNourriture();
				betise.nom_betise = nom_bonbon;	
			}
		}
		if(GUI.Button(new Rect(10, 100, 50, 50),betiseTexture(listBetiseJoueur[4]))) {
			if(autorisation){
				joueurPTAC = joueurPTAC + 10;
				BetiseUtilise(listBetiseJoueur[4]);
				Debug.Log("Betise 4");
				//Debug.Log(listBetiseJoueur[4]);
				SupprimeBetiseSac(listBetiseJoueur,4);

				betise = bacASable.AddComponent<Betises>();
				betise.nom_betise = Invocations.BOUT_DE_BOIS;
			}
		}
		
		if(GUI.Button(new Rect(70, 100, 50, 50),betiseTexture(listBetiseJoueur[5]))) {
			if(autorisation){
				joueurPTAC = joueurPTAC + 10;
				BetiseUtilise(listBetiseJoueur[5]);
				Debug.Log("Betise 5");
				//Debug.Log(listBetiseJoueur[5]);
				SupprimeBetiseSac(listBetiseJoueur,5);
				betise = bacASable.AddComponent<Betises>();
				betise.nom_betise = Invocations.SCARABEE;
			}
		}
		
		if(GUI.Button(new Rect(130, 100, 50, 50),betiseTexture(listBetiseJoueur[6]))) {
			if(autorisation){
				joueurPTAC = joueurPTAC + 10;
				BetiseUtilise(listBetiseJoueur[6]);
				Debug.Log("Betise 6");
				//Debug.Log(listBetiseJoueur[6]);
				SupprimeBetiseSac(listBetiseJoueur,6);
				betise = bacASable.AddComponent<Betises>();
				betise.nom_betise = Invocations.BOMBE_EAU;
			}
		}
		GUI.EndGroup();
		
	}
	
	//betise à utiliser
	void BetiseUtilise(int val){
		switch (val) {
			
		case 1:
			Debug.Log("caillou selectionné");
			break;
		case 2:
			Debug.Log("pistolet selectionné");
			break;
		case 3:
			Debug.Log("bonbons selectionné");
			break;
		case 4:
			Debug.Log("bout de bois selectionné");
			break;
		case 5:
			Debug.Log("deux fois selectionné");
			break;
		case 6:
			Debug.Log("force selectionné");
			break;
		case 7:
			flagDirectionVent = true;
			Debug.Log("ventilateur selectionné");
			break;
		case 8:
			Debug.Log("bombe à eau selectionné");
			break;
		case 9:
			Debug.Log("Scarabee selectionné");
			break;
		case 10:
			Debug.Log("Gelée royale selectionné");
			break;
		default:
			break;	
		}
	}
	
	
	//Methode Bouton Son et quitter
	void GestionSon(){
		if (GUI.Button(new Rect(1250, 20, 30, 30), textureBoutonMusique)){
			lancerMusique();
		}
	}
	
	
	void GestionQuitter(){
		if (GUI.Button(new Rect(1280, 20, 30, 30), image_quitter)){
			Application.Quit();
		}
	}
	
	private bool sound = false;
	void lancerMusique(){
		
		// Rapprocher la boite à musique de la caméra
		// pour bien l'entendre
		//GameObject camera = GameObject.Find("Main Camera");
		//gameObject.transform.localPosition = camera.transform.position;
		//gameObject.transform.Translate(100, 0, 100);
		
		if(sound == true){
			//this.guiTexture.texture = mutted;
			textureBoutonMusique = mutted;
			audio.Stop();
			//Debug.Log("Son stoppé !");
			sound = false;
		}
		else{
			//this.guiTexture.texture = onAir;
			audio.Play();
			textureBoutonMusique = onAir;
			//Debug.Log("Son lancé !");
			sound = true;
			
		}
	} 
	
	
}


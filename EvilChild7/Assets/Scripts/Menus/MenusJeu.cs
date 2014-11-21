using UnityEngine;
using System.Collections;

public class MenusJeu : MonoBehaviour {
	
	
	public GUISkin skin_test;
	public Rect windowRect = new Rect(0, 0, 200, 400);
	public Texture2D image_bonbons;
	public Texture2D image_pistolet;
	public Texture2D image_scarabee;
	public Texture2D image_caillou;
	public Texture2D image_bout_de_bois;
	public Texture2D image_ventilateur;
	//	public Texture2D image_pheromone_contremaitre;
	//	public Texture2D image_pheromone_ouvriere;
	public Texture2D image_oeuf;
	public int val1;
	public int val2;
	public int val3;
	public int[] listBetise = {0,0,0,0,0,0,0};
	public bool flagChoixBetise = true;
	public float seconde;
	
	public GUIStyle smallFont;
	public GUIStyle largeFont;
	public Texture2D image_quitter;
	public Texture2D onAir;
	public Texture2D mutted;
	public Texture2D textureBoutonMusique;

	public bool DragWindow2 = false;
	
	private void Start()
	{
		val1 = Random.Range (1, 6);
		val2 = Random.Range (1, 6);
		val3 = Random.Range (1, 6);
		seconde = 0;
		smallFont = new GUIStyle();
		largeFont = new GUIStyle();
		
		smallFont.fontSize = 10;
		largeFont.fontSize = 32;
	}
	
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	#region interface
	/// <summary>
	/// Fonction temporaire de boutons cliquables
	/// </summary>
	void OnGUI() 
	{
		
		GUI.skin = skin_test;
		//windowRect = GUI.Window (0, windowRect, DoMyWindow, "Menu de jeu");
		if (seconde < 5) {
			this.afficherDebut();
		}
		else{
			if (flagChoixBetise){
				this.ChoixDeBetise ();
			}
			this.BarVie (90);
			this.GestionTour ();
			this.gestionFinDeTour ();
			this.SacABetise ();
			this.PTACLabel(60,200);
			this.GestionSon();
			this.GestionQuitter();
			//Debug.Log(val1);
		}	
		
		
		
	}

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

	public void ChoixDeBetise(){
		GUI.BeginGroup(new Rect(500, 250, 240, 100));
		GUI.Box(new Rect(0, 0, 240, 100), "Choice of foolishness ?");
		if (GUI.Button (new Rect (40, 40, 50, 50), betiseTexture (val1))) {
			EnvoieBetiseVersSac(val1);
			flagChoixBetise = false;
		}
		else if(GUI.Button (new Rect (100, 40, 50, 50), betiseTexture (val2))){
			EnvoieBetiseVersSac(val2);
			flagChoixBetise = false;
		}
		else if (GUI.Button (new Rect (160, 40, 50, 50), betiseTexture (val3))){
			EnvoieBetiseVersSac(val3);
			flagChoixBetise = false;
		}
		GUI.EndGroup();
	}
	
	public void BarVie(int vie){
		GUI.HorizontalScrollbar(new Rect (20,20,200,20), 0, vie,0, 100);
		GUI.Label(new Rect (20,40,200,20), vie + "/" + "100");
	}
	
	public Texture2D betiseTexture(int val){
		switch (val) {
			
		case 1:
			return image_bonbons;
		case 2:
			return image_pistolet;
		case 3:
			return image_scarabee;
		case 4:
			return image_caillou;
		case 5:
			return image_bout_de_bois;
		case 6:
			return image_ventilateur;
			/*case 7:
			return image_pheromone_contremaitre;
		case 8:
			return image_pheromone_ouvriere;*/
		default:
			return null;
		}
	}
	
	void GestionTour(){
		GUI.BeginGroup(new Rect(1150, 400, 150, 100));
		GUI.Box(new Rect(0,0,150,150), "management round");
		if(GUI.Button(new Rect(30, 30, 50, 50),image_oeuf)) {
			
			Debug.Log("bonbon selectionné");
		}
		GUI.EndGroup();
		
		
		
	}
	void gestionFinDeTour(){
		//GUI.BeginGroup(new Rect(1150, 400, 150, 100));
		//GUI.Box(new Rect(0,0,150,150), "fin tour");
		if(GUI.Button(new Rect(1150, 530, 70, 30),"end turn")) {
			
			//if(GUI.Button(new Rect(30, 100, 80, 20),"end turn")){
			Debug.Log("Fin de tour");
		}
		//GUI.EndGroup();


	} 
	
	void EnvoieBetiseVersSac(int val){
		bool plein = true;
		bool pasfait = true;
		for (int i = 1; i < listBetise.Length; i++){ 
			if(listBetise[i] == 0 && pasfait){
				listBetise[i] = val;
				plein = false;
				pasfait = false;
			}
		}
		
		if (plein) {
			listBetise[1] = val;
		}
	}
	
	void SupprimeBetiseSac(int val){
		listBetise [val] = 0;
	}
	
	
	
	void SacABetise(){
		GUI.BeginGroup(new Rect(1150, 200, 200, 190));
		GUI.Box(new Rect(0,0,200,190), "Choisir les betises");
		
		
		if(GUI.Button(new Rect(10, 40, 50, 50),betiseTexture(listBetise[1]))) {
			BetiseUtilise(listBetise[1]);
			Debug.Log("Betise 1");
			SupprimeBetiseSac(1);
		}
		if(GUI.Button(new Rect(70, 40, 50, 50),betiseTexture(listBetise[2]))) {
			BetiseUtilise(listBetise[2]);
			Debug.Log("Betise 2");
			SupprimeBetiseSac(1);
		}
		if(GUI.Button(new Rect(130, 40, 50, 50),betiseTexture(listBetise[3]))) {
			BetiseUtilise(listBetise[3]);
			Debug.Log("Betise 3");
			SupprimeBetiseSac(1);
		}
		if(GUI.Button(new Rect(10, 100, 50, 50),betiseTexture(listBetise[4]))) {
			BetiseUtilise(listBetise[4]);
			Debug.Log("Betise 4");
			SupprimeBetiseSac(1);
		}
		
		if(GUI.Button(new Rect(70, 100, 50, 50),betiseTexture(listBetise[5]))) {
			BetiseUtilise(listBetise[5]);
			Debug.Log("Betise 5");
			SupprimeBetiseSac(1);
		}
		
		if(GUI.Button(new Rect(130, 100, 50, 50),betiseTexture(listBetise[6]))) {
			BetiseUtilise(listBetise[6]);
			Debug.Log("Betise 6");
			SupprimeBetiseSac(1);
		}
		GUI.EndGroup();
		
	}
	
	void PTACLabel(int poid,int PTAC){
		GUI.Label(new Rect (1180,100,150,60), "Your PTAC");
		GUI.Label(new Rect (1180,120,150,60), poid + "/" + PTAC);
	}
	
	
	
	
	void BetiseUtilise(int val){
		switch (val) {
		case 1:
			Debug.Log("bonbon selectionné");
			break;
		case 2:
			Debug.Log("pistolet selectionné");
			break;
		case 3:
			Debug.Log("scarabee selectionné");
			break;
		case 4:
			Debug.Log("caillou selectionné");
			break;
		case 5:
			Debug.Log("bout_de_bois selectionné");
			break;
		case 6:
			Debug.Log("ventilateur selectionné");
			break;
		case 7:
			Debug.Log("Gelée royale");
			break;
		case 8:
			Debug.Log("Gelée royale");
			break;
		case 9:
			Debug.Log("Gelée royale");
			break;
		default:
			break;	
		}
	}
	
	
	void afficherDebut(){
		seconde = seconde + 1 * Time.deltaTime;
		largeFont.normal.textColor = Color.blue;
		GUI.Label (new Rect (600, 200, 200, 60), "beginning part !",largeFont);
	}
	
	void afficherTonTurn(){
		largeFont.normal.textColor = Color.blue;
		GUI.Label (new Rect (600, 200, 200, 60), "Your Turn!",largeFont);
	}
	
	void afficherVictoire(){
		largeFont.normal.textColor = Color.green;
		GUI.Label (new Rect (600, 200, 200, 60), "Victory!",largeFont);
	}
	
	void afficherDefait(){
		largeFont.normal.textColor = Color.red;
		GUI.Label (new Rect (600, 200, 200, 60), "Defeat!",largeFont);
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
#endregion
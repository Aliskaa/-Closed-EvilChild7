using UnityEngine;
using System.Collections;

public class AccueilScript : MonoBehaviour {
	
	public GUISkin guiSkin;
	public Texture2D background;
	public bool DragWindow = false;
	public string levelToLoadWhenClickedPlay = "";
	private string clicked = "", MessageDisplayOnHelp = "Aide \n ", MessageDisplayOnGame= "Description du jeu\n";
	private Rect WindowRect = new Rect((Screen.width / 4), Screen.height / 2, 600, 600);
	private float volume = 1.0f;
	
	//Variable serveur
	private HostData[] hostList = null;
	private const string typeName = "EvilChild";
	private NetworkManager networkmanager;
	private string NameServer = "Nom du serveur";
	
	private void Start()
	{
		MessageDisplayOnHelp += "Echap. pour revenir";
		//MessageDisplayOnGame +=  "Echap. pour revenir";
		MessageDisplayOnGame += 
			"\n\n\n\tDeux enfants se confrontent en faisant s'affronter deux colonies\n\n de fourmis dans un bac à sable.\n\n" +
				"Chaque enfant démarre avec une reine fourmi qui pond des oeufs.\n\n" +
				"\tCes oeufs donnent naissance à différentes sortes de fourmis\n qui ont une mission.\n\n" +
				"\t\tLes fourmis pourront alors s'affronter, déambuler\n sur le terrain, rapporter de la nourriture\n et surtout, tuer l'autre reine !\n";
		networkmanager = new NetworkManager(typeName,NameServer);
	}
	private void OnGUI()
	{
		//Debug.Log(Screen.height);
		GUI.skin = guiSkin;
		if (background != null)
			GUI.DrawTexture(new Rect(0,0,Screen.width , Screen.height),background);
		
		
		switch (clicked) {
			
		case "Play":
			WindowRect = GUI.Window(1, WindowRect, optionsPlay, "");
			break;
			
		case "options":
			WindowRect = GUI.Window(1, WindowRect, optionsFunc, "");
			break;
			
		case "help":
			GUI.Box(new Rect (0,0,Screen.width,Screen.height), MessageDisplayOnHelp);
			break;
		case "descriptions":
			GUI.Box ( new Rect ( 0, 0, Screen.width,  Screen.height), MessageDisplayOnGame); 
			
			break;
			
		case "resolution":
			GUILayout.BeginVertical();
			for (int x = 0; x < Screen.resolutions.Length;x++ )
			{
				if (GUILayout.Button(Screen.resolutions[x].width + "X" + Screen.resolutions[x].height))
				{
					Screen.SetResolution(Screen.resolutions[x].width,Screen.resolutions[x].height,true);
				}
			}
			GUILayout.EndVertical();
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Retour"))
			{
				clicked = "options";
			}
			GUILayout.EndHorizontal();
			break;
		case "Multiplayer":
			//this.optionsMultiplayer();
			break;
		case "StartServer":
			//this.optionStartServer();
			break;
			
		default:
			
			WindowRect = GUI.Window(0, WindowRect, menuFunc, "Evil Child");
			break;
		}
	}
	
	private void optionsPlay(int id)
	{
		if (GUILayout.Button ("Joueur contre IA")) {
			Application.LoadLevel(1);
		} 
		/*
        else if (GUILayout.Button ("Multiplayer")) {
            clicked = "Multiplayer";
        } 
        */
		else if (GUILayout.Button ("Bac à sable")) {
			Application.LoadLevel(3);
		}
		else if (GUILayout.Button("Retour")){
			clicked = "";
		}
		
		if (DragWindow)
			GUI.DragWindow(new Rect (0,0,Screen.width,Screen.height));
		
	}
	
	//Option Start Server
	private void optionStartServer(){
		//Box 1
		GUI.BeginGroup(new Rect((Screen.width*66)/100, (Screen.height*42)/100, (Screen.width*26)/100, (Screen.height*50)/100));
		GUI.Box(new Rect(0, 0, (Screen.width*26)/100, (Screen.height*50)/100), "Lancer le serveur !");
		// Nom du serveur
		GUI.BeginGroup(new Rect((Screen.width*7)/100, (Screen.height*7)/100, (Screen.width*22)/100, (Screen.height*7)/100));
		GUILayout.BeginHorizontal();
		GUILayout.Label("Nom du serveur:", GUILayout.Width(100));
		NameServer = GUILayout.TextField(NameServer);
		GUILayout.EndHorizontal();
		GUI.EndGroup();
		
		if (GUI.Button(new Rect( (Screen.width*2)/100, (Screen.height*13)/100, (Screen.width*22)/100, (Screen.height*7)/100), "Lancer" ) )
		{
			networkmanager.setGameName(NameServer);
			networkmanager.StartServer();
			//Debug.Log("launch");
			Application.LoadLevel(2);
		}
		if (GUI.Button(new Rect( (Screen.width*2)/100, (Screen.height*20)/100, (Screen.width*22)/100, (Screen.height*7)/100), "Vider" ))
		{
			NameServer = "Name server";
		}
		
		if (GUI.Button(new Rect( (Screen.width*2)/100, (Screen.height*42)/100, (Screen.width*22)/100, (Screen.height*7)/100),"Retour")){
			clicked = "Multiplayer";
		}
		
		GUI.EndGroup();
	}
	
	
	//Option Multiplayer
	private void optionsMultiplayer()
	{
		
		
		if (!Network.isClient && !Network.isServer)
		{
			//Box 1
			GUI.BeginGroup(new Rect((Screen.width*37)/100, (Screen.height*42)/100, (Screen.width*26)/100, (Screen.height*50)/100));
			GUI.Box(new Rect(0, 0, (Screen.width*26)/100, (Screen.height*50)/100), "Choix du serveur ?");
			networkmanager.RefreshHostList();
			hostList = networkmanager.getHostList();
			
			if (hostList != null){
				for (int i = 0; i < hostList.Length; i++)
				{
					if(GUI.Button(new Rect((Screen.width*3)/100, (Screen.height*7)/100 + ((Screen.height*7)/100 * i), (Screen.width*22)/100, (Screen.height*7)/100), hostList[i].gameName)){
						networkmanager.JoinServer(hostList[i]);
					}
				}
			}
			else{
				GUI.Button(new Rect( (Screen.width*3)/100, (Screen.height*7)/100, (Screen.width*22)/100, (Screen.height*7)/100), "Pas de serveur disponible");
			}
			
			GUI.EndGroup();
			
			//Box 2
			GUI.BeginGroup(new Rect((Screen.width*66)/100, (Screen.height*42)/100, (Screen.width*26)/100, (Screen.height*50)/100));
			GUI.Box(new Rect(0, 0, (Screen.width*26)/100, (Screen.height*50)/100),"Gestion du serveur");
			
			if (GUI.Button(new Rect( (Screen.width*3)/100, (Screen.height*7)/100, (Screen.width*22)/100, (Screen.height*7)/100), "Lancer le serveur")) {
				clicked = "StartServer";
				//networkmanager.StartServer();
			} 
			else if (GUI.Button(new Rect( (Screen.width*3)/100, (Screen.height*13)/100, (Screen.width*22)/100, (Screen.height*7)/100 ), "Rafraichir")) {
				networkmanager.RefreshHostList();
				hostList = networkmanager.getHostList();
				//Debug.Log("Refresh Hosts!");
			}
			else if (GUI.Button(new Rect( (Screen.width*3)/100, (Screen.height*42)/100, (Screen.width*22)/100, (Screen.height*7)/100 ), "Retour")){
				clicked = "";
			}
			
			GUI.EndGroup();
			
		}
		
	}
	
	private void optionsFunc(int id)
	{
		if (GUILayout.Button("Résolution"))
		{
			clicked = "resolution";
		}
		GUILayout.Label("Volume");
		volume = GUILayout.HorizontalSlider(volume ,0.0f,1.0f);
		AudioListener.volume = volume;
		if (GUILayout.Button("Retour"))
		{
			clicked = "";
		}
		if (DragWindow)
			GUI.DragWindow(new Rect (0,0,Screen.width,Screen.height));
	}
	
	private void menuFunc(int id)
	{
		//buttons 
		if (GUILayout.Button("Lancer"))
		{
			clicked = "Play";
		}
		if (GUILayout.Button("Options"))
		{
			clicked = "options";
		}
		if (GUILayout.Button("Aide"))
		{
			clicked = "help";
		}
		
		if (GUILayout.Button("Descriptions"))
		{
			clicked = "descriptions";
		}
		
		if (DragWindow)
			GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
	}
	
	private void Update()
	{
		if ((clicked == "help" || clicked == "descriptions") && Input.GetKey (KeyCode.Escape))
			clicked = "";
	}
}
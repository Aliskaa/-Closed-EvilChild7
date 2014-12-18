using UnityEngine;
using System.Collections;

public class AccueilScript : MonoBehaviour {
	
	public GUISkin guiSkin;
	public Texture2D background;
	public bool DragWindow = false;
	public string levelToLoadWhenClickedPlay = "";
	private string clicked = "", MessageDisplayOnHelp = "Help \n ";
	private Rect WindowRect = new Rect((Screen.width / 2) - 100, Screen.height / 2, 600, 230);
	private float volume = 1.0f;
	
	//Variable serveur
	private HostData[] hostList = null;
	private const string typeName = "EvilChild";
	private NetworkManager networkmanager;
	private string NameServer = "Name server";
	
	private void Start()
	{
		MessageDisplayOnHelp += "Press Esc To Go Back";
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
			if (GUILayout.Button("Back"))
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
		if (GUILayout.Button ("Player vs IA")) {
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
		else if (GUILayout.Button("Back")){
			clicked = "";
		}
		
		if (DragWindow)
			GUI.DragWindow(new Rect (0,0,Screen.width,Screen.height));
		
	}
	
	//Option Start Server
	private void optionStartServer(){
		//Box 1
		GUI.BeginGroup(new Rect((Screen.width*66)/100, (Screen.height*42)/100, (Screen.width*26)/100, (Screen.height*50)/100));
		GUI.Box(new Rect(0, 0, (Screen.width*26)/100, (Screen.height*50)/100), "Start Server !");
		// Nom du serveur
		GUI.BeginGroup(new Rect((Screen.width*7)/100, (Screen.height*7)/100, (Screen.width*22)/100, (Screen.height*7)/100));
		GUILayout.BeginHorizontal();
		GUILayout.Label("Name server:", GUILayout.Width(100));
		NameServer = GUILayout.TextField(NameServer);
		GUILayout.EndHorizontal();
		GUI.EndGroup();
		
		if (GUI.Button(new Rect( (Screen.width*2)/100, (Screen.height*13)/100, (Screen.width*22)/100, (Screen.height*7)/100), "launch" ) )
		{
			networkmanager.setGameName(NameServer);
			networkmanager.StartServer();
			//Debug.Log("launch");
			Application.LoadLevel(2);
		}
		if (GUI.Button(new Rect( (Screen.width*2)/100, (Screen.height*20)/100, (Screen.width*22)/100, (Screen.height*7)/100), "Reset" ))
		{
			NameServer = "Name server";
		}
		
		if (GUI.Button(new Rect( (Screen.width*2)/100, (Screen.height*42)/100, (Screen.width*22)/100, (Screen.height*7)/100),"Back")){
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
			GUI.Box(new Rect(0, 0, (Screen.width*26)/100, (Screen.height*50)/100), "Choice of servers ?");
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
				GUI.Button(new Rect( (Screen.width*3)/100, (Screen.height*7)/100, (Screen.width*22)/100, (Screen.height*7)/100), "No servers available!");
			}
			
			GUI.EndGroup();
			
			//Box 2
			GUI.BeginGroup(new Rect((Screen.width*66)/100, (Screen.height*42)/100, (Screen.width*26)/100, (Screen.height*50)/100));
			GUI.Box(new Rect(0, 0, (Screen.width*26)/100, (Screen.height*50)/100),"Server management");
			
			if (GUI.Button(new Rect( (Screen.width*3)/100, (Screen.height*7)/100, (Screen.width*22)/100, (Screen.height*7)/100), "Start Server")) {
				clicked = "StartServer";
				//networkmanager.StartServer();
			} 
			else if (GUI.Button(new Rect( (Screen.width*3)/100, (Screen.height*13)/100, (Screen.width*22)/100, (Screen.height*7)/100 ), "Refresh Hosts")) {
				networkmanager.RefreshHostList();
				hostList = networkmanager.getHostList();
				//Debug.Log("Refresh Hosts!");
			}
			else if (GUI.Button(new Rect( (Screen.width*3)/100, (Screen.height*42)/100, (Screen.width*22)/100, (Screen.height*7)/100 ), "Back")){
				clicked = "";
			}
			
			GUI.EndGroup();
			
		}
		
	}
	
	private void optionsFunc(int id)
	{
		if (GUILayout.Button("Resolution"))
		{
			clicked = "resolution";
		}
		GUILayout.Box("Volume");
		volume = GUILayout.HorizontalSlider(volume ,0.0f,1.0f);
		AudioListener.volume = volume;
		if (GUILayout.Button("Back"))
		{
			clicked = "";
		}
		if (DragWindow)
			GUI.DragWindow(new Rect (0,0,Screen.width,Screen.height));
	}
	
	private void menuFunc(int id)
	{
		//buttons 
		if (GUILayout.Button("Play "))
		{
			clicked = "Play";
		}
		if (GUILayout.Button("Options"))
		{
			clicked = "options";
		}
		if (GUILayout.Button("Help"))
		{
			clicked = "help";
		}
		/*
        if (GUILayout.Button("Quit "))
        {
            Application.Quit();
        }
        */
		if (DragWindow)
			GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
	}
	
	private void Update()
	{
		if (clicked == "help" && Input.GetKey (KeyCode.Escape))
			clicked = "";
	}
}
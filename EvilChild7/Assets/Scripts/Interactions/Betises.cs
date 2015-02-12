/// <summary>
/// Betises
/// Fichier pour le lancer de betises de tout type
/// </summary>
/// 
/// <remarks>
/// G POLLET & S RETHORE - version 580000
/// </remarks>


//TODO MEGA PRIORITE : meme touche pour direction et lancé !!

using UnityEngine;
using System.Collections;

public class Betises : MonoBehaviour,Betise
{
	#region variables privees
	//Constantes de l'update
	private const int CHOIX_FORCE = 0;
	private const int CHOIX_DIRECTION = 1;
	private const int CREATION = 2;
	
	//variables de force
	private const float FORCE_MAX = 100.0f;
	private const float FORCE_MIN = 1.0f;
	private float curseur_force = 0.0f;
	
	//variables de direction
	private const float DEGRE_MAX = 180.0f;
	private const float DEGRE_MIN = 0.0f;
	private float curseur = 90.0f;
	
	//game
	private int tour = CHOIX_DIRECTION;
	private bool croissance = true;
	private bool play = true;
	public GameObject fleche2;
	private Vector3 position_arrivee;
	
	//temps
	private float cTime = 0.0f;
	
	//textures
	private Texture2D barre;
	
	//betises
	private InvocateurObjetsScript invoc;
	private float trajectoryHeight = 15;
	private Vector3 position_depart = new Vector3(200.0f,0.0f,95.0f);
	//private Vector3 position_depart_fleche = new Vector3(200.0f,0.0f,200.0f);
	#endregion
	
	#region variables publiques
	//betises 
	public GameObject betise;
	public Invocations nom_betise;
	
	//game 
	public bool isAvailable = true;
	public bool lancerIA = false;
	public Interface monInterface = null;
	public bool randomiserFait = false;
	public bool flechedestroyed;
	
	//textures
	public GUISkin skin_test;
	
	//vent initialisé à "PAS DE VENT"
	//vent sur le cotésss
	public static float vent_axe_z = 1.0f;
	//vent en face et en arriere
	public static float vent_axe_x = 1.0f;
	
	//lancer de betises
	public float forceLancerIA = 0.0f;
	public float directionIA = 0.0f;
	
	public TypesObjetsRencontres maFourmi;
	#endregion
	
	
	
	#region affichage
	
	/// <summary>
	/// Raises the GU event. Pour l'affichage des sliders
	/// </summary>
	void OnGUI(){
		GUI.skin = skin_test;
		if (isAvailable) {
			//barre de force
			//GUI.VerticalSlider (new Rect (10, 100, 100, 100), curseur_force, FORCE_MAX, FORCE_MIN); 
			if(!lancerIA){
				//curseur_force = LabelSliderVertical (new Rect (10, 100, 100, 100), curseur_force, FORCE_MAX, FORCE_MIN,"Force");
				Texture2D barre_force_pleine = (Texture2D)Resources.LoadAssetAtPath("Assets/Resources/force_pleine.png", typeof(Texture2D));
				Texture2D barre_force_vide = (Texture2D)Resources.LoadAssetAtPath("Assets/Resources/force_vide.png", typeof(Texture2D));
				
				float avancement = (curseur_force - FORCE_MIN)/FORCE_MAX;
				GUI.BeginGroup (new Rect ((Screen.width*1)/100,(Screen.height*30)/100,(Screen.width * 2) / 100,(Screen.height*20)/100));
				GUI.DrawTexture (new Rect (0,0, (Screen.width * 2) / 100,(Screen.height*20)/100), barre_force_pleine);
				GUI.BeginGroup (new Rect (0, 0, (Screen.width * 2) / 100, ((Screen.height*20)/100) - (avancement * (Screen.height*20)/100)));
				GUI.DrawTexture(new Rect (0,0, (Screen.width * 2) / 100,(Screen.height*20)/100), barre_force_vide);
				GUI.EndGroup ();
				GUI.EndGroup ();
			}	
		}
	}

	/// <summary>
	/// Labels the slider.
	/// </summary>
	/// <returns>The slider.</returns>
	/// <param name="screenRect">Screen rect.</param>
	/// <param name="sliderValue">Slider value.</param>
	/// <param name="sliderMaxValue">Slider max value.</param>
	/// <param name="labelText">Label text.</param>
	float LabelSliderVertical (Rect screenRect, float sliderValue, float sliderMaxValue,float sliderMinValue, string labelText) 
	{
		GUI.Label (screenRect, labelText);
		// <- Push the Slider to the end of the Label
		screenRect.y += screenRect.width; 
		sliderValue = GUI.VerticalSlider (screenRect, sliderValue,  sliderMaxValue, sliderMinValue);
		return sliderValue;
	}
	#endregion
	
	
	
	/// <summary>
	/// Instantiation de  la betise à lancer
	/// </summary>
	void Start(){
		flechedestroyed = false;
		GameObject bacASable = GameObject.FindGameObjectWithTag ("BAC_A_SABLE");
		invoc = bacASable.GetComponent<InvocateurObjetsScript> ();					
		
		skin_test = Resources.LoadAssetAtPath<GUISkin>("Assets/Skins/GuiSkinForMenuJeu.guiskin");
		
		//instanciation de la betise à lancer
		betise = invoc.InvoquerObjetAvecOffset (nom_betise, position_depart, new Vector3 (0.0f, 15.0f, 0.0f));
		
		//instanciation de la flèche de lancer
		fleche2 = invoc.InvoquerObjetAvecOffset (Invocations.FLECHE2, position_depart, new Vector3 (0.0f, 0.2f, 0.0f));
		
		if (nom_betise == Invocations.OEUF_FOURMI) {
			betise.GetComponent<OeufScript>().fourmi = genererInvoc();
		}
		//freezer la position mieux que ça
		betise.rigidbody.isKinematic = true;
		//betise.rigidbody.constraints = RigidbodyConstraints.FreezePosition;
	}
	
	/// <summary>
	/// Choix successif de la force de lancer, de la direction et du lancer de la betise
	/// </summary>
	void Update(){
		if (play){
			isAvailable = true;
			switch (tour){
			case CHOIX_DIRECTION:
				if (croissance) {
					if (curseur >= DEGRE_MAX){
						croissance = false;
					} else {
						curseur += 1.0f;
						fleche2.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), -1.0f);
					}
				} else {
					if (curseur <= DEGRE_MIN){
						croissance = true;
					} else {
						curseur -= 1.0f;
						fleche2.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 1.0f);
					}
				}
				if (Input.GetKey(KeyCode.Backspace)){
					tour = CHOIX_FORCE;
				}
				break;
				
			case CHOIX_FORCE:
				if (croissance) {
					if (curseur_force >= FORCE_MAX){
						croissance = false;
					} else {
						curseur_force = curseur_force + 1.0f;
					}
				} else {
					if (curseur_force <= FORCE_MIN){
						croissance = true;
					} else {
						curseur_force = curseur_force - 1.0f;
					}
				}
				if (Input.GetKey(KeyCode.Return)){
					GameObject go = invoc.InvoquerObjet(Invocations.PETIT_CAILLOU, new Vector3(150.0f - ForceLancer(),10.0f,95.0f));
					go.transform.RotateAround(fleche2.transform.position, Vector3.up, 90.0f); // trick un peu sale pour permettre de bien tenir compte de la force
					go.transform.RotateAround(fleche2.transform.position, Vector3.up, -Direction());
					position_arrivee = go.transform.localPosition;
					Destroy(go);
					tour = CREATION;
				}
				break;
				
			case CREATION:
				if(monInterface!=null){
					monInterface.lancerEnCours = false;
				}
				
				if (fleche2 != null){
					Destroy(fleche2);
					lancerIA = true;
				}
				//la betise est lancée à ce moment
				if(betise == null){
					break;
				}
				
				betise.rigidbody.isKinematic = false;
				
				cTime += 0.01f;
				
				HexagoneInfo hexPlusProche = TerrainUtils.HexagonePlusProche(position_arrivee);
				// calcul du trajet de la betise entre son point de depart et son point d'arrivée
				Vector3 currentPos = Vector3.Lerp(position_depart,hexPlusProche.positionLocaleSurTerrain , cTime);
				
				currentPos.y += trajectoryHeight * Mathf.Sin(Mathf.Clamp01(cTime) * Mathf.PI);
				
				//changement de position de la betise
				betise.transform.localPosition= currentPos;
				
				if (betise.transform.localPosition == hexPlusProche.positionLocaleSurTerrain) {
					isAvailable = !isAvailable;
					play = !play;
				}
				break;
			default:
				break;
			}
		}
	}
	
	private float Direction(){
		return curseur;
	}
	
	private float ForceLancer(){
		return curseur_force;
	}
	
	public int getPoids(){
		if (nom_betise == Invocations.CAILLOU) {
			return (int)PoidsObjetsAlancer.POIDS_CAILLOU;
		}
		
		if (nom_betise == Invocations.BOMBE_EAU) {
			return (int)PoidsObjetsAlancer.POIDS_BOMBE_A_EAU;
		}
		
		if (nom_betise == Invocations.OEUF_FOURMI) {
			return (int)PoidsObjetsAlancer.POIDS_OEUFS;
		}
		
		if (nom_betise == Invocations.PISTOLET) {
			return 0 ;
		}
		
		if (nom_betise == Invocations.OEUF_FOURMI) {
			return (int)PoidsObjetsAlancer.POIDS_OEUFS;
		}
		
		if (nom_betise == Invocations.SCARABEE) {
			return (int)PoidsObjetsAlancer.POIDS_SCARABEE;
		}
		
		if (nom_betise == Invocations.BOUT_DE_BOIS) {
			return (int)PoidsObjetsAlancer.POIDS_BOUT_DE_BOIS;
		}
		
		if (nom_betise == Invocations.BOUT_DE_BOIS) {
			return (int)PoidsObjetsAlancer.POIDS_BOUT_DE_BOIS;
		}
		
		return 0;
	}
	
	public void lancer(){
		this.play = true;
	}
	public void lancerCeTour(TourDeJeu tour){
		lancer ();
	}
	
	Invocations genererInvoc(){
		switch (maFourmi) {
		case(TypesObjetsRencontres.OUVRIERE_BLANCHE):
			return Invocations.FOURMI_BLANCHE_OUVRIERE;
			
		case(TypesObjetsRencontres.OUVRIERE_NOIRE):
			return Invocations.FOURMI_NOIRE_OUVRIERE;
			
		case(TypesObjetsRencontres.CONTREMAITRE_BLANCHE):
			return Invocations.FOURMI_BLANCHE_CONTREMAITRE;
			
		case(TypesObjetsRencontres.CONTREMAITRE_NOIRE):
			return Invocations.FOURMI_NOIRE_CONTREMAITRE;
			
		case(TypesObjetsRencontres.COMBATTANTE_NOIRE):
			return Invocations.FOURMI_NOIRE_COMBATTANTE;
			
		case(TypesObjetsRencontres.COMBATTANTE_BLANCHE):
			return Invocations.FOURMI_BLANCHE_COMBATTANTE;
			
		case(TypesObjetsRencontres.GENERALE_NOIRE):
			return Invocations.FOURMI_NOIRE_GENERALE;
			
		case(TypesObjetsRencontres.GENERALE_BLANCHE):
			return Invocations.FOURMI_BLANCHE_GENERALE;
			
		default:
			return Invocations.RIEN;
			
		}
	}
	
	public static void FinTour(){
		//Destroy (this.fleche2);
		
	}
}
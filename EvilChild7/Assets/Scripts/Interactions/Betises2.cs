/// <summary>
/// Betises
/// Fichier pour le lancer de betises de tout type DU COTE DU TERRAIN OU IL Y A DES FLEURS BLEUES PAR TERRE
/// </summary>
/// 
/// <remarks>
/// G POLLET & S RETHORE - version 580000
/// </remarks>

using UnityEngine;
using System.Collections;

public class Betises2 : MonoBehaviour,Betise
{
	#region variables privees
	//Constantes de l'update
	private const int CHOIX_FORCE = 0;
	private const int CHOIX_DIRECTION = 1;
	private const int CREATION = 2;
	
	//variables de force
	private const float FORCE_MAX = 20.0f;
	private const float FORCE_MIN = 5.0f;
	private float curseur_force = 0.0f;
	
	//variables de direction
	private const float DEGRE_MAX = 20.0f;
	private const float DEGRE_MIN = 0.0f;
	private float curseur = 0.0f;
	
	//game
	private int tour = CHOIX_FORCE;
	private bool croissance = true;
	private bool play = true;
	private GameObject fleche;

	//temps
	private float cTime = 0.0f;
	private bool randomiserFait = false;

	//betises
	private InvocateurObjetsScript invoc;
	private float trajectoryHeight = 15;
	private Vector3 position_depart = new Vector3(0.0f,0.0f,100.0f);
	private Vector3 position_depart_fleche = new Vector3(0.0f,0.0f,0.0f);
	#endregion

	#region variables publiques
	//betises 
	public GameObject betise;
	public Invocations nom_betise;

	//game 
	public bool isAvailable = true;
	public bool lancerIA = false;
	public Interface monInterface = null;

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
			curseur_force = LabelSliderVertical (new Rect (10, 100, 100, 100), curseur_force, FORCE_MAX, FORCE_MIN,"FORCE");
			//barre de direction
			GUI.HorizontalSlider(new Rect(50, Screen.height - 20, Screen.width - 100, 50), curseur, DEGRE_MAX, DEGRE_MIN);
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

		GameObject bacASable = GameObject.FindGameObjectWithTag ("BAC_A_SABLE");
		invoc = bacASable.GetComponent<InvocateurObjetsScript> ();

		skin_test = Resources.LoadAssetAtPath<GUISkin>("Assets/Scripts/Menu/GuiSkinForMenuJeu.guiskin");

		//instanciation de la bétise à lancer
		betise = invoc.InvoquerObjetAvecOffset (nom_betise, position_depart, new Vector3 (0.0f, 15.0f, 0.0f));
		//instaciation de la flèche de lancer
		fleche = invoc.InvoquerObjetAvecOffset (Invocations.FLECHE, position_depart_fleche, new Vector3 (0.0f, 0.2f, 0.0f));

		
		if (nom_betise == Invocations.OEUF_FOURMI) {
			betise.GetComponent<OeufScript>().fourmi = genererInvoc();
		}

		betise.rigidbody.isKinematic = true;

	}

	/// <summary>
	/// Choix successif de la force de lancer, de la direction et du lancer de la betise
	/// </summary>
	void Update(){

		if (play){
			if(lancerIA){
				if(!randomiserFait){

					randomiserLancer();
				}
				tour = CREATION;
			}
			//Sélection de la force
			switch (tour){
				
			case CHOIX_FORCE:
				//Debug.Log ("Force");
				if (croissance) {
					if (curseur_force >= FORCE_MAX){
						croissance = false;
					} else {
						if (curseur_force <6){
							fleche.transform.localScale = new Vector3(fleche.transform.localScale.x,fleche.transform.localScale.y,curseur_force/8f);
						}
						else {
							fleche.transform.localScale = new Vector3(fleche.transform.localScale.x,fleche.transform.localScale.y,curseur_force/3f);
						}
						curseur_force = curseur_force + 0.1f;
					}
				} else {
					if (curseur_force <= FORCE_MIN){
						croissance = true;
					} else {
						if (curseur_force <6){
							fleche.transform.localScale = new Vector3(fleche.transform.localScale.x,fleche.transform.localScale.y,curseur_force/8f);
						}else {
							fleche.transform.localScale = new Vector3(fleche.transform.localScale.x,fleche.transform.localScale.y,curseur_force/3f);
						}
						curseur_force = curseur_force - 0.1f;
					}
				}
				if (Input.GetKey(KeyCode.Backspace)){
					ForceLancer();
					//fleche.transform.localScale = new Vector3(fleche.transform.localScale.x,fleche.transform.localScale.y,ForceLancer()/5f);
					//Debug.Log("force de lancé" + ForceLancer());
					tour = CHOIX_DIRECTION;
				}
				break;
				
			case CHOIX_DIRECTION:
				//Debug.Log("Direction");
				if (croissance) {
					if (curseur >= DEGRE_MAX){
						croissance = false;
					} else {
						curseur += 0.1f; 
						fleche.transform.localPosition = new Vector3(fleche.transform.localPosition.x,fleche.transform.localPosition.y,fleche.transform.localPosition.z+0.44f);
					}
				} else {
					if (curseur <= DEGRE_MIN){
						croissance = true;
					} else {
						curseur -= 0.1f;
						fleche.transform.localPosition = new Vector3(fleche.transform.localPosition.x,fleche.transform.localPosition.y,fleche.transform.localPosition.z-0.44f);

					}
				} 
				if (Input.GetKey(KeyCode.Return)){
					Direction();
					//fleche.transform.Rotate(new Vector3(0.0f,-(Direction()*10f+270),0.0f)); 

					//Debug.Log("direction" + Direction());
					tour = CREATION;
				}
				break;
			case CREATION:
				if(monInterface!=null){
					monInterface.lancerEnCours = false;
				}
				if(betise == null){
					break;
				}
				//la betise est lancée à ce moment
				betise.rigidbody.isKinematic = false;
				//Debug.Log("vent: "+ vent_axe_z);

				cTime += 0.01f;
				Vector3 position_arrivee;

				//Vérification d'un lancé fait par l'IA ou non
				if (lancerIA==true){
					//Debug.Log(forceLancerIA);
					//Debug.Log(directionIA);
					position_arrivee = new Vector3(forceLancerIA*10f*vent_axe_x,0.1f,directionIA*10f*vent_axe_z);
					//lancerIA = false;
				}else {
					//Debug.Log ("coucou");
					position_arrivee = new Vector3(ForceLancer()*10f*vent_axe_x,0.1f,Direction ()*10f*vent_axe_z);
				
					}
				HexagoneInfo hexPlusProche = TerrainUtils.HexagonePlusProche(position_arrivee);
				// calcul du trajet de la betise entre son point de depart et son point d'arrivée
				Vector3 currentPos = Vector3.Lerp(position_depart,hexPlusProche.positionLocaleSurTerrain , cTime);
				
				currentPos.y += trajectoryHeight * Mathf.Sin(Mathf.Clamp01(cTime) * Mathf.PI);


				//changement de position de la betise
				betise.transform.localPosition= currentPos;


				//on sort du tour de jeu et on enleve l'affichage des sliders une fois la betise a sa place
				if (betise.transform.localPosition == hexPlusProche.positionLocaleSurTerrain) {
					isAvailable = !isAvailable;
					Destroy (fleche);
					play = !play;
					lancerIA = false;
				}

				//Destroy (fleche);
				break;
			default:
				//Debug.Log("Je ne sais pas quel choix faire");
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
			return 0;
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

	public void randomiserLancer(){
		this.forceLancerIA = Random.Range (FORCE_MIN, FORCE_MAX-2);
		this.directionIA = Random.Range (DEGRE_MIN, DEGRE_MAX);
		randomiserFait = true;
	}

}
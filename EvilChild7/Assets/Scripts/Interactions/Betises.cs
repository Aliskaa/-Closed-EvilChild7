/// <summary>
/// Betises
/// Fichier pour le lancer de betises de tout type
/// </summary>
/// 
/// <remarks>
/// G POLLET & S RETHORE - version 580000
/// </remarks>

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
	private GameObject fleche2;
	
	//temps
	private float cTime = 0.0f;
	
	//textures
	private Texture2D barre;
	
	//betises
	private InvocateurObjetsScript invoc;
	private float trajectoryHeight = 15;
	private Vector3 position_depart = new Vector3(200.0f,0.0f,100.0f);
	private Vector3 position_depart_fleche = new Vector3(200.0f,0.0f,200.0f);
	#endregion
	
	#region variables publiques
	//betises 
	public GameObject betise;
	public Invocations nom_betise;
	
	//game 
	public bool isAvailable = true;
	public bool lancerIA = false;
	public Interface monInterface = null;
	private bool randomiserFait = false;
	
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
			curseur_force = LabelSliderVertical (new Rect (10, 100, 100, 100), curseur_force, FORCE_MAX, FORCE_MIN,"Force");
			//barre de direction
			GUI.HorizontalSlider(new Rect (0, Screen.height - 10, Screen.width, 50), curseur, DEGRE_MAX, DEGRE_MIN);
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

		skin_test = Resources.LoadAssetAtPath<GUISkin>("Assets/Skins/GuiSkinForMenuJeu.guiskin");
		
		//instanciation de la bétise à lancer
		betise = invoc.InvoquerObjetAvecOffset (nom_betise, position_depart, new Vector3 (0.0f, 15.0f, 0.0f));
		//instaciation de la flèche de lancer
		fleche2 = invoc.InvoquerObjetAvecOffset (Invocations.FLECHE2, position_depart_fleche, new Vector3 (0.0f, 0.2f, 0.0f));

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
			isAvailable = true;
			//Debug.Log("yo");
			//Sélection dela force
			if(lancerIA){
				if(!randomiserFait){
					
					randomiserLancer();
				}
				tour = CREATION;
			}
			switch (tour){
				
			case CHOIX_FORCE:
				//Debug.Log ("Force");
				if (croissance) {
					if (curseur_force >= FORCE_MAX){
						croissance = false;
					} else {
						if (curseur_force <6){
							fleche2.transform.localScale = new Vector3(fleche2.transform.localScale.x,fleche2.transform.localScale.y,curseur_force/7f);
						}
						else {
							fleche2.transform.localScale = new Vector3(fleche2.transform.localScale.x,fleche2.transform.localScale.y,curseur_force/2.6f);
						}
						curseur_force = curseur_force + 0.1f;
					}
				} else {
					if (curseur_force <= FORCE_MIN){
						croissance = true;
					} else {
						if (curseur_force <6){
							fleche2.transform.localScale = new Vector3(fleche2.transform.localScale.x,fleche2.transform.localScale.y,curseur_force/7f);
						}else {
							fleche2.transform.localScale = new Vector3(fleche2.transform.localScale.x,fleche2.transform.localScale.y,curseur_force/2.6f);
						}
						curseur_force = curseur_force - 0.1f;
					}
				}
					if (Input.GetKey(KeyCode.Backspace)){
						ForceLancer();
						//Debug.Log("force de lancé" + ForceLancer());
						fleche2.transform.localScale = new Vector3(fleche2.transform.localScale.x,fleche2.transform.localScale.y,curseur_force/2.5f);

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
						fleche2.transform.localPosition = new Vector3(fleche2.transform.localPosition.x,fleche2.transform.localPosition.y,fleche2.transform.localPosition.z-0.44f);
						
					}
				} else {
					if (curseur <= DEGRE_MIN){
						croissance = true;
					} else {
						curseur -= 0.1f;
						fleche2.transform.localPosition = new Vector3(fleche2.transform.localPosition.x,fleche2.transform.localPosition.y,fleche2.transform.localPosition.z+0.44f);
						
					}
				}
					if (Input.GetKey(KeyCode.Return)){
						Direction();
						//Debug.Log("direction" + Direction());
						tour = CREATION;
					}

				break;
			case CREATION:
				if(monInterface!=null){
					monInterface.lancerEnCours = false;
				}
				//la betise est lancée à ce moment
				if(betise == null){
					break;
				}
			
				betise.rigidbody.isKinematic = false;
				//Debug.Log("vent: "+ vent_axe_z);
				
				cTime += 0.01f;
				Vector3 position_arrivee;
				
				//Vérification d'un lancé fait par l'IA ou non
				if (lancerIA ){
					position_arrivee = new Vector3(forceLancerIA*10f*vent_axe_x,0.1f,directionIA*10f*vent_axe_z);
					lancerIA = false;
				}
				else {
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
					Destroy (fleche2);
					play = !play;
				}
				//Destroy (fleche2);
				break;
			default:
				//Debug.Log("Je ne sais pas quel choix faire");
				break;
			}
		}
	}
	
	private float Direction(){
		if (curseur < 10) {
			if (curseur <= 10 && curseur >9){
				return curseur + 2;
			}
			if (curseur <= 9 && curseur >8){
				return curseur + 4;
			}
			if (curseur <= 8 && curseur >7){
				return curseur + 6;
			}
			if (curseur <= 7 && curseur >6){
				return curseur + 8;
			}
			if (curseur <= 6 && curseur >5){
				return curseur + 10;
			}
			if (curseur <= 5 && curseur >4){
				return curseur + 12;
			}
			if (curseur <= 4 && curseur >3){
				return curseur + 14;
			}
			if (curseur <= 3 && curseur >2){
				return curseur + 16;
			}
			if (curseur <= 2 && curseur >1){
				return curseur + 18;
			}
			if (curseur <= 1 && curseur >0){
				return curseur + 20;
			}
			
		} 
		if (curseur > 10) {
			if (curseur > 10 && curseur <=11){
				return curseur - 2;
			}
			if (curseur > 11 && curseur <=12){
				return curseur - 4;
			}
			if (curseur > 12 && curseur <=13){
				return curseur - 6;
			}
			if (curseur > 13 && curseur <=14){
				return curseur - 8;
			}
			if (curseur > 14 && curseur <=15){
				return curseur - 10;
			}
			if (curseur > 15 && curseur <=16){
				return curseur - 12;
			}
			if (curseur > 16 && curseur <=17){
				return curseur_force - 14;
			}
			if (curseur > 17 && curseur <=18){
				return curseur - 16;
			}
			if (curseur > 18 && curseur <=19){
				return curseur - 18;
			}
			if (curseur > 19 && curseur <=20){
				return curseur - 20;
			}
		}
		return curseur;
	}
	
	private float ForceLancer(){
		//Debug.Log ("val curseur" + curseur_force);
		if (curseur_force < 10) {
			if (curseur_force <= 10 && curseur_force >9){
				return curseur_force + 2;
			}
			if (curseur_force <= 9 && curseur_force >8){
				return curseur_force + 4;
			}
			if (curseur_force <= 8 && curseur_force >7){
				return curseur_force + 6;
			}
			if (curseur_force <= 7 && curseur_force >6){
				return curseur_force + 8;
			}
			if (curseur_force <= 6 && curseur_force >5){
				return curseur_force + 10;
			}
			if (curseur_force <= 5 && curseur_force >4){
				return curseur_force + 12;
			}
			if (curseur_force <= 4 && curseur_force >3){
				return curseur_force + 14;
			}
			if (curseur_force <= 3 && curseur_force >2){
				return curseur_force + 16;
			}
			if (curseur_force <= 2 && curseur_force >1){
				return curseur_force + 18;
			}
			if (curseur_force <= 1 && curseur_force >0){
				return curseur_force + 20;
			}
			
		} 
		if (curseur_force > 10) {
			if (curseur_force > 10 && curseur_force <=11){
				return curseur_force - 2;
			}
			if (curseur_force > 11 && curseur_force <=12){
				return curseur_force - 4;
			}
			if (curseur_force > 12 && curseur_force <=13){
				return curseur_force - 6;
			}
			if (curseur_force > 13 && curseur_force <=14){
				return curseur_force - 8;
			}
			if (curseur_force > 14 && curseur_force <=15){
				return curseur_force - 10;
			}
			if (curseur_force > 15 && curseur_force <=16){
				return curseur_force - 12;
			}
			if (curseur_force > 16 && curseur_force <=17){
				return curseur_force - 14;
			}
			if (curseur_force > 17 && curseur_force <=18){
				return curseur_force - 16;
			}
			if (curseur_force > 18 && curseur_force <=19){
				return curseur_force - 18;
			}
			if (curseur_force > 19 && curseur_force <=20){
				return curseur_force - 20;
			}
		}
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
	public void randomiserLancer(){
		this.forceLancerIA = Random.Range (FORCE_MIN, FORCE_MAX-2);
		this.directionIA = Random.Range (DEGRE_MIN, DEGRE_MAX);
		randomiserFait = true;
	}


}
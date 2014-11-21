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

public class Betises : MonoBehaviour
{
	#region variables
	//Constantes de l'update
	private const int CHOIX_FORCE = 0;
	private const int CHOIX_DIRECTION = 1;
	private const int CREATION = 2;
	
	//variables de force
	private const float FORCE_MAX = 20.0f;
	private const float FORCE_MIN = 0.0f;
	private float curseur_force = 0.0f;
	
	//variables de direction
	private const float DEGRE_MAX = 20.0f;
	private const float DEGRE_MIN = 0.0f;
	private float curseur = 0.0f;
	
	//game
	private int tour = CHOIX_FORCE;
	private bool croissance = true;
	private bool play = true;
	public bool isAvailable = true;


	//textures
	private Texture2D barre;

	
	//betises
	public GameObject betise;
	public Invocations nom_betise;
	private InvocateurObjetsScript invoc;
	private float trajectoryHeight = 15;
	private Vector3 position_depart = new Vector3(0.0f,0.0f,100.0f);

	//vent initialisé à "PAS DE VENT"
	//vent sur le coté
	public static float vent_axe_z = 1.0f;
	//vent en face et en arriere
	public static float vent_axe_x = 1.0f;

	//temps
	private float cTime = 0.0f;

	#endregion

	#region affichage

	/// <summary>
	/// Raises the GU event. Pour l'affichage des sliders
	/// </summary>
	void OnGUI(){

		if (isAvailable) {
						//barre de force
						GUI.VerticalSlider (new Rect (10, 100, 100, 100), curseur_force, FORCE_MAX, FORCE_MIN); 
		
						//barre de direction
						GUI.HorizontalSlider (new Rect (0, Screen.height - 10, Screen.width, 50), curseur, DEGRE_MAX, DEGRE_MIN);
				}
		
	}
	#endregion

	/// <summary>
	/// Instantiation de  la betise à lancer
	/// </summary>
	void Start(){

		GameObject bacASable = GameObject.FindGameObjectWithTag ("BAC_A_SABLE");
		invoc = bacASable.GetComponent<InvocateurObjetsScript> ();

		betise = invoc.InvoquerObjetAvecOffset (nom_betise, position_depart, new Vector3 (0.0f, 15.0f, 0.0f));
		betise.rigidbody.isKinematic = true;

	}

	/// <summary>
	/// Choix successif de la force de lancer, de la direction et du lancer de la betise
	/// </summary>
	void Update(){

		if (play){
			//Sélection de la force
			switch (tour){
				
			case CHOIX_FORCE:
				//Debug.Log ("Force");
				if (croissance) {
					if (curseur_force >= FORCE_MAX){
						croissance = false;
					} else {
						curseur_force = curseur_force + 0.2f;
					}
				} else {
					if (curseur_force <= FORCE_MIN){
						croissance = true;
					} else {
						curseur_force = curseur_force - 0.2f;
					}
				}
				if (Input.GetKey(KeyCode.Backspace)){
					ForceLancer();
					Debug.Log("force de lancé" + ForceLancer());
					tour = CHOIX_DIRECTION;
				}
				break;
				
			case CHOIX_DIRECTION:
				//Debug.Log("Direction");
				if (croissance) {
					if (curseur >= DEGRE_MAX){
						croissance = false;
					} else {
						curseur += 0.2f;
					}
				} else {
					if (curseur <= DEGRE_MIN){
						croissance = true;
					} else {
						curseur -= 0.2f;
					}
				}
				if (Input.GetKey(KeyCode.Return)){
					Direction();
					Debug.Log("direction" + Direction());
					tour = CREATION;
				}
				break;
			case CREATION:
				//la betise est lancée à ce moment
				betise.rigidbody.isKinematic = false;
				Debug.Log("vent: "+ vent_axe_z);
				Vector3 position_arrivee = new Vector3(ForceLancer()*10f*vent_axe_x,0.1f,Direction ()*10f*vent_axe_z);
				HexagoneInfo hexPlusProche = TerrainUtils.HexagonePlusProche(position_arrivee);
		
				cTime += 0.01f;

				// calcul du trajet de la betise entre son point de depart et son point d'arrivée
				Vector3 currentPos = Vector3.Lerp(position_depart,hexPlusProche.positionLocaleSurTerrain , cTime);

				currentPos.y += trajectoryHeight * Mathf.Sin(Mathf.Clamp01(cTime) * Mathf.PI);

				//changement de position de la betise
				betise.transform.localPosition= currentPos;


				//on sort du tour de jeu et on enleve l'affichage des sliders une fois la betise a sa place
				if (betise.transform.localPosition == hexPlusProche.positionLocaleSurTerrain) {
					isAvailable = !isAvailable;
					play = !play;
				}
				break;
			default:
				Debug.Log("Je ne sais pas quel choix faire");
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

}
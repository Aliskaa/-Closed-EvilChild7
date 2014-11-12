/// <summary>
/// CameraScript.cs
/// Fichier CS pour gérer la caméra, c'est à dire sa position, son déplacement, etc.
/// http://forum.unity3d.com/threads/rts-camera-script.72045/
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 2.0.0
/// </remarks>

using UnityEngine;

/// <summary>
/// Classe pour gérer la caméra : position, mouvements, positions et angles autorisés ou non, etc.
/// </summary>

// TODO : Touche haut et bas : monter et descendre, aps de zoom
// TODO : rotation, déplacements dans tous les sens, zomm in et out
// TODO : bloquer la caméra pour aps qu'elle aille plus loin que necessaire
public class CameraScript : MonoBehaviour {


	/* ********* *
	 * Attributs *
	 * ********* */

#region Constantes privées
	/// <summary>
	/// La vitesse de scroll
	/// </summary>
	private const float vitesseScroll = 15;
	
	/// <summary>
	/// 
	/// </summary>
	private const float edgeScroll = 0.1f;
	
	/// <summary>
	/// 
	/// </summary>
	private const float vitessePanoramique = 100;

	/// <summary>
	/// La vitesse de zoom
	/// </summary>
	private const float vitesseZoom = 20;

	/// <summary>
	/// La vitesse de rotation
	/// </summary>
	private const float vitesseRotation = 1;

	/// <summary>
	/// 
	/// </summary>
	private const float rotateSpeed = 10;

	/// <summary>
	/// Clic gauche avec la souris
	/// </summary>
	private const int CLIC_GAUCHE = 0;

	/// <summary>
	/// Touche pour aller devant / monter
	/// </summary>
	private const string TOUCHE_HAUT = "z";

	/// <summary>
	/// Touche pour aller derrière / descendre
	/// </summary>
	private const string TOUCHE_BAS = "s";

	/// <summary>
	/// Touche pour aller à gauche
	/// </summary>
	private const string TOUCHE_GAUCHE = "q";

	/// <summary>
	/// Touche pour aller à droite
	/// </summary>
	private const string TOUCHE_DROITE = "d";

	/// <summary>
	/// Touche de rotation à gauche
	/// </summary>
	private const string TOUCHE_ROTATION_GAUCHE = "a";

	/// <summary>
	/// Touche de rotation à droite
	/// </summary>
	private const string TOUCHE_ROTATION_DROITE = "e";

	/// <summary>
	/// Touche pour réinitialiser la caméra
	/// </summary>
	private const string TOUCHE_REINIT = "space";

	/// <summary>
	/// Zoom avant avec la souris	
	/// </summary>
	private const string MOLETTE_ZOOM = "Mouse ScrollWheel";
#endregion

#region Attributs privés
	
	/// <summary>
	/// Concerne le zoom
	/// </summary>
	private Vector2 zoomRange = new Vector2( -10, 100 );

	/// <summary>
	/// Concerne le zoom
	/// </summary>
	private float currentZoom = 0;
	
	/// <summary>
	/// Concerne le zoom
	/// </summary>
	private Vector2 zoomAngleRange;

	/// <summary>
	/// La position initiale
	/// </summary>
	private Vector3 positionInitiale;

	/// <summary>
	/// La rotation initiale
	/// </summary>
	private Vector3 rotationInitiale;

	/// <summary>
	/// Le zoom initial
	/// </summary>
	private float zoomInitial;
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes package
	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake(){
		zoomAngleRange = new Vector2( 20, 70 );
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start(){
		positionInitiale = transform.position;      
		rotationInitiale = transform.eulerAngles;
		zoomInitial = camera.fieldOfView;
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update(){

		/*
		 * Clic gauche : panoramique
		 * /
		if ( Input.GetMouseButtonDown(CLIC_GAUCHE) ){

			Debug.Log("SAUCISSE"	);
			transform.Translate(Vector3.right * Time.deltaTime * vitessePanoramique * (Input.mousePosition.x - Screen.width * 0.5f) / (Screen.width * 0.5f), Space.World);
			transform.Translate(Vector3.forward * Time.deltaTime * vitessePanoramique * (Input.mousePosition.y - Screen.height * 0.5f) / (Screen.height * 0.5f), Space.World);
		
			// zoom in/out
			currentZoom -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 1000 * vitesseZoom;
			
			currentZoom = Mathf.Clamp( currentZoom, zoomRange.x, zoomRange.y );
			
			transform.position = new Vector3( transform.position.x, transform.position.y - (transform.position.y - (positionInitiale.y + currentZoom)) * 0.1f, transform.position.z );
			
			float x = transform.eulerAngles.x - (transform.eulerAngles.x - (rotationInitiale.x + currentZoom * vitesseRotation)) * 0.1f;
			x = Mathf.Clamp( x, zoomAngleRange.x, zoomAngleRange.y );
			
			transform.eulerAngles = new Vector3( x, transform.eulerAngles.y, transform.eulerAngles.z );

		} else {
		*/
		/*
		 * Touche d : aller à droite
		 */
		if ( Input.GetKey(TOUCHE_DROITE) ){          
			transform.Translate(Vector3.right * Time.deltaTime * vitessePanoramique, Space.Self );  
			return;
		}
		/*
		 * Touche a : aller à gauche
		 */
		if ( Input.GetKey(TOUCHE_GAUCHE) ){         
			transform.Translate(Vector3.left * Time.deltaTime * vitessePanoramique, Space.Self );              
			return;
		}
		/*
		 * Touche z : aller devant / monter
		 */
		if ( Input.GetKey(TOUCHE_HAUT) ){// || Input.mousePosition.y >= Screen.height * (1 - edgeScroll) ) {            
			transform.Translate(Vector3.forward * Time.deltaTime * vitessePanoramique, Space.Self );             
			return;
		}
		/*
		 * Touche s : aller derrière / descendre
		 */
		if ( Input.GetKey(TOUCHE_BAS) ){//|| Input.mousePosition.y <= Screen.height * edgeScroll ) {         
			transform.Translate(Vector3.forward * Time.deltaTime * -vitessePanoramique, Space.Self );            
			return;
		}  
		/*
		 * Touche a : rotation gauche
		 */
		if ( Input.GetKey(TOUCHE_ROTATION_GAUCHE) ){//|| Input.mousePosition.x <= Screen.width * edgeScroll ) {
			transform.Rotate(Vector3.up * Time.deltaTime * -rotateSpeed, Space.World);
			return;
		}
		/*
		 * Touche e : rotation droite
		 */
		if ( Input.GetKey(TOUCHE_ROTATION_DROITE) ){//|| Input.mousePosition.x >= Screen.width * (1 - edgeScroll) ) {
			transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.World);
			return;
		}
		/*
		 * Touche espace : réinitialisation de la caméra
		 */
		if ( Input.GetKey(TOUCHE_REINIT) ){
			transform.position = positionInitiale;
			transform.eulerAngles = rotationInitiale;
			Camera.main.fieldOfView = zoomInitial;
			return;
		}
		/*
		 * Molette vers le haut : zoom
		 */
		float zoom = Input.GetAxis(MOLETTE_ZOOM);
		if (zoom > 0 ){
			float currentFOV = camera.fieldOfView;
			// Mathf.Clamp(valeur à cadrer, minimum, maximum);
			Camera.main.fieldOfView -= Mathf.Clamp(currentFOV, zoom, Time.deltaTime * vitesseZoom);
			return;
		}
		/*
		 * Molette vers le bas : dézoom
		 */
		zoom = Input.GetAxis(MOLETTE_ZOOM);
		if (zoom < 0 ){
			float currentFOV = camera.fieldOfView;
			// Mathf.Clamp(valeur à cadrer, minimum, maximum);
			Camera.main.fieldOfView += Mathf.Clamp(currentFOV, zoom, Time.deltaTime * vitesseZoom);
			return;
		}

	}
#endregion

}

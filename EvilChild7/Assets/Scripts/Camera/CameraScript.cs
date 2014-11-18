/// <summary>
/// CameraScript.cs
/// Fichier CS pour gérer la caméra, c'est à dire sa position, son déplacement, etc.
/// http://forum.unity3d.com/threads/rts-camera-script.72045/
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 3.1.0
/// </remarks>

using UnityEngine;

/// <summary>
/// Classe pour gérer la caméra : position, mouvements, positions et angles autorisés ou non, etc.
/// </summary>
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

	/// <summary>
	/// Touche pour activer le positionnement en rase motte
	/// </summary>
	private const string TOUCHE_RAZMOT = "left ctrl";

	/// <summary>
	/// Clic gauche de la souris
	/// </summary>
	private const int CLIC_GAUCHE_SOURIS = 0;
#endregion

#region Attributs privés
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
		 * Touche d : aller à droite
		 */
		if ( Input.GetKey(TOUCHE_DROITE) ){   
			if ( transform.position.z <= -25 ){
				transform.Translate(Vector3.right * Time.deltaTime * vitessePanoramique, Space.Self );  
			}
			return;
		}

		/*
		 * Touche a : aller à gauche
		 */
		if ( Input.GetKey(TOUCHE_GAUCHE) ){
			if ( transform.position.z >= -75 ){
				transform.Translate(Vector3.left * Time.deltaTime * vitessePanoramique, Space.Self );              
			}
			return;
		}
	
		/*
		 * Touche z : aller devant / monter
		 */
		if ( Input.GetKey(TOUCHE_HAUT) ){
			Vector3 posCour = transform.position;
			if ( posCour.x >= 270 && posCour.y <= 452 ){
				transform.Translate(Vector3.up * Time.deltaTime * vitessePanoramique, Space.Self );       
			}
			return;
		}

		/*
		 * Touche s : aller derrière / descendre
		 */
		if ( Input.GetKey(TOUCHE_BAS) ){
			Vector3 posCour = transform.position;
			if ( posCour.x <= 355 && posCour.y >= 290 ){
				transform.Translate(Vector3.down * Time.deltaTime * vitessePanoramique, Space.Self );            
			}
			return;
		}  

		/*
		 * Touche a : rotation gauche
		 */
		if ( Input.GetKey(TOUCHE_ROTATION_GAUCHE) ){
			float angleDeg = Mathf.Rad2Deg*transform.localRotation.y;
			if (angleDeg <= 39 ){
				transform.Rotate(Vector3.up * Time.deltaTime * -rotateSpeed, Space.World);
			}
			return;
		}

		/*
		 * Touche e : rotation droite
		 */
		if ( Input.GetKey(TOUCHE_ROTATION_DROITE) ){
			float angleDeg = Mathf.Rad2Deg*transform.localRotation.y;
			if (angleDeg >= 34 ){
				transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.World);
			}
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
		 * Rase motte : ctrl + souris moeltte bas
		 */
		float zoom = Input.GetAxis(MOLETTE_ZOOM);
		if ( Input.GetKey(TOUCHE_RAZMOT) && zoom < 0 ){
			Vector3 pos = transform.position;
			if ( pos.x <= 422 && pos.y >= 260 && pos.z >= -60 ){
				transform.Translate(0,50*zoom * Time.deltaTime * vitessePanoramique,0, Space.Self );       
				transform.Rotate(50*zoom* Time.deltaTime * rotateSpeed, 0,0, Space.Self);
			}
		}
		/*
		 * Rase ciel : ctrl + souris molette haut
		 */
		//zoom = Input.GetAxis(MOLETTE_ZOOM);
		if ( Input.GetKey(TOUCHE_RAZMOT) && zoom > 0 ){
			Vector3 pos = transform.position;
			if ( pos.x >= 185 && pos.y <= 450 && pos.z >= -60 ){
				transform.Translate(0,50*zoom * Time.deltaTime * vitessePanoramique,0, Space.Self );       
				transform.Rotate(50*zoom* Time.deltaTime * rotateSpeed, 0,0, Space.Self);
			}
		}
		/*
		 * Molette vers le haut : zoom
		 */
		//zoom = Input.GetAxis(MOLETTE_ZOOM);
		float currentFOV = camera.fieldOfView;
		if ( ! Input.GetKey(TOUCHE_RAZMOT) && zoom > 0 && currentFOV >= 1.30 ){
			Camera.main.fieldOfView -= Mathf.Clamp(currentFOV, zoom, Time.deltaTime * vitesseZoom);
			return;
		}
		/*
		 * Molette vers le bas : dézoom
		 */
		//zoom = Input.GetAxis(MOLETTE_ZOOM);
		currentFOV = camera.fieldOfView;
		if ( ! Input.GetKey(TOUCHE_RAZMOT) && zoom < 0 && currentFOV <= 16.81){
			Camera.main.fieldOfView += Mathf.Clamp(currentFOV, zoom, Time.deltaTime * vitesseZoom);
			return;
		}
	}
#endregion

}

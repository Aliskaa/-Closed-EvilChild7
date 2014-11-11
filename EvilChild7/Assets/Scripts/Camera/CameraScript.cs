/// <summary>
/// CameraScript.cs
/// Fichier CS pour gérer la caméra, c'est à dire sa position, son déplacement, etc.
/// http://forum.unity3d.com/threads/rts-camera-script.72045/
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;

/// <summary>
/// Classe pour gérer la caméra : position, mouvements, positions et angles autorisés ou non, etc.
/// </summary>

// TODO : touche purr evenir position normale
// TODO : voir si pas de problème avec les cheats codes (memes touches utilsiées)
// TODO : rotation, d"épalcemetns dans tous les sens, zomm in et out
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
	private const float vitessePan = 10;

	/// <summary>
	/// La vitesse de zoom
	/// </summary>
	private const float vitesseZoom = 1;

	/// <summary>
	/// La vitesse de rotation
	/// </summary>
	private const float vitesseRotation = 1;

	/// <summary>
	/// 
	/// </summary>
	private const float rotateSpeed = 10;
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
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update(){

		// panning     
		if ( Input.GetMouseButton( 0 ) ) {
			transform.Translate(Vector3.right * Time.deltaTime * vitessePan * (Input.mousePosition.x - Screen.width * 0.5f) / (Screen.width * 0.5f), Space.World);
			transform.Translate(Vector3.forward * Time.deltaTime * vitessePan * (Input.mousePosition.y - Screen.height * 0.5f) / (Screen.height * 0.5f), Space.World);
		}
		
		else {
			if ( Input.GetKey("d") ) {             
				transform.Translate(Vector3.right * Time.deltaTime * vitessePan, Space.Self );   
			}
			else if ( Input.GetKey("a") ) {            
				transform.Translate(Vector3.right * Time.deltaTime * -vitessePan, Space.Self );              
			}
			
			if ( Input.GetKey("w") || Input.mousePosition.y >= Screen.height * (1 - edgeScroll) ) {            
				transform.Translate(Vector3.forward * Time.deltaTime * vitessePan, Space.Self );             
			}
			else if ( Input.GetKey("s") || Input.mousePosition.y <= Screen.height * edgeScroll ) {         
				transform.Translate(Vector3.forward * Time.deltaTime * -vitessePan, Space.Self );            
			}  
			
			if ( Input.GetKey("q") || Input.mousePosition.x <= Screen.width * edgeScroll ) {
				transform.Rotate(Vector3.up * Time.deltaTime * -rotateSpeed, Space.World);
			}
			else if ( Input.GetKey("e") || Input.mousePosition.x >= Screen.width * (1 - edgeScroll) ) {
				transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.World);
			}
		}
		
		// zoom in/out
		currentZoom -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 1000 * vitesseZoom;
		
		currentZoom = Mathf.Clamp( currentZoom, zoomRange.x, zoomRange.y );
		
		transform.position = new Vector3( transform.position.x, transform.position.y - (transform.position.y - (positionInitiale.y + currentZoom)) * 0.1f, transform.position.z );
		
		float x = transform.eulerAngles.x - (transform.eulerAngles.x - (rotationInitiale.x + currentZoom * vitesseRotation)) * 0.1f;
		x = Mathf.Clamp( x, zoomAngleRange.x, zoomAngleRange.y );
		
		transform.eulerAngles = new Vector3( x, transform.eulerAngles.y, transform.eulerAngles.z );
	}
#endregion

}

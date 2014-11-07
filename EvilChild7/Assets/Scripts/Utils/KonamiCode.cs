/// <summary>
/// KonamiCode.cs
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;

/// <summary>
/// Konami code. Et ouais.
/// </summary>
public class KonamiCode : MonoBehaviour {

	/* ********* *
	 * Attributs * 
	 * ********** */

#region Attribut privés
	/// <summary>
	/// Les codes pour les touches
	/// </summary>
	private KeyCode[] codesTouches;

	/// <summary>
	/// Un indice
	/// </summary>
	private int indice = 0;

	/// <summary>
	/// Temps écoulé depuis le début de la saisie du code
	/// </summary>
	float tempDepuisDebut = 0f;

	/// <summary>
	/// Temps écoulé depuis la dernière touche saisie
	/// </summary>
	float tempsDepuisDerniereTouche = 0f;
#endregion

#region Attributs publics
	/// <summary>
	/// En millisecondes
	/// </summary>
	public float timeKey = 0f;

	/// <summary>
	/// En millisecondes
	/// </summary>
	public float timeCode = 0f;

	/// <summary>
	/// 
	/// </summary>
	public GameObject receiver;

	/// <summary>
	/// 
	/// </summary>
	public string message;
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes package

	/// <summary>
	/// Routine appelée automatiquement par Unity au lancement du script
	/// </summary>
	void Awake(){
		codesTouches = new KeyCode[]{
			KeyCode.UpArrow,
			KeyCode.UpArrow,
			KeyCode.DownArrow,
			KeyCode.DownArrow,
			KeyCode.LeftArrow,
			KeyCode.RightArrow,
			KeyCode.LeftArrow,
			KeyCode.RightArrow,
			KeyCode.B,
			KeyCode.A
		};
		indice = 0;
		tempDepuisDebut = 0f;
		tempsDepuisDerniereTouche = 0f;
		timeKey = 0f;
		timeCode = 0f;
	}

	/// <summary>
	/// 
	/// </summary>
	//void OnEnable(){
		//if (receiver == null) enabled = false;
	//}

	/// <summary>
	/// Routine appelée automatiquement par Unity à la mise à jour du script
	/// </summary>
	void Update(){
		tempsDepuisDerniereTouche += Time.deltaTime;
		tempDepuisDebut += Time.deltaTime;
/*
		if ( Input.anyKeyDown == false ){
			Debug.Log(">>>>> KO");
			return;
		}
*/
		/*
		if ( Input.GetKeyDown(codesTouches[indice]) == false /*
		    	|| tempDepuisDebut >= timeCode 
		    	|| tempsDepuisDerniereTouche >= timeKey* / ){
			Debug.Log(">>>>> Trop tard");
			indice = 0;
		}
		*/
		if ( Input.GetKeyDown(this.codesTouches[indice]) ){
			if ( indice == 0 ) tempDepuisDebut = 0f;
			tempsDepuisDerniereTouche = 0f;
			indice++;
			Debug.Log(">>>>> Ah ?");
			if ( indice >= codesTouches.Length ){
				Debug.Log(">>>>> KONAMI !!");
				//if ( receiver != null ){
				//	receiver.SendMessage(message, SendMessageOptions.DontRequireReceiver);
				//}
				indice = 0;
			}
		}
	}

#endregion

}

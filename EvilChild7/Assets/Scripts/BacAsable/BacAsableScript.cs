/// <summary>
/// BacAsableScript.cs.
/// Script pour manipuler le mode bac à sable avec entre autres les touches à utiliser
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;

/// <summary>
/// Classe pour manipuler le mode bac à sable avec entre autres les touches à utiliser
/// </summary>
public class BacAsableScript : MonoBehaviour {

	/* ********* *
	 * Attributs * 
	 * ********** */

#region Attribut privés

#endregion

	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes privées

#endregion

#region Méthodes package
	/// <summary>
	/// Routine appelée automatiquement par Unity pour traiter les évènements liés
	/// à la GUI/UI
	/// </summary>
	void OnGUI(){
		Event e = Event.current;
		if ( e!= null && e.isKey & Input.anyKeyDown && e.keyCode.ToString () != "None" ){
			KeyCode touche = e.keyCode;

		}
	}
#endregion

}

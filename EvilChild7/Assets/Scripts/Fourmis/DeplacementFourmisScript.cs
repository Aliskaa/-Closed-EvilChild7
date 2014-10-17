/// <summary>
/// DeplacementFourmisScript.cs
/// Script pour gérer le déplacements des fourmis sur le terrain
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;
using System.Collections;

public class DeplacementFourmisScript : MonoBehaviour {


	/* ******** *
	 * Méthodes *
	 * ******** */

	#region Méthodes privées
	/// <summary>
	/// Récupère la case sur laquelle est posée la fourmis
	/// </summary>
	/// <remarks>Méthode non implémentée</remarks>
	private void GetCaseCourante(){

		// Lancement d'un rayon vers la case sous la fourmis
		RaycastHit hit;
		if ( Physics.Raycast(transform.position, -Vector3.up, out hit, 100.0F) ){
			// Récupération du pmorceau de terrain contenant plusieurs hexagones
			Component morceauTerrain = hit.transform.gameObject.GetComponent<HexagonesVueScript>();
			Debug.Log("Touché :"+morceauTerrain);
			//Debug.DrawLine(transform.position, hit.point);
			// Calcul de la case par rapport à la position locale de la furmis par rapport au morceau
			// TODO
		}

	}

	/// <summary>
	/// Retourne les cases visibles par la fourmis
	/// </summary>
	private void GetCasesVisibles(){

	}

	/// <summary>
	/// Effectue une rotation de la fourmis
	/// </summary>
	/// <param name="rotation">Le sens de rotation</param>
	private void FaireRotation( Rotation rotation ){

	}

	/// <summary>
	/// Fait avancer la fourmis de nbCases cases
	/// </summary>
	/// <param name="nbCases">Le nombre de cases à avancer</param>
	private void Avancer( int nbCases ){

	}
	#endregion

	#region Méthodes package
	// Use this for initialization
	void Start(){
	
	}
	
	// Update is called once per frame
	void Update(){
		GetCaseCourante ();
	}
	#endregion

	#region Méthodes publiques
	/// <summary>
	/// Méthode de déplacement de la fourmis
	/// </summary>
	public void Deplacement(){

	}
	#endregion

}

#region Rotation
/// <summary>
/// Les types de rotations possibles
/// </summary>
public enum Rotation { 
	/// <summary>
	/// Nord : rien, coté de l'hexagone où est la tete de la fourmis
	/// </summary>
	NORD, 
	/// <summary>
	/// Nord Est : coté en haut à droite
	/// </summary>
	NORD_EST,
	/// <summary>
	/// Sud Est : coté en bas à droite
	/// </summary>
	SUD_EST,
	/// <summary>
	/// Sud : à l'opposé de là où regarde la fourmis
	/// </summary>
	SUD,
	/// <summary>
	/// Nord Ouest : coté en haut à gauche
	/// </summary>
	NORD_OUEST,
	/// <summary>
	/// Sud Ouest : coté en bas à gauche
	/// </summary>
	SUD_OUEST
}
#endregion

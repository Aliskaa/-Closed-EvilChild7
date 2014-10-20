/// <summary>
/// DeplacementsFourmisScript.cs
/// Script pour gérer les déplacements des fourmis sur le terrain avec entre autres
/// 	- le déplacement à proprement parler
/// 	- la connaissance du terrain de la fourmis et de la place qu'elle a
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.3.0
/// </remarks>

using UnityEngine;
using System.Collections;

/// <summary>
/// Script pour gérer le déplacement d'une fourmis
/// </summary>
/// <see cref="TerrainManagerScript"/>
/// <see cref="HexagonesVueScript"/>
/// <see cref="HexagoneInfo"/> 
public class DeplacementsFourmisScript : MonoBehaviour {


	/* ********* *
	 * Attributs *
	 * ********* */

#region Attributs privés
	/// <summary>
	/// Le vecteur pour le déplacement
	/// </summary>
	private Vector2 deplacementDirection;

//	/// <summary>
//	/// Le controleur pour la fourmis
//	/// </summary>
	//	private CharacterController controleurPerso;
#endregion

#region Attributs publics
	/// <summary>
	/// La vitesse de déplacement
	/// </summary>
	public float deplacementVitesse;
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes privées
	/// <summary>
	/// Récupère le morceau de terrain, i.e. le bloc, sur lequel est posée la fourmis,
	/// c'est à dire le bloc ayant un pool d'hexagones de meme texture.
	/// </summary>
	/// <returns>Le bloc de terrain en tant que GameObject</returns>
	private GameObject GetBlocCourantAsGO(){

		// Lancement d'un rayon vers la case sous la fourmis
		RaycastHit hit;
		if ( Physics.Raycast(transform.position, -Vector3.up, out hit, 100.0F) ){
			// Récupération du morceau de terrain contenant plusieurs hexagones
			GameObject goBlocTerrain = hit.transform.gameObject;
			//HexagonesVueScript compBlocTerrain =  goBlocTerrain.GetComponent<HexagonesVueScript>();
			//Debug.DrawLine(transform.position, hit.point);
			return goBlocTerrain;
		} else {
			return null;
		}

	}

	/// <summary>
	/// Récupère le morceau de terrain, i.e. le bloc, sur lequel est posée la fourmis,
	/// c'est à dire le bloc ayant un pool d'hexagones de meme texture.
	/// </summary>
	/// <returns>Le bloc de terrain en tant que string, au format JSON</returns>
	private string GetBlocCourantAsString(){
		GameObject goBlocTerrain = GetBlocCourantAsGO();
		return JSONUtils.parseBlocTerrain(goBlocTerrain);
	}

	/// <summary>
	/// Retourne les cases visibles par la fourmis
	/// </summary>
	private void GetCasesVisibles(){
		// TODO
	}

	/// <summary>
	/// Effectue une rotation de la fourmis
	/// </summary>
	/// <param name="rotation">Le sens de rotation</param>
	private void FaireRotation( Rotation rotation ){
		// TODO
	}

	/// <summary>
	/// Fait avancer la fourmis de nbCases cases
	/// </summary>
	/// <param name="nbCases">Le nombre de cases à avancer</param>
	private void Avancer( int nbCases ){
		// TODO
		deplacementDirection.x = deplacementDirection.x + 1 * deplacementVitesse;
		rigidbody.velocity = new Vector3(1*deplacementVitesse, 0, 0);
	}

	/// <summary>
	/// Fait déambuler la fourmis
	/// </summary>
	private void Deambuler(){

		// Réalisation d'une rotation (random)
		// TODO
		FaireRotation(Rotation.AUCUN);

		// Déplacement dans une direction (face à soit)
		// FIXME Déviation sur le long terme
		Avancer(1);

	}

	/// <summary>
	/// Retourne la position courante de l'objet
	/// </summary>
	private void GetPositionCourante(){
		// TODO
		Debug.Log("BlocTerrain :" + GetBlocCourantAsGO());
		Debug.Log("BlocTerrain = " + GetBlocCourantAsString());
		Debug.Log ("Infos = " + Get3dInfos());
	}
#endregion

#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity dès que le script va se lancer
	/// (appel précédant celui de Start()).
	/// </summary>
	void Awake(){
		deplacementDirection = Vector2.zero;
		//controleurPerso = GetComponent<CharacterController>();
	}
	
	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame
	/// </summary>
	void Update(){
		Deplacement();
		GetPositionCourante();
	}
#endregion

#region Méthodes publiques
	/// <summary>
	/// Méthode de déplacement de la fourmis
	/// </summary>
	public void Deplacement(){
		Deambuler();
		// Ou autre type de déplacement (attaque, ...)
	}

	/// <summary>
	/// Retourne les infos 3D de la fourmis à savoir sa rotation en (x,y,z) et sa position en (x,y,z).
	/// String de  la forme :
	/// 
	/// 	{position:{x:XXX,y:YYY,z:ZZZ},rotation:{x:UUU,y:VVV,z:WWW}}
	/// 
	/// </summary>
	/// <returns>Un string au format JSON</returns>
	public string Get3dInfos(){
		return JSONUtils.parseInfos3D(gameObject.transform.position, gameObject.transform.rotation);
	}
#endregion

}

#region Rotation
/// <summary>
/// Les types de rotations possibles
/// </summary>
public enum Rotation { 
	/// <summary>
	/// Pas de rotation
	/// </summary>
	AUCUN,
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

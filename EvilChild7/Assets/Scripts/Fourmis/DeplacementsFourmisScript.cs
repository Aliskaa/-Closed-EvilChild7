﻿/// <summary>
/// DeplacementsFourmisScript.cs
/// Script pour gérer les déplacements des fourmis sur le terrain avec entre autres
/// 	- le déplacement à proprement parler
/// 	- la connaissance du terrain de la fourmis et de la place qu'elle a
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.6.0
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
	/// Flag pour indiquer si l'objet a été rencentré ou pas.
	/// </summary>
	private bool recentrageFait;
#endregion

#region Attributs publics
	/// <summary>
	/// La vitesse de déplacement
	/// </summary>
	public float deplacementVitesse;

	/// <summary>
	/// Flag indiquant que l'objet est en mouvement afin d'appeler entre
	/// autres les méthodes de déplacement
	/// </summary>
	private bool enMouvement;
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
			return hit.transform.gameObject;
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
	/// Effectue une rotation de la fourmis
	/// </summary>
	/// <param name="rotation">Le sens de rotation</param>
	private void FaireRotation( Rotation rotation ){
		// TODO
	}

	/// <summary>
	/// Fait avancer la fourmis de nbCases cases.
	/// Si nbCases est à -1, l'objet ne bouge plus
	/// </summary>
	/// <param name="nbCases">Le nombre de cases à avancer</param>
	private void Avancer( int nbCases ){

		if  (nbCases == -1 ){
			rigidbody.velocity = new Vector3 (0, 0, 0);
			return;
		}

		// TODO : Avancer du nombre de cases voulu
		// Calculer le point d'arrivé, mettre à jour les flags
		// Distance de 5.5 de centre à centre hexagonal ?
		rigidbody.velocity = new Vector3 (1 * deplacementVitesse, 0, 0);

	}

	/// <summary>
	/// Stoppe l'objet en mouvement
	/// </summary>
	private void Stopper(){
		Avancer(-1);
	}

	/// <summary>
	/// Fait déambuler la fourmis
	/// </summary>
	private void Deambuler(){

		// TODO
		// Choix du nombre de cases et de l'angle de rotation
		Rotation rotation = Rotation.AUCUN;
		int nbCases = 1;

		// Réalisation d'une rotation (random)
		FaireRotation(rotation);

		// Déplacement dans une direction (face à soit)
		Avancer(nbCases);

	}

	/// <summary>
	/// Retourne la position courante de l'objet
	/// </summary>
	private void AfficherPositionCourante(){
		Debug.Log("BlocTerrain :" + GetBlocCourantAsGO());
		Debug.Log("BlocTerrain :" + GetBlocCourantAsString());
		Debug.Log("Infos :" + Get3dInfos());
	}

	/// <summary>
	/// Idéalement, dès qu'un objet apparait sur le terrain, il faut le recentrer sur un hexagone.
	/// Pour cela, la classe utilitaire TerrainUtils permet de trouver l'hexagone le plus proche.
	/// Une fois cet hexagone trouvé, il faut recentrer l'objet courant dessus une bonne fois pour toute.
	/// Par la suite, il suffirait de déplacer l'objet courant avec une distance correspondant à
	/// un multiple de la distance entre deux centres d'hexagones afin d'avoir un objet toujours centré.
	/// </summary>
	/// <remarks>
	/// Il vaut mieu appeler cette fonction un minimum de fois car l'opéraiton est gourmande.
	/// </remarks>
	private void Recentrer(){
		HexagoneInfo hexPlusProche = TerrainUtils.hexagonePlusProche(transform.position);
		transform.position = hexPlusProche.positionGobale;
	}
#endregion

#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity au démarrage du script
	/// </summary>
	void Awake(){
		recentrageFait = false;
		enMouvement = true;
	}

	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame
	/// </summary>
	void Update(){
		if (! recentrageFait){
			Recentrer();
			recentrageFait = true;
		}
		if (enMouvement){
			Deambuler();
			//AfficherPositionCourante();
			enMouvement = false;// DEBUG
		} else {
			//Stopper();
			// FIXME
			// Revoir la partie déplacement :
			// Mettre une force qui décroit, force bine ajustée pour avancer
			// d'un multiple de distances de cases
		}
	}
#endregion

#region Méthodes publiques
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

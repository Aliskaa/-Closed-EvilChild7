/// <summary>
/// CatchableScript.cs
/// Script pour gérer les captures d'objets avec les doigts de la main de la leap
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.1
/// </remarks>
/// 
/// 
using UnityEngine;
using System.Collections;

public class CatchableScript : MonoBehaviour {


	
	/* ********* *
	 * Attributs *
	 * ********* */
	
#region Attributs privés
	/// <summary>
	/// Un objet (doit, os...) en contact avec l'objet
	/// </summary>
	private Collider doigt_1;
	/// <summary>
	/// Un autre objet (doit, os...) en contact avec l'objet
	/// </summary>
	private Collider doigt_2;
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes privées
	/// <summary>
	/// Vérification pour les collisions avec les doigts
	/// </summary>
	/// <param name="coll">Le gameobject avec le collider de l'objet entrain en collision avec soit</param>
	private void GererPriseDoigts( GameObject coll ){
		string nomObjetTouche = coll.name;
		TypesObjetsRencontres objetTouche = GameObjectUtils.parseToType (nomObjetTouche);
		// On sort si on est pas en collsion avec un morceau de doigts
		/*
		if ( objetTouche != TypesObjetsRencontres.OS_DOIGT_1
		    && objetTouche != TypesObjetsRencontres.OS_DOIGT_2
		    && objetTouche != TypesObjetsRencontres.OS_DOIGT_3 ) {
			return;
			// TODO Si on a deux objets différents, on prend l'objet courant et le colle à la main
		}
		*/
	}
#endregion


#region Méthodes package-only
	/// <summary>
	/// Routine appellée automatiquement par Unity
	/// lorsqu'il y a une collision, i.e. lorsque des rigidbodys avec des colliders
	/// entrent en contact entre eux.
	/// Si celà arrive, arret du mouvement pour l'objet.
	/// Celui-ci sera remis en mouvement via le script de déplacement
	/// </summary>
	/// <param name="coll">Le collider de l'objet entrain en collision avec soit</param>
	void OnCollisionEnter( Collision coll ){
		GererPriseDoigts(coll.gameObject);
	}
	
	/// <summary>
	/// Routine appellée automatiquement par Unity
	/// lorsqu'il y a une collision, i.e. lorsque des rigidbodys avec des colliders
	/// entrent en contact entre eux ET que ces colliders sont de type "trigger", i.e.
	/// où la collision est détectée (trigger) mais omù la physique ne s'applique pas.
	/// </summary>
	/// <param name="coll">Le collider de l'objet entrain en collision avec soit</param>
	void OnTriggerEnter( Collider coll ){
		GererPriseDoigts(coll.gameObject);
	}
#endregion

}

/// <summary>
/// SubmersibleScript.cs
/// Script pour faire couler un objet / le faire disparaitre en cas de contact avec de l'eau
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

/// <summary>
/// Classe pour faire couler un objet / le faire disparaitre en cas de contact avec de l'eau
/// </summary>
public class SubmersibleScript : MonoBehaviour {
	
	/* ******** *
	 * Méthodes *
	 * ******** */
	
#region Méthodes privées
	/// <summary>
	/// Méthode pour faire couler l'objet. Plouf.
	/// </summary>
	private void FaireCouler(){
		gameObject.transform.Translate(0, -3 * Time.deltaTime, 0);
		Destroy(gameObject);
	}
#endregion


#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity
	/// lorsqu'il y a une collision, i.e. lorsque des rigidbodys avec des colliders
	/// entrent en contact entre eux.
	/// Si celà arrive, arret du mouvement pour l'objet.
	/// Celui-ci sera remis en mouvement via le script de déplacement
	/// </summary>
	/// <param name="coll">Le collider de l'objet entrain en collision avec soit</param>
	void OnCollisionEnter( Collision coll ){
		string nomObjetTouche = coll.gameObject.name;
		TypesObjetsRencontres objetTouche = GameObjectUtils.parseToType(nomObjetTouche);
		if ( objetTouche == TypesObjetsRencontres.EAU
		    || objetTouche == TypesObjetsRencontres.EAU3D ){
			FaireCouler();
		}
	}
	
	/// <summary>
	/// Routine appellée automatiquement par Unity
	/// lorsqu'il y a une collision, i.e. lorsque des rigidbodys avec des colliders
	/// entrent en contact entre eux ET que ces colliders sont de type "trigger", i.e.
	/// où la collision est détectée (trigger) mais omù la physique ne s'applique pas.
	/// </summary>
	/// <param name="coll">Le collider de l'objet entrain en collision avec soit</param>
	void OnTriggerEnter( Collider coll ){
		string nomObjetTouche = coll.gameObject.name;
		TypesObjetsRencontres objetTouche = GameObjectUtils.parseToType(nomObjetTouche);
		if ( objetTouche == TypesObjetsRencontres.EAU
		    || objetTouche == TypesObjetsRencontres.EAU3D ){
			FaireCouler();
		}
	}
#endregion

}

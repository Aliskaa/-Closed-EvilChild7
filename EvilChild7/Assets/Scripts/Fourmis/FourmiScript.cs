/// <summary>
/// FourmiScript.cs
/// Script pour gérer les scarabées
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Classe pour gérer les fourmis
/// </summary>
public class FourmiScript : MonoBehaviour {


	/* ********* *
	 * Attributs *
	 * ********* */
	
#region Attributs privés
	/// <summary>
	/// Les points de vie
	/// </summary>
	// FIXME Voir avec l'IA
	//private int pointsDeVie;
#endregion

#region Attributs publics
	/// <summary>
	/// Le camps de la fourmi qui sortira de l'oeuf
	/// </summary>
	public TypesCamps camps;
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */
	
#region Méthodes privées
	/// <summary>
	/// Mort de l'objet
	/// </summary>
	private void Mourrir(){
		Vector3 position = transform.localPosition;
		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		scriptInvoc.InvoquerObjet(Invocations.PARTICULES_MORT_BESTIOLE, position);
		//MeshRenderer meshRender = gameObject.GetComponent<MeshRenderer>();
	//	meshRender.enabled = false;
		Destroy(gameObject);
	}

	/// <summary>
	/// Pour des besoins de debug
	/// </summary>
	private IEnumerator AttendreEtMourrir(){
		yield return new WaitForSeconds(5);
		Mourrir();
	}
#endregion
	
#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity au démarrage du script
	/// </summary>
	/*
	IEnumerator  Start(){
		yield return StartCoroutine(AttendreEtMourrir());
	}
	*/

	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame.
	/// </summary>
	void Update(){
		//if ( pointsDeVie <= 0 ){ // FIXME Voir avec l'IA
		//	Mourrir();
		//}
	}
#endregion

}

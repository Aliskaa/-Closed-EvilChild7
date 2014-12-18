/// <summary>
/// OeufScript.cs
/// Script pour gérer les oeufs
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 3.0.0
/// </remarks>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Classe pour gérer les oeufs
/// </summary>
public class OeufScript : MonoBehaviour {


	/* ********* *
	 * Attributs *
	 * ********* */
	
#region Attributs privés
	/// <summary>
	/// Le pseudo timer
	/// </summary>
	private float Timer;
#endregion

#region Constantes privées
	/// <summary>
	/// La duréer d'incubation avant éclosion
	/// </summary>
	private const int DUREE_INCUBATION = 20;
#endregion
	
#region Attributs publics
	/// <summary>
	/// Le type de fourmis qui va éclore
	/// </summary>
	public Invocations fourmi;
	
	/// <summary>
	/// Le camps de la fourmi qui sortira de l'oeuf.
	/// Pour des besoins de bouchonnage.
	/// </summary>
	[HideInInspector]
	public TypesCamps camps;
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */
	
#region Méthodes privées
	/// <summary>
	/// Provoque l'éclosion de la fourmis.
	/// Pour celà, va masquer l'oeuf, invoquer une fourmis à sa position, et va ensuite
	/// se détruire.
	/// </summary>
	private void Eclosion(){
		//Debug.Log("Eclosion de "+fourmi);
		Vector3 position = transform.localPosition;
		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		scriptInvoc.InvoquerObjet(Invocations.PARTICULES_ECLOSION, position);
		MeshRenderer meshRender = gameObject.GetComponent<MeshRenderer>();
		meshRender.enabled = false;
		scriptInvoc.InvoquerObjet(fourmi, position);
		Destroy(gameObject);
	}

	/// <summary>
	/// Provoque la mort de l'oeuf et de son contenu.
	/// Pour celà, va masquer l'oeuf, invoquer des particules, et va ensuite
	/// se détruire.
	/// </summary>
	private void Mourir(){
		//Debug.Log("Eclosion de "+fourmi);
		Vector3 position = transform.localPosition;
		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		Invocations i = (InvocateurObjetsScript.MODE_TRASH
		                 ? Invocations.PARTICULES_MORT_BESTIOLE_TRASH
		                 : Invocations.PARTICULES_MORT_BESTIOLE);
		scriptInvoc.InvoquerObjet(i, position);
		MeshRenderer meshRender = gameObject.GetComponent<MeshRenderer>();
		meshRender.enabled = false;
		Destroy(gameObject);
	}

	/// <summary>
	/// Bouchonne cette instance
	/// </summary>
	private void Bouchonner(){
		int campsInt = Random.Range(1, 2);
		camps = (TypesCamps)campsInt;
		int fourmiInt;
		if ( camps == TypesCamps.BLANC ){
			fourmiInt = Random.Range(30,33);
		} else {
			fourmiInt = Random.Range(20,23);
		}
		fourmi = (Invocations)fourmiInt;
	}
#endregion
	
#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity au lancement du script
	/// </summary>
	void Awake(){
		this.Timer = DUREE_INCUBATION;
		//Bouchonner();
		//Debug.Log("Fourmi de type : "+fourmi);
	}
	
	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame.
	/// </summary>
	void Update(){
		this.Timer -= Time.deltaTime;
		if ( this.Timer <= 0 ){
			Eclosion();
		}
	}


#endregion

}

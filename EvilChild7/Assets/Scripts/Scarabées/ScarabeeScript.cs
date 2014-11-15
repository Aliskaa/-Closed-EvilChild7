/// <summary>
/// ScarabeeScript.cs
/// Script pour gérer les scarabées
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 2.0.0
/// </remarks>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Classe pour gérer les scarabées
/// </summary>
public class ScarabeeScript : MonoBehaviour, IAreaction {


	/* ********* *
	 * Attributs *
	 * ********* */
	
#region Attributs privés
	/// <summary>
	/// Les points de vie
	/// </summary>
	// FIXME Voir avec l'IA
	private int chrono;
#endregion

#region Constantes privées
	/// <summary>
	/// Le nombre de secondes avant la mort
	/// </summary>
	private const int NOMBRE_CYCLES_VIEILLISSEMENT = 25;
#endregion

#region Attributs publics
	/// <summary>
	/// Le camps de la fourmi qui sortira de l'oeuf
	/// </summary>
	[HideInInspector]
	public TypesCamps camps;

	/// <summary>
	/// L'IA
	/// </summary>
	public IAappel iaBestiole;
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */
	
#region Méthodes privées
	/// <summary>
	/// Appliquer le vieillissemnt sur le scarabée
	/// </summary>
	private void Vieillir(){
		chrono -= 100;
	}

	/// <summary>
	/// Mort de l'objet
	/// </summary>
	private void Mourrir(){
		Vector3 position = transform.localPosition;
		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		scriptInvoc.InvoquerObjet(Invocations.PARTICULES_MORT_BESTIOLE, position);
		MeshRenderer meshRender = gameObject.GetComponent<MeshRenderer>();
		meshRender.enabled = false;
		Destroy(gameObject);
	}
#endregion
	
#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity au lancement du script
	/// </summary>
	void Awake(){
		camps = TypesCamps.AUCUN;
		chrono = NOMBRE_CYCLES_VIEILLISSEMENT;
		chrono = 2500; // FIXME Voir avec l'IA
		InvokeRepeating("Vieillir", 1 /* départ*/, 1 /*intervalle en secondes*/);
		IAreaction reaction = (IAreaction) this;
		iaBestiole = new IAscarabee(reaction);
	}

	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame.
	/// </summary>
	void Update(){
		if ( chrono <= 0 ){ // FIXME Voir avec l'IA
			Mourrir();
		}
	}
#endregion

#region Méthodes publiques	
	/// <summary>
	/// Bouger the specified direction and nbCases.
	/// </summary>
	/// <param name="direction">Direction.</param>
	/// <param name="nbCases">Nb cases.</param>
	public void bouger(TypesAxes direction, int nbCases){
		Debug.Log("bouger scarabée");
	}
	
	/// <summary>
	/// Deambuler this instance.
	/// </summary>
	public void deambuler(){
		Debug.Log("deambuler scarabée");
	}
	
	/// <summary>
	/// Mourir this instance.
	/// </summary>
	public void mourir(){
		Mourrir();
	}
	
	/// <summary>
	/// Rentrers the base.
	/// </summary>
	public void rentrerBase(){
		Debug.Log("rentrerBase scarabée");
	}
	
	/// <summary>
	/// Posers the pheromones.
	/// </summary>
	public void poserPheromones( bool activation ){
		Debug.Log("poserPheromones scarabée");
		return;
	}
#endregion

}

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
	private int timerDisparition;
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
	/// Une référence vers l'IA du scarabée
	/// </summary>
	[HideInInspector]
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
		timerDisparition -= 100;
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
		timerDisparition = NOMBRE_CYCLES_VIEILLISSEMENT;
		timerDisparition = 2500; // FIXME Voir avec l'IA
		InvokeRepeating("Vieillir", 1 /* départ*/, 1 /*intervalle en secondes*/);
		IAreaction reaction = (IAreaction) this;
		iaBestiole = new IAscarabee(reaction);
	}

	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame.
	/// </summary>
	void Update(){
		if ( timerDisparition <= 0 ){ // FIXME Voir avec l'IA
			Mourrir();
		}
	}
#endregion

#region Méthodes publiques venant de IAreaction
	/// <summary>
	/// Fait bouger le scarabée
	/// </summary>
	/// <param name="direction">Direction à prendre</param>
	/// <param name="nbCases">Nombre de case à avancer</param>
	public void bouger( TypesAxes direction, int nbCases ){
		Debug.Log("bouger scarabée");
		// TODO
		return;
	}
	
	/// <summary>
	/// Fait déambuler le scarabée
	/// </summary>
	public TypesAxes deambuler(){
		Debug.Log("deambuler scarabée");
		// TODO
		return TypesAxes.AUCUN;
	}
	
	/// <summary>
	/// Fait mourrir le scarabée
	/// </summary>
	public void mourir(){
		Mourrir();
	}
	
	/// <summary>
	/// Ne fait rien (!). Obligation d'implémentation d'après
	/// la conception de l'IA
	/// </summary>
	public TypesAxes rentrerBase(){
		return TypesAxes.AUCUN;
	}
	
	/// <summary>
	/// Ne fait rien (!). Obligation d'implémentation d'après
	/// la conception de l'IA
	/// </summary>
	public void poserPheromones( bool activation ){
		return;
	}
#endregion

}

/// <summary>
/// FourmiScript.cs
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
/// Classe pour gérer les fourmis
/// </summary>
public class FourmiScript : MonoBehaviour, IAreaction {


	/* ********* *
	 * Attributs *
	 * ********* */

#region Attributés privés
	/// <summary>
	/// Une référence vers le script de déplacement
	/// </summary>
	private DeplacementsFourmisScript scriptDeplacement;
#endregion

#region Attributs publics
	/// <summary>
	/// Le camps de la fourmi qui sortira de l'oeuf
	/// </summary>
	//public TypesCamps camps;

	/// <summary>
	/// L'IA
	/// </summary>
	[HideInInspector]
	public IAappel iaBestiole;
	
	/// <summary>
	/// Le dernier axe utilisé
	/// </summary>
	[HideInInspector]
	public TypesAxes dernierAxeUtilise;
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */
	
#region Méthodes privées
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
	/// Routine appellée automatiquement par Unity
	/// </summary>
	void Awake(){
		IAreaction reaction = (IAreaction) this;
		DeplacementsFourmisScript dfs = gameObject.GetComponent<DeplacementsFourmisScript>();
		scriptDeplacement = dfs;
		TypesFourmis tf = dfs.typeFourmi;
		switch (tf) {
			case TypesFourmis.COMBATTANTE_BLANCHE:
				iaBestiole = new IAsoldat(TypesObjetsRencontres.COMBATTANTE_BLANCHE, reaction);
				break;
			case TypesFourmis.COMBATTANTE_NOIRE:
				iaBestiole = new IAsoldat(TypesObjetsRencontres.COMBATTANTE_NOIRE, reaction);
				break;
			case TypesFourmis.CONTREMAITRE_BLANCHE:
				iaBestiole = new IAcontremaitre(TypesObjetsRencontres.CONTREMAITRE_BLANCHE, reaction);
				break;
			case TypesFourmis.CONTREMAITRE_NOIRE:
				iaBestiole = new IAcontremaitre(TypesObjetsRencontres.CONTREMAITRE_NOIRE, reaction);
				break;
			case TypesFourmis.GENERALE_BLANCHE:
				iaBestiole = new IAgenerale(TypesObjetsRencontres.GENERALE_BLANCHE, reaction);
				break;
			case TypesFourmis.GENERALE_NOIRE:
				iaBestiole = new IAgenerale(TypesObjetsRencontres.GENERALE_NOIRE, reaction);
				break;
			case TypesFourmis.OUVRIERE_BLANCHE:
				iaBestiole = new IAouvriere(TypesObjetsRencontres.OUVRIERE_BLANCHE, reaction);
				break;
			case TypesFourmis.OUVRIERE_NOIRE:
				iaBestiole = new IAouvriere(TypesObjetsRencontres.OUVRIERE_NOIRE, reaction);
				break;
		}
	}

	/// <summary>
	/// Routine appellée automatiquement par Unity au démarrage du script
	/// </summary>
	/*
	IEnumerator  Start(){
		yield return StartCoroutine(AttendreEtMourrir());
	}
	*/
#endregion

#region Méthodes publiques venant de IAreaction
	/// <summary>
	/// Mort "visible" de l'objet. Mort à cause d'une bestiole ou d'un objet.
	/// </summary>
	public void Mourrir(){
		Vector3 position = transform.localPosition;
		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		scriptInvoc.InvoquerObjet(Invocations.PARTICULES_MORT_BESTIOLE, position);
		//MeshRenderer meshRender = gameObject.GetComponent<MeshRenderer>();
		//meshRender.enabled = false;
		Destroy(gameObject);
	}

	/// <summary>
	/// Mort "visible" de l'objet. Mort par noyade.
	/// </summary>
	public void Noyade(){
		Destroy(gameObject);
	}

	/// <summary>
	/// Fait bouger la fourmi.
	/// </summary>
	/// <param name="direction">Direction de la fourmi.</param>
	/// <param name="nbCases">Nombre de cases à avancer.</param>
	public void bouger( TypesAxes direction, int nbCases ){

		if ( scriptDeplacement.objectifAtteint ){
		
			dernierAxeUtilise = direction;

			// Récupération de la rotation courante afin
			// d'en obtenir un indice
			TypesRotations rotationCourante = scriptDeplacement.orientationCourante;
			int indiceRotationCourante;
			switch(rotationCourante){
				case TypesRotations.NORD:
					indiceRotationCourante = 1;
					break;
				case TypesRotations.NORD_EST:
					indiceRotationCourante = 2;
					break;
				case TypesRotations.SUD_EST:
					indiceRotationCourante = 3;
					break;
				case TypesRotations.SUD:
					indiceRotationCourante = 4;
					break;
				case TypesRotations.SUD_OUEST:
					indiceRotationCourante = 5;
					break;
				case TypesRotations.NORD_OUEST:
					indiceRotationCourante = 6;
					break;
				default:
					indiceRotationCourante = 0;
					break;
			}

			// Calcul de la nouvelle rotation avec l'indice trouvé précedemment
			// et la direction demandée
			int nouvelleRotationInt = (indiceRotationCourante-1)+(int)direction;
			nouvelleRotationInt = ( nouvelleRotationInt > 6 ? nouvelleRotationInt % 6 : nouvelleRotationInt);
			TypesRotations nouvelleRotation;
			switch(nouvelleRotationInt){
				case 1:
					nouvelleRotation = TypesRotations.NORD;
					break;
				case 2:
					nouvelleRotation = TypesRotations.NORD_EST;
					break;
				case 3:
					nouvelleRotation = TypesRotations.SUD_EST;
					break;
				case 4:
					nouvelleRotation = TypesRotations.SUD;
					break;
				case 5:
					nouvelleRotation = TypesRotations.SUD_OUEST;
					break;
				case 6:
					nouvelleRotation = TypesRotations.NORD_OUEST;
					break;
				default:
					nouvelleRotation = TypesRotations.AUCUN;
					break;
			}

			// Action ! On se déplace
			scriptDeplacement.FaireRotation(nouvelleRotation);
			scriptDeplacement.Avancer(nbCases);

		}
		
	}

	/// <summary>
	/// Provoque la déambulation de la fourmi
	/// </summary>
	public TypesAxes deambuler(){
		TypesAxes axe = (TypesAxes) Random.Range(1, 6);
		dernierAxeUtilise = axe;
		bouger(axe, 1);
		return axe;
	}

	/// <summary>
	/// Provuqe la mort de la fourmi
	/// </summary>
	public void mourir(){
		Mourrir();
	}

	/// <summary>
	/// Fait rentrer la fourmi à la base
	/// </summary>
	public TypesAxes rentrerBase(){
		Debug.Log("rentrerBase");
		// TODO
		return TypesAxes.AUCUN;
	}

	/// <summary>
	/// Active le dépot de phéromones
	/// </summary>
	public void poserPheromones( bool activation ){
		scriptDeplacement.activerDepotPheromone = activation;
	}
#endregion

}

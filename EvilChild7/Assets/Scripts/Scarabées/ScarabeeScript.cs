/// <summary>
/// ScarabeeScript.cs
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

	/// <summary>
	/// Référence vers le script de déplacement de l'objet
	/// </summary>
	private DeplacementsScarabeeScript scriptDeplacement;
#endregion

#region Constantes privées
	/// <summary>
	/// Le nombre de secondes avant la mort
	/// </summary>
	private const int NOMBRE_CYCLES_VIEILLISSEMENT = 25;
#endregion

#region Attributs publics
	/// <summary>
	/// Le camps du scarabée
	/// </summary>
	[HideInInspector]
	public TypesCamps camps;

	/// <summary>
	/// Une référence vers l'IA du scarabée
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
		Destroy(gameObject);
	}
#endregion
	
#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity au lancement du script
	/// </summary>
	void Awake(){
		scriptDeplacement = (DeplacementsScarabeeScript)gameObject.GetComponent<DeplacementsScarabeeScript> ();
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
	/// Fait déambuler le scarabée
	/// </summary>
	public TypesAxes deambuler(){
		TypesAxes axe = (TypesAxes) Random.Range(1, 6);
		dernierAxeUtilise = axe;
		bouger(axe, 1);
		return axe;
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

#region Autres méthodes publiques
	/// <summary>
	/// Mort "visible" de l'objet. Mort par noyade.
	/// </summary>
	public void Noyade(){
		Destroy(gameObject);
	}
#endregion
}

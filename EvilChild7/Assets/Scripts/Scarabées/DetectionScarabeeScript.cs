/// <summary>
/// DetectionScarabeeScript.cs
/// Script pour gérer les détections d'objets par le scarabée.
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
/// Classe pour gérer les détections d'objets par le scarabée
/// </summary>
public class DetectionScarabeeScript : MonoBehaviour {
	
	
	/* ********* *
	 * Attributs *
	 * ********* */
	
	#region Attributs privés
	/// <summary>
	/// Référence vers le script de déplacement de l'objet
	/// </summary>
	private DeplacementsScarabeeScript scriptDeplacement;
	
	/// <summary>
	/// Référence vers le script du scarabée
	/// </summary>
	private ScarabeeScript scarabeeScript;
	
	/// <summary>
	/// Une liste d'objets qui ont été vus et qui n'ont pas encore été signalé à "l'IA"
	/// </summary>
	private List<Cible> objetsDetectes;
	#endregion
	
	#region Constantes privées
	/// <summary>
	/// La distance pour aller du centre d'une case à un autre
	/// </summary>
	private const int DISTANCE_CASE = 5;
	
	/// <summary>
	/// La visée max du scarabée
	/// </summary>
	private const int VISEE = 2;
	
	/// <summary>
	/// L'intervalle de temps en secondes au bout duquel la vue et l'odorat doivent etre appliqués
	/// </summary>
	private const int INTERVALLE_DETECTION = 1;
	#endregion
	
	/* ******** *
	 * Méthodes *
	 * ******** */
	
	#region Méthodes privées
	/// <summary>
	/// Méthode pour détecter des objets sur un axe donné.
	/// Equivaut à la vue et à l'odorat
	/// </summary>
	/// <param name="direction">La direction de détection.</param>
	/// <param name="visee">La longueur du raydon de visée</param>
	private void VoirEtSentir( TypesAxes direction, float visee ){
		
		// Convertion direction <-> vecteur
		Color debugRayColor = Color.black;
		Vector3 directionV;
		switch ( direction ){
		case TypesAxes.DEVANT:
			debugRayColor = Color.blue;
			directionV = Vector3.forward;
			break;
		case TypesAxes.DERRIERE:
			debugRayColor = Color.green;
			directionV = Vector3.back;
			break;
		case TypesAxes.DERRIERE_DROITE:
			debugRayColor = Color.red;
			directionV = Vector3.back + Vector3.right;
			break;
		case TypesAxes.DERRIERE_GAUCHE:
			debugRayColor = Color.yellow;
			directionV = Vector3.back + Vector3.left;
			break;
		case TypesAxes.DEVANT_DROITE:
			debugRayColor = Color.white;
			directionV = Vector3.forward + Vector3.right;
			break;
		case TypesAxes.DEVANT_GAUCHE:
			debugRayColor = Color.magenta;
			directionV = Vector3.forward + Vector3.left;
			break;
		case TypesAxes.GAUCHE:
			directionV = Vector3.left;
			break;
		case TypesAxes.DROITE:
			directionV = Vector3.right;
			break;
		case TypesAxes.DESSUS:
			directionV = Vector3.up;
			break;
		case TypesAxes.DESSOUS:
			directionV = Vector3.down;
			break;
		default:
			directionV = Vector3.forward;
			break;
		}
		
		// Création du rayon qui va partir dans le bonne direction
		Vector3 positionRayon = transform.position;
		positionRayon.y += 0.5f;
		
		Vector3 directionRayon = transform.TransformDirection(directionV);
		directionRayon.Normalize();
		
		Ray charles = new Ray(positionRayon, directionRayon);
		RaycastHit hit;
		
		/*
		 * Détection en lançant un rayon calculant précédemment en fonction d'une direction
		 */
		//if ( Physics.Raycast(charles.origin, charles.direction, out hit, visee*30) ){ // DEBUG
		if ( Physics.Raycast(charles.origin, charles.direction, out hit, visee) ){
			
			/*
			 * Etape 1 : Récupération des données de l'objet repéré
			 */
			GameObject objetRencontre = hit.transform.gameObject;
			string nomObjetProche = objetRencontre.name;
			TypesObjetsRencontres objetSurChemin = GameObjectUtils.parseToType(nomObjetProche);
			//Debug.Log("Détecté : "+objetSurChemin);
			
			Debug.DrawLine(charles.origin, hit.point, debugRayColor);
			
			int codeObjet = (int) objetSurChemin;
			IAobjet iaObjet = null;
			Cible objetRepere = null;
			
			/*
			 * Etape 2 : Récuépration d'autres donénes utiles en fonction de l'objet repéré.
			 * Ces données utiles seront à transmettre à l'IA
			 */
			
			// L'objet vu est une fourmi
			if ( (codeObjet >= (int)TypesObjetsRencontres.OUVRIERE_NOIRE 
			      && codeObjet <= (int)TypesObjetsRencontres.GENERALE_NOIRE)
			    || (codeObjet >= (int)TypesObjetsRencontres.OUVRIERE_BLANCHE
			    && codeObjet <=  (int)TypesObjetsRencontres.GENERALE_BLANCHE) ){
				
				FourmiScript fs = objetRencontre.GetComponent<FourmiScript>();
				iaObjet = fs.iaBestiole;
				objetRepere = new Cible(hit.distance, iaObjet, direction, objetSurChemin);
				objetsDetectes.Add(objetRepere);
				return;
				
			} 
			
			// L'objet vu est une des reines
			if ( objetSurChemin == TypesObjetsRencontres.REINE_NOIRE 
			    || objetSurChemin == TypesObjetsRencontres.REINE_BLANCHE ){
				
				ReineScript rs = objetRencontre.GetComponent<ReineScript>();
				iaObjet = rs.iaReine;
				objetRepere = new Cible(hit.distance, iaObjet, direction, objetSurChemin);
				objetsDetectes.Add(objetRepere);
				return;
				
			}
			
			// L'objet rencontré et de l'eau ou un caillou
			if ( (direction == TypesAxes.DEVANT
			      /*|| direction == TypesAxes.DEVANT_DROITE 
			      || direction == TypesAxes.DEVANT_GAUCHE*/)
			    && ( objetSurChemin == TypesObjetsRencontres.EAU 
			    || objetSurChemin == TypesObjetsRencontres.EAU3D
			    || objetSurChemin == TypesObjetsRencontres.CAILLOU
			    || objetSurChemin == TypesObjetsRencontres.TRES_GROS_CAILLOUX
			    || objetSurChemin == TypesObjetsRencontres.PETIT_CAILLOU)){
				scriptDeplacement = gameObject.GetComponent<DeplacementsScarabeeScript>();
				scriptDeplacement.StopperParCollision( objetSurChemin );
			}
			
		} // End if ( Physics.Raycast(charles.origin, charles.direction, out hit, visee) )
		
	}
	
	/// <summary>
	/// Signale les objets repérés à l'IA.
	/// Vide la liste stockant ces objets une fois l'IA avertie.
	/// </summary>
	private void SignalerDetections(){
		IAappel aTartes = scarabeeScript.iaBestiole;
		if (objetsDetectes.Count > 0) {
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("Objets detectés :\n");
			foreach (Cible c in objetsDetectes) {
				sb.Append ("\t").Append (c).Append ("\n");
			}
			//Debug.Log(sb.ToString());
			aTartes.signaler(objetsDetectes);
			objetsDetectes = new List<Cible> ();
		} else {
			//Debug.Log("Rien de détecté");
			objetsDetectes = new List<Cible> ();
			aTartes.signaler(objetsDetectes);
		}
	}
	
	/// <summary>
	/// Coroutine pour que le scarabe voit et sente dans les 6 directions
	/// </summary>
	/// <returns>Un enumérateur</returns>
	/// <param name="intervalle">L'intervalle de temps (en secondes) au bout duquel
	/// la bestiole doit voir et sentir autour d'elle</param>
	private IEnumerator VoirEtSentir( int intervalle ){
		
		while(true){
			
			//Debug.Log("VoirEtSentir() : "+System.DateTime.Now.ToString("dd/mm/yy HH:mm"));
			VoirEtSentir(TypesAxes.DEVANT, VISEE * DISTANCE_CASE);
			VoirEtSentir(TypesAxes.DEVANT_DROITE, VISEE * DISTANCE_CASE);
			VoirEtSentir(TypesAxes.DEVANT_GAUCHE, VISEE * DISTANCE_CASE);
			VoirEtSentir(TypesAxes.DERRIERE, VISEE * DISTANCE_CASE);
			VoirEtSentir(TypesAxes.DERRIERE_DROITE, VISEE * DISTANCE_CASE);
			VoirEtSentir(TypesAxes.DERRIERE_GAUCHE, VISEE * DISTANCE_CASE);
			SignalerDetections();
			
			yield return new WaitForSeconds(intervalle);
			
		}
		
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
		if (objetTouche == TypesObjetsRencontres.BLOC_TERRAIN)			 return;
		if (objetTouche == TypesObjetsRencontres.FOND) 	 				 return;
		if (objetTouche == TypesObjetsRencontres.PHEROMONES_CM_BLANCHE)	 return;
		if (objetTouche == TypesObjetsRencontres.PHEROMONES_CM_NOIRE) 	 return;
		if (objetTouche == TypesObjetsRencontres.PHEROMONES_OUV_BLANCHE) return;
		if (objetTouche == TypesObjetsRencontres.PHEROMONES_OUV_NOIRE) 	 return;
		//Debug.Log ("Collision OnCollisionEnter avec : " + objetTouche + " / " + nomObjetTouche);
		scriptDeplacement.StopperParCollision(objetTouche);
	}
	
	/// <summary>
	/// Routine appellée automatiquement par Unity
	/// lorsqu'il y a une collision, i.e. lorsque des rigidbodys avec des colliders
	/// entrent en contact entre eux ET que ces colliders sont de type "trigger", i.e.
	/// où la collision est détectée (trigger) mais omù la physique ne s'applique pas.
	/// </summary>
	/// <param name="coll">Le collider de l'objet entrain en collision avec soit</param>
	void OnTriggerEnter( Collider coll ){
		//Debug.Log ("Collision OnTriggerEnter avec : " + coll);
		string nomObjetTouche = coll.gameObject.name;
		TypesObjetsRencontres objetTouche = GameObjectUtils.parseToType (nomObjetTouche);
		if ( objetTouche == TypesObjetsRencontres.EAU3D 
		    || objetTouche == TypesObjetsRencontres.EAU ){
			ScarabeeScript ss = gameObject.GetComponent<ScarabeeScript>();
			ss.Noyade();
		}
	}
	
	/// <summary>
	/// Routine appellée automatiquement par Unity au lancement du script
	/// </summary>
	void Awake(){
		scriptDeplacement = (DeplacementsScarabeeScript) gameObject.GetComponent<DeplacementsScarabeeScript>();
		scarabeeScript = (ScarabeeScript) gameObject.GetComponent<ScarabeeScript>();
		objetsDetectes = new List<Cible>();
	}
	
	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame.
	/// Va vérifier ce qu'il y a autour du scarabee dans les 6 sens possibles.
	/// </summary>
	/*
	void Update(){
		VoirEtSentir(TypesAxes.DEVANT, VISEE * DISTANCE_CASE);
		VoirEtSentir(TypesAxes.DEVANT_DROITE, VISEE * DISTANCE_CASE);
		VoirEtSentir(TypesAxes.DEVANT_GAUCHE, VISEE * DISTANCE_CASE);
		VoirEtSentir(TypesAxes.DERRIERE, VISEE * DISTANCE_CASE);
		VoirEtSentir(TypesAxes.DERRIERE_DROITE, VISEE * DISTANCE_CASE);
		VoirEtSentir(TypesAxes.DERRIERE_GAUCHE, VISEE * DISTANCE_CASE);
		SignalerDetections();
	}
	*/
	
	/// <summary>
	/// Routine appellée automatiquement par Unity au démrrage du script.
	/// Va vérifier ce qu'il y a autour de le scarabée dans les 6 sens possibles/
	/// </summary>
	void Start(){
		StartCoroutine(VoirEtSentir(INTERVALLE_DETECTION));
	}
	
}
#endregion


/// <summary>
/// DetectionFourmisScript.cs
/// Script pour gérer les détections d'objets avec l'odorat et la vue ains que les collisions des fourmis
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 5.0.0
/// </remarks>

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Classe pour gérer les détections d'objets avec l'odorat et la vue ains que les collisions des fourmis
/// </summary>
public class DetectionFourmisScript : MonoBehaviour {
	
	
	/* ********* *
	 * Attributs *
	 * ********* */
	
	#region Attributs privés
	/// <summary>
	/// Référence vers le script de déplacement de l'objet
	/// </summary>
	private DeplacementsFourmisScript scriptDeplacement;
	
	/// <summary>
	/// Une lite d'objets qui ont été vus et qui n'ont pas encore été signalé à "l'IA"
	/// </summary>
	private List<Cible> objetsDetectes;
	
	/// <summary>
	/// La visée qui est appliquée à la fourmis, en fonction de sa caste
	/// </summary>
	private int viseeAppliquee;
	#endregion
	
	#region Constantes privées
	/// <summary>
	/// La distance pour aller du centre d'une case à un autre
	/// </summary>
	private const int DISTANCE_CASE = 5;
	
	/// <summary>
	/// L'intervalle de temps en secondes au bout duquel la vue et l'odorat doivent etre appliqués
	/// </summary>
	private const int INTERVALLE_DETECTION = 1;
	#endregion
	
	#region Attributs publics
	/// <summary>
	/// Le type de fourmi auquel doit s'appliquer le script
	/// </summary>
	public TypesFourmis typeFourmi;
	#endregion
	
	/* ******** *
	 * Méthodes *
	 * ******** */
	
	#region Méthodes privées
	/// <summary>
	/// Méthode pour détecter des objets sur un axe donné.
	/// Equivaut à la vue et à l'odorat
	/// </summary>
	/// <remarks>Pour détecter la reine, qui est un GO plus grand que els autres tels
	/// que les phéromones, les fourmis ou le sbétises, il faut lancer des rayons assez hauts
	/// pour ne pas détecter les petits objets comme les phéromones</remarks>
	/// <param name="direction">La direction de détection.</param>
	/// <param name="visee">La longueur du raydon de visée</param>
	/// <param name="offsetY">Un offset à apliquer sur le rayon lancé, offset vertical</param>
	private void VoirEtSentir( TypesAxes direction, float visee, float offsetY ){
		
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
		positionRayon.y += offsetY;
		
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
			
			// L'objet sur le chemin est un scarabéé
			if ( objetSurChemin == TypesObjetsRencontres.SCARABEE ){
				ScarabeeScript ss = objetRencontre.GetComponent<ScarabeeScript>();
				iaObjet = ss.iaBestiole;
				objetRepere = new Cible(hit.distance, iaObjet, direction, objetSurChemin);
				objetsDetectes.Add(objetRepere);
				return;
			} 
			
			// L'objet rencontré est une phéromone noire (cm ou ouv) ou blanche (cm ou ouv)
			if ( objetSurChemin == TypesObjetsRencontres.PHEROMONES_CM_BLANCHE
			    || objetSurChemin == TypesObjetsRencontres.PHEROMONES_CM_NOIRE
			    || objetSurChemin == TypesObjetsRencontres.PHEROMONES_OUV_BLANCHE
			    || objetSurChemin == TypesObjetsRencontres.PHEROMONES_OUV_NOIRE){
				objetRepere = new Cible(hit.distance, objetRencontre, direction, objetSurChemin);
				objetsDetectes.Add(objetRepere);
				return;
			} 
			
			// L'objet rencontré est de la nourriture
			if ( codeObjet >= ((int)TypesObjetsRencontres.BONBON_ANGLAIS_BLEU) 
			    && codeObjet <= ((int)TypesObjetsRencontres.BONBON_VERT) ){
				
				NourrituresScript ns = objetRencontre.GetComponent<NourrituresScript>();
				iaObjet = ns.iaNourriture;
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
				DeplacementsFourmisScript dfs = gameObject.GetComponent<DeplacementsFourmisScript>();
				dfs.StopperParCollision( objetSurChemin );
			}
			
		} // End if ( Physics.Raycast(charles.origin, charles.direction, out hit, visee) )
		
	}
	
	/// <summary>
	/// Signale les objets repérés à l'IA.
	/// Vide la liste stockant ces objets une fois l'IA avertie.
	/// </summary>
	private void SignalerDetections(){
		FourmiScript fs = gameObject.GetComponent<FourmiScript>();
		IAappel aTartes = fs.iaBestiole;
		if (objetsDetectes.Count > 0) {
			/*
			System.Text.StringBuilder sb = new System.Text.StringBuilder ();
			sb.Append("Objets detectés :\n");
			foreach (Cible c in objetsDetectes) {
				sb.Append ("\t").Append (c.getType()).Append ("\n");
			}
			Debug.Log(sb.ToString());
			*/
			aTartes.signaler(objetsDetectes);
			objetsDetectes = new List<Cible> ();
		} else {
			//Debug.Log("Rien de détecté");
			objetsDetectes = new List<Cible> ();
			aTartes.signaler(objetsDetectes);
		}
	}
	
	/// <summary>
	/// Initialise les variables qui dépendent de la caste de la fourmis
	/// </summary>
	private void InitialiserVariablesFourmi(){
		switch ( typeFourmi ){
		case TypesFourmis.COMBATTANTE_NOIRE:
			viseeAppliquee = (int) ViseesFourmis.VISEE_COMBATTANTE;
			break;
		case TypesFourmis.CONTREMAITRE_NOIRE:
			viseeAppliquee = (int) ViseesFourmis.VISEE_CONTREMAITRE;	
			break;
		case TypesFourmis.GENERALE_NOIRE:
			viseeAppliquee = (int) ViseesFourmis.VISEE_GENERALE;	
			break;
		case TypesFourmis.OUVRIERE_NOIRE:
			viseeAppliquee = (int) ViseesFourmis.VISEE_OUVRIERE;	
			break;
		case TypesFourmis.COMBATTANTE_BLANCHE:
			viseeAppliquee = (int) ViseesFourmis.VISEE_COMBATTANTE;
			break;
		case TypesFourmis.CONTREMAITRE_BLANCHE:
			viseeAppliquee = (int) ViseesFourmis.VISEE_CONTREMAITRE;	
			break;
		case TypesFourmis.GENERALE_BLANCHE:
			viseeAppliquee = (int) ViseesFourmis.VISEE_GENERALE;	
			break;
		case TypesFourmis.OUVRIERE_BLANCHE:
			viseeAppliquee = (int) ViseesFourmis.VISEE_OUVRIERE;	
			break;
		}
		//Debug.Log("Initialisation d'une fourmi "+typeFourmi+" avec une visee de "+viseeAppliquee);
	}
	
	/// <summary>
	/// Coroutine pour que la fourmi voit et sente dans les 6 directions
	/// </summary>
	/// <returns>Un enumérateur</returns>
	/// <param name="intervalle">L'intervalle de temps (en secondes) au bout duquel
	/// la bestiole doit voir et sentir autour d'elle</param>
	private IEnumerator VoirEtSentir( int intervalle ){
		
		while(true){
			
			//Debug.Log("VoirEtSentir() : "+System.DateTime.Now.ToString("dd/mm/yy HH:mm"));
			
			const float hauteurDetection = 0.5f;
			const float hauteurDetectionReine = 1.5f;
			
			// FIXME : Bourrin comme c'est pas permi...
			// Détecter presque tout
			VoirEtSentir(TypesAxes.DEVANT, viseeAppliquee * DISTANCE_CASE, hauteurDetection);
			VoirEtSentir(TypesAxes.DEVANT_DROITE, viseeAppliquee * DISTANCE_CASE, hauteurDetection);
			VoirEtSentir(TypesAxes.DEVANT_GAUCHE, viseeAppliquee * DISTANCE_CASE, hauteurDetection);
			VoirEtSentir(TypesAxes.DERRIERE, viseeAppliquee * DISTANCE_CASE, hauteurDetection);
			VoirEtSentir(TypesAxes.DERRIERE_DROITE, viseeAppliquee * DISTANCE_CASE, hauteurDetection);
			VoirEtSentir(TypesAxes.DERRIERE_GAUCHE, viseeAppliquee * DISTANCE_CASE, hauteurDetection);
			
			// Détecter la reine (sans détecter les phéromones qui peuvent etre entre l'objet courant et la reine
			VoirEtSentir(TypesAxes.DEVANT, viseeAppliquee * DISTANCE_CASE, hauteurDetectionReine);
			VoirEtSentir(TypesAxes.DEVANT_DROITE, viseeAppliquee * DISTANCE_CASE, hauteurDetectionReine);
			VoirEtSentir(TypesAxes.DEVANT_GAUCHE, viseeAppliquee * DISTANCE_CASE, hauteurDetectionReine);
			VoirEtSentir(TypesAxes.DERRIERE, viseeAppliquee * DISTANCE_CASE, hauteurDetectionReine);
			VoirEtSentir(TypesAxes.DERRIERE_DROITE, viseeAppliquee * DISTANCE_CASE, hauteurDetectionReine);
			VoirEtSentir(TypesAxes.DERRIERE_GAUCHE, viseeAppliquee * DISTANCE_CASE, hauteurDetectionReine);
			
			// Signaler à l'IA ce que l'on a vu
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
			FourmiScript fs = gameObject.GetComponent<FourmiScript>();
			fs.Noyade();
		}
	}
	
	/// <summary>
	/// Routine appellée automatiquement par Unity au lancement du script
	/// </summary>
	void Awake(){
		scriptDeplacement = (DeplacementsFourmisScript) gameObject.GetComponent(typeof(DeplacementsFourmisScript));
		objetsDetectes = new List<Cible>();
		InitialiserVariablesFourmi();
	}
	
	/// <summary>
	/// Routine appellée automatiquement par Unity au démrrage du script.
	/// Va vérifier ce qu'il y a autour de la fourmis dans les 6 sens possibles/
	/// </summary>
	void Start(){
		StartCoroutine(VoirEtSentir(INTERVALLE_DETECTION));
	}
	
	/// <summary>
	/// Routine appellée par Uinty à chaque frame
	/// </summary>
	/*
	void Update(){

		const float hauteurDetection = 0.5f;
		const float hauteurDetectionReine = 1.5f;
		
		// FIXME : Bourrin comme c'est pas permi...
		// Détecter presque tout
		VoirEtSentir(TypesAxes.DEVANT, viseeAppliquee * DISTANCE_CASE, hauteurDetection);
		VoirEtSentir(TypesAxes.DEVANT_DROITE, viseeAppliquee * DISTANCE_CASE, hauteurDetection);
		VoirEtSentir(TypesAxes.DEVANT_GAUCHE, viseeAppliquee * DISTANCE_CASE, hauteurDetection);
		VoirEtSentir(TypesAxes.DERRIERE, viseeAppliquee * DISTANCE_CASE, hauteurDetection);
		VoirEtSentir(TypesAxes.DERRIERE_DROITE, viseeAppliquee * DISTANCE_CASE, hauteurDetection);
		VoirEtSentir(TypesAxes.DERRIERE_GAUCHE, viseeAppliquee * DISTANCE_CASE, hauteurDetection);
		
		// Détecter la reine (sans détecter les phéromones qui peuvent etre entre l'objet courant et la reine
		VoirEtSentir(TypesAxes.DEVANT, viseeAppliquee * DISTANCE_CASE, hauteurDetectionReine);
		VoirEtSentir(TypesAxes.DEVANT_DROITE, viseeAppliquee * DISTANCE_CASE, hauteurDetectionReine);
		VoirEtSentir(TypesAxes.DEVANT_GAUCHE, viseeAppliquee * DISTANCE_CASE, hauteurDetectionReine);
		VoirEtSentir(TypesAxes.DERRIERE, viseeAppliquee * DISTANCE_CASE, hauteurDetectionReine);
		VoirEtSentir(TypesAxes.DERRIERE_DROITE, viseeAppliquee * DISTANCE_CASE, hauteurDetectionReine);
		VoirEtSentir(TypesAxes.DERRIERE_GAUCHE, viseeAppliquee * DISTANCE_CASE, hauteurDetectionReine);
		
		// Signaler à l'IA ce que l'on a vu
		SignalerDetections();

	}
	*/
	#endregion
	
}

#region VitessesFourmis
/// <summary>
/// Les visées des différentes castes de fourmis (vue et odorat confondues)
/// </summary>
public enum ViseesFourmis : int { 
	/// <summary>
	/// La visée de l'ouvrière, à 5/4 = 1
	/// </summary>
	VISEE_OUVRIERE = 1,
	/// <summary>
	/// La visée de la contremaitre, à 20/4 = 5
	/// </summary>
	VISEE_CONTREMAITRE = 2,
	/// <summary>
	/// La visée de la combattante, à 5/4 = 1
	/// </summary>
	VISEE_COMBATTANTE = 1,
	/// <summary>
	/// La visée de la générale, à 20/4 = 5
	/// </summary>
	VISEE_GENERALE = 2,
}
#endregion

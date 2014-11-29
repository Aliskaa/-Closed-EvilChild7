/// <summary>
/// DeplacementsFourmisScript.cs
/// Script pour gérer les déplacements des fourmis sur le terrain avec entre autres
/// 	- le déplacement à proprement parler
/// 	- la connaissance du terrain de la fourmis et de la place qu'elle a
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 4.1.0
/// </remarks>

using UnityEngine;
using System.Collections;

#region DeplacementsFourmisScript
/// <summary>
/// Script pour gérer le déplacement d'une fourmis
/// </summary>
public class DeplacementsFourmisScript : MonoBehaviour {


	/* ********* *
	 * Attributs *
	 * ********* */

#region Attributs privés
	/// <summary>
	/// Référence vers un script permettant de faire apapraitre des objets
	/// </summary>
	private InvocateurObjetsScript scriptInvocation;

	/// <summary>
	/// Le type de phéromones à invoquer selon la fourmi
	/// </summary>
	private Invocations typePheromone;

	/// <summary>
	/// La position que doit atteindre l'objet, i.e. le centre d'une case.
	/// Position mise à jour régulièrement.
	/// </summary>
	private Vector3 positionAatteindre;

	/// <summary>
	/// Distance de la translation à effectuer de la positon courante de départ
	/// à la positon finale à atteindre
	/// </summary>
	private float distanceTranslation;
	
	/// <summary>
	/// Le début au début de la translation
	/// </summary>
	private float tempsDebutTranslation;

	/// <summary>
	/// La vitesse qui est appliquée à la fourmis, en fonction de sa caste
	/// </summary>
	private int vitesseAppliquee;

	/// <summary>
	/// Flag indiquant que la fourmi s'est prise un obstacle de type bétise
	/// </summary>
	private bool collisionFrontaleBetise;

	/// <summary>
	/// Une références vers l'objet gérant le terrain
	/// </summary>
	private TerrainManagerScript scriptTerrainManager;

	/// <summary>
	/// Les coordonnées de la reine
	/// </summary>
	private Vector3 positionReine;
#endregion

#region Constantes privées
	/// <summary>
	/// La distance pour aller du centre d'une case à un autre, égale à 5
	/// </summary>
	public static /*const*/ int DISTANCE_CASE = 5;

	/// <summary>
	/// Le nombre de case maximum que peut parcourir la fourmi en une traite
	/// </summary>
	private const int AVANCEMENT_CASE = 1;

	/// <summary>
	/// Un coefficient pour la vitesse de déplacement l'objet
	/// </summary>
	private const float COEFF_VITESSE = 1f;
#endregion

#region Attributs publics
	/// <summary>
	/// Le type de fourmi auquel doit s'appliquer le script
	/// </summary>
	public TypesFourmis typeFourmi;

	/// <summary>
	/// Flag indiquant qu'il faut déposer des phéromones
	/// </summary>
	[HideInInspector]
	public bool activerDepotPheromone;

	/// <summary>
	/// L'orientation courante de l'objet
	/// </summary>
	[HideInInspector]
	public TypesRotations orientationCourante;

	/// <summary>
	/// Flag indiquant que l'objet est en mouvement ou non
	/// Flag mis à jour régulièrement.
	/// </summary>
	[HideInInspector]
	public bool enMouvement;
	
	/// <summary>
	/// Flag indiquant que l'objectif, i.e. la position de la case à atteindre
	/// a été atteint.
	/// Flag mis à jour régulièrement
	/// </summary>
	[HideInInspector]
	public bool objectifAtteint;

	/// <summary>
	/// Flag indiquant que le retour à la base est en cours
	/// </summary>
	[HideInInspector]
	public bool retourBaseEnCours;

	/// <summary>
	/// Flag indiquant qu'il faut commencer le retour
	/// </summary>
	[HideInInspector]
	public bool retourCommence;
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes privées
	/// <summary>
	/// Récupère le morceau de terrain, i.e. le bloc, sur lequel est posée la fourmis,
	/// c'est à dire le bloc ayant un pool d'hexagones de meme texture.
	/// </summary>
	/// <returns>Le bloc de terrain en tant que GameObject, null si aucun objet en dessous</returns>
	private GameObject GetBlocCourantAsGO(){
		// Lancement d'un rayon vers la case sous la fourmis
		RaycastHit hit;
		if ( Physics.Raycast(transform.position, -Vector3.up, out hit, 100.0F) ){
			// Récupération du morceau de terrain contenant plusieurs hexagones
			return hit.transform.gameObject;
		} else {
			return null;
		}
	}

	/// <summary>
	/// Récupère le morceau de terrain, i.e. le bloc, sur lequel est posée la fourmis,
	/// c'est à dire le bloc ayant un pool d'hexagones de meme texture.
	/// </summary>
	/// <returns>Le bloc de terrain en tant que string, au format JSON</returns>
	private string GetBlocCourantAsString(){
		GameObject goBlocTerrain = GetBlocCourantAsGO();
		return JSONUtils.parseBlocTerrain(goBlocTerrain);
	}

	/// <summary>
	/// Retourne la position courante de l'objet
	/// </summary>
	private void AfficherPositionCourante(){
		Debug.Log("BlocTerrain :" + GetBlocCourantAsGO());
		Debug.Log("BlocTerrain :" + GetBlocCourantAsString());
		Debug.Log("Infos :" + Get3dInfos());
	}

	/// <summary>
	/// Idéalement, dès qu'un objet apparait sur le terrain, il faut le recentrer sur un hexagone.
	/// Pour cela, la classe utilitaire TerrainUtils permet de trouver l'hexagone le plus proche.
	/// Une fois cet hexagone trouvé, il faut recentrer l'objet courant dessus une bonne fois pour toute.
	/// Par la suite, il suffirait de déplacer l'objet courant avec une distance correspondant à
	/// un multiple de la distance entre deux centres d'hexagones afin d'avoir un objet toujours centré.
	/// </summary>
	/// <remarks>
	/// Il vaut mieu appeler cette fonction un minimum de fois car l'opéraiton est gourmande.
	/// </remarks>
	private void Recentrer(){
		HexagoneInfo hexPlusProche = TerrainUtils.HexagonePlusProche(transform.localPosition);
		//Debug.Log("Recentrage de l="+transform.localPosition+" vers "+hexPlusProche.positionGlobale);
		transform.localPosition = hexPlusProche.positionLocaleSurTerrain;
	}

	/// <summary>
	/// Effectue une rotation de la fourmis.
	/// Les valeurs utilisées pour la rotation sont des valeurs euleriennes,
	/// elles peuvent etre récupérées par transform.rotation.eulerAngles.
	/// </summary>
	/// <param name="rotation">Le sens de rotation</param>
	//private void FaireRotation( TypesRotations rotation ){
	public void FaireRotation( TypesRotations rotation ){
		TypesRotations r;
		if (rotation == TypesRotations.RANDOM){
			// Définition de la rotation : plage de valeur définie dans[-135;+135]
			// avec un pas de +45. L'idée est de tirer un nombre qui servira de coefficient
			// multiplicateur pour 45
			int typeRotation = Random.Range(-3, +3);
			int angle = (+45)*typeRotation;
			// Problem si on tire un angle de 0 ou de 180
			if ( angle == 0 ) angle = 90; 
			if ( angle == 180 ) angle = -90; 
			r = (TypesRotations)angle;
		} else {
			r = rotation;
		}
		orientationCourante = r;
		transform.rotation = Quaternion.identity;
		switch (r){
			case TypesRotations.NORD:
				transform.Rotate(0,-90,0);
				break;
			case TypesRotations.NORD_OUEST:
				transform.Rotate(0,-135,0);
				break;
			case TypesRotations.SUD_OUEST:
				transform.Rotate(0,+135,0);
				break;
			case TypesRotations.SUD:
				transform.Rotate(0,+90,0);
				break;
			case TypesRotations.SUD_EST:
				transform.Rotate(0,+45,0);
				break;
			case TypesRotations.NORD_EST:
				transform.Rotate(0,-45,0);
				break;
			case TypesRotations.AUCUN:
				//Debug.LogError("Aucune rotation ?");
				break;
			default:
				Debug.LogError("ERREUR: FaireRotation() : Rotation non gérée :"+r);
				break;
		}
		//Debug.Log("Rotation actuelle en Y : " + r);
	}

	/// <summary>
	/// Routine appelée par la routine Unity Update().
	/// Fait déambuler la fourmis.
	/// Pour ce faire, va faire une rotation ou non, et va faire avancer la fourmis
	/// d'un certain nombre de case.
	/// </summary>
	//private void Deambuler(){
	public void Deambuler(){

		if ( retourBaseEnCours && (objectifAtteint || retourCommence) ){
			RetourBase();
			Avancer(AVANCEMENT_CASE);
		}

		// Cas de déambulation classique
		if ( enMouvement && !objectifAtteint ){

			//transform.position = Vector3.Lerp (transform.position, positionAatteindre, COEFF_VITESSE * vitesseAppliquee);
			float distCovered = (Time.time - tempsDebutTranslation) * COEFF_VITESSE * vitesseAppliquee;/*speed*/;
			float fracJourney = distCovered / distanceTranslation;
			transform.position = Vector3.Lerp(transform.position, positionAatteindre, fracJourney);

			//Debug.Log ("Déambuler");
			//Debug.Log("Je suis en "+transform.position+" et dois aller en "+ positionAatteindre);
			//Debug.Log("Distance :"+Vector3.Distance (transform.position, positionAatteindre));

			if ( Mathf.Abs(Mathf.Abs(transform.position.x)-Mathf.Abs(positionAatteindre.x)) <= 1
			    && Mathf.Abs(Mathf.Abs(transform.position.z)-Mathf.Abs(positionAatteindre.z)) <= 1 ){
				enMouvement = false;
				objectifAtteint = true;
				Avancer(-1);
				return;
			}

		}

	}

	/// <summary>
	/// Initialise les variables qui dépendent de la caste de la fourmis
	/// </summary>
	private void InitialiserVariablesFourmi(){
		switch ( typeFourmi ){
			case TypesFourmis.COMBATTANTE_NOIRE:
				vitesseAppliquee = (int) VitessesFourmis.VITESSE_COMBATTANTE;
				typePheromone = Invocations.RIEN;
				activerDepotPheromone = false;
				break;
			case TypesFourmis.CONTREMAITRE_NOIRE:
				vitesseAppliquee = (int) VitessesFourmis.VITESSE_CONTREMAITRE;	
				typePheromone = Invocations.PHEROMONES_CONTREMAITRE_NOIRE;
				activerDepotPheromone = true;
				break;
			case TypesFourmis.GENERALE_NOIRE:
				vitesseAppliquee = (int) VitessesFourmis.VITESSE_GENERALE;	
				typePheromone = Invocations.RIEN;
				activerDepotPheromone = false;
				break;
			case TypesFourmis.OUVRIERE_NOIRE:
				vitesseAppliquee = (int) VitessesFourmis.VITESSE_OUVRIERE;	
				typePheromone = Invocations.PHEROMONES_OUVRIERE_NOIRE;
				activerDepotPheromone = false;
				break;
			case TypesFourmis.COMBATTANTE_BLANCHE:
				vitesseAppliquee = (int) VitessesFourmis.VITESSE_COMBATTANTE;
				typePheromone = Invocations.RIEN;
				activerDepotPheromone = false;
				break;
			case TypesFourmis.CONTREMAITRE_BLANCHE:
				vitesseAppliquee = (int) VitessesFourmis.VITESSE_CONTREMAITRE;	
				typePheromone = Invocations.PHEROMONES_CONTREMAITRE_BLANCHE;
				activerDepotPheromone = true;	
				break;
			case TypesFourmis.GENERALE_BLANCHE:
				vitesseAppliquee = (int) VitessesFourmis.VITESSE_GENERALE;	
				typePheromone = Invocations.RIEN;	
				activerDepotPheromone = false;
				break;
			case TypesFourmis.OUVRIERE_BLANCHE:
				vitesseAppliquee = (int) VitessesFourmis.VITESSE_OUVRIERE;	
				typePheromone = Invocations.PHEROMONES_OUVRIERE_BLANCHE;
				activerDepotPheromone = false;	
				break;
			default:
				vitesseAppliquee = 0;
				typePheromone = Invocations.RIEN;
				activerDepotPheromone = false;	
				break;
		}
		//Debug.Log("Initialisation d'une fourmi "+typeFourmi+" avec une vitesse de "+vitesseAppliquee);
	}

	/// <summary>
	/// Dépose des phéromones
	/// </summary>
	/// <param name="position"></param>
	private void DeposerPheromones( Vector3 position ){
		//Debug.Log("Dépot de phéromones "+typePheromone+" en "+position);
		GameObject goPhero	 = scriptInvocation.InvoquerObjet(typePheromone, position);
		PheromonesScript ps = goPhero.GetComponent<PheromonesScript>();
		FourmiScript fs = this.gameObject.GetComponent<FourmiScript>();
		ps.direction = fs.dernierAxeUtilise;
	}
#endregion

#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity au démarrage du script
	/// </summary>
	void Awake(){
		enMouvement = false;
		objectifAtteint = true;
		retourBaseEnCours = false;
		collisionFrontaleBetise = false;
		InitialiserVariablesFourmi();
		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		scriptInvocation = bacAsable.GetComponent<InvocateurObjetsScript>();
		scriptTerrainManager = bacAsable.GetComponent<TerrainManagerScript>();
		int type = (int)typeFourmi;
		TypesCamps monCamps = (type >= 1 && type <= 4 ? TypesCamps.NOIR : TypesCamps.BLANC);
		positionReine = (monCamps == TypesCamps.NOIR ? scriptTerrainManager.PositionReineNoire() : scriptTerrainManager.PositionReineBlanche());
	}

	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame
	/// </summary>
	void FixedUpdate(){
		Deambuler();
	}
#endregion

#region Méthodes publiques
	/// <summary>
	/// Fait avancer la fourmis de nbCases cases.
	/// Si nbCases est à <= 0, l'objet ne bouge plus
	/// </summary>
	/// <param name="nbCases">Le nombre de cases à avancer</param>
	public void Avancer( int nbCases ){

		// Arret de l'objet
		if  (nbCases <= 0 ){
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			enMouvement = false;
			objectifAtteint = true;
			return;
		}

		//Debug.Log("Dois avancer de "+nbCases+" cases");
		positionAatteindre = transform.position;

		switch (orientationCourante){
			case TypesRotations.NORD:
				positionAatteindre.x += (-1) * nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				break;
			case TypesRotations.NORD_EST:
				positionAatteindre.x += (-1) * nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				positionAatteindre.z += nbCases*DISTANCE_CASE;
				break;
			case TypesRotations.NORD_OUEST:
				positionAatteindre.x += (-1) * nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				positionAatteindre.z += (-1) * nbCases*DISTANCE_CASE;
				break;
			case TypesRotations.SUD:
				positionAatteindre.x += nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				break;
			case TypesRotations.SUD_EST:
				positionAatteindre.x += nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				positionAatteindre.z += nbCases*DISTANCE_CASE;
				break;
			case TypesRotations.SUD_OUEST:
				positionAatteindre.x += nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				positionAatteindre.z += (-1) * nbCases*DISTANCE_CASE;
				break;
			case TypesRotations.AUCUN:
				//Debug.LogError("Aucune rotation ?");
				//FIXME : Cas ne devant jamais apapraitre normalement
				positionAatteindre.x += (-1) * nbCases*DISTANCE_CASE;
				positionAatteindre.y = 0;
				positionAatteindre.z += (-1) * nbCases*DISTANCE_CASE;
				break;
			default:
				Debug.LogError("ERREUR: Valeur non gérée dans switch : "+orientationCourante);
				break;
		}

		enMouvement = true;
		objectifAtteint = false;
		rigidbody.isKinematic = false;

		distanceTranslation = Vector3.Distance(transform.position, positionAatteindre);
		tempsDebutTranslation = Time.time;

		if ( activerDepotPheromone ) DeposerPheromones(transform.localPosition);

		//Debug.Log("Je suis en " + transform.position + ", je dois aller en " + positionAatteindre);

	}

	/// <summary>
	/// Stoppe l'objet en mouvement
	/// </summary>
	public void Stopper(){
		Avancer(-1);
	}

	/// <summary>
	/// Stoppe l'objet en mouvement à cause d'une collision
	/// </summary>
	/// <param name="causeCollision">
	/// L'objet qui a causé la collision. En fonction de cet objet, l'objet courant
	/// bouge différement (ex: ne pas s'acharner à aller sur un coté du bac à sable)
	/// </param>
	public void StopperParCollision( TypesObjetsRencontres causeCollision ){
		// Arret de l'objet
		Avancer(-1);
		// Changement de direction su on tape dans un coté du bac à sable
		switch (causeCollision){
			case TypesObjetsRencontres.COTE_BAC_1:
				collisionFrontaleBetise = false;
				FaireRotation(TypesRotations.SUD_EST);
				Avancer(AVANCEMENT_CASE);
				break;
			case TypesObjetsRencontres.COTE_BAC_2:
				collisionFrontaleBetise = false;
				FaireRotation(TypesRotations.SUD_OUEST);
				Avancer(AVANCEMENT_CASE);
				break;
			case TypesObjetsRencontres.COTE_BAC_3:
				collisionFrontaleBetise = false;
				FaireRotation(TypesRotations.SUD);
				Avancer(AVANCEMENT_CASE);
				break;
			case TypesObjetsRencontres.COTE_BAC_4:
				collisionFrontaleBetise = false;
				FaireRotation(TypesRotations.NORD);
				Avancer(AVANCEMENT_CASE);
				break;
			case TypesObjetsRencontres.PETIT_CAILLOU:
			case TypesObjetsRencontres.CAILLOU:
			case TypesObjetsRencontres.TRES_GROS_CAILLOUX:
			case TypesObjetsRencontres.EAU:
			case TypesObjetsRencontres.EAU3D:
				collisionFrontaleBetise = true;
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// Retourne l'hexagone sur lequel est la fourmis
	/// </summary>
	/// <returns>L'hexagone sur lequel est la fourmis</returns>
	public HexagoneInfo HexagoneCourant(){
		HexagoneInfo hexagoneCourant = TerrainUtils.HexagonePlusProche(transform.localPosition);
		//Debug.Log("Hexagone courant : pos=" + hexagoneCourant.positionLocaleSurTerrain + "/ texture=" + hexagoneCourant.GetTypeTerrain());
		return hexagoneCourant;
	}

	/// <summary>
	/// Retourne les infos 3D de la fourmis à savoir sa rotation en (x,y,z) et sa position en (x,y,z).
	/// String de  la forme :
	/// 
	/// 	{position:{x:XXX,y:YYY,z:ZZZ},rotation:{x:UUU,y:VVV,z:WWW}}
	/// 
	/// </summary>
	/// <returns>Un string au format JSON</returns>
	public string Get3dInfos(){
		return JSONUtils.parseInfos3D(gameObject.transform.position, gameObject.transform.rotation);
	}

	/// <summary>
	/// Retour à la base d'une fourmi.
	/// Typiquement, va changer l'orientation de la fourmi puisque le déplacement
	/// à proprement parler est géré ailleurs.
	/// </summary>
	public void RetourBase(){

		// Calculer la distance entre la reine et la fourmi en X
		float distanceEnX = positionReine.x - transform.localPosition.z;
		// Calculer la distance entre la reine et la fourmi en Z
		float distanceEnZ = positionReine.z - transform.localPosition.z;

		//Debug.Log("Ma reine est en "+positionReine+", je suis en "+transform.localPosition +", distance en X de "+distanceEnX+", distance en Z de "+distanceEnZ);

		// Déterminer en fonction des résultats la direction à prendre pour rejoindra la reine
		TypesRotations directionReine;
		// Distance en X positive : la reine est plus en bas sous la fourmi qui doit descendre
		// Distance en X négative : la reine est plus en haut, au dessus de la fourmi qui doit monter
		if ( distanceEnX > 0 ){
			directionReine = TypesRotations.SUD;
		} else {//if ( distanceEnX < 0 ){
			directionReine = TypesRotations.NORD;
		}
		// Distance en Z positive : la reine est sur la droite
		// Distance en Z négative : la reine est sur la gauche
		if ( distanceEnZ > 0 ){
			if ( directionReine == TypesRotations.SUD ){
				directionReine = TypesRotations.SUD_EST;
			} else { // Reine au nord
				directionReine = TypesRotations.NORD_EST;
			}
		} else { //if ( distanceEnZ < 0 ){
			if ( directionReine == TypesRotations.SUD ){
				directionReine = TypesRotations.SUD_OUEST;
			} else { // Reine au nord
				directionReine = TypesRotations.NORD_OUEST;
			}
		}

		// Effectuer le déplacement
		// Je tourne si je suis pas dans le bon sens
		if ( orientationCourante != directionReine ){
			FaireRotation(directionReine);
		}

	}
#endregion

}
#endregion 

#region VitessesFourmis
/// <summary>
/// Les vitesses des différentes castes de fourmis
/// </summary>
public enum VitessesFourmis : int { 
	/// <summary>
	/// La vitesse de l'ouvrière, à 1
	/// </summary>
	VITESSE_OUVRIERE = 1,
	/// <summary>
	/// La vitesse de la contremaitre, à 1
	/// </summary>
	VITESSE_CONTREMAITRE = 1,
	/// <summary>
	/// La vitesse de la combattante, à 1
	/// </summary>
	VITESSE_COMBATTANTE = 1,
	/// <summary>
	/// La vitesse de la générale, à 1
	/// </summary>
	VITESSE_GENERALE = 1,
	/// <summary>
	/// La vitesse du scarabée, à 2
	/// </summary>
	VITESSE_SCARABEE = 2
}
#endregion

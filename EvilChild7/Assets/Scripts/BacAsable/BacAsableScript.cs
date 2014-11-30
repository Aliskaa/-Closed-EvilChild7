/// <summary>
/// BacAsableScript.cs.
/// Script pour manipuler le mode bac à sable avec entre autres les touches à utiliser
/// 
/// Clic gauche souris : créer un objet
/// Clic droit souis : supprimer l'objet pointer
/// Touches du clavier : préparer un objet à etre créé
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 2.1.0
/// </remarks>

using UnityEngine;

/// <summary>
/// Classe pour manipuler le mode bac à sable avec entre autres les touches à utiliser
/// </summary>
public class BacAsableScript : MonoBehaviour {

	/* ********* *
	 * Attributs * 
	 * ********** */

#region Attributs privés
	/// <summary>
	/// Une référence vers le bac à sable
	/// </summary>
	private GameObject bacAsable;

	/// <summary>
	/// Une référence vers le script eprmettant de faire invoquer des objets
	/// </summary>
	private InvocateurObjetsScript scriptInvoc;

	/// <summary>
	/// Une référence vers le script gérant le terrain
	/// </summary>
	private TerrainManagerScript scriptTerrain;

	/// <summary>
	/// Le type d'objets à invoquer
	/// </summary>
	private Invocations objetAmettre;

	/// <summary>
	/// Flag indiquant qu'une reine noire, unique, a été posée
	/// </summary>
	private bool reineNoirePosee;

	/// <summary>
	/// Flag indiquant qu'une reine blanche, unique, a été posée
	/// </summary>
	private bool reineBlanchePosee;
#endregion

#region Constantes privées pour les touches du clavier

	/// <summary>
	/// La touche pour faire apparaitre une ouvrière blanche.
	/// Touche 1 du clavier (hors pavé numérique)
	/// </summary>
	private const KeyCode toucheOuvriereBlanche = KeyCode.Alpha1;
	/// <summary>
	/// La touche pour faire apparaitre une soldate blanche.
	/// Touche 2 du clavier (hors pavé numérique)
	/// </summary>
	private const KeyCode toucheSoldateBlanche = KeyCode.Alpha2;
	/// <summary>
	/// La touche pour faire apparaitre une contremaitre blanche.
	/// Touche 3 du clavier (hors pavé numérique)
	/// </summary>
	private const KeyCode toucheContremaitreBlanche = KeyCode.Alpha3;
	/// <summary>
	/// La touche pour faire apparaitre une générale blanche.
	/// Touche 4 du clavier (hors pavé numérique)
	/// </summary>
	private const KeyCode toucheGeneraleBlanche = KeyCode.Alpha4;
	/// <summary>
	/// La touche pour faire apparaitre une reine blanche.
	/// Touche 5 du clavier (hors pavé numérique)
	/// </summary>
	private const KeyCode toucheReineBlanche = KeyCode.Alpha5;

	/// <summary>
	/// La touche pour faire apparaitre une ouvrière noire.
	/// Touche 6 du clavier (hors pavé numérique)
	/// </summary>
	private const KeyCode toucheOuvriereNoire = KeyCode.Alpha6;
	/// <summary>
	/// La touche pour faire apparaitre une soldate noire.
	/// Touche 7 du clavier (hors pavé numérique)
	/// </summary>
	private const KeyCode toucheSoldateNoire = KeyCode.Alpha7;
	/// <summary>
	/// La touche pour faire apparaitre une contremaitre noire.
	/// Touche 8 du clavier (hors pavé numérique)
	/// </summary>
	private const KeyCode toucheContremaitreNoire = KeyCode.Alpha8;
	/// <summary>
	/// La touche pour faire apparaitre une générale noire.
	/// Touche 9 du clavier (hors pavé numérique)
	/// </summary>
	private const KeyCode toucheGeneraleNoire = KeyCode.Alpha9;
	/// <summary>
	/// La touche pour faire apparaitre une reine blanche.
	/// Touche 0 du clavier (hors pavé numérique)
	/// </summary>
	private const KeyCode toucheReineNoire = KeyCode.Alpha0;

	/// <summary>
	/// La touche pour faire apparaitre une phéromone d'ouvrière blanche
	/// Touche w du clavier
	/// </summary>
	private const KeyCode touchePheroOuvBlanche = KeyCode.W;
	/// <summary>
	/// La touche pour faire apparaitre une phéromone de contremaitre blanche
	/// Touche x du clavier
	/// </summary>
	private const KeyCode touchePheroCMBlanche = KeyCode.X;
	/// <summary>
	/// La touche pour faire apparaitre une phéromone d'ouvrière noire
	/// Touche c du clavier
	/// </summary>
	private const KeyCode touchePheroOuvNoire= KeyCode.C;
	/// <summary>
	/// La touche pour faire apparaitre une phéromone de contremaitre noire
	/// Touche v du clavier
	/// </summary>
	private const KeyCode touchePheroCMNoire = KeyCode.V;

	/// <summary>
	/// La touche pour ne plus faire apparaitre d'objet
	/// Touche * du clavier
	/// </summary>
	private const KeyCode toucheRien = KeyCode.Asterisk;

	/// <summary>
	/// La touche pour avoir un petit caillou
	/// Touche i du clavier
	/// </summary>
	private const KeyCode touchePetitCaillou = KeyCode.I;
	/// <summary>
	/// La touche pour avoir un caillou
	/// Touche o du clavier
	/// </summary>
	private const KeyCode toucheCaillou = KeyCode.O;
	/// <summary>
	/// La touche pour avoir un gros caillou
	/// Touche p du clavier
	/// </summary>
	private const KeyCode toucheGrosCaillou = KeyCode.P;

	/// <summary>
	/// La touche pour avoir un oeuf
	/// Touche k du clavier
	/// </summary>
	private const KeyCode toucheOeuf = KeyCode.K;
	/// <summary>
	/// La touche pour avoir de la nourriture
	/// Touche l du clavier
	/// </summary>
	private const KeyCode toucheNourriture = KeyCode.L;
	/// <summary>
	/// La touche pour avoir un scarabée
	/// Touche m du clavier
	/// </summary>
	private const KeyCode toucheScarabee = KeyCode.M;
#endregion

#region Constantes privées pour la souris
	/// <summary>
	/// Clic gauche de la souris
	/// </summary>
	private const int CLIC_GAUCHE_SOURIS = 0;
	/// <summary>
	/// Clic droit de la souris
	/// </summary>
	private const int CLIC_DROIT_SOURIS = 1;
	/// <summary>
	/// Clic millieu de la souris
	/// </summary>
	private const int CLIC_CENTRE_SOURIS = 2;
#endregion

	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes privées pour la gestion de la souris
	/// <summary>
	/// Méthode pour détecter le survol de la souris pour convertir ces coordonnées
	/// x/y en coordonnées x/y/z locales au terrain.
	/// Pour cela, va récupérer la position de la souris, puis va récupérer le plan du sol.
	/// Puis va lancer un rayon avec ce sol.
	/// Là où est la souris, un game object nommé "Selection case" de tag POINTEUR sera créé.
	/// </summary>
	private void DetecterSurvol(){
		Ray rayon = Camera.main.ScreenPointToRay(Input.mousePosition); 
		float distance;
		Plane planDuSol = new Plane(Vector3.up, transform.position);
		if ( planDuSol.Raycast(rayon, out distance) ){ 
			Vector3 pointImpact = rayon.GetPoint(distance);
			// Récupération de l'hexagone si on a la souris sur le terrain :
			// pour ce faire vérifier la position de la souris par rapport aux coord globales
			if ( IsSourisSurTerrain(pointImpact) ){
				//Debug.Log("Coordonnées de la souris sur le plan  = " + pointImpact );
				Vector3 coordLocales = transform.worldToLocalMatrix.MultiplyPoint(pointImpact);
				//Debug.Log("Coordonnées locales p/r au terrain = " + coordLocales );
				HexagoneInfo hexagoneClick = TerrainUtils.HexagonePlusProche(coordLocales);
				//Debug.Log("Hexagone le plus proche du clic = " + hexagoneClick.positionLocaleSurTerrain );
				GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
				InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
				SupprimerPointeurs( hexagoneClick );
				// Création d'un nouveau gameobject
				scriptInvoc.InvoquerObjet(Invocations.SELECTION_CASE, hexagoneClick.positionLocaleSurTerrain);
			} else {
				SupprimerPointeurs( null );
			}
		}
	}
	
	/// <summary>
	/// Méthode pour détecter le click de l'utilisateur et pour convertir ces coordonnées
	/// x/y en coordonnées x/y/z locales au terrain.
	/// Pour cela, va récupérer la position de la souris, puis va récupérer le plan du sol.
	/// Puis va lancer un rayon avec ce sol.
	/// </summary>
	private void DetecterClick(){
		/*
		 * Clic gauche de la souris : faire apparaitre un objet
		 */
		if ( Input.GetMouseButtonDown(CLIC_GAUCHE_SOURIS) ){
			Ray rayon = Camera.main.ScreenPointToRay(Input.mousePosition); 
			float distance;
			Plane planDuSol = new Plane(Vector3.up, transform.position);
			if ( planDuSol.Raycast(rayon, out distance) ){ 
				Vector3 pointImpact = rayon.GetPoint(distance);
				if ( IsSourisSurTerrain(pointImpact) ){
					//Debug.Log("Coordonnées de la souris sur le plan  = " + pointImpact );
					Vector3 coordLocales = transform.worldToLocalMatrix.MultiplyPoint(pointImpact);
					//Debug.Log("Coordonnées locales p/r au terrain = " + coordLocales );
					HexagoneInfo hexagoneClick = TerrainUtils.HexagonePlusProche(coordLocales);
					//Debug.Log("Clic sur hexagone : " + hexagoneClick.positionLocaleSurTerrain );
					// Création d'un nouveau gameobject
					if ( objetAmettre == Invocations.FOURMI_NOIRE_REINE){
						if ( ! reineNoirePosee ){
							scriptInvoc.InvoquerObjet(objetAmettre, hexagoneClick.positionLocaleSurTerrain);
							reineNoirePosee = true;
							scriptTerrain.positionReineNoire = hexagoneClick.positionLocaleSurTerrain;
						}
						return;
					}
					if ( objetAmettre == Invocations.FOURMI_BLANCHE_REINE ){
						if ( ! reineBlanchePosee ){
							scriptInvoc.InvoquerObjet(objetAmettre, hexagoneClick.positionLocaleSurTerrain);
							reineBlanchePosee = true;
							scriptTerrain.positionReineBlanche = hexagoneClick.positionLocaleSurTerrain;
						}
						return;
					}
					scriptInvoc.InvoquerObjet(objetAmettre, hexagoneClick.positionLocaleSurTerrain);
				}
			}
			return;
		}
		/*
		 * Clic droit de l'objet : faire disparaitre l'objet
		 */
		if ( Input.GetMouseButtonDown(CLIC_DROIT_SOURIS) ){
			Ray rayon = Camera.main.ScreenPointToRay(Input.mousePosition); 
			RaycastHit hit;
			if(Physics.Raycast(rayon, out hit)){
				GameObject go = hit.transform.gameObject;
				if ( go != null && IsObjetSupprimable(go) ){
					TypesObjetsRencontres tor = GameObjectUtils.parseToType(go.name);
					if ( tor == TypesObjetsRencontres.REINE_BLANCHE ){
						reineBlanchePosee = false;
						scriptTerrain.positionReineBlanche = Vector3.zero;
					} else if ( tor == TypesObjetsRencontres.REINE_NOIRE ){
						reineNoirePosee = false;
						scriptTerrain.positionReineNoire = Vector3.zero;
					}
					Destroy(hit.transform.gameObject);
				}
			}
			return;
		}
	}
	
	/// <summary>
	/// Supprime les game objects ayant servi en tant que pointeurs
	/// </summary>
	/// <param name="h">Hexagone où est la souris, null si la souris n'est plus sur le terrain</param>
	private void SupprimerPointeurs( HexagoneInfo h ){
		GameObject[] anciens = GameObject.FindGameObjectsWithTag("POINTEUR");
		// Cas où la souris n'est plus sur le terrain
		if ( h == null ){
			foreach ( GameObject go in anciens ) if ( go != null ) Destroy(go);
			return;
		}
		// Supprimer le game object de selection si un autre hexagone est visé
		if ( anciens.Length > 1 ){
			for ( int i = 0; i < anciens.Length; i++ ){
				if ( anciens[i] != null ) Destroy(anciens[i]);
			}
		}
	}

	/// <summary>
	/// Indique si la souris pointe sur le terrain
	/// </summary>
	/// <remarks>
	/// L'angle -x/-z du terrain : x=-98 et z=-94.
	/// L'angle +x/+z du terrain : x=0 et z=-8
	/// </remarks>
	/// <returns><c>true</c> Si la souris pointe vers le terrain, <c>false</c> sinon</returns>
	/// <param name="position">La position de la souris</param>
	private bool IsSourisSurTerrain( Vector3 position ){
		if (position.x < -98) return false;
		if (position.x > 0) return false;
		if (position.z < -94) return false;
		if (position.z > -8) return false;
		return true;
	}

	/// <summary>
	/// Indique si l'objet peut etre supprimé
	/// </summary>
	/// <returns><c>true</c> si l'objet peut etre supprimé, sinon <c>false</c>.</returns>
	/// <param name="go">Le game object.</param>
	private bool IsObjetSupprimable( GameObject go ){
		TypesObjetsRencontres tor = GameObjectUtils.parseToType(go.name);
		int torInt = (int) tor;
		// 10 : début de la numérotation pour des betises inertes
		// 69 : dernière numérotation pour la nourriture
		// Les valeurs entre deux sont soit sur les betises, soit la nourriture, soit les bestioles
		return (torInt >= 10 && torInt <= 69);
	}
#endregion

#region Méthodes privées pour la création d'objets
	/// <summary>
	/// Plus aucun objet n'apparaitra
	/// </summary>
	private void ViderObjet(){
		//Debug.Log("Plus d'objet à mettre");
		objetAmettre = Invocations.RIEN;
	}

	/// <summary>
	/// Prépare un oeuf
	/// </summary>
	private void PreparerOeuf(){
		//Debug.Log("Préparation d'un oeuf");
		objetAmettre = Invocations.OEUF_FOURMI;
	}
	
	/// <summary>
	/// Prépare une ouvrière blanche qui apparaitra au clic
	/// </summary>
	private void PreparerOuvriereBlanche(){
		//Debug.Log("Préparation d'une ouvrière blanche");
		objetAmettre = Invocations.FOURMI_BLANCHE_OUVRIERE;
	}

	/// <summary>
	/// Prépare une ouvrière noire qui apparaitra au clic
	/// </summary>
	private void PreparerOuvriereNoire(){
		//Debug.Log("Préparation d'une ouvrière noire");
		objetAmettre = Invocations.FOURMI_NOIRE_OUVRIERE;
	}

	/// <summary>
	/// Prépare une soldate blanche qui apparaitra au clic
	/// </summary>
	private void PreparerSoldateBlanche(){
		//Debug.Log("Préparation d'une soldate blanche");
		objetAmettre = Invocations.FOURMI_BLANCHE_COMBATTANTE;
	}

	/// <summary>
	/// Prépare une soldate noire qui apparaitra au clic
	/// </summary>
	private void PreparerSoldateNoire(){
		//Debug.Log("Préparation d'une soldate noire");
		objetAmettre = Invocations.FOURMI_NOIRE_COMBATTANTE;
	}

	/// <summary>
	/// Prépare une contremaitre blanche qui apparaitra au clic
	/// </summary>
	private void PreparerContremaitreBlanche(){
		//Debug.Log("Préparation d'une contremaitre blanche");
		objetAmettre = Invocations.FOURMI_BLANCHE_CONTREMAITRE;
	}

	/// <summary>
	/// Prépare une contremaitre noire qui apparaitra au clic
	/// </summary>
	private void PreparerContremaitreNoire(){
		//Debug.Log("Préparation d'une contremaitre noire");
		objetAmettre = Invocations.FOURMI_NOIRE_CONTREMAITRE;
	}

	/// <summary>
	/// Prépare une générale blanche qui apparaitra au clic
	/// </summary>
	private void PreparerGeneraleBlanche(){
		//Debug.Log("Préparation d'une générale blanche");
		objetAmettre = Invocations.FOURMI_BLANCHE_GENERALE;
	}

	/// <summary>
	/// Prépare une générale noire qui apparaitra au clic
	/// </summary>
	private void PreparerGeneraleNoire(){
		//Debug.Log("Préparation d'une générale noire");
		objetAmettre = Invocations.FOURMI_NOIRE_GENERALE;
	}

	/// <summary>
	/// Prépare une reine blanche qui apparaitra au clic
	/// </summary>
	private void PreparerReineBlanche(){
		//Debug.Log("Préparation d'une reine blanche");
		objetAmettre = Invocations.FOURMI_BLANCHE_REINE;
	}

	/// <summary>
	/// Prépare une reine noire qui apparaitra au clic
	/// </summary>
	private void PreparerReineNoire(){
		//Debug.Log("Préparation d'une reine noire");
		objetAmettre = Invocations.FOURMI_NOIRE_REINE;
	}

	/// <summary>
	/// Prépare un scarabée qui apparaitra au clic
	/// </summary>
	private void PreparerScarabee(){
		//Debug.Log("Préparation d'un scarabée");
		objetAmettre = Invocations.SCARABEE;
	}

	/// <summary>
	/// Prépare un de la nourriture qui apparaitra au clic
	/// </summary>
	private void PreparerNourriture(){
		//Debug.Log("Préparation de la nourriture");
		int n = UnityEngine.Random.Range(50, 55);
		objetAmettre = (Invocations)n;
	}

	/// <summary>
	/// Prépare une phéromone d'ouvrière blanche qui apparaitra au clic
	/// </summary>
	private void PreparerPheroOuvBlanche(){
		//Debug.Log("Préparation d'un phéromone d'ouvrière blanche");
		objetAmettre = Invocations.PHEROMONES_OUVRIERE_BLANCHE;
	}

	/// <summary>
	/// Prépare une phéromone d'ouvrière noire qui apparaitra au clic
	/// </summary>
	private void PreparerPheroOuvNoire(){
		//Debug.Log("Préparation d'un phéromone d'ouvrière noire");
		objetAmettre = Invocations.PHEROMONES_OUVRIERE_NOIRE;
	}

	/// <summary>
	/// Prépare une phéromone de contremaitre blanche qui apparaitra au clic
	/// </summary>
	private void PreparerPheroCmBlanche(){
		//Debug.Log("Préparation d'un phéromone de contremaitre blanche");
		objetAmettre = Invocations.PHEROMONES_CONTREMAITRE_BLANCHE;
	}

	/// <summary>
	/// Prépare une phéromone de contremaitre noire qui apparaitra au clic
	/// </summary>
	private void PreparerPheroCmNoire(){
		//Debug.Log("Préparation d'un phéromone de contremaitre noire");
		objetAmettre = Invocations.PHEROMONES_CONTREMAITRE_NOIRE;
	}

	/// <summary>
	/// Prépare un petit caillou qui apparaitra au clic
	/// </summary>
	private void PreparerPetitCaillou(){
		//Debug.Log("Préparation d'un petit caillou");
		objetAmettre = Invocations.PETIT_CAILLOU;
	}

	/// <summary>
	/// Prépare un caillou qui apparaitra au clic
	/// </summary>
	private void PreparerCaillou(){
		//Debug.Log("Préparation d'un caillou");
		objetAmettre = Invocations.CAILLOU;
	}

	/// <summary>
	/// Prépare un gros caillou qui apparaitra au clic
	/// </summary>
	private void PreparerGrosCaillou(){
		//Debug.Log("Préparation d'un gros caillou");
		objetAmettre = Invocations.TRES_GROS_CAILLOU;
	}
#endregion

#region Méthodes package
	/// <summary>
	/// Routine appelée automatiquement par Unity au réveil du script
	/// </summary>
	void Awake(){
		//Debug.Log("Mode Bac à Sable activé !");
		bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		scriptTerrain = bacAsable.GetComponent<TerrainManagerScript>();
		objetAmettre = Invocations.RIEN;
		reineBlanchePosee = false;
		reineNoirePosee = false;
	}

	/// <summary>
	/// Routine appellée automatiquement par Unity au lancement du script
	/// </summary>
	void Update(){
		DetecterSurvol();
		DetecterClick();
	}

	/// <summary>
	/// Routine appelée automatiquement par Unity pour traiter les évènements liés
	/// à la GUI/UI
	/// </summary>
	void OnGUI(){
		Event e = Event.current;
		if ( e!= null && e.isKey & Input.anyKeyDown && e.keyCode.ToString () != "None" ){
			KeyCode touche = e.keyCode;
			if ( touche == toucheRien ){
				ViderObjet();
				return;
			}
			if ( touche == toucheOuvriereBlanche ){
				PreparerOuvriereBlanche();
				return;
			}
			if ( touche == toucheOuvriereNoire ){
				PreparerOuvriereNoire();
				return;
			}
			if ( touche == toucheSoldateBlanche ){
				PreparerSoldateBlanche();
				return;
			}
			if ( touche == toucheSoldateNoire ){
				PreparerSoldateNoire();
				return;
			}
			if ( touche == toucheContremaitreBlanche ){
				PreparerContremaitreBlanche();
				return;
			}
			if ( touche == toucheContremaitreNoire ){
				PreparerContremaitreNoire();
				return;
			}
			if ( touche == toucheGeneraleBlanche ){
				PreparerGeneraleBlanche();
				return;
			}
			if ( touche == toucheGeneraleNoire ){
				PreparerGeneraleNoire();
				return;
			}
			if ( touche == toucheReineBlanche ){
				if ( reineBlanchePosee ) ViderObjet();
				else PreparerReineBlanche();
				return;
			}
			if ( touche == toucheReineNoire ){
				if ( reineNoirePosee ) ViderObjet();
				else PreparerReineNoire();
				return;
			}
			if ( touche == toucheNourriture ){
				PreparerNourriture();
				return;
			}
			if ( touche == toucheScarabee ){
				PreparerScarabee();
				return;
			}
			if ( touche == touchePetitCaillou ){
				PreparerPetitCaillou();
				return;
			}
			if ( touche == toucheCaillou ){
				PreparerCaillou();
				return;
			}
			if ( touche == toucheGrosCaillou ){
				PreparerGrosCaillou();
				return;
			}
			if ( touche == touchePheroOuvBlanche ){
				PreparerPheroOuvBlanche();
				return;
			}
			if ( touche == touchePheroOuvNoire ){
				PreparerPheroOuvNoire();
				return;
			}
			if ( touche == touchePheroCMBlanche ){
				PreparerPheroCmBlanche();
				return;
			}
			if ( touche == touchePheroCMNoire ){
				PreparerPheroCmNoire();
				return;
			}
			if ( touche == toucheOeuf ){
				PreparerOeuf();
				return;
			}
		}
	}
#endregion

}

/// <summary>
/// DebugScript.cs
/// 
/// Script pour faire du débogage, c'est à dire profiter
/// des routines telles que Awake(), Start(), Update() ou autres pour faire des tests
/// sans squatter des scripts qui n'ont rien à voir.
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.7.0
/// </remarks>

using UnityEngine;

/// <summary>
/// Script de debug à coller n'importe où et à complèter avec n'importe quoi.
/// </summary>
public class DebugScript : MonoBehaviour {


	/* ********* *
	 * Attributs *
	 * ********* */

#region Attributs
	/// <summary>
	/// Flag indiquant si le terrain est rempli, i.e. si tous les hexagones ont été mis en places
	/// </summary>
	private bool terrainRempli;

	/// <summary>
	/// 
	/// </summary>
	private bool flagOKPop;

	/// <summary>
	/// 
	/// </summary>
	private GameObject bacAsable;

	/// <summary>
	/// 
	/// </summary>
	private InvocateurObjetsScript scriptInvoc;

	/// <summary>
	/// 
	/// </summary>
	private TerrainManagerScript tms;
#endregion


	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes privées
	/// <summary>
	/// Vérifie si le terrain est rempli ou non
	/// </summary>
	/// <returns><c>true</c>, si le terrain est rempli, <c>false</c> sinon.</returns>
	private bool VerifierRemplissageTerrain(){
		GameObject terrain = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		TerrainManagerScript tms = terrain.GetComponent<TerrainManagerScript>();
		return ( tms != null ? tms.VerifierRemplissageTerrain() : false );
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
#endregion

#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity au réveil du script
	/// </summary>
	void Awake(){
		terrainRempli = false;
		flagOKPop = false;
		bacAsable = GameObject.Find("Bac à sable");
		scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		tms = bacAsable.GetComponent<TerrainManagerScript>();
	}

	/// <summary>
	/// Routine appellée automatiquement par Unity à chaque frame
	/// </summary>
	void Update(){

		/*
		 * Il faut s'assurer que le terrain soit créé avant de placer les objets
		 */
		if ( !flagOKPop && (terrainRempli || VerifierRemplissageTerrain()) ){

			terrainRempli = true;
			flagOKPop = true;

			#region Debogage IA
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_COMBATTANTE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_OUVRIERE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_CONTREMAITRE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_GENERALE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_REINE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_COMBATTANTE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_CONTREMAITRE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_OUVRIERE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_GENERALE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_REINE, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.OEUF_FOURMI, new Vector3(85f, 0.1f, 95f));
			//scriptInvoc.InvoquerObjet(Invocations.SCARABEE, new Vector3(85f, 0.1f, 95f));
			#endregion

		}

		/*
		 * 
		 */
		if ( Input.GetMouseButtonDown(1/*CLIC_DROIT_SOURIS*/) ){
			Ray rayon = Camera.main.ScreenPointToRay(Input.mousePosition); 
			float distance;
			Plane planDuSol = new Plane(Vector3.up, transform.position);
			if ( planDuSol.Raycast(rayon, out distance) ){ 
				Vector3 pointImpact = rayon.GetPoint(distance);
				if ( IsSourisSurTerrain(pointImpact) ){
					Vector3 click = tms.ConvertirCoordonnes(pointImpact);
					//tms.ConvertirCaseEau(click);

					#region Debogage IA
					//scriptInvoc.InvoquerObjet(Invocations.TRES_GROS_CAILLOU, click);
					//scriptInvoc.InvoquerObjet(Invocations.PETIT_CAILLOU, click);
					//scriptInvoc.InvoquerObjet(Invocations.CAILLOU, click);
					//scriptInvoc.InvoquerObjet(Invocations.BONBON_MURE, click);
					//scriptInvoc.InvoquerObjet(Invocations.BONBON_ANGLAIS_BLEU, click);
					//scriptInvoc.InvoquerObjet(Invocations.BONBON_ANGLAIS_ROSE, click);
					//scriptInvoc.InvoquerObjet(Invocations.BONBON_ORANGE, click);
					//scriptInvoc.InvoquerObjet(Invocations.BONBON_VERT, click);
					//scriptInvoc.InvoquerObjet(Invocations.BONBON_ROSE, click);
					//scriptInvoc.InvoquerObjet(Invocations.OEUF_FOURMI, click);
					//scriptInvoc.InvoquerObjet(Invocations.BOUT_DE_BOIS, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_COMBATTANTE, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_CONTREMAITRE, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_GENERALE, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_OUVRIERE, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_GENERALE, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_CONTREMAITRE, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_OUVRIERE, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_COMBATTANTE, click);
					//scriptInvoc.InvoquerObjet(Invocations.SCARABEE, click);
					//scriptInvoc.InvoquerObjet(Invocations.PHEROMONES_CONTREMAITRE_BLANCHE, click);
					//GameObject bite =  ios.InvoquerObjet(Invocations.PHEROMONES_OUVRIERE_BLANCHE, click);
					//GameObject bite =  ios.InvoquerObjet(Invocations.PHEROMONES_CONTREMAITRE_BLANCHE, click);
					//GameObject bite =  ios.InvoquerObjet(Invocations.PHEROMONES_OUVRIERE_NOIRE, click);
					//GameObject bite =  ios.InvoquerObjet(Invocations.PHEROMONES_CONTREMAITRE_NOIRE, click);
					//bite.GetComponent<PheromonesScript>().direction = TypesAxes.DEVANT;
					#endregion 

					#region Debogage collision
					//tms.ConvertirCaseEau(click);
					//scriptInvoc.InvoquerObjet(Invocations.TRES_GROS_CAILLOU, click);
					//scriptInvoc.InvoquerObjet(Invocations.CAILLOU, click);
					//scriptInvoc.InvoquerObjet(Invocations.PETIT_CAILLOU, click);
					//scriptInvoc.InvoquerObjet(Invocations.BONBON_ANGLAIS_BLEU, click);
					//scriptInvoc.InvoquerObjet(Invocations.BONBON_ANGLAIS_ROSE, click);
					//scriptInvoc.InvoquerObjet(Invocations.BONBON_MURE, click);
					//scriptInvoc.InvoquerObjet(Invocations.BONBON_ORANGE, click);
					//scriptInvoc.InvoquerObjet(Invocations.BONBON_ROSE, click);
					//scriptInvoc.InvoquerObjet(Invocations.BONBON_VERT, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_COMBATTANTE, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_CONTREMAITRE, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_GENERALE, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_OUVRIERE, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_REINE	, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_COMBATTANTE, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_CONTREMAITRE, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_GENERALE, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_OUVRIERE, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_REINE, click);
					//scriptInvoc.InvoquerObjet(Invocations.OEUF_FOURMI, click);
					//scriptInvoc.InvoquerObjet(Invocations.SCARABEE, click);
					#endregion

					#region Débogage retour à la base
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_CONTREMAITRE, click);
					scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_CONTREMAITRE, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_OUVRIERE, click);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_OUVRIERE, click);
					#endregion

					//Debug.Log("Coordonnées converties : "+tms.ConvertirCoordonnes(pointImpact));
				}
			}

		}

		/*
		 * 
		 */
		if ( Input.GetMouseButtonDown(0/*CLIC_GAUCHE_SOURIS*/) ){
			Ray rayon = Camera.main.ScreenPointToRay(Input.mousePosition); 
			float distance;
			Plane planDuSol = new Plane(Vector3.up, transform.position);
			if ( planDuSol.Raycast(rayon, out distance) ){ 
				Vector3 pointImpact = rayon.GetPoint(distance);
				if ( IsSourisSurTerrain(pointImpact) ){
					GameObject terrainGo = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
					TerrainManagerScript tms = terrainGo.GetComponent<TerrainManagerScript>();
					Vector3 click = tms.ConvertirCoordonnes(pointImpact);

					#region Débogage oeufs
					//GameObject noeunoeuf = scriptInvoc.InvoquerObjet(Invocations.OEUF_FOURMI, click);
					//OeufScript os = noeunoeuf.GetComponent<OeufScript>();
					//int tirageTypeFourmi = Random.Range(20, 23);
					//int tirageCamps = Random.Range(0,1);
					//os.fourmi = (Invocations)(tirageTypeFourmi + tirageCamps*10);
					#endregion

					#region Débogage retour à la base
					scriptInvoc.InvoquerObjet(Invocations.BONBON_MURE, click);
					//scriptInvoc.InvoquerObjet(Invocations.TRES_GROS_CAILLOU, click);
					#endregion
				}
			}
		}

		/*
		 * 
		 */
		if ( Input.GetMouseButtonDown(2/*CLIC_MOLETTE*/) ){
			Ray rayon = Camera.main.ScreenPointToRay(Input.mousePosition); 
			float distance;
			Plane planDuSol = new Plane(Vector3.up, transform.position);
			if ( planDuSol.Raycast(rayon, out distance) ){ 
				Vector3 pointImpact = rayon.GetPoint(distance);
				if ( IsSourisSurTerrain(pointImpact) ){
					GameObject terrainGo = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
					TerrainManagerScript tms = terrainGo.GetComponent<TerrainManagerScript>();
					Vector3 click = tms.ConvertirCoordonnes(pointImpact);
					//scriptInvoc.InvoquerObjet(Invocations.FOURMI_NOIRE_OUVRIERE, click);
					scriptInvoc.InvoquerObjet(Invocations.FOURMI_BLANCHE_OUVRIERE, click);
				}
			}
		}

	}
#endregion

}

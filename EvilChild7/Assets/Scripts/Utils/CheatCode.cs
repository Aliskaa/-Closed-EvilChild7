/// <summary>
/// CheatCode.cs
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 2.0.0
/// </remarks>

using UnityEngine;

/// <summary>
/// Des cheats code. Et ouais.
/// </summary>
public class CheatCode : MonoBehaviour {

	/* ********* *
	 * Attributs * 
	 * ********** */

#region Attribut privés
	/// <summary>
	/// Les codes pour les touches du konami code
	/// </summary>
	private KeyCode[] codesTouchesKonami;

	/// <summary>
	/// Les codes pour les touches du code rakayou
	/// </summary>
	private KeyCode[] codesTouchesRakayou;

	/// <summary>
	/// Les codes pour les touches du code suicide squad
	/// </summary>
	private KeyCode[] codesTouchesSsqd;

	/// <summary>
	/// Un indice pour le tableau du konami code
	/// </summary>
	private int indiceToucheActuelleKonami = 0;

	/// <summary>
	/// Un indice pour le tableau du code rakayou
	/// </summary>
	private int indiceToucheActuelleRakayou = 0;

	
	/// <summary>
	/// Un indice pour le tableau du code rakayou
	/// </summary>
	private int indiceToucheActuelleSsqd = 0;
#endregion

	/* ******** *
	 * Méthodes *
	 * ******** */

#region Méthodes privées
	/// <summary>
	/// Armageddon version bonbons : des bonbons tombent
	/// du ciel et atterissent sur le terrain.
	/// </summary>
	private void Bonbongedon(){
		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		int nombreBonbons = UnityEngine.Random.Range (10, 50);
		for ( int i = 0; i < nombreBonbons; i++ ){
			int bonbon =  UnityEngine.Random.Range(50,55);
			Invocations bonbonInvoc = (Invocations)bonbon;
			int randomX = UnityEngine.Random.Range(55,148);    // Limites en X du terrain
			//int randomY = UnityEngine.Random.Range(15,100);    // Altitudes min et max
			int randomZ = UnityEngine.Random.Range(52,132);     // Limites en Z du terrain
			scriptInvoc.InvoquerObjetAvecOffset( 
			                  bonbonInvoc, 
			                  new Vector3(randomX, 0, randomZ),
			                  new Vector3(0,0/*randomY*/,0));
		}
	}

	/// <summary>
	/// Armageddon version scarabées : des scarabées tombent
	/// du ciel et atterissent sur le terrain.
	/// </summary>
	private void Scarabgedon(){
		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		int nombreScarabees = UnityEngine.Random.Range(1, 3);
		for ( int i = 0; i < nombreScarabees; i++ ){
			int randomX = UnityEngine.Random.Range(55,148);   // Limites en X du terrain
			//int randomY = UnityEngine.Random.Range(15,100);   // Altitudes min et max
			int randomZ = UnityEngine.Random.Range(52,132);   // Limites en Z du terrain
			scriptInvoc.InvoquerObjetAvecOffset( 
			                Invocations.SCARABEE, 
			                new Vector3(randomX, 0, randomZ),
			                new Vector3(0,0/*randomY*/,0));
		}
	}

	
	/// <summary>
	/// Armageddon version fourmis noires : des noires tombent
	/// du ciel et atterissent sur le terrain.
	/// </summary>
	private void Allblacksgedon(){
		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		int nombreFourmisNoires = UnityEngine.Random.Range (10, 50);
		for ( int i = 0; i < nombreFourmisNoires; i++ ){
			int fourmi =  UnityEngine.Random.Range(20,23);
			Invocations fourmiInvoc = (Invocations)fourmi;
			int randomX = UnityEngine.Random.Range(55,148);    // Limites en X du terrain
			//int randomY = UnityEngine.Random.Range(15,100);    // Altitudes min et max
			int randomZ = UnityEngine.Random.Range(52,132);     // Limites en Z du terrain
			scriptInvoc.InvoquerObjetAvecOffset( 
			                                    fourmiInvoc, 
			                                    new Vector3(randomX, 0, randomZ),
			                                    new Vector3(0,0/*randomY*/,0));
		}
	}

	/// <summary>
	/// Eboulement de cailloux
	/// </summary>
	private void Eboulement(){
		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		int nbCailloux = UnityEngine.Random.Range(20, 80);
		for ( int i = 0; i < nbCailloux; i++ ){
			int caillou =  UnityEngine.Random.Range(11,13);
			Invocations caillouInvoc = (Invocations)caillou;
			int randomX = UnityEngine.Random.Range(55,148);    // Limites en X du terrain
			//int randomY = UnityEngine.Random.Range(15,100);    // Altitudes min et max
			int randomZ = UnityEngine.Random.Range(52,132);     // Limites en Z du terrain
			scriptInvoc.InvoquerObjetAvecOffset( 
			                                    caillouInvoc, 
			                                    new Vector3(randomX, 0, randomZ),
			                                    new Vector3(0,0/*randomY*/,0));
		}
	}

	/// <summary>
	/// Armageddon version fourmis rouges : des rouges tombent
	/// du ciel et atterissent sur le terrain.
	/// </summary>
	private void Rednecksgedon(){
		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		int nombreFourmisRouges = UnityEngine.Random.Range (10, 50);
		for ( int i = 0; i < nombreFourmisRouges; i++ ){
			int fourmi =  UnityEngine.Random.Range(30,23);
			Invocations fourmiInvoc = (Invocations)fourmi;
			int randomX = UnityEngine.Random.Range(55,148);    // Limites en X du terrain
			//int randomY = UnityEngine.Random.Range(15,100);    // Altitudes min et max
			int randomZ = UnityEngine.Random.Range(52,132);     // Limites en Z du terrain
			scriptInvoc.InvoquerObjetAvecOffset( 
			                                    fourmiInvoc, 
			                                    new Vector3(randomX, 0, randomZ),
			                                    new Vector3(0,0/*randomY*/,0));
		}
	}

	/// <summary>
	/// Tata Yoyo, qu'est-ce qu'il y a sous ton gros chapeau ?
	/// </summary>
	private void Tatayoyo(){
		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		// Invocations des scarabées
		for ( int i = 0; i < 5; i++ ){
			int randomX = UnityEngine.Random.Range(55,148);
			int randomZ = UnityEngine.Random.Range(52,132);
			scriptInvoc.InvoquerObjetAvecOffset( 
			                                    Invocations.SCARABEE, 
			                                    new Vector3(randomX, 0, randomZ),
			                                    new Vector3(0,0/*randomY*/,0));
		}
		// Invocations des générales rouges
		for ( int i = 0; i < 10; i++ ){
			int randomX = UnityEngine.Random.Range(55,148);
			int randomZ = UnityEngine.Random.Range(52,132);
			scriptInvoc.InvoquerObjetAvecOffset( 
			                                    Invocations.FOURMI_ROUGE_GENERALE, 
			                                    new Vector3(randomX, 0, randomZ),
			                                    new Vector3(0,0/*randomY*/,0));
		}
		// Invocations des générales noires
		for ( int i = 0; i < 10; i++ ){
			int randomX = UnityEngine.Random.Range(55,148);
			int randomZ = UnityEngine.Random.Range(52,132);
			scriptInvoc.InvoquerObjetAvecOffset( 
			                                    Invocations.FOURMI_NOIRE_GENERALE, 
			                                    new Vector3(randomX, 0, randomZ),
			                                    new Vector3(0,0/*randomY*/,0));
		}
	}

	/// <summary>
	/// Code pour égrer le konami code
	/// </summary>
	/// <returns><c>true</c>, si c'est bien en rapport avec le konami <c>false</c> sinon.</returns>
	/// <param name="touche">La ouche appuyée qui peut etre en rapport avec le konami code</param>
	private bool GererKonami( KeyCode touche ){
		if ( touche == codesTouchesKonami[indiceToucheActuelleKonami] ){
			indiceToucheActuelleKonami++;
			if ( indiceToucheActuelleKonami+1 > codesTouchesKonami.Length ){
				indiceToucheActuelleKonami = 0;
				int random = UnityEngine.Random.Range(1,10);
				if ( random <= 2 ){
					Scarabgedon();
				} else if ( random == 9 ){
					Allblacksgedon();
				} else if ( random == 10 ){
					Rednecksgedon();
				} else {
					Bonbongedon();
				}
				return true;
			}
		} else {
			indiceToucheActuelleKonami = 0;
		}
		return false;
	}

	/// <summary>
	/// Code pour gérer le code rakayou le konami code
	/// </summary>
	/// <returns><c>true</c>, si c'est bien en rapport avec le code rakayou <c>false</c> sinon.</returns>
	/// <param name="touche">La touche appuyée qui peut etre en rapport avec le rakayou code</param>
	private bool GererRakayou( KeyCode touche ){
		if ( touche == codesTouchesRakayou[indiceToucheActuelleRakayou] ){
			indiceToucheActuelleRakayou++;
			if ( indiceToucheActuelleRakayou+1 > codesTouchesRakayou.Length ){
				indiceToucheActuelleRakayou = 0;
				Eboulement();
				return true;
			}
		} else {
			indiceToucheActuelleRakayou = 0;
		}
		return false;
	}

	/// <summary>
	/// Code pour gérer le code suicide squad
	/// </summary>
	/// <returns><c>true</c>, si c'est bien en rapport avec le code suicide squad <c>false</c> sinon.</returns>
	/// <param name="touche">La touche appuyée qui peut etre en rapport avec le suicide squad code</param>
	private bool GererSuicideSquad( KeyCode touche ){
		if ( touche == codesTouchesSsqd[indiceToucheActuelleSsqd] ){
			indiceToucheActuelleSsqd++;
			if ( indiceToucheActuelleSsqd+1 > codesTouchesSsqd.Length ){
				indiceToucheActuelleSsqd = 0;
				Tatayoyo();
				return true;
			}
		} else {
			indiceToucheActuelleSsqd = 0;
		}
		return false;
	}
#endregion

#region Méthodes package
	/// <summary>
	/// Routine appelée automatiquement par Unity au lancement du script
	/// </summary>
	void Awake(){
		codesTouchesKonami = new KeyCode[]{
			KeyCode.UpArrow,
			KeyCode.UpArrow,
			KeyCode.DownArrow,
			KeyCode.DownArrow,
			KeyCode.LeftArrow,
			KeyCode.RightArrow,
			KeyCode.LeftArrow,
			KeyCode.RightArrow,
			KeyCode.B,
			KeyCode.A
		};
		indiceToucheActuelleKonami = 0;
		codesTouchesRakayou = new KeyCode[]{
			KeyCode.R,
			KeyCode.A,
			KeyCode.K,
			KeyCode.A,
			KeyCode.Y,
			KeyCode.O,
			KeyCode.U,
		};
		indiceToucheActuelleSsqd = 0;
		codesTouchesSsqd = new KeyCode[]{
			KeyCode.S,
			KeyCode.U,
			KeyCode.I,
			KeyCode.C,
			KeyCode.I,
			KeyCode.D,
			KeyCode.E,
			KeyCode.S,
			KeyCode.Q,
			KeyCode.U,
			KeyCode.A,
			KeyCode.D
		};
	}

	/// <summary>
	/// Routine appelée automatiquement par Unity pour traiter les évènements liés
	/// à la GUI/UI
	/// </summary>
	void OnGUI(){
		Event e = Event.current;
		if ( e!= null && e.isKey & Input.anyKeyDown && e.keyCode.ToString () != "None" ){
			KeyCode touche = e.keyCode;
			GererKonami(touche);
			GererRakayou(touche);
			GererSuicideSquad(touche);
		}
	}
#endregion

}

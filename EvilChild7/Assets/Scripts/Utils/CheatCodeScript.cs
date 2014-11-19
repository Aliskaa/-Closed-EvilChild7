/// <summary>
/// CheatCodeScript.cs.
/// Parfois les développeurs se font plaisir.
/// Quand meme, l'easter egg et le cheat code sont des choses auxquelels tout bon dev qui se respecte
/// doit songer !
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 4.1.0
/// </remarks>

/*
// Ce poro est d'accord !

                                                                                                   
                                                    `                                               
                                                ```.`  `           ``                               
                                             `..`     ``   `.-ooossyhs:-`                           
                        ``             ::.```               :/+hh/:+yosh+-`                         
                   ````---.           `+.`                  ./sdo/odysyysyy`                        
                  `:::-----           `                     `/ydhhhssossyhhs:`                      
                   .:::---`         ``                       `.so//:::--:::-::.                     
                    ``...          `           ``.``         `..--.`    ``````...`                  
                                 -`           `+sy/-.                         `..:-                 
                                ``            .dhNd:-.                          `.::                
                                              `syyy:-.`                           `::               
                                               `.--..`                             `+:              
                              `                         `.`                         .+.             
                             `                           .-`                         `-.`           
                            ``-                        `.`..``                        `.-.          
                            ``.`-`   ` `-    --```  ````....``                         .:`          
                              `-/-` `:`-/-```-::---`----.```                          ``-           
                               +-..`.`..:.......---:+--.`                          ```-/.           
                               /.```````-````````.-:+..``                      ```.--:/.            
                               -.``   ``.````` ``.-:+.````                  ``....-:+o`             
                               ..``    `````   ``.-:o..``    ```       ````..----//os:              
                               `-.````````     ``.-//..`````.```  ```....---::/+++oo/               
                                ./-`````````````.-:/....-----....----:://///++:.`.:+.               
                                 `----.........----.``  `.:oyso:://++o+:--oo/.`` .-+`               
                                      `.s+/osssddhsyy+/.`-+sdddsyo/+:.   :dh/+ys/+/:                
                                        syoydhhdhdydmhdd/...//+++/-``````-oysydhs/+`                
                                         ````..``:::::--``...............``..----.`                 
                                         ``````````..```````........```````......```                
                                              ```````````````````````````````````                   
                                              ````````````````                                      
                                              
*/
using UnityEngine;

/// <summary>
/// Des cheats code. Et ouais.
/// </summary>
public class CheatCodeScript : MonoBehaviour {

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
	/// Les codes pour les touches du code kinder surpriz
	/// </summary>
	private KeyCode[] codesTouchesKs;

	/// <summary>
	/// Les codes pour les touches du code trash
	/// </summary>
	private KeyCode[] codesTouchesTrash;

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

	/// <summary>
	/// Un indice pour le tableau du code kinder surpriz
	/// </summary>
	private int indiceToucheActuelleKs = 0;

	/// <summary>
	/// Un indice pour le tableau du code trash
	/// </summary>
	private int indiceToucheActuelleTrash = 0;
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
		int nombreBonbons = UnityEngine.Random.Range(3, 10);
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
		int nombreFourmisNoires = UnityEngine.Random.Range(3, 10);
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
		int nbCailloux = UnityEngine.Random.Range(3, 10);
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
	/// Armageddon version fourmis blanches : des blanches tombent
	/// du ciel et atterissent sur le terrain.
	/// </summary>
	private void Ghostsgedon(){
		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		int nombreFourmisRouges = UnityEngine.Random.Range(3, 10);
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
			                                    Invocations.FOURMI_BLANCHE_GENERALE, 
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
	/// Kinder surpise !
	/// </summary>
	private void KinderSurpriz(){
		GameObject bacAsable = GameObject.FindGameObjectWithTag("BAC_A_SABLE");
		InvocateurObjetsScript scriptInvoc = bacAsable.GetComponent<InvocateurObjetsScript>();
		int nombreOeufs = UnityEngine.Random.Range(2, 5);
		for ( int i = 0; i < nombreOeufs; i++ ){
			int randomX = UnityEngine.Random.Range(55,148);    // Limites en X du terrain
			//int randomY = UnityEngine.Random.Range(15,100);    // Altitudes min et max
			int randomZ = UnityEngine.Random.Range(52,132);     // Limites en Z du terrain
			scriptInvoc.InvoquerObjetAvecOffset( 
			                                    Invocations.OEUF_FOURMI, 
			                                    new Vector3(randomX, 0, randomZ),
			                                    new Vector3(0,0/*randomY*/,0));
		}
	}

	/// <summary>
	/// Saucisse
	/// </summary>
	private void Trash(){
		InvocateurObjetsScript.MODE_TRASH = true;
	}

	/// <summary>
	/// Code pour égrer le konami code
	/// </summary>
	/// <returns><c>true</c>, si c'est bien en rapport avec le konami <c>false</c> sinon.</returns>
	/// <param name="touche">La ouche appuyée qui peut etre en rapport avec le konami code</param>
	private bool GererKonami( KeyCode touche ){
		if ( indiceToucheActuelleKonami < codesTouchesKonami.Length 
		    && touche == codesTouchesKonami[indiceToucheActuelleKonami] ){
			indiceToucheActuelleKonami++;
			if ( indiceToucheActuelleKonami+1 > codesTouchesKonami.Length ){
				indiceToucheActuelleKonami = 0;
				int random = UnityEngine.Random.Range(1,10);
				if ( random <= 2 ){
					Scarabgedon();
				} else if ( random == 9 ){
					Allblacksgedon();
				} else if ( random == 10 ){
					Ghostsgedon();
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
		if ( indiceToucheActuelleRakayou < codesTouchesRakayou.Length
		    && touche == codesTouchesRakayou[indiceToucheActuelleRakayou] ){
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
		if ( indiceToucheActuelleSsqd < codesTouchesSsqd.Length
		    && touche == codesTouchesSsqd[indiceToucheActuelleSsqd] ){
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

	/// <summary>
	/// Code pour gérer le code kinder surpriz
	/// </summary>
	/// <returns><c>true</c>, si c'est bien en rapport avec le code kinder surpriz <c>false</c> sinon.</returns>
	/// <param name="touche">La touche appuyée qui peut etre en rapport avec le kinder surpriz code</param>
	private bool GererKinderSupriz( KeyCode touche ){
		if ( indiceToucheActuelleKs < codesTouchesKs.Length
		    && touche == codesTouchesKs[indiceToucheActuelleKs] ){
			indiceToucheActuelleKs++;
			if ( indiceToucheActuelleKs+1 > codesTouchesKs.Length ){
				indiceToucheActuelleKs = 0;
				KinderSurpriz();
				return true;
			}
		} else {
			indiceToucheActuelleKs = 0;
		}
		return false;
	}

	/// <summary>
	/// Code pour gérer le code trash
	/// </summary>
	/// <returns><c>true</c>, si c'est bien en rapport avec le code trash <c>false</c> sinon.</returns>
	/// <param name="touche">La touche appuyée qui peut etre en rapport avec le trash code</param>
	private bool GererTrash( KeyCode touche ){
		if ( indiceToucheActuelleTrash < codesTouchesTrash.Length
		    && touche == codesTouchesTrash[indiceToucheActuelleTrash] ){
			indiceToucheActuelleTrash++;
			if ( indiceToucheActuelleTrash+1 > codesTouchesTrash.Length ){
				indiceToucheActuelleKs = 0;
				Trash();
				return true;
			}
		} else {
			indiceToucheActuelleTrash = 0;
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
			KeyCode.DownArrow,
			KeyCode.DownArrow,
			KeyCode.UpArrow,
			KeyCode.DownArrow,
			KeyCode.R,
			KeyCode.K,
			KeyCode.U,
		};
		indiceToucheActuelleSsqd = 0;
		codesTouchesSsqd = new KeyCode[]{
			KeyCode.LeftArrow,
			KeyCode.RightArrow,
			KeyCode.LeftArrow,
			KeyCode.RightArrow,
			KeyCode.LeftArrow,
			KeyCode.RightArrow,
			KeyCode.UpArrow,
			KeyCode.DownArrow,
			KeyCode.S,
			KeyCode.Q,
			KeyCode.D
		};
		indiceToucheActuelleKs = 0;
		codesTouchesKs = new KeyCode[]{
			KeyCode.UpArrow,
			KeyCode.LeftArrow,
			KeyCode.DownArrow,
			KeyCode.RightArrow,
			KeyCode.UpArrow,
			KeyCode.LeftArrow,
			KeyCode.DownArrow,
			KeyCode.RightArrow,
			KeyCode.K,
			KeyCode.Z
		};
		codesTouchesTrash = new KeyCode[]{
			KeyCode.S,
			KeyCode.A,
			KeyCode.U,
			KeyCode.C,
			KeyCode.I,
			KeyCode.S,
			KeyCode.S,
			KeyCode.E
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
			if (GererKonami(touche)) return;
			if (GererRakayou(touche)) return;
			if (GererSuicideSquad(touche)) return;
			if (GererKinderSupriz(touche)) return;
			if (GererTrash(touche)) return;
		}
	}
#endregion

}

/// <summary>
/// KonamiCode.cs
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;

/// <summary>
/// Konami code. Et ouais.
/// </summary>
public class KonamiCode : MonoBehaviour {

	/* ********* *
	 * Attributs * 
	 * ********** */

#region Attribut privés
	/// <summary>
	/// Les codes pour les touches
	/// </summary>
	private KeyCode[] codesTouches;

	/// <summary>
	/// Un indice
	/// </summary>
	private int indiceToucheActuelle = 0;
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
#endregion

#region Méthodes package
	/// <summary>
	/// Routine appelée automatiquement par Unity au lancement du script
	/// </summary>
	void Awake(){
		codesTouches = new KeyCode[]{
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
		indiceToucheActuelle = 0;
	}

	/// <summary>
	/// Routine appelée automatiquement par Unity pour traiter les évènements liés
	/// à la GUI/UI
	/// </summary>
	void OnGUI(){
		Event e = Event.current;
		if ( e!= null && e.isKey & Input.anyKeyDown && e.keyCode.ToString () != "None" ){
			KeyCode kc = e.keyCode;
			if ( kc == codesTouches[indiceToucheActuelle] ){
				indiceToucheActuelle++;
				if ( indiceToucheActuelle+1 > codesTouches.Length ){
					indiceToucheActuelle = 0;
					if ( UnityEngine.Random.Range(1,10) <= 3 ){
						Scarabgedon();
					} else {
						Bonbongedon();
					}
				}
			} else {
				indiceToucheActuelle = 0;
			}
		}
	}
#endregion

}

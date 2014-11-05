/// <summary>
/// Rendoring3DCasesScript.cs
/// Script pour appliquer un rendu/une texture sur les gameobjects placé sur les cases
/// (eau, sable)
/// </summary>
/// 
/// <remarks>
/// PY Lapersonne - Version 1.0.0
/// </remarks>

using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

/// <summary>
/// Classe pour appliquer un rendu/une texture sur les gameobjects placé sur les cases
/// (eau, sable)
/// </summary>
public class Rendoring3DCasesScript : MonoBehaviour {


	/* ********* *
	 * Attributs *
	 * ********* */
#region Attributs publics
	/// <summary>
	/// La texture de l'objet
	/// </summary>
	public Texture texture;
#endregion

	/* ******** *
	 * Méthodes *
	 * ******** */
	
#region Méthodes privées
	/// <summary>
	/// Méthode pour créer le rendu du gameobject de l'eau
	/// </summary>
	private void CreerRendu(){
		MeshRenderer meshRender = gameObject.GetComponent<MeshRenderer>();
		meshRender.material.mainTexture = texture;
		meshRender.enabled = false;
	}
#endregion


#region Méthodes package
	/// <summary>
	/// Routine appellée automatiquement par Unity au lancement du script
	/// </summary>
	void Awake(){
		CreerRendu();
	}
#endregion

}

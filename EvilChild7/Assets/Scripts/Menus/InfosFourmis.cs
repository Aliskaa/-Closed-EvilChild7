using UnityEngine;
using System.Collections;

public class InfosFourmis : MonoBehaviour {

	public GUISkin guiSkin;

	public Texture icon_reine;
	public Texture icon_generale;
	public Texture icon_combattante;
	public Texture icon_contremaitre;
	public Texture icon_ouvriere;

	private string def_reine="La reine produit des oeufs. Sa productivité augmente si elle est bien nourrie. Protège-la!";
	private string def_generale="La générale élimine les menaces pesant sur la colonnie.";
	private string def_combattante="La combattante élimine les menaces pesant sur la colonnie.";
	private string def_contremaitre="La contremaitresse laisse des phéromones que l'ouvrière suit pour trouver de la nourriture.";
	private string def_ouvriere="L'ouvrière trouve de la nourriture et la ramène à sa reine. Elle est passive sauf en cas d'attaque.";



	void OnGUI() {

		GUI.skin = guiSkin;

		//GUI.BeginGroup(new Rect(Screen.width / 2 + 100, 10, 300, 100));
		GUI.Box(new Rect(Screen.width / 2 + 100, 10, 230, 70), "Mais qui sont ces fourmis? ");
			//GUI.Box(new Rect(Screen.width-500, 0, 190, 60), "Mais qui sont ces fourmis?");
		GUI.Button(new Rect(Screen.width / 2 + 110, 30, 40, 40), new GUIContent(icon_reine, def_reine));
		GUI.Button(new Rect(Screen.width / 2 + 150, 30, 40, 40), new GUIContent(icon_generale,def_generale));
		GUI.Button(new Rect(Screen.width / 2 + 190, 30, 40, 40), new GUIContent(icon_combattante, def_combattante));
		GUI.Button(new Rect(Screen.width / 2 + 230, 30, 40, 40), new GUIContent(icon_contremaitre, def_contremaitre));
		GUI.Button(new Rect(Screen.width / 2 + 270, 30, 40, 40), new GUIContent(icon_ouvriere, def_ouvriere));

		GUI.Label(new Rect(Screen.width / 2 + 160, 80, 100, 150), GUI.tooltip);
		//GUI.EndGroup();
	}
}

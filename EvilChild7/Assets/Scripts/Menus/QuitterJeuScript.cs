using UnityEngine;
using System.Collections;

public class QuitterJeuScript : MonoBehaviour {
	public Texture2D Quitter;
	private bool quitter = true;
	public void OnMouseUp(){
		
		if(quitter == true){
			Application.Quit();

			quitter = false;
		}
	}
}
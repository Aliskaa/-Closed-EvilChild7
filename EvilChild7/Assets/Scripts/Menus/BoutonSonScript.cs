using UnityEngine;
using System.Collections;

public class BoutonSonScript : MonoBehaviour {
	public Texture2D onAir;
	public Texture2D mutted;
	//private bool sound = true;
	private bool sound = false;
	//public void OnMouseUp(){
	public void Start(){

		// Rapprocher la boite à musique de la caméra
		// pour bien l'entendre
		GameObject camera = GameObject.Find("Main Camera");
		gameObject.transform.localPosition = camera.transform.position;
		gameObject.transform.Translate(100, 0, 100);

		if(sound == true){
			this.guiTexture.texture = mutted;
			audio.Stop();
			//Debug.Log("Son stoppé !");
			sound = false;
		}
		else{
			this.guiTexture.texture = onAir;
			audio.Play();
			//Debug.Log("Son lancé !");
			sound = true;
			
		}
	} 
	/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/
}

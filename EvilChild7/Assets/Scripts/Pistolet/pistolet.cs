/// <summary>
/// Pistolet
///Positionnement du pistolet à eau
/// </summary>
/// <remarks>
/// S Rethore - version 1.0
/// </remarks>
using UnityEngine;
using System.Collections;

public class pistolet : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
		
		transform.rotation = Quaternion.AngleAxis(240, Vector3.up);
		transform.position =  new Vector3(transform.position.x, transform.position.y-0.12f, transform.position.z);
		//transform.localScale *=2;
	}


}

using UnityEngine;
using System.Collections;

public class GetUpgrade : MonoBehaviour {
	
	void OnCollisionEnter(Collision thecollision){
		Debug.Log("Collided with :" + thecollision.gameObject.name);	
	}
	
	// Update is called once per frame
	void Update () {		
	}
}

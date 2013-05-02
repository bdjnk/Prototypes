using UnityEngine;
using System.Collections;

public class PG_Building : MonoBehaviour {
	
	GameObject lightGameObject = null;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (lightGameObject == null) {
	        lightGameObject = new GameObject("The Light");
	        lightGameObject.AddComponent<Light>();
	        lightGameObject.light.color = Color.white;
	        lightGameObject.transform.position = transform.position;
		}
	}
}

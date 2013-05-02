using UnityEngine;
using System.Collections;

public class PG_Shot : MonoBehaviour {
	
	public float persist = 6f;

	// Use this for initialization
	void Start() {
		Destroy(gameObject, persist);    
	}
	
	void OnTriggerEnter(Collider other) {
		Destroy(gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class PG_Gun : MonoBehaviour {
	
	public GameObject shot;
	public float speed = 20f;
	public float rate = 0.5f;
	private float delay = 0;
	
	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire1") && Time.time > delay) {
			delay = Time.time + rate;
			Vector3 pos = transform.position + transform.forward * transform.localScale.z * 1f;
			GameObject clone = Instantiate(shot, pos, transform.rotation) as GameObject;
			clone.rigidbody.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
			//Physics.IgnoreCollision(clone.collider, transform.root.collider);
		}
	}
}

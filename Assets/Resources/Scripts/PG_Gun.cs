using UnityEngine;
using System.Collections;

public class PG_Gun : MonoBehaviour {
	
	public GameObject shot;
	public float speed = 20f; // speed of shot
	public float rate = 0.5f; // rate of fire, portion of a second before firing again
	private float delay = 0;
	
	public Texture2D crosshairImage;
	
	void OnGUI()
	{
		float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
		float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
		GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
	}
	
	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButton("Fire1") && Time.time > delay)
		{
			Screen.showCursor = false;
			delay = Time.time + rate;
			Vector3 pos = transform.position + transform.forward * transform.localScale.z * 1f;
			GameObject clone = Instantiate(shot, pos, transform.rotation) as GameObject;
			clone.rigidbody.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
		}
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			Screen.showCursor = true;
		}
	}

}

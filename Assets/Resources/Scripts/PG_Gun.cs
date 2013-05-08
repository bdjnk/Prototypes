using UnityEngine;
using System.Collections;

public class PG_Gun : MonoBehaviour {
	
	public GameObject shot;
	public float speed = 15f; // speed of shot
	public float rate = 0.2f; // rate of fire, portion of a second before firing again
	public float power = 3f;
	private float delay = 0;
	
	public Texture2D crosshairImage;
	
	void OnGUI()
	{
		if (transform.parent.tag != "bot")
		{
			float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
			float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
			GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
		}
	}
	
	void Start ()
	{
		Screen.showCursor = false;
	}
	
	void Update ()
	{
		if (transform.parent.tag != "bot")
		{
			if (Input.GetButton("Fire1"))
			{
				Screen.showCursor = false;
				Shoot();
			}
			if (Input.GetKeyUp(KeyCode.Escape))
			{
				Network.Disconnect();
				Application.LoadLevel("PrototypeMenu");
			}
		}
	}
	
	public void Shoot()
	{
		if (Time.time > delay)
		{
			delay = Time.time + rate;
			Vector3 pos = transform.position + transform.forward * transform.localScale.z * 1f;
			//GameObject clone = Instantiate(shot, pos, transform.rotation) as GameObject;
			//should change to separate group?
			GameObject clone;
			if (Network.connections.Length > 0)
			//if (Network.isClient || Network.isServer)
			{
				clone = Network.Instantiate(shot, pos, transform.rotation,0) as GameObject;
			}
			else
			{
				clone = Instantiate(shot, pos, transform.rotation) as GameObject;
			}
			clone.rigidbody.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
			clone.GetComponent<PG_Shot>().gun = this;
		}
	}
}

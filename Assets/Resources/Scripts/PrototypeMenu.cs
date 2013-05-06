using UnityEngine;
using System.Collections;

public class PrototypeMenu : MonoBehaviour {

	void OnGUI()
	{
		float height = Screen.height / 6.0f;
		
		if (GUI.Button(new Rect(0, height*0, Screen.width, height), "Basic Controls"))
		{
			Application.LoadLevel("Basics");
		}
		if (GUI.Button(new Rect(0, height*1, Screen.width, height), "Level (Map) Auto Generation"))
		{
			Application.LoadLevel("AutoMap");
		}
		if (GUI.Button(new Rect(0, height*2, Screen.width, height), "Auto Tracking Turret Bot"))
		{
			Application.LoadLevel("TurretBot");
		}
		if (GUI.Button(new Rect(0, height*3, Screen.width, height), "Multiplayer Networking"))
		{
			Application.LoadLevel("Networking");
		}
		if (GUI.Button(new Rect(0, height*4, Screen.width, height), "Upgrades"))
		{
			Application.LoadLevel("Upgrades");
		}
		if (GUI.Button(new Rect(0, height*5, Screen.width, height), "Lighting"))
		{
			Application.LoadLevel("Lighting");
		}
	}
	
	void Start () {
		Screen.showCursor = true;
	}
}

using UnityEngine;
using System.Collections;

public class PrototypeMenu : MonoBehaviour {

	void OnGUI()
	{
		if (GUI.Button(new Rect(0, 0, Screen.width, 50), "Basic Controls"))
		{
			Application.LoadLevel("Basics");
		}
		if (GUI.Button(new Rect(0, 50, Screen.width, 50), "Level (Map) Auto Generation"))
		{
			Application.LoadLevel("AutoMap");
		}
		if (GUI.Button(new Rect(0, 100, Screen.width, 50), "Auto Tracking Turret Bot"))
		{
			Application.LoadLevel("TurretBot");
		}
		if (GUI.Button(new Rect(0, 150, Screen.width, 50), "Multiplayer Networking"))
		{
			Application.LoadLevel("Networking");
		}
		if (GUI.Button(new Rect(0, 200, Screen.width, 50), "Upgrades"))
		{
			Application.LoadLevel("Upgrades");
		}
		if (GUI.Button(new Rect(0, 250, Screen.width, 50), "Lighting"))
		{
			Application.LoadLevel("Lighting");
		}
	}
	
	void Start () {
		Screen.showCursor = true;
	}
}

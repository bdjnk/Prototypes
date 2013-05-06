using UnityEngine;
using System.Collections;

public class TitleMenu : MonoBehaviour {

void OnGUI()
	{
		if (GUI.Button(new Rect(0, 0, Screen.width, 50), "Level (Map) Auto Generation"))
		{
			Application.LoadLevel("AutoMap");
		}
		if (GUI.Button(new Rect(0, 50, Screen.width, 50), "Auto Tracking Turret Bot"))
		{
			Application.LoadLevel("TurretBot");
		}
		if (GUI.Button(new Rect(0, 100, Screen.width, 50), "Networking"))
		{
			Application.LoadLevel("Networking");
		}
	}
}

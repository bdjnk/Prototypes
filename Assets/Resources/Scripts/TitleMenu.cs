using UnityEngine;
using System.Collections;

public class TitleMenu : MonoBehaviour {

void OnGUI()
	{
		if (GUI.Button(new Rect(0, 0, Screen.width, 50), "Auto Mapping"))
		{
			Application.LoadLevel("AutoMap");
		}
	}
}

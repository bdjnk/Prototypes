using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour {
	
	private int playerNumber;
	private int blueTeamTotalPlayers;
	private int redTeamTotalPlayers;
	private string myTeam = "red";
	private bool mouseLooking = true;
	
	private Color playerColor = Color.red;
	
	// Use this for initialization
	void Start () {
		//update data from server
		
		//check team balance
		
		//assign team
		
		//set colors
	}
	
	public string getMyTeamColor(){
		return myTeam;
	}
	
	[RPC]
	void SetMyName(string name){
		this.name = name;	
	}
	
	[RPC]
	void updateTeamCounts(int red,int blue){
		redTeamTotalPlayers += red;
		blueTeamTotalPlayers += blue;
	}
	
	[RPC]
	void setNewPlayerData(string teamColor, string myName) {
		//default color is red
		GameObject playerShotColor = Resources.Load("Prefabs/RedShot") as GameObject;
		Material playerMaterialColor = Resources.Load("Materials/Red") as Material;
		if (teamColor == "blue"){
			playerShotColor = Resources.Load("Prefabs/BlueShot") as GameObject;
			playerMaterialColor = Resources.Load ("Materials/Blue") as Material;
			this.name = "Blue Team Player (" + myName + ")";
		} else {
			this.name = "Red Team Player (" + myName + ")";
		}
		this.GetComponentInChildren<Camera>().GetComponent<PG_Gun>().shot = playerShotColor;
		this.GetComponentInChildren<MeshRenderer>().renderer.material = playerMaterialColor;
	}
	// Update is called once per frame
	void Update () {
	
			//temporary pause updates while p is down
		
		if (Input.GetKeyDown("p")){
			updateMouseLook(!mouseLooking);
			mouseLooking = !mouseLooking;
		}
		
	}
	
	public void updateMouseLook(bool mouseLooking){
		GetComponentInChildren<Camera>().GetComponent<MouseLook>().enabled = mouseLooking;
		//GetComponentInChildren<Camera>().enabled = mouseLooking;
		GetComponent<FPSInputController>().enabled = mouseLooking;
		GetComponent<CharacterMotor>().enabled = mouseLooking;
		GetComponent<MouseLook>().enabled = mouseLooking;
		Screen.lockCursor = mouseLooking;
		Screen.showCursor = mouseLooking;
	
	}
	
}

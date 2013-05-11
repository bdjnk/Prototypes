using UnityEngine;
using System.Collections;

public class PlayerDataScript : MonoBehaviour {
	
	private GameObject mainGame = null;
	private int blueTeamTotalPlayers;
	private int redTeamTotalPlayers;
	
	private int playerNumber;
	private int playerName;
	private int playerNetworkID;
	private bool showGUI = false;
	
	//sizes for buttons
	private float buttonX = Screen.width*0.02f;
	private float buttonY = Screen.width*0.02f;
	private float buttonW = Screen.width*0.12f;
	private float buttonH = Screen.width*0.2f;
	
	
	private string myTeam = "red";
	private bool mouseLooking = true;
	private bool updateGUI = false;
	//private float timeCheck;
	private int guiSecondsToWait = 10;//will display half this time
	
	private string redTeamPlayersString;
	private string blueTeamPlayersString;
	
	private Color playerColor = Color.red;
	
	// Use this for initialization
	void Start () {
		//timeCheck = Time.realtimeSinceStartup;
		mainGame = GameObject.Find ("GameManager");
		//playerNetworkID = Network.player.guid;
		//update data from server
		
		//check team balance
		
		//assign team
		
		//set colors
	}
	/*
	public string getNameFromNetworkID(){
		for (int i=0;i<playerNetworkID.Length;i++){
			if 
		}
		
		return playerNetworkID;
	}
	*/
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
			myTeam = "blue";
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
		
		//set when to update GUI lists
		if((int) Time.realtimeSinceStartup%guiSecondsToWait == 0){
			updateGUI = true;
		} else {
			updateGUI = false;
		}
			
		if (Input.GetKeyDown("t")){
			showGUI = !showGUI;
		}
		
	}
	
	void OnGUI(){
		if(showGUI){
			if(updateGUI){
				//get update to current player lists
				redTeamPlayersString = mainGame.GetComponent<GameManagerScript>().getRedTeamString();
				blueTeamPlayersString = mainGame.GetComponent<GameManagerScript>().getBlueTeamString();
			}
			//display the lists
			if(myTeam == "red")
				GUI.Box(new Rect(buttonX,buttonY,buttonW,buttonH),"Red Team: \n" + redTeamPlayersString);
			if(myTeam == "blue")
				GUI.Box(new Rect(buttonX,buttonY,buttonW,buttonH),"Blue Team: \n" + blueTeamPlayersString);
		}
		
		
	}
	
	public void updateMouseLook(bool mouseLookingSet){
		GetComponentInChildren<Camera>().GetComponent<MouseLook>().enabled = mouseLookingSet;
		GetComponent<FPSInputController>().enabled = mouseLookingSet;
		GetComponent<CharacterMotor>().enabled = mouseLookingSet;
		GetComponent<MouseLook>().enabled = mouseLookingSet;
		Screen.lockCursor = mouseLookingSet;
		Screen.showCursor = mouseLookingSet;
	
	}
	
}

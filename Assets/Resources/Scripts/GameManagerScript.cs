using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{
	private int numberOfPlayers;
	private int redTeamTotalPlayers;
	private int blueTeamTotalPlayers;
	private string[] blueTeamPlayers;
	private string[] redTeamPlayers;
	private int maxPlayers;
	
	void Start(){
		redTeamTotalPlayers = 0;
		blueTeamTotalPlayers = 0;
		maxPlayers = 8;
		blueTeamPlayers = new string[maxPlayers];
		redTeamPlayers = new string[maxPlayers];
		//numberOfPlayers++;
	}
	
	public int getRedTeamCount(){
		return redTeamTotalPlayers;
	}
	
	public int getBlueTeamCount(){
		return blueTeamTotalPlayers;
	}
	
	public string getRedTeamString(){
		string temp = "";
		for (int i = 0;i<redTeamPlayers.Length;i++){
			temp += redTeamPlayers[i] + "\n";
		}
		return temp;
	}
	
	public string getBlueTeamString(){
		string temp = "";
		for (int i = 0;i<blueTeamPlayers.Length;i++){
			temp += blueTeamPlayers[i] + "\n";
		}
		return temp;
	}

	[RPC]
	public void updateTeamData(string teamColor, string name){
		//GameObject playerShotColor = Resources.Load("Prefabs/RedShot") as GameObject;
		//Material playerMaterialColor = Resources.Load ("Materials/Red") as Material;
		
		if(teamColor=="blue"){
			//TO DO: add to team name list 
			blueTeamPlayers[blueTeamTotalPlayers] = name;
			blueTeamTotalPlayers++;
			
			//mainPlayer.name = "Blue Player (" + name +")";
			//playerShotColor = Resources.Load("Prefabs/BlueShot") as GameObject;
			//playerMaterialColor = Resources.Load ("Materials/Blue") as Material;
		}
		else{
			
			redTeamPlayers[redTeamTotalPlayers] = name;
			redTeamTotalPlayers++;
			//mainPlayer.name = "Red Player (" + name +")";
		}
		//mainPlayer.GetComponentInChildren<Camera>().GetComponent<PG_Gun>().shot = playerShotColor;
		//mainPlayer.GetComponentInChildren<MeshRenderer>().renderer.material = playerMaterialColor;

	}
	
	//probably dont need this...?
	[RPC]
	void updateTeamCounts(int red,int blue){
		redTeamTotalPlayers += red;
		blueTeamTotalPlayers += blue;
	}
	
}

using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{
	private int numberOfPlayers;
	private int redTeamTotalPlayers;
	private int blueTeamTotalPlayers;
	
	void Start(){
		redTeamTotalPlayers = 0;
		blueTeamTotalPlayers = 0;
		//numberOfPlayers++;
	}
	
	public int getRedTeamCount(){
		return redTeamTotalPlayers;
	}
	
	public int getBlueTeamCount(){
		return blueTeamTotalPlayers;
	}

	[RPC]
	public void updateTeamData(string teamColor, string name){
		//GameObject playerShotColor = Resources.Load("Prefabs/RedShot") as GameObject;
		//Material playerMaterialColor = Resources.Load ("Materials/Red") as Material;
		
		if(teamColor=="blue"){
			//TO DO: add to team name list 
			blueTeamTotalPlayers++;
			//mainPlayer.name = "Blue Player (" + name +")";
			//playerShotColor = Resources.Load("Prefabs/BlueShot") as GameObject;
			//playerMaterialColor = Resources.Load ("Materials/Blue") as Material;
		}
		else{
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

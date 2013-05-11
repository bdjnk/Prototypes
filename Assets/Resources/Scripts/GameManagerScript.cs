using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{
	private int totalCubesInThisWorld = 54;
	
	private int redTeamTotalPlayers;
	private int blueTeamTotalPlayers;
	private string[] blueTeamPlayers;
	private string[] redTeamPlayers;
	private int redTeamTotalScore;
	private int blueTeamTotalScore;
		
	//array to relate player id to name
	public struct playerInfo{
		public string playerName;
		public string playerGuid;
		public string playerTeamColor;
		public int playerTotalClaims;
		public int playerTotalOwned;
		public int percentOfTeamTotal;
	}
	
	private playerInfo[] allPlayers;//need to initialize!!!
	private int numberOfPlayers;
	
	private int maxPlayers = 8;
	
	void Start(){
		resetAllData();
	}
	
	public void resetAllData(){
		redTeamTotalPlayers = 0;
		blueTeamTotalPlayers = 0;
		blueTeamPlayers = new string[maxPlayers];
		redTeamPlayers = new string[maxPlayers];
		allPlayers = new playerInfo[maxPlayers];
		redTeamTotalScore = 0;
		blueTeamTotalScore = 0;
		for (int i=0;i<maxPlayers;i++){
			allPlayers[i].playerName="";
			allPlayers[i].playerGuid="";
			allPlayers[i].playerTeamColor="";
			allPlayers[i].playerTotalClaims=0;
			allPlayers[i].playerTotalOwned=0;
			allPlayers[i].percentOfTeamTotal=0;
			
			
		}
		
	}
	
	public int getTotalCubes(){
		return totalCubesInThisWorld;
	}
	
	public int getRedTeamCount(){
		return redTeamTotalPlayers;
	}
	
	public int getBlueTeamCount(){
		return blueTeamTotalPlayers;
	}
	
	public int getRedTeamScore(){
		return redTeamTotalScore;
	}
	
	public int getBlueTeamScore(){
		return blueTeamTotalScore;
	}
	
	public int getMyTotalClaims(string myID){
		for (int i=0;i<maxPlayers;i++){
			if(allPlayers[i].playerGuid == myID){
				return allPlayers[i].playerTotalClaims;
			}
		}
		return 0;
	}
	
	public int getMyTotalOwned(string myID){
		for (int i=0;i<maxPlayers;i++){
			if(allPlayers[i].playerGuid == myID){
				return allPlayers[i].playerTotalOwned;
			}
		}
		return 0;
	}
	
	public int getMyPercentage(string myID){
		for (int i=0;i<maxPlayers;i++){
			if(allPlayers[i].playerGuid == myID){
				return allPlayers[i].percentOfTeamTotal;
			}
		}
		return 0;
	}
	
	[RPC]
	public void updatePlayersScore(string newOwnerID,string prevOwnerID){
		if(newOwnerID != ""){//make sure we have an owner
			//search list and update scores
			for (int i=0;i<maxPlayers;i++){
				if(allPlayers[i].playerGuid == newOwnerID){
					allPlayers[i].playerTotalClaims++;
					allPlayers[i].playerTotalOwned++;
				}
				if(prevOwnerID!=""){
					if(allPlayers[i].playerGuid == prevOwnerID){
						allPlayers[i].playerTotalOwned--;
					}
				}
				if(allPlayers[i].playerTeamColor=="red" && getRedTeamScore()!=0){
					allPlayers[i].percentOfTeamTotal = (int) 100.0f * allPlayers[i].playerTotalOwned/getRedTeamScore();
				} else if (getBlueTeamScore()!=0){
					allPlayers[i].percentOfTeamTotal = (int) 100.0f * allPlayers[i].playerTotalOwned/getBlueTeamScore();
				}
			}
		}
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
	
	void Update(){
		
	}
	
	[RPC]
	public void removePlayer(string playerID){
		//get name from ID
		string myName=null;
		string teamColor=null;
		for (int i =0;i<allPlayers.Length;i++){
			//Debug.Log ("Looking for: " + playerID);
			//Debug.Log ("position i: " + i + " =" + allPlayers[i].playerGuid);
			if(allPlayers[i].playerGuid == playerID){
				//Debug.Log ("found at: " + i);
				myName = allPlayers[i].playerName;
				teamColor = allPlayers[i].playerTeamColor;
			}
		}
		
		Debug.Log ("player to remove: " + myName + ", color: " + teamColor);
		
		if (myName !=null){
			//move last item to index of name, remove last item, decrement
			if(teamColor=="blue"){//blue team
				int index = System.Array.IndexOf(blueTeamPlayers,myName);
				blueTeamPlayers[index] = blueTeamPlayers [blueTeamTotalPlayers-1];
				//Debug.Log ("revising index: " + index + " from " + blueTeamPlayers[index] + " to " + blueTeamPlayers[blueTeamTotalPlayers-1]);
				blueTeamPlayers[blueTeamTotalPlayers-1]=null;
				blueTeamTotalPlayers--;
				//Debug.Log ("revised blue team: " + getBlueTeamString());
			}
			else{	//red team 
				int index = System.Array.IndexOf(redTeamPlayers,myName);
				redTeamPlayers[index] = redTeamPlayers [redTeamTotalPlayers-1];
				//Debug.Log ("revising index: " + index + " from " + redTeamPlayers[index] + " to " + redTeamPlayers[redTeamTotalPlayers-1]);
				redTeamPlayers[redTeamTotalPlayers-1]=null;
				redTeamTotalPlayers--;
				//Debug.Log ("revised red team: " + getRedTeamString());
			}
			int indexLocation = 0;
			for (int i=0;i<allPlayers.Length;i++){
				if(allPlayers[i].playerName == myName && allPlayers[i].playerTeamColor == teamColor)
					indexLocation = i;
			}
			allPlayers[indexLocation] = allPlayers[numberOfPlayers-1];
			allPlayers[numberOfPlayers-1] = new playerInfo();
			//Debug.Log ("revised all players: " + allPlayers.ToString());
			numberOfPlayers--;
		}
	}
	[RPC]
	public void blueScore(int hits){
		blueTeamTotalScore += hits;
		if (blueTeamTotalScore<0){
			blueTeamTotalScore = 0;
		}//could also check max?
	}
	
	[RPC]
	public void redScore(int hits){
		redTeamTotalScore += hits;
		if (redTeamTotalScore<0){
			redTeamTotalScore = 0;
		}//could also check max?
	}
	
	
	[RPC]
	public void updateTeamData(string teamColor, string name, string playerID){
				
		if(teamColor=="blue"){

			blueTeamPlayers[blueTeamTotalPlayers] = name;
			blueTeamTotalPlayers++;
			
		}
		else{
			
			redTeamPlayers[redTeamTotalPlayers] = name;
			redTeamTotalPlayers++;
		}
		
		allPlayers[numberOfPlayers] = new playerInfo();
		allPlayers[numberOfPlayers].playerName = name;
		allPlayers[numberOfPlayers].playerGuid = playerID;
		allPlayers[numberOfPlayers].playerTeamColor = teamColor;
		numberOfPlayers++;

	}
	
	//probably dont need this...?
	[RPC]
	void updateTeamCounts(int red,int blue){
		redTeamTotalPlayers += red;
		blueTeamTotalPlayers += blue;
	}
	
}

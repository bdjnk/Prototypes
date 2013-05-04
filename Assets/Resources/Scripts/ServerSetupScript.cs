using UnityEngine;
using System.Collections;


public class ServerSetupScript : MonoBehaviour {
	
	//for testing of object movement
	public GameObject playerPrefab = null;  
	private GameObject mainPlayer = null;  
	public float mSpeed = 5.0f;
	public int playerNumber = 0;
	
	private float buttonX = Screen.width*0.05f;
	private float buttonY = Screen.width*0.05f;
	private float buttonW = Screen.width*0.2f;
	private float buttonH = Screen.width*0.1f;
	
	public float spawnX = 8f;
	public float spawnY = 3f;
	public float spawnZ = -20f;

	
	private bool refreshing = false;
	private HostData[] hostData = null;
	
	private string gameName = "123Paint the Town123";//could make this public if we want
	public string defaultGameInstanceName = "Paint The Town - Ben";
	
	// Use this for initialization
	void Start () {
		//StartServer();
		//TestCube ();
		
	}
	
	// Update is called once per frame
	void Update () {
	
		/*
		  //for testing of object movement
		if(mainPlayer!=null){
			Vector3 translatePosition = new Vector3((Input.GetAxis ("Vertical")  * mainPlayer.transform.forward * 
			 mSpeed).x,0f, (Input.GetAxis ("Vertical")  * mainPlayer.transform.forward *
			 mSpeed).z);	
			
			mainPlayer.transform.position += translatePosition;
			mainPlayer.transform.RotateAround (Vector3.up, Input.GetAxis("Horizontal")*0.1f);
			
		}
		*/
		/*
		if (networkView.name!=playerName){
			GameObject gObj = GameObject.Find(playerName);
			gObj.GetComponent<PlayerScript>().enabled = false;
		
		}
		*/
		
		if(refreshing){
			if(MasterServer.PollHostList().Length > 0){
				refreshing = false;
				Debug.Log (MasterServer.PollHostList().Length);
				hostData = MasterServer.PollHostList();
			}
		}
		

	}
	void refreshHostList(){
		MasterServer.RequestHostList(gameName);
		refreshing = true;
		
	}
		
	
	void OnGUI() {
		if(!Network.isClient && !Network.isServer){
			if(GUI.Button(new Rect(buttonX,buttonY,buttonW,buttonH),"Start Server")){
				Debug.Log ("Starting Server");
				StartServer ();
			}
			
			if(GUI.Button(new Rect(buttonX,buttonY + 1.2f * buttonW,buttonW,buttonH),"Refresh Hosts")){
				Debug.Log ("Refreshing");
				refreshHostList();
				//StartServer ();
			}
			if(hostData!=null){
				for (int i=0;i<hostData.Length;i++)
				{
					if(GUI.Button(new Rect(buttonX*1.5f + buttonW,buttonY*1.2f+(buttonH*i),buttonW*3f,buttonH*0.5f),hostData[i].gameName)){
						Network.Connect(hostData[i]);
						//this wont really work to track largest value of # of players
						//(could get duplicates)
						playerNumber = hostData[i].connectedPlayers + 1;
						
					}
				}
			}	
		}
	}

	void StartServer(){
		//if we want password
		//Network.incomingPassword("test123");
				
		// Use NAT punchthrough if no public IP present 
		//Initialize this server (local)
		Network.InitializeServer(8, 26000, !Network.HavePublicAddress());
		//Network.InitializeServer (32,23466, !Network.HavePublicAddress());
		
		//Use this if using our own local masterserver instead of Unity's
		//MasterServer.ipAddress = "127.0.0.1";
		//MasterServer.port = 23466;
		//MasterServer.dedicatedServer = true;
		
		//Register our game with Unitys Master Server
		MasterServer.RegisterHost(gameName, defaultGameInstanceName, "CSS385 Game");
		
		//Lists the IP address for MasterServer
		//Debug.Log("Master Server Info:" + MasterServer.ipAddress +":"+ MasterServer.port);
	}
	
	void OnServerInitialized(){
		Debug.Log ("Server initialized");
		SpawnPlayer();
	}
	
	void OnConnectedToServer(){
		Debug.Log ("Connected to Server");
		SpawnPlayer();
	}
	
	void SpawnPlayer(){
		//playerNumber++;
		//TODO: make a unique name - check if it already exists before saving it
		mainPlayer = (GameObject) Network.Instantiate(playerPrefab, new Vector3(spawnX,spawnY,spawnZ),Quaternion.identity, 0);    
		mainPlayer.name += playerNumber;
		mainPlayer.GetComponentInChildren<Camera>().enabled = true;
		mainPlayer.GetComponent<FPSInputController>().enabled = true;
		mainPlayer.GetComponent<CharacterMotor>().enabled = true;
		mainPlayer.GetComponent<MouseLook>().enabled = true;
		mainPlayer.GetComponentInChildren<Camera>().GetComponent<MouseLook>().enabled = true;
		mainPlayer.GetComponentInChildren<Camera>().GetComponent<PG_Gun>().enabled = true;
		//mainPlayer.transform.localScale = new Vector3 (10f,10f,10f);
	}
	
	void OnMasterServerEvent(MasterServerEvent mse){
		if(mse == MasterServerEvent.RegistrationSucceeded){
			Debug.Log ("Registered Server");
		}
	
	}
}

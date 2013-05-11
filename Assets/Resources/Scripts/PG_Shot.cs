using UnityEngine;
using System.Collections;

public class PG_Shot : MonoBehaviour
{
	public PG_Gun gun;
	
	public float persist = 6f;
	private float timeAtStart;
	
	
	// Use this for initialization
	void Start()
	{	
		if (!(Network.isServer || Network.isClient)){
			//Destroy(gameObject, persist);
		} 
		
		timeAtStart = Time.time;	
	}
	
	void Update()
	{
		if(gameObject!=null && gameObject.networkView.viewID.ToString() != "0"){
			//persist for network functionality
			if(Time.time - timeAtStart > persist + 1f){
				//Debug.Log ("on delay, network view is: " + networkView.isMine + " id: " + networkView.viewID);
				if ((Network.isServer || Network.isClient) && networkView.isMine)
				{
					//if(gameObject.networkView
					Network.Destroy(gameObject);
				}
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		PG_Cube cubeScript =  other.GetComponent<PG_Cube>();
		
		//PG_Shot temp = new PG_Shot();
		
		//temp = this;
		
		if (cubeScript != null)
		{
			//cubeScript.networkView.RPC("Struck", RPCMode.AllBuffered,this);
			cubeScript.Struck(this);
		}
		
		//other.enabled = false;
		//yield return new WaitForSeconds(5.0F);
		//if (Network.isServer || Network.isClient)
		//Debug.Log ("on trigger, network view is: " + networkView.isMine + " id: " + networkView.viewID);
		
		
		if ((Network.isClient || Network.isServer) && networkView.isMine)
			{	
				Network.Destroy(gameObject);
			
		}
		else {
			//Destroy(gameObject);
		}
		
	}
}

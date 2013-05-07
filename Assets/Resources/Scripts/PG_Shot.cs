using UnityEngine;
using System.Collections;

public class PG_Shot : MonoBehaviour
{
	public float persist = 6f;
	public int power = 3;
	private float timeAtStart;
	
	// Use this for initialization
	void Start()
	{	
		if (!(Network.isServer || Network.isClient)){
			Destroy(gameObject, persist);
		} 
		
		timeAtStart = Time.realtimeSinceStartup;	
	}
	
	void Update()
	{
		//persist for network functionality
		if(Time.realtimeSinceStartup - timeAtStart > persist+1f){
			
			if (Network.isServer || Network.isClient)
			{
				Network.Destroy(gameObject);
			}
		}
			
	}
	
	void OnTriggerEnter(Collider other)
	{
		PG_Cube cubeScript =  other.GetComponent<PG_Cube>();
		if (cubeScript != null)
		{
			//cubeScript.networkView.RPC("Struck", RPCMode.AllBuffered,this);
			cubeScript.Struck(this);
		}
		if (Network.isServer || Network.isClient)
		{
			Network.Destroy(gameObject);
		}
		else {
			Destroy(gameObject);
		}
	}
}

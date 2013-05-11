using UnityEngine;
using System.Collections;

public class PG_Shot : MonoBehaviour
{
	public PG_Gun gun;
	
	public float persist = 6f;//4f in unity
	private float timeAtStart;
	
	
	// Use this for initialization
	void Start()
	{	
		Destroy(gameObject, persist+3f);//cleanup if network doesn't delete
		
		timeAtStart = Time.time;	
	}
	
	void Update()
	{
		//persist for network functionality
		if(Time.time - timeAtStart > persist + 1f){
			//Debug.Log ("on delay, network view is: " + networkView.isMine + " id: " + networkView.viewID);
			if ((Network.isServer || Network.isClient) && networkView.isMine)
			{
				Network.Destroy(gameObject);
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(networkView.isMine){
			Debug.Log ("Collision!" + other.gameObject.ToString());
			PG_Cube cubeScript =  other.GetComponent<PG_Cube>();
			
			if (cubeScript != null)
			{
				cubeScript.Struck(this);
			}
		
			if ((Network.isClient || Network.isServer))
				{	
					Network.Destroy(gameObject);
			}
		}
	}
}

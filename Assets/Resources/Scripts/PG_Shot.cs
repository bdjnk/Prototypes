using UnityEngine;
using System.Collections;

public class PG_Shot : MonoBehaviour
{
	public float persist = 6f;
	public int power = 3;

	// Use this for initialization
	void Start()
	{
		Destroy(gameObject, persist);
	}
	
	void OnTriggerEnter(Collider other)
	{
		PG_Cube cubeScript =  other.GetComponent<PG_Cube>();
		if (cubeScript != null)
		{
			//cubeScript.networkView.RPC("Struck", RPCMode.AllBuffered,this);
			cubeScript.Struck(this);
		}
		if (Network.isServer)
		{
			Network.Destroy(gameObject);
		}
		else {
			Destroy(gameObject);
		}
	}
}

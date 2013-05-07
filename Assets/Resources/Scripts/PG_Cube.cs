using UnityEngine;
using System.Collections;

/* Cubes should keep track of their color
 *    and which building they belong to.
 * They don't even need an update function.
 */
public class PG_Cube : MonoBehaviour
{
	public Material gray;
	public Material red;
	public Material blue;
	
	public int resistence = 3;
	public int maxColor = 4;
	private float amountBlue;
	private float amountRed;
	
	private PG_Building building;
	
	void Start()
	{
		building = transform.parent.GetComponent<PG_Building>();
	}
	
	
	public void Struck(PG_Shot shot)
	{
		foreach (Transform child in transform.parent)
		{
			float distance = Vector3.Distance(transform.position, child.position);
			if (distance < 2.9f) // only consider adjacent cubes
			{
				PG_Cube cubeScript = child.GetComponent<PG_Cube>();
				if (cubeScript != null) // this is a cube
				{
					//cubeScript.networkView.RPC ("Effects",RPCMode.AllBuffered,shot,distance);
					cubeScript.Effects(shot, distance);
				}
			}
		}
	}
	
	
	public void Effects(PG_Shot shot, float distance)
	{
		float effect = shot.power - distance;
		
		if (shot.renderer.sharedMaterial == blue)
		{
			amountRed = Mathf.Max(0, amountRed - effect);
			amountBlue = Mathf.Min(maxColor, amountBlue + effect);
			
			if (amountBlue > resistence)
			{
				networkView.RPC ("UpdateCubeMaterial",RPCMode.AllBuffered,"blue");
				//renderer.material = blue;
			}
		}
		else if (shot.renderer.sharedMaterial == red)
		{
			amountBlue = Mathf.Max(0, amountBlue - effect);
			amountRed = Mathf.Min(maxColor, amountRed + effect);
			
			if (amountRed > resistence)
			{
				networkView.RPC ("UpdateCubeMaterial",RPCMode.AllBuffered,"red");
				//renderer.material = red;
			}
		}
	}
	
	[RPC]
	public void UpdateCubeMaterial(string newMaterial){
	
		if (newMaterial == "blue"){
			renderer.material = blue;
		} else if (newMaterial == "red"){
			renderer.material = red;
		}
			
	}
}

using UnityEngine;
using System.Collections;

/* Cubes should keep track of their color
 *    and which building they belong to.
 * They don't even need an update function.
 */
public class PG_Cube : MonoBehaviour
{
	public Material gray;
	public Texture red;
	public Texture blue;
	
	public GUITexture test;
	
	public int resistence = 4;
	public int maxColor = 5;
	private float amountBlue;
	private float amountRed;
	
	private PG_Building building;
	
	void Start()
	{
		building = transform.parent.GetComponent<PG_Building>();
	}
	
	public void Struck(PG_Shot shot)
	{		
		foreach (Transform child in transform.parent) // splash effect
		{
			float distance = Vector3.Distance(transform.position, child.position);
			//testing for minimum reaction on adjacent
			if (distance < 2.9f) // only consider adjacent cubes
			{
				PG_Cube cubeScript = child.GetComponent<PG_Cube>();
				if (cubeScript != null) // this is a cube
				{
					cubeScript.Effects(shot, distance);
				}
			}
		}
		
		Texture upgrade = renderer.material.GetTexture("_DecalTex");
		if (upgrade != null)
		{
			/* this is silly, fix with an enum in production
			 * 
			 * advantages:
			 * 
			 */
			if (upgrade.name == "BlastShots")
			{
				if (shot.gun.bs == null) {
					shot.gun.bs = Resources.Load("Textures/BlastShots") as Texture;
					shot.gun.power += 2;
				}
			}
			else if (upgrade.name == "EvadeBots")
			{
				if (shot.gun.eb == null) {
					shot.gun.eb = Resources.Load("Textures/EvadeBots") as Texture;
					// do nothing else, for now
				}
			}
			else if (upgrade.name == "FastShots")
			{
				if (shot.gun.fs == null) {
					shot.gun.fs = Resources.Load("Textures/FastShots") as Texture;
					shot.gun.speed *= 2;
				}
			}
			else if (upgrade.name == "QuickMove")
			{
				if (shot.gun.qm == null) {
					shot.gun.qm = Resources.Load("Textures/QuickMove") as Texture;
					CharacterMotor cm = shot.gun.transform.parent.GetComponent<CharacterMotor>();
					cm.jumping.baseHeight = 4;
					cm.movement.maxForwardSpeed *= 2;
					cm.movement.maxSidewaysSpeed *= 2;
					cm.movement.maxBackwardsSpeed *= 2;
					cm.movement.maxGroundAcceleration *= 3;
				}
			}
			else if (upgrade.name == "RapidFire")
			{
				if (shot.gun.rf == null) {
					shot.gun.rf = Resources.Load("Textures/RapidFire") as Texture;
					shot.gun.rate /= 2;
				}
			}
			renderer.material.SetTexture("_DecalTex", null);
		}
	}
	
	public void Effects(PG_Shot shot, float distance)
	{
		float effect = shot.gun.power - distance;
		
		Texture texture = shot.renderer.sharedMaterial.mainTexture;
		if (texture == blue)
		{
			amountRed = Mathf.Max(0, amountRed - effect);
			amountBlue = Mathf.Min(maxColor, amountBlue + effect);
			
			if (amountBlue > resistence)
			{
				if (Network.isClient || Network.isServer)
				{
					networkView.RPC("UpdateCubeMaterial", RPCMode.AllBuffered, "blue");
				}
				else // remove when all shall be networked (in the final game)
				{
					//renderer.material = blue;
					renderer.material.SetTexture("_MainTex", blue);
				}
			}
		}
		else if (texture == red)
		{
			amountBlue = Mathf.Max(0, amountBlue - effect);
			amountRed = Mathf.Min(maxColor, amountRed + effect);
			
			if (amountRed > resistence)
			{
				if (Network.isClient || Network.isServer)
				{
					networkView.RPC("UpdateCubeMaterial", RPCMode.AllBuffered, "red");
				}
				else // remove when all shall be networked (in the final game)
				{
					//renderer.material = red;
					renderer.material.SetTexture("_MainTex", red);
				}
			}
		}
	}
	
	[RPC]
	public void UpdateCubeMaterial(string newMaterial){
	
		if (newMaterial == "blue")
		{
			//renderer.material = blue;
			renderer.material.SetTexture("_MainTex", blue);
		}
		else if (newMaterial == "red")
		{
			//renderer.material = red;
			renderer.material.SetTexture("_MainTex", red);
		}
			
	}
}

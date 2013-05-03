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
	
	private PG_Building building;
	
	void Start()
	{
		building = transform.parent.GetComponent<PG_Building>();
	}
	
	public void Struck(Material color)
	{
		if (color == blue)
		{
			//TODO tell building script for analysis and response
			renderer.material = blue;
			building.Struck(transform.position);
		}
		else if (color == red)
		{
			//TODO tell building script for analysis and response
			renderer.material = red;
			building.Struck(transform.position);
		}
	}
}

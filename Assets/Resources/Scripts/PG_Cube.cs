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
	
	public void Struck(Material color)
	{
		if (color == red)
		{
			renderer.material = red;
		}
	}
}

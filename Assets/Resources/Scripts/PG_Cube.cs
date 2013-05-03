using UnityEngine;
using System.Collections;

/* Cubes should keep track of their color
 *    and which building they belong to.
 * They don't even need an update function.
 */
public class PG_Cube : MonoBehaviour
{
	private Color color = Color.gray;
	
	void Start()
	{
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		
	}
}

using UnityEngine;
using System.Collections;

/* Buildings should keep track of:
 * 
 * 
 */
public class PG_Building : MonoBehaviour
{
	void Update()
	{
	}
	
	public void Struck(Vector3 pos)
	{
		foreach (Transform child in transform)
		{
			float distance = Vector3.Distance(pos, child.position);
			if (distance < 2)
			{
				Debug.Log("distance = " + distance);
			}
		}
	}
}

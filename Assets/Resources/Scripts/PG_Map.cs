using UnityEngine;
using System.Collections;

public class PG_Map : MonoBehaviour
{
	public bool flat = true;
	public GameObject cube;
	
	// Use this for initialization
	void Start()
	{
		Vector3 offset = Vector3.zero;
		Vector3 temp = Vector3.zero;
		
		int width = Random.Range(3, 6);
		int height = Random.Range(3, 6);
		int depth = Random.Range(3, 6);
		
		for (int w = 0; w < width; w++)
		{
			for (int h = 0; h < height; h++)
			{
				for (int d = 0; d < depth; d++)
				{
					temp.Set(offset.x * w, offset.y * h, offset.z * d);
					offset = Vector3.Max(offset, MakeBuilding(temp));
				}
			}
		}
	}
	
	Vector3 MakeBuilding(Vector3 offset)
	{
		GameObject building = new GameObject("Building");
		
		int width = Random.Range(1, 5);
		int height = Random.Range(1, 5);
		int depth = Random.Range(1, 5);
		
		for (int w = 0; w < width; w++)
		{
			for (int h = 0; h < height; h++)
			{
				for (int d = 0; d < depth; d++)
				{
					GameObject c = Instantiate(cube) as GameObject;
			    	c.transform.parent = building.transform;
					c.transform.localPosition = new Vector3(1.5f * w, 1.5f * h, 1.5f * d);
				}
			}
		}
		building.transform.position += offset;
		
		return new Vector3(width * 1.5f, height * 1.5f, depth * 1.5f);
	}
}

using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
	public GameObject cube;
	
	// Use this for initialization
	void Start ()
	{
		GameObject building = new GameObject();
		
		int width = 2;
		int height = 2;
		int depth = 2;
		
		for (int w = 0; w < width; w++)
		{
			for (int h = 0; h < height; h++)
			{
				for (int d = 0; d < depth; d++)
				{
					GameObject c = Instantiate(cube) as GameObject;
			    	c.transform.parent = transform;
					c.transform.position = new Vector3(1.5f * w, 1.5f * h, 1.5f * d);
				}
			}
		}
		building.transform.localPosition = new Vector3(-1, 2, -1);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

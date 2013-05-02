using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
	public GameObject cube;
	
	// Use this for initialization
	void Start ()
	{
		GameObject building = new GameObject();
		
		int width = 3;
		int height = 4;
		int depth = 2;
		
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
		building.transform.position += new Vector3(-3, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

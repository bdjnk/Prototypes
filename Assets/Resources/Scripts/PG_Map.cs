using UnityEngine;
using UnityEditor;
using System.Collections;

public class PG_Map : MonoBehaviour
{
	public bool floor;
	public float spacing;
	
    public int[] citySize = {6, 1, 6};
	
    public int[] minBuildingSize = {1, 1, 1};
    public int[] maxBuildingSize = {3, 3, 3};
	
	public GameObject cube;
	
	// Use this for initialization
	void Start()
	{
		Vector3 offset = Vector3.zero;
		Vector3 temp = Vector3.zero;
		
		int width = citySize[0];
		int height = citySize[1];
		int depth = citySize[2];
		
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
		
		Vector3 center = Vector3.zero;
		
		int width = Random.Range(minBuildingSize[0], maxBuildingSize[0]+1);
		int height = Random.Range(minBuildingSize[1], maxBuildingSize[1]+1);
		int depth = Random.Range(minBuildingSize[2], maxBuildingSize[2]+1);
		
		for (int w = 0; w < width; w++)
		{
			for (int h = 0; h < height; h++)
			{
				for (int d = 0; d < depth; d++)
				{
					GameObject c = Instantiate(cube) as GameObject;
			    	c.transform.parent = building.transform;
					c.transform.localPosition = new Vector3(1.5f * w, 1.5f * h, 1.5f * d);
					c.AddComponent("PG_Cube");
					
					center += c.transform.position;
				}
			}
		}
		building.transform.position += offset;
		building.AddComponent("PG_Building");
		
		int count = width * height * depth;
		center /= count;
		
		GameObject light = new GameObject("Light");// as GameObject;
		light.AddComponent(typeof(Light));
		light.transform.parent = building.transform;
		light.transform.localPosition = center;
		light.light.intensity = count / 20f;
		
		return (new Vector3(width * 1.5f, height * 1.5f, depth * 1.5f) * spacing);
	}
}

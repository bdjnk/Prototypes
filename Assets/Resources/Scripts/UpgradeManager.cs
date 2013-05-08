using UnityEngine;
using System.Collections;

public class UpgradeManager : MonoBehaviour {
	
	private GameObject[] cubes;

	// Use this for initialization
	void Start () {
		cubes = GameObject.FindGameObjectsWithTag("Cube");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.E))
		{
			GameObject cube = cubes[Random.Range(0, cubes.Length)]; // O(1)
			
		}
	}
}

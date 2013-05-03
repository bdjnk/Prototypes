using UnityEngine;
using System.Collections;

public class PG_Fly : MonoBehaviour {
	
	//private bool flight = false;
	
	public CharacterMotor motor;

	// Use this for initialization
	void Start () {
		motor = GameObject.Find("First Person Controller").GetComponent<CharacterMotor>();
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if(Input.GetKey(KeyCode.Space))
			motor.movement.gravity = 0;
		else
			motor.movement.gravity = 20;
			*/
	}
}

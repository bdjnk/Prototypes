using UnityEngine;
using System.Collections;

public class PG_Bot : MonoBehaviour
{
	public float fov = 60.0f;
	private RaycastHit hit;
	
	public GameObject shot;
	public float speed = 20f; // speed of shot
	public float rate = 0.5f; // rate of fire, portion of a second before firing again
	private float delay = 0;
	
	private Transform enemy;
	
	void Start ()
	{
		enemy = GameObject.Find("First Person Controller").transform;
		// make an array of transforms, and use GameObject.FindGameObjectsWithTag("blue");
	}
	
	void Update ()
	{
		if (Time.time > delay
			&& Vector3.Distance(enemy.position, transform.position) < 30
			&& LineOfSight(enemy))
		{
			Screen.showCursor = false;
			delay = Time.time + rate;
			transform.forward = enemy.position - transform.position;
			Vector3 pos = transform.position + transform.forward * transform.localScale.z;// * 1f;
			GameObject clone = Instantiate(shot, pos, transform.rotation) as GameObject;
			clone.rigidbody.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
		}
	}
	
	private bool LineOfSight(Transform target)
	{
		if (Vector3.Angle(target.position - transform.position, transform.forward) <= fov
			&& Physics.Linecast(transform.position, target.position, out hit)
			&& hit.collider.transform == target)
		{
			return true;
		}
		return false;
	}
}

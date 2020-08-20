using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltrasonicSensor : MonoBehaviour
{
	public GameObject from;
	public List<GameObject> objects = new List<GameObject>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }

	void OnTriggerEnter(Collider other)
	{
		objects.Add(other.gameObject);
	}

	void OnTriggerStay(Collider other)
	{
		Debug.DrawLine(from.transform.position, other.ClosestPointOnBounds(from.transform.position),new Color(255,0,0));
	}

	void OnTriggerExit(Collider other)
	{
		objects.Remove(other.gameObject);
	}
}

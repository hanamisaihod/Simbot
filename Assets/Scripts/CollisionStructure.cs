using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionStructure : MonoBehaviour
{
    [HideInInspector]
    public List<Collider> colliders = new List<Collider>();

    void OnTriggerEnter(Collider c)
    {
        Debug.Log("c.tag:" + c.tag);
        if(c.tag == "Building")
        {
            colliders.Add(c);
        }
    }

    void OnTriggerExit(Collider c)
    {
        if(c.tag == "Building")
        {
            colliders.Remove(c);
        }
    }
}

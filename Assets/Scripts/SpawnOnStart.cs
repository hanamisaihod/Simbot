using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnStart : MonoBehaviour
{
    public static Transform startPosition;
    public static bool Onstart = false;
    // Start is called before the first frame update
    void Start()
    {
        
        startPosition = gameObject.transform;
        StartCoroutine(getStart());
        
    }
    IEnumerator getStart()
    {
        yield return new WaitForSeconds(1f);
        Onstart = true;
    }
}

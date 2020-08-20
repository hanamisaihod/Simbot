using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableHighlight : MonoBehaviour
{
    public List<GameObject> Highlight = new List<GameObject>();
    // Update is called once per frame
    void UpdateWhenAttach()
    {
        Rigidbody rb;
        BoxCollider bc;
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject item in allObjects)
        {
            if(item.GetComponent<Rigidbody>() != null)
            {
                Highlight.Add(item);
            }
        }

        foreach (GameObject go in Highlight)
        {
            //Highlight
        }
    }
}

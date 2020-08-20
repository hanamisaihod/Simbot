using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public float zoom;
    public float scroll;
    // Start is called before the first frame update
    void Start()
    {
        zoom = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miniboss1Mute : MonoBehaviour
{
    private CanvasFX_Controller canvasFX;
    public AudioSource miniboss1AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        canvasFX = FindObjectOfType<CanvasFX_Controller>();
    }

    void Update()
    {
        if (canvasFX.won)
		{
            miniboss1AudioSource.Stop();
        }
    }
}

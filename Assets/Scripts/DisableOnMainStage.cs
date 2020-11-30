using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableOnMainStage : MonoBehaviour
{
    public GameObject slideFrame;
    public GameObject confirm;
    public GameObject delete;
    public GameObject rotate;
    public GameObject save;

    // Start is called before the first frame update
    public void disableOnMain()
    {
        slideFrame.SetActive(false);
        confirm.SetActive(false);
        delete.SetActive(false);
        rotate.SetActive(false);
        save.SetActive(false);
    }

}

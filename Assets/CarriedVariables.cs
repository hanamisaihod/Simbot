using UnityEngine;

public class CarriedVariables : MonoBehaviour
{
    public string currentMapName;
    public bool newMap = false;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}

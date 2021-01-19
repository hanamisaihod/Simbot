using UnityEngine;

public class CarriedVariables : MonoBehaviour
{
    public static string currentMapName;    
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}

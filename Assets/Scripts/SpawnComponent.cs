using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] private GameObject modelPrefab;
    public static GameObject model;
    public void OnClickSpawn()
    {
        GameObject parentComponent = GameObject.Find("Component");
        Debug.Log("parentComponent.transform.childCount = "+ parentComponent.transform.childCount);
        if(parentComponent.transform.childCount >= 1)
        {
            Debug.Log("222222222222222");
            GameObject CurrentChild = parentComponent.transform.GetChild(0).gameObject;
            Destroy(CurrentChild);
            model = Instantiate(modelPrefab, new Vector3(0,1,0),Quaternion.identity);
        
            parentComponent.transform.localScale = new Vector3(1,1,1);
            model.transform.SetParent(parentComponent.transform);
            parentComponent.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        }
        else
        {
            Debug.Log("11111111111111");
            model = Instantiate(modelPrefab, new Vector3(0,1,0),Quaternion.identity);
        
            parentComponent.transform.localScale = new Vector3(1,1,1);
            model.transform.SetParent(parentComponent.transform);
            parentComponent.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotStatus : MonoBehaviour
{
	private float maxHealth = 100;
	public float playerHealth = 100;
	public int complete = 0; // -1 is lose and 1 is win
	public bool falling;
	public RectTransform healthRect;
	public GameObject boomController;

    void Start()
    {
		healthRect = GameObject.Find("HP_Blood").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void DamagePlayer(float damage)
	{
		Debug.Log("Damge: " + damage);
		playerHealth -= damage;
		healthRect.sizeDelta = new Vector2(healthRect.sizeDelta.x - (damage*3.5f), healthRect.sizeDelta.y);
		boomController.GetComponent<Robot_Boom_Controller>().UpdateBoomEffect(playerHealth, maxHealth);
	}
}

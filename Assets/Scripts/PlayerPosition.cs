using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPosition : MonoBehaviour
{
    public static GameObject PlayerObject;
    public static bool isMovingForward = false;
    private  Vector3 startMovingPos;
    private Vector3 goalPos;
    private float movingTimer;
    public float movingSeconds = 1;
    public static int moveToUserInputGrid;
    void Update()
    {
        if(SpawnOnStart.Onstart == true)
        {
			gameObject.transform.position = SpawnOnStart.startPosition.position;
            gameObject.transform.rotation = SpawnOnStart.startPosition.rotation;
            gameObject.transform.position = gameObject.transform.position + new Vector3(0,-0.302f,0);
            SpawnOnStart.Onstart = false;
        }
        if(Input.GetKeyDown("space")) // get input to move forward
        {
            moveToUserInputGrid = 1;
            StartCoroutine(moveForward(moveToUserInputGrid));            
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            //anim.SetTrigger("Rotate");
            StartCoroutine(RotatingRight());
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //anim.SetTrigger("Rotate");
            StartCoroutine(RotatingLeft());
        }
        PlayerObject = gameObject;
    }

	public void callMoveForward()
	{
		moveToUserInputGrid = 1;
		StartCoroutine(moveForward(moveToUserInputGrid));
	}
	public void callRotateLeft()
	{
		StartCoroutine(RotatingLeft());
	}
	public void callRotateRight()
	{
		StartCoroutine(RotatingRight());
	}

	IEnumerator moveForward(int number)
    {
        //Debug.Log(gameObject.transform.position);
        //Debug.Log("gameObject: "+ gameObject.name);
        
        Vector3 playerDirection = gameObject.transform.forward;
        playerDirection.x = (float) Math.Round(playerDirection.x,MidpointRounding.AwayFromZero);
        playerDirection.y = (float) Math.Round(playerDirection.y,MidpointRounding.AwayFromZero);
        playerDirection.z = (float) Math.Round(playerDirection.z,MidpointRounding.AwayFromZero);
       // Debug.Log("Game Forward x : "+ playerDirection.x + "Game Forward y : "+ playerDirection.y + "Game Forward z : "+ playerDirection.z);
        Vector3 nextPos = gameObject.transform.position + gameObject.transform.forward;
        nextPos.x = (float) Math.Round(nextPos.x,MidpointRounding.AwayFromZero);
        nextPos.y = (float) Math.Round(nextPos.y,MidpointRounding.AwayFromZero);
        nextPos.z = (float) Math.Round(nextPos.z,MidpointRounding.AwayFromZero);
        foreach (GameObject item in DetectEnvironment.keepPosition)
        {
           // Debug.Log(nextPos.x + "=" + item.transform.position.x + "tag" + item.tag + nextPos.y + "=" + item.transform.position.y + "tag" + item.tag + nextPos.z + "=" + item.transform.position.z + "tag" + item.tag);
            if(nextPos.x == item.transform.position.x && nextPos.y == item.transform.position.y && nextPos.z == item.transform.position.z && item.tag != "Wall")
            {
                //Debug.Log("Check2");
                for (int i = 0; i < item.transform.childCount; i++)
                {
                    
                    GameObject child = item.transform.GetChild(i).gameObject;
                    if(child.tag != "Wall")
                    {
                        //Debug.Log("Forward x : "+ playerDirection.x + "Forward y : "+ playerDirection.y + "Forward x : "+ playerDirection.z);
                        if(playerDirection.x == 1 || playerDirection.x == -1)
                        {
                            //Debug.Log("????");
                            isMovingForward = true;
                            LeanTween.moveX(gameObject,nextPos.x + number-1,number).setEaseOutCubic();
                            yield return new WaitForSeconds(number);
                            isMovingForward = false;
                        }

                        if(playerDirection.y == 1 || playerDirection.y == -1)
                        {
                            isMovingForward = true;
                            LeanTween.moveY(gameObject,nextPos.y + number-1,number).setEaseOutCubic();
                            yield return new WaitForSeconds(number);
                            isMovingForward = false;
                        }

                        if(playerDirection.z == 1 || playerDirection.z == -1)
                        {
                            isMovingForward = true;
                            LeanTween.moveZ(gameObject,nextPos.z + number-1,number).setEaseOutCubic();
                            yield return new WaitForSeconds(number);
                            isMovingForward = false;
                        }

                    }
                    
                }
            }                
        }
    }

    IEnumerator RotatingRight()
    {
        //Debug.Log("robotParent: "+ robotParent.name);
        LeanTween.rotateAround(gameObject, Vector3.up, 90, 0.67f).setEaseInOutBack();
        yield return new WaitForSeconds(0.67f);
    }

    IEnumerator RotatingLeft()
    {
        //Debug.Log("robotParent: "+ robotParent.name);
        LeanTween.rotateAround(gameObject, Vector3.up, -90, 0.67f).setEaseInOutBack();
        yield return new WaitForSeconds(0.67f);
    }

}

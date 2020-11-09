using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovementTest : MonoBehaviour
{
    public float speed;
    public float torque;
    public float delay;
    public float currentDelay;
    public bool isOnIce = false;
    public bool startedReading;
    private Rigidbody rbd;

    private void Start()
    {
        rbd = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {   
        if (delay > 0.01999961)
        {
            if (speed != 0)
            {
                if (isOnIce)
                {
                    rbd.AddForce(transform.forward * 2 * speed);
                }
                else
                {
                    rbd.velocity = transform.forward * speed / 0.99f;
                }
            }
            if (torque != 0)
            {
                if (isOnIce)
                {
                    rbd.AddTorque(transform.up * torque /4.0f / 57.273f);
                }
                else
                {
                    rbd.angularVelocity = transform.up * torque/ 57.273f / 0.99f;
                }
            }
            delay -= Time.fixedDeltaTime;
        }
        else
        {
            if (!isOnIce)
            {
                rbd.velocity = new Vector3(0, 0, 0);
                rbd.angularVelocity = new Vector3(0, 0, 0);
            }
            if (startedReading)
            {
                if (nextBlock)
                {
                    ReadBlock(nextBlock);
                }
            }
        }

    }

    public void StartReading()
    {
        currentTimes = 0;
        repeats = new List<GameObject>();
        GameObject startBlock = GameObject.FindGameObjectWithTag("StartBlock");
        if (!startBlock.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().attachedBy)
        {
            Debug.LogError("There is nothing connected to the start block!");
        }
        else
        {
            Debug.Log("Start interpreting . . .");
            ReadBlock(startBlock.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().attachedBy.transform.parent.gameObject);
            startedReading = true;
        }
    }
    public List<GameObject> repeats;
    public GameObject player;
    public int currentTimes = 0;
    private GameObject nextBlock; // set this as the next block of read next block

    public void ReadBlock(GameObject block)
    {
        currentTimes = 0;
        Debug.Log(block.name);
        if (block.tag == "DoBlock")
        {
            speed = block.GetComponent<BuildingHandler>().speedChoice;
            torque = block.GetComponent<BuildingHandler>().torqueChoice;
            delay = block.GetComponent<BuildingHandler>().delayChoice;

            if (block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy)
            {
                nextBlock = block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy.transform.parent.gameObject;
            }
            else
            {
                if (repeats.Count > 0)
                {
                    nextBlock = repeats[repeats.Count - 1];
                }
                else
                {
                    nextBlock = null;
                }
            }
        }
        else if (block.tag == "IfBlock")
        {
            if (CheckIf())
            {

            }
        }
        else if (block.tag == "RepeatBlock")
        {
            if (CheckRepeat(block))
            {
                if (!repeats.Contains(block))
                {
                    repeats.Add(block);
                }
                if (block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().attachedBy)
                {
                    nextBlock = (block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().attachedBy.transform.parent.gameObject);
                }
                else
                {
                    if (repeats.Count > 0)
                    {
                        nextBlock = repeats[repeats.Count - 1];
                    }
                    else
                    {
                        nextBlock = null;
                    }
                }
            }
            else
            {
                if (block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy)
                {
                    nextBlock = block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy.transform.parent.gameObject;
                }
                else
                {
                    if (repeats.Count > 0)
                    {
                        nextBlock = repeats[repeats.Count - 1];
                    }
                    else
                    {
                        nextBlock = null;
                    }
                }
            }
        }
    }

    public bool CheckRepeat(GameObject block)
    {
        if (block.GetComponent<BuildingHandler>().repeatChoice == 0) // Forever
        {
            if (!block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().attachedBy)
            {
                return false;
            }
        }
        else if (block.GetComponent<BuildingHandler>().repeatChoice == 1) // X times
        {
            if (block.GetComponent<BuildingHandler>().currentRepeatTimes < block.GetComponent<BuildingHandler>().timesChoice)
            {
                block.GetComponent<BuildingHandler>().currentRepeatTimes++;
                return true;
            }
            else
            {
                repeats.Remove(block);
                block.GetComponent<BuildingHandler>().currentRepeatTimes = 0;
                return false;
            }
        }
        else
        {
            if (CheckIf())
            {

            }
            else
            {

            }
        }
        return true; //remove
    }

    public bool CheckIf()
    {
        //Check if conditions (have to work on sensors first)
        return true;
    }
}
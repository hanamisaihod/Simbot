using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovementTest : MonoBehaviour
{
    public float speed;
    public float torque;
    public float delay;
    public float currentDelay;
    public float degree;
    public float distance;
    public float distanceFromHit;
    public bool isOnIce = false;
    public bool startedReading;
    private Rigidbody rbd;
    public GameObject distanceSensor;
    public GameObject leftColorSensor;
    public GameObject rightColorSensor;

    private void Start()
    {
        rbd = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (delay == Mathf.Infinity)
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
            else
            {
                if (!isOnIce)
                {
                    rbd.velocity = new Vector3(0, 0, 0);
                }
            }
            if (torque != 0)
            {
                if (isOnIce)
                {
                    rbd.AddTorque(transform.up * torque / 4.0f / 57.273f);
                }
                else
                {
                    rbd.angularVelocity = transform.up * torque / 57.273f / 0.99f;
                }
            }
            else
            {
                if (!isOnIce)
                {
                    rbd.angularVelocity = new Vector3(0, 0, 0);
                }
            }
            if (startedReading)
            {
                if (nextBlock)
                {
                    ReadBlock(nextBlock);
                }
            }
        }
        else if (delay > 0.01999961)
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
            else
            {
                if (!isOnIce)
                {
                    rbd.velocity = new Vector3(0, 0, 0);
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
            else
            {
                if (!isOnIce)
                {
                    rbd.angularVelocity = new Vector3(0, 0, 0);
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
    private GameObject nextBlock;

    private void ReadBlock(GameObject block)
    {
        currentTimes = 0;
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
            degree = block.GetComponent<BuildingHandler>().degreeChoice;
            distance = block.GetComponent<BuildingHandler>().distanceChoice;
            if (CheckIf(block))
            {
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
        else if (block.tag == "RepeatBlock")
        {
            degree = block.GetComponent<BuildingHandler>().degreeChoice;
            distance = block.GetComponent<BuildingHandler>().distanceChoice;
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

    private bool CheckRepeat(GameObject block)
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
        else if (block.GetComponent<BuildingHandler>().repeatChoice == 2) // Distance
        {
            if (CheckDistanceSensor(block))
            {
                return true;
            }
            else
            {
                repeats.Remove(block);
                return false;
            }
        }
        else // Color
        {
            if (CheckColorSensor(block))
            {
                return true;
            }
            else
            {
                repeats.Remove(block);
                return false;
            }
        }
        return true; //remove
    }

    private bool CheckIf(GameObject block)
    {
        Debug.Log("Enter if:" + block.name);
        if (block.GetComponent<BuildingHandler>().ifChoice == 0)
        {
            if (CheckDistanceSensor(block))
            {
                return true;
            }
        }
        else
        {
            if (CheckColorSensor(block))
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckDistanceSensor(GameObject block)
    {
        RaycastHit hit;
        Vector3 sensorForward = distanceSensor.transform.forward;
        Quaternion spreadAngle = Quaternion.AngleAxis(degree, new Vector3(0, 1, 0));
        Vector3 sensorAngle = spreadAngle * sensorForward;
        Physics.Raycast(distanceSensor.transform.position, sensorAngle, out hit);
        //distanceFromHit = Vector3.Distance(distanceSensor.transform.position, hit.point);
        distanceFromHit = hit.distance;
        if (distanceFromHit == 0)
        {
            distanceFromHit = Mathf.Infinity;
        }
        switch (block.GetComponent<BuildingHandler>().compareDegreeChoice)
        {
            case 0:
                if (distanceFromHit == distance)
                {
                    return true;
                }
                break;
            case 1:
                if (distanceFromHit != distance)
                {
                    return true;
                }
                break;
            case 2:
                if (distanceFromHit < distance)
                {
                    return true;
                }
                break;
            case 3:
                if (distanceFromHit <= distance)
                {
                    return true;
                }
                break;
            case 4:
                if (distanceFromHit > distance)
                {
                    return true;
                }
                break;
            case 5:
                if (distanceFromHit >= distance)
                {
                    return true;
                }
                break;
        }
        return false;
    }

    private bool CheckColorSensor(GameObject block)
    {
        RaycastHit leftHit;
        RaycastHit rightHit;
        Physics.Raycast(leftColorSensor.transform.position, -leftColorSensor.transform.up, out leftHit);
        Physics.Raycast(rightColorSensor.transform.position, -rightColorSensor.transform.up, out rightHit);
        Color leftColor = Color.white;
        if (leftHit.transform.gameObject.GetComponent<SpriteRenderer>())
        {
            leftColor = leftHit.transform.gameObject.GetComponent<SpriteRenderer>().color;
        }
        Color rightColor = Color.white;
        if (rightHit.transform.gameObject.GetComponent<SpriteRenderer>())
        {
            rightColor = rightHit.transform.gameObject.GetComponent<SpriteRenderer>().color;
        }
        //Debug.Log(leftColor + "\t" + rightColor);
        if (CheckSensors(block, leftColor, rightColor) == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int CheckSensors(GameObject block, Color leftColor, Color rightColor)
    {
        int correctCount = 0;

        if (block.GetComponent<BuildingHandler>().colorLeftChoice == 0)
        {
            if (block.GetComponent<BuildingHandler>().compareLeftChoice == 0)
            {
                if (leftColor == Color.black)
                {
                    correctCount++;
                }
            }
            else
            {
                if (leftColor != Color.black)
                {
                    correctCount++;
                }
            }
        }
        else
        {
            if (block.GetComponent<BuildingHandler>().compareRightChoice == 1)
            {
                if (leftColor == Color.red)
                {
                    correctCount++;
                }
            }
            else
            {
                if (leftColor != Color.red)
                {
                    correctCount++;
                }
            }
        }
        if (block.GetComponent<BuildingHandler>().colorRightChoice == 0)
        {
            if (block.GetComponent<BuildingHandler>().compareRightChoice == 0)
            {
                if (rightColor == Color.black)
                {
                    correctCount++;
                }
            }
            else
            {
                if (rightColor != Color.black)
                {
                    correctCount++;
                }
            }
        }
        else
        {
            if (block.GetComponent<BuildingHandler>().compareRightChoice == 1)
            {
                if (rightColor == Color.red)
                {
                    correctCount++;
                }
            }
            else
            {
                if (rightColor != Color.red)
                {
                    correctCount++;
                }
            }
        }
        return correctCount;
    }
    
}
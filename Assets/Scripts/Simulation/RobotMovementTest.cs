using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovementTest : MonoBehaviour
{
    public bool falling = false;
    public float speed;
    public float torque;
    public float delay;
    public float currentDelay;
    public float degree;
    public float distance;
    public float distanceFromHit;
    public int isOnIceCount = 0;
    public int isOnFloorCount = 0;
    public int isAtWallCount = 0;
    public int colorHoldingObject = 0; // 1 = Green, 2 = Purple, 3 = Red, 4 = Yellow
    public float wallDamageCounter = 0;
    public bool startedReading = false;
    public bool isHoldingObject = false;
    public List<Collider> turbineList = new List<Collider>();
    private Rigidbody rbd;
    public GameObject distanceSensor;
    public GameObject leftColorSensor;
    public GameObject rightColorSensor;
    private RobotStatus robotStatScript;
    private GameObject levelController;

    public GameObject greenProduct;
    public GameObject purpleProduct;
    public GameObject redProduct;
    public GameObject yellowProduct;

    private void Start()
    {
        rbd = GetComponent<Rigidbody>();
        robotStatScript = GetComponent<RobotStatus>();
        levelController = GameObject.FindGameObjectWithTag("LevelController");
    }
    void FixedUpdate()
    {
        if (levelController.GetComponent<LevelController>().stopRobot)
        {
            delay = 0;
        }
        float calculatedSpeed = speed;
        float calculatedTorque = torque;
        if (GameObject.Find("ARModeSwitcher"))
        {
            calculatedSpeed = speed * 0.02f;
            //calculatedTorque = torque * 0.02f;
        }
        if (delay == Mathf.Infinity)
        {
            if (calculatedSpeed != 0)
            {
                if (isOnIceCount != 0)
                {
                    rbd.AddForce(transform.forward * 2 * calculatedSpeed);
                }
                else
                {
                    rbd.velocity = transform.forward * calculatedSpeed / 0.99f;
                }
            }
            else
            {
                if (isOnIceCount == 0)
                {
                    rbd.velocity = new Vector3(0, 0, 0);
                }
            }
            if (calculatedTorque != 0)
            {
                if (isOnIceCount != 0)
                {
                    rbd.AddTorque(transform.up * calculatedTorque / 4.0f / 57.273f);
                }
                else
                {
                    rbd.angularVelocity = transform.up * calculatedTorque / 57.273f / 0.99f;
                }
            }
            else
            {
                if (isOnIceCount == 0)
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
            if (calculatedSpeed != 0)
            {
                if (isOnIceCount != 0)
                {
                    rbd.AddForce(transform.forward * 2 * calculatedSpeed);
                }
                else
                {
                    rbd.velocity = transform.forward * calculatedSpeed / 0.99f;
                }
            }
            else
            {
                if (isOnIceCount == 0)
                {
                    rbd.velocity = new Vector3(0, 0, 0);
                }
            }
            if (calculatedTorque != 0)
            {
                if (isOnIceCount != 0)
                {
                    rbd.AddTorque(transform.up * calculatedTorque / 4.0f / 57.273f);
                }
                else
                {
                    rbd.angularVelocity = transform.up * calculatedTorque / 57.273f / 0.99f;
                }
            }
            else
            {
                if (isOnIceCount == 0)
                {
                    rbd.angularVelocity = new Vector3(0, 0, 0);
                }
            }
            delay -= Time.fixedDeltaTime;
        }
        else
        {
            if (isOnIceCount == 0)
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
        for (int x = 0; x < turbineList.Count; x++)
        {
            Vector3 Angle = Quaternion.Euler(0, turbineList[x].transform.parent.eulerAngles.y, 0) * transform.forward;
            float posChance = Random.value;
            if (posChance < 0.2) // No push
            {

            }
            else if (posChance < 0.6)
            {
                transform.position += Angle * 0.0001f;
            }
            else
            {
                transform.position += Angle * 0.0002f;
            }
            float rotChance = Random.value;
            if (posChance < 0.2) // No push
            {

            }
            else if (posChance < 0.6)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, turbineList[x].transform.parent.eulerAngles.y, 0), 0.00001f * Time.time);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, turbineList[x].transform.parent.eulerAngles.y, 0), 0.00002f * Time.time);
            }
        }
        if (isAtWallCount > 0)
        {
            //if (rbd.velocity.magnitude > 0.1f)
            //{
            //    if (wallDamageCounter > 1)
            //    {
            //        wallDamageCounter += Time.fixedDeltaTime;
            //        //robotStatScript.playerHealth -= rbd.velocity.magnitude * 10.0f;
            //        robotStatScript.DamagePlayer(rbd.velocity.magnitude * 10.0f);
            //        wallDamageCounter = 0;
            //    }
            //}
            if (wallDamageCounter > 1)
            {
                //robotStatScript.playerHealth -= rbd.velocity.magnitude * 10.0f;
                robotStatScript.DamagePlayer(rbd.velocity.magnitude * 12f);
                wallDamageCounter = 0;
            }
            if (rbd.velocity.magnitude > 0)
            {
                wallDamageCounter += Time.fixedDeltaTime * 5;
            }
        }
    }

    public void StartReading()
    {
        currentTimes = 0;
        repeats = new List<GameObject>();


        //GameObject startBlock = GameObject.FindGameObjectWithTag("StartBlock"); //Find startblock when want to read (change?)

        GameObject startBlock = GameObject.Find("BlockSaveManager").GetComponent<BlockSaveAndLoad>().startBlock[0];

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
        //Debug.Log("Enter if:" + block.name);
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
        if (distanceSensor == null)
        {
            return false;
        }
        RaycastHit hit = new RaycastHit();
        Vector3 sensorForward = distanceSensor.transform.forward;
        Quaternion spreadAngle = Quaternion.AngleAxis(degree, new Vector3(0, 1, 0));
        Vector3 sensorAngle = spreadAngle * sensorForward;
        Physics.Raycast(distanceSensor.transform.position, sensorAngle, out hit);
        //if (Physics.Raycast(distanceSensor.transform.position, sensorAngle))
        //{
        //    if (!hit.transform.CompareTag("Wall"))
        //    {
        //        return false;
        //    }
        //}
        //distanceFromHit = Vector3.Distance(distanceSensor.transform.position, hit.point);
        distanceFromHit = hit.distance;
        Debug.Log("Hit distance: " + distanceFromHit + "Degree: " + degree);
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
        if (leftColorSensor == null)
        {
            return false;
        }
        RaycastHit leftHit;
        RaycastHit rightHit;
        int correctCount = 0;
        if (Physics.Raycast(leftColorSensor.transform.position, -leftColorSensor.transform.up, out leftHit, Mathf.Infinity))
        {
            Debug.LogError("left sensor found tag: " + leftHit.collider.tag);
            if (block.GetComponent<BuildingHandler>().colorLeftChoice == 0) // If left choose black
            {
                if (block.GetComponent<BuildingHandler>().compareLeftChoice == 0) // If left choose equal
                {
                    if (leftHit.collider.CompareTag("BlackLine"))
                    {
                        correctCount++;
                    }
                }
                else
                {
                    if (!leftHit.collider.CompareTag("BlackLine"))
                    {
                        correctCount++;
                    }
                }
            }
            else // If left choose red
            {
                if (block.GetComponent<BuildingHandler>().compareLeftChoice == 0) // If left choose equal
                {
                    if (leftHit.collider.CompareTag("RedWarning"))
                    {
                        correctCount++;
                    }
                }
                else
                {
                    if (!leftHit.collider.CompareTag("RedWarning"))
                    {
                        correctCount++;
                    }
                }
            }
        }
        if (Physics.Raycast(rightColorSensor.transform.position, -rightColorSensor.transform.up, out rightHit, Mathf.Infinity))
        {
            Debug.LogError("right sensor found tag: " + rightHit.collider.tag);
            if (block.GetComponent<BuildingHandler>().colorRightChoice == 0) // If right choose black
            {
                if (block.GetComponent<BuildingHandler>().compareRightChoice == 0) // If right choose equal
                {
                    if (rightHit.collider.CompareTag("BlackLine"))
                    {
                        correctCount++;
                    }
                }
                else
                {
                    if (!rightHit.collider.CompareTag("BlackLine"))
                    {
                        correctCount++;
                    }
                }
            }
            else // If right choose red
            {
                if (block.GetComponent<BuildingHandler>().compareRightChoice == 0) // If right choose equal
                {
                    if (rightHit.collider.CompareTag("RedWarning"))
                    {
                        correctCount++;
                    }
                }
                else
                {
                    if (!rightHit.collider.CompareTag("RedWarning"))
                    {
                        correctCount++;
                    }
                }
            }
        }
        Debug.LogError("correctCount: " + correctCount);
        if (correctCount == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //  private bool CheckColorSensor(GameObject block)
    //  {
    //      RaycastHit leftHit;
    //      RaycastHit rightHit;
    ////Physics.Raycast(leftColorSensor.transform.position, -leftColorSensor.transform.up, out leftHit);
    ////Physics.Raycast(rightColorSensor.transform.position, -rightColorSensor.transform.up, out rightHit);
    ////Color leftColor = Color.white;
    ////if (leftHit.transform.gameObject.GetComponent<SpriteRenderer>())
    ////{
    ////    leftColor = leftHit.transform.gameObject.GetComponent<SpriteRenderer>().color;
    ////}
    ////Color rightColor = Color.white;
    ////if (rightHit.transform.gameObject.GetComponent<SpriteRenderer>())
    ////{
    ////    rightColor = rightHit.transform.gameObject.GetComponent<SpriteRenderer>().color;
    ////}
    //Color leftColor = Color.white;
    //Color rightColor = Color.white;
    //if (Physics.Raycast(leftColorSensor.transform.position, -leftColorSensor.transform.up, out leftHit))
    //{
    //	if (leftHit.transform.gameObject.GetComponent<SpriteRenderer>())
    //	{
    //		leftColor = leftHit.transform.gameObject.GetComponent<SpriteRenderer>().color;
    //	}
    //}
    //if (Physics.Raycast(rightColorSensor.transform.position, -rightColorSensor.transform.up, out rightHit))
    //{
    //	if (rightHit.transform.gameObject.GetComponent<SpriteRenderer>())
    //	{
    //		rightColor = rightHit.transform.gameObject.GetComponent<SpriteRenderer>().color;
    //	}
    //}
    //if (CheckSensors(block, leftColor, rightColor) == 2)
    //      {
    //          return true;
    //      }
    //      else
    //      {
    //          return false;
    //      }
    //  }

    //  private int CheckSensors(GameObject block, Color leftColor, Color rightColor)
    //  {
    //      int correctCount = 0;
    //      if (block.GetComponent<BuildingHandler>().colorLeftChoice == 0) // If left choose black
    //      {
    //          if (block.GetComponent<BuildingHandler>().compareLeftChoice == 0) // If left choose equal
    //          {
    //              if (leftColor == Color.black)
    //              {
    //                  correctCount++;
    //              }
    //          }
    //          else
    //          {
    //              if (leftColor != Color.black)
    //              {
    //                  correctCount++;
    //              }
    //          }
    //      }
    //      else // If left choose red
    //      {
    //          if (block.GetComponent<BuildingHandler>().compareLeftChoice == 0) // If left choose equal
    //	{
    //              if (leftColor == Color.red)
    //              {
    //                  correctCount++;
    //              }
    //          }
    //          else
    //          {
    //              if (leftColor != Color.red)
    //              {
    //                  correctCount++;
    //              }
    //          }
    //      }
    //      if (block.GetComponent<BuildingHandler>().colorRightChoice == 0) // If right choose black
    //{
    //          if (block.GetComponent<BuildingHandler>().compareRightChoice == 0) // If right choose equal
    //	{
    //              if (rightColor == Color.black)
    //              {
    //                  correctCount++;
    //              }
    //          }
    //          else
    //          {
    //              if (rightColor != Color.black)
    //              {
    //                  correctCount++;
    //              }
    //          }
    //      }
    //      else
    //      {
    //          if (block.GetComponent<BuildingHandler>().compareRightChoice == 0) // If right choose equal
    //	{
    //              if (rightColor == Color.red)
    //		{
    //			correctCount++;
    //              }
    //          }
    //          else
    //          {
    //              if (rightColor != Color.red)
    //              {
    //                  correctCount++;
    //              }
    //          }
    //}
    //return correctCount;
    //  }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "IceFloor")
        {
            isOnIceCount++;
        }
        if (other.transform.tag == "Turbine")
        {
            turbineList.Add(other);
        }
        if (other.transform.tag == "Lava")
        {
            //robotStatScript.playerHealth -= 100;
            robotStatScript.DamagePlayer(100);
        }
        if (other.transform.tag == "ProductPickup")
        {
            if (leftColorSensor != null) // If this is stovebot
            {
                if (!isHoldingObject)
                {
                    if (other.transform.name == "Green_Cube") // If picked up green
                    {
                        colorHoldingObject = 1;
                        greenProduct.SetActive(true);
                    }
                    else if (other.transform.name == "Purple_Cube") // If picked up purple
                    {
                        colorHoldingObject = 2;
                        purpleProduct.SetActive(true);
                    }
                    if (other.transform.name == "Red_Cube") // If picked up red
                    {
                        colorHoldingObject = 3;
                        redProduct.SetActive(true);
                    }
                    if (other.transform.name == "Yellow_Cube") // If picked up yellow
                    {
                        colorHoldingObject = 4;
                        yellowProduct.SetActive(true);
                    }
                    other.transform.parent.parent.GetComponent<SpawnerController>().ObjectTaken();
                    isHoldingObject = true;
                }
            }
        }
        if (other.transform.tag == "ProductDeliver")
        {
            if (isHoldingObject)
            {
                if (other.transform.parent.name == "Green_Destination")
                {
                    if (colorHoldingObject == 1) // If holding green
                    {
                        greenProduct.SetActive(false);
                        colorHoldingObject = 0;
                        isHoldingObject = false;
                        other.transform.parent.GetComponent<DestinationController>().ObjectReceived();
                    }
                }
                if (other.transform.parent.name == "Purple_Destination")
                {
                    if (colorHoldingObject == 2) // If holding purple
                    {
                        purpleProduct.SetActive(false);
                        colorHoldingObject = 0;
                        isHoldingObject = false;
                        other.transform.parent.GetComponent<DestinationController>().ObjectReceived();
                    }
                }
                if (other.transform.parent.name == "Red_Destination")
                {
                    if (colorHoldingObject == 3) // If holding red
                    {
                        redProduct.SetActive(false);
                        colorHoldingObject = 0;
                        isHoldingObject = false;
                        other.transform.parent.GetComponent<DestinationController>().ObjectReceived();
                    }
                }
                if (other.transform.parent.name == "Yellow_Destination")
                {
                    if (colorHoldingObject == 4) // If holding yellow
                    {
                        yellowProduct.SetActive(false);
                        colorHoldingObject = 0;
                        isHoldingObject = false;
                        other.transform.parent.GetComponent<DestinationController>().ObjectReceived();
                    }
                }
            }
        }
        if (other.transform.tag == "GoalFinishZone")
        {
            levelController.GetComponent<LevelController>().FinishMission();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "IceFloor")
        {
            isOnIceCount--;
        }
        if (other.transform.tag == "Turbine")
        {
            turbineList.Remove(other);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Wall")
        {
            float relativeVec = other.relativeVelocity.magnitude;
            Debug.Log("Velocity magnitude at collision: " + relativeVec);
            isAtWallCount++;
            if (relativeVec > 0.1f)
            {
                robotStatScript.DamagePlayer(relativeVec * 10.0f);
            }
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.transform.tag == "Wall")
        {
            wallDamageCounter = 0;
            isAtWallCount--;
        }
    }

    public void FallAnimation()
    {
        LeanTween.moveY(gameObject, transform.position.y - 10, 5);
        StartCoroutine(FallHealthDecrease());
    }
    IEnumerator FallHealthDecrease()
	{
        yield return new WaitForSeconds(0.25f);
        robotStatScript.DamagePlayer(10f);
        yield return new WaitForSeconds(0.25f);
        robotStatScript.DamagePlayer(10f);
        yield return new WaitForSeconds(0.25f);
        robotStatScript.DamagePlayer(10f);
        yield return new WaitForSeconds(0.25f);
        robotStatScript.DamagePlayer(10f);
        yield return new WaitForSeconds(0.25f);
        robotStatScript.DamagePlayer(10f);
        yield return new WaitForSeconds(0.25f);
        robotStatScript.DamagePlayer(10f);
        yield return new WaitForSeconds(0.25f);
        robotStatScript.DamagePlayer(10f);
        yield return new WaitForSeconds(0.25f);
        robotStatScript.DamagePlayer(10f);
        yield return new WaitForSeconds(0.25f);
        robotStatScript.DamagePlayer(10f);
        yield return new WaitForSeconds(0.25f);
        robotStatScript.DamagePlayer(10f);
    }
}
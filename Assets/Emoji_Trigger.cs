using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emoji_Trigger : MonoBehaviour
{
    [SerializeField] private Emoji_Controller emojiController = null;
    [SerializeField] private int turbineCount, lavaCount, wallCount = 0;
    public GameObject[] groundCheckers;
    [SerializeField] private bool nearbyWall = false;
    [SerializeField] private bool inWind = false;
    [SerializeField] private bool nearbyLava = false;
    [SerializeField] private bool nearbyEdge = false;
    private ParticleSystem.MainModule psDrop1, psDrop2;


    void Start()
    {
        //if (GameObject.Find("ARModeSwitcher"))
        //{
        //    gameObject.transform.localScale = gameObject.transform.localScale * 0.02f;
        //}
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            gameObject.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
            gameObject.transform.eulerAngles =  new Vector3(
                                                90,
                                                GameObject.FindGameObjectWithTag("Player").transform.eulerAngles.y,
                                                GameObject.FindGameObjectWithTag("Player").transform.eulerAngles.z
);
        }
        if (emojiController == null)
        {
            emojiController = FindObjectOfType<Emoji_Controller>();
        }
        else
        {
            ManageEmojiDisplay(0);
        }
        if (GameObject.FindGameObjectWithTag("Player") && emojiController != null)
        {
            psDrop1 = emojiController.Drop1.main;
            psDrop2 = emojiController.Drop2.main;
            psDrop1.gravityModifierMultiplier = 0.0024f;
            psDrop2.gravityModifierMultiplier = 0.0024f;
        }
    }

	private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Turbine")
        {
            turbineCount++;
        }
        if (other.transform.tag == "Lava")
        {
            lavaCount++;
        }
        if (other.transform.tag == "Wall")
        {
            wallCount++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Turbine")
        {
            turbineCount--;
        }
        if (other.transform.tag == "Lava")
        {
            lavaCount--;
        }
        if (other.transform.tag == "Wall")
        {
            wallCount--;
        }
    }
    /*
     * Confuse > Ehh > Sad
     * Force to show the top priority emoji
     * Confuse: hit wall really hard
     * Ehh: detect wall/lava near or close to an edge
     * Sad: in wind
     * ExternalCommand - 1: Nothing
     *                   2: Force confused
     */

    public void ManageEmojiDisplay(int externalCommand)
	{
        if (externalCommand == 1) // Force confused
        {
            emojiController.CryTrigger = false;
            emojiController.EhhTrigger = false;
            emojiController.Ehh.Stop();
            emojiController.ConfuseTrigger = true;
        }
        if (!emojiController.Confuse1.isPlaying) // Ehh
		{
            if (FindObjectOfType<RobotMovementTest>())
            {
                if (GameObject.Find("ARModeSwitcher"))
                {
                    if (FindObjectOfType<RobotMovementTest>().rbd.velocity.magnitude > 0.2f * 0.02f)
                    {
                        if (lavaCount > 0 || wallCount > 0 || CheckNearbyEdge())
                        {
                            emojiController.EhhTrigger = true;
                            emojiController.CryTrigger = false;
                        }
                    }
                }
				else
                {
                    if (FindObjectOfType<RobotMovementTest>().rbd.velocity.magnitude > 0.2f)
                    {
                        if (lavaCount > 0 || wallCount > 0 || CheckNearbyEdge())
                        {
                            emojiController.EhhTrigger = true;
                            emojiController.CryTrigger = false;
                        }
                    }
                }
            }
		}
		else
        {
            emojiController.EhhTrigger = false;
            emojiController.Ehh.Stop();
        }
        if (!emojiController.Confuse1.isPlaying && !emojiController.Ehh.isPlaying) // Cry
		{
            if (turbineCount > 0)
            {
                emojiController.CryTrigger = true;
            }
            else
			{
                emojiController.CryTrigger = false;
            }
        }
	}

    private bool CheckNearbyEdge() // true if there is an edge nearby
	{
        if (!Physics.Raycast(groundCheckers[0].transform.position, -Vector3.up)
                   || !Physics.Raycast(groundCheckers[1].transform.position, -Vector3.up)
                   || !Physics.Raycast(groundCheckers[2].transform.position, -Vector3.up)
                   || !Physics.Raycast(groundCheckers[3].transform.position, -Vector3.up))
        {
            return true;
        }
        return false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    public GameObject player;

    //private Rigidbody2D playerRigid;

    private PowerUpControl powerupControl;

    private platformCreator pCreator;

    //public GameObject platforms;

    public bool onPlatform = false;

    private int platformNum;
    
    public Text scoringText;
    
    public float scoreAmount;
    
    //public float pointsIncreasedPerPlatform;

    private float timeRemaining;

    private bool controllable = true;

    private float tempSpeed;


    void Awake()
    {
        powerupControl = player.GetComponent<PowerUpControl>();
        //pCreator = platforms.GetComponent<platformCreator>();
    }

    void Update()
    {
        scoringText.text = (int)scoreAmount + "";

        controllable = powerupControl.getControls();

        //if (controllable == false)
        //{
        //    if (timeRemaining > 0)
        //    {
        //        timeRemaining -= Time.deltaTime; //timer
        //    }
        //    else
        //    {
        //        controllable = powerupControl.fixBoost(pCreator.current_platform_speed);
        //        //fixBoost(tempSpeed);
        //    }
        //}
    }

    public void MoveLeft() {
        if(controllable == true) { 
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platforms");
        float minDist = Mathf.Infinity;
        platformNum = -100;
        for (int i = 0; i < platforms.Length; i++) //platform i
        {
            if (platforms[i].transform.position.y > player.transform.position.y
                && platforms[i].transform.position.x < 0) //check if i is above player and on left
            {
                float dist = Vector3.Distance(platforms[i].transform.position, player.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    platformNum = i;
                }
            }
        }
        player.transform.position = new Vector2(platforms[platformNum].transform.position.x
            , platforms[platformNum].transform.position.y + platforms[platformNum].transform.localScale.y / 2  //magic happens
            + player.transform.localScale.y / 2);

        platforms = null; //delete all objects inside
        }
     }

    public void MoveRight() {
        if (controllable == true)
        {
            GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platforms");
            float minDist = Mathf.Infinity;
            platformNum = -100;
            for (int i = 0; i < platforms.Length; i++) //platform i
            {
                if (platforms[i].transform.position.y > player.transform.position.y
                    && platforms[i].transform.position.x > 0) //check if its above player and on right
                {
                    float dist = Vector3.Distance(platforms[i].transform.position, player.transform.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        platformNum = i;
                    }
                }
            }
            player.transform.position = new Vector2(platforms[platformNum].transform.position.x
                , platforms[platformNum].transform.position.y + platforms[platformNum].transform.localScale.y / 2  //magic happens
                + player.transform.localScale.y / 2);

            platforms = null; //delete all objects inside
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platforms" && controllable)
        {
            player.transform.parent = collision.gameObject.transform;
            onPlatform = true;
            scoreAmount++;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platforms")
        {
            player.transform.parent = null;
            onPlatform = false;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Boost") // TODO: 1) freeze player 2) platforms faster 
    //    {

    //        Debug.Log("Boosting NOW!!!!");
    //        Destroy(collision.gameObject);
    //        player.transform.parent = null;
    //        playerRigid.constraints = RigidbodyConstraints2D.FreezePosition;
    //        player.transform.GetComponent<BoxCollider2D>().enabled = false;
    //        controllable = false;
    //        timeRemaining = 3;

    //        GameObject[] platformsOnScreen = GameObject.FindGameObjectsWithTag("Platforms"); //Getting the platforms constantly so we can speed up the platforms on the screen.
    //        tempSpeed = pCreator.current_platform_speed;

    //        pCreator.current_platform_speed = tempSpeed * 5;
    //        pCreator.spawnDelay /= 5;

    //        foreach (GameObject platform in platformsOnScreen)
    //        {
    //            platform.GetComponent<platform>().setSpeed(tempSpeed * 5);
    //        }

    //    }
    //    else if (collision.gameObject.tag == "Invincible") //TODO: Timer
    //    {
    //        Debug.Log("Invincible NOW!!!");
    //    }
    //    else if (collision.gameObject.tag == "DoublePoints") //TODO: Timer
    //    {
    //        Debug.Log("DoulePoints NOW!!!");
    //    }
    //}

    //private void fixBoost(float tempSpeed)
    //{
    //    controllable = true;
    //    playerRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    //    player.transform.GetComponent<BoxCollider2D>().enabled = true;

    //    GameObject[] platformsOnScreen = GameObject.FindGameObjectsWithTag("Platforms");
    //    foreach (GameObject platform in platformsOnScreen)
    //    {
    //        platform.GetComponent<platform>().setSpeed(tempSpeed);
    //    }

    //    pCreator.current_platform_speed = tempSpeed;
    //    pCreator.spawnDelay *= 5;
    //}

}

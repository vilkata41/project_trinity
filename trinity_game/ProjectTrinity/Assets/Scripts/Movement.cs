using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    public GameObject player;

    private Rigidbody2D playerRigid;

    private platformCreator pCreator;

    public GameObject platforms;

    public bool onPlatform = false;

    private int platformNum;
    
    public Text scoringText;
    
    public float scoreAmount;
    
    public float pointsIncreasedPerPlatform;

    public float timeRemaining;

    public bool controllable = true;


    void Awake()
    {
        pCreator = platforms.GetComponent<platformCreator>();
    }

    void Update()
    {
        scoringText.text = (int)scoreAmount + "";
        playerRigid = GetComponent<Rigidbody2D>();
        if(controllable == false)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                fixBoost();
            }
        }
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
           if (collision.gameObject.tag == "Platforms") {
                player.transform.parent = collision.gameObject.transform;
                onPlatform = true;
                scoreAmount++;
            }
        }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boost") // TODO: 1) freeze player 2) platforms faster 
        {
            Debug.Log("Boosting NOW!!!!");
            playerRigid.constraints = RigidbodyConstraints2D.FreezeAll;
            player.transform.GetComponent<BoxCollider2D>().enabled = false;
            controllable = false;
            timeRemaining = 3;
            //platforms.GetComponent<platformCreator>().current_platform_speed = 15.0f;
        }
        else if (collision.gameObject.tag == "Invincible") //TODO: Timer
        {
            Debug.Log("Invincible NOW!!!");
        }
        else if (collision.gameObject.tag == "DoublePoints") //TODO: Timer
        {
            Debug.Log("DoulePoints NOW!!!");
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

    private void fixBoost()
    {
        controllable = true;
        playerRigid.constraints = RigidbodyConstraints2D.None;
        player.transform.GetComponent<BoxCollider2D>().enabled = true;
        //platforms.GetComponent<platformCreator>().current_platform_speed = 6.0f;
    }

}

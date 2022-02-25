using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    public GameObject player;

    private platformCreator pCreator;

    public GameObject platforms;

    public bool onPlatform = false;

    private int platformNum;
    
    public Text scoringText;
    
    public float scoreAmount;
    
    public float pointsIncreasedPerPlatform;

    private bool controllable;


    void Awake()
    {
        pCreator = platforms.GetComponent<platformCreator>();
    }
    private void Start()
    {
        controllable = true;
    }

    void Update()
    {
        scoringText.text = (int)scoreAmount + "";
    }

    public void MoveLeft() {
        controllable = this.GetComponent<PowerUpController>().isControllable();
        if(controllable) { 
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
        controllable = this.GetComponent<PowerUpController>().isControllable();
        if (controllable)
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

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Platforms" && controllable) {
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
}

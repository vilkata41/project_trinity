using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public GameObject player;
    //public GameObject[] platforms;
    private int platformNum;

    // Update is called once per frame
    void Update()
    {
        //GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platforms"); //constantly renews the list of tagged objects


        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platforms");
            float minDist = Mathf.Infinity;
            platformNum = -100;
            for (int i = 0; i < platforms.Length; i++) //platform i
            {
                if (platforms[i].transform.position.y > player.transform.position.y 
                    && platforms[i].transform.position.x < 0) //check if i is above player and on left
                {
                    float dist = Vector3.Distance(platforms[i].transform.position, player.transform.position);
                    if(dist < minDist)
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

        if (Input.GetKeyDown(KeyCode.D))
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
}

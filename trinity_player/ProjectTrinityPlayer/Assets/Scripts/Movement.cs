using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public GameObject player;
    //public GameObject[] platforms;
    private bool check;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platforms"); //constantly renews the list of tagged objects

        if (Input.GetKeyDown(KeyCode.A))
        {
            for (int i = 0; i < platforms.Length; i++) //platform i
            {
                check = true; //true is default value
                if (platforms[i].transform.position.y > player.transform.position.y 
                    && platforms[i].transform.position.x < 0) //check if i is above player and on left
                {
                     
                    for (int j = 0; j < platforms.Length; j++) //platform j
                    {
                        if (platforms[j].transform.position.x < 0 //check if j on the left
                            && platforms[j].transform.position.y > player.transform.position.y) //check j higher than player
                        {
                            float temp1 = platforms[i].transform.position.y - player.transform.position.y; //dist between platform and player
                            float temp2 = platforms[j].transform.position.y - player.transform.position.y;
                            if (temp1 > temp2)
                            {
                                check = false;
                            }
                        }
                    }


                    if (check == true)
                    {
                        player.transform.position = new Vector2(platforms[i].transform.position.x
                            , platforms[i].transform.position.y + platforms[i].transform.localScale.y / 2  //magic happens
                            + player.transform.localScale.y/2);
                    }

                }

            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            for (int i = 0; i < platforms.Length; i++) //platform i
            {   
                check = true; //true is default value
                if (platforms[i].transform.position.y > player.transform.position.y
                    && platforms[i].transform.position.x < 0) //check if its above player and on right
                {
                    //Debug.Log(platforms[i].transform.position.y + "   " + player.transform.position.y);
                    for (int j = 0; j < platforms.Length; j++) //platform j
                    {
                        if (platforms[j].transform.position.x > 0  //check if j on the left
                            && platforms[j].transform.position.y > player.transform.position.y) //check j higher than player
                        {
                            //Debug.Log("Plat1, plat2" + platforms[i].transform.position.y + " " + platforms[j].transform.position.y);
                            float temp1 = platforms[i].transform.position.y - player.transform.position.y; //dist between platform and player
                            float temp2 = platforms[j].transform.position.y - player.transform.position.y;
                            if (temp1 > temp2)
                            {
                                check = false;
                            }
                        }
                    }


                    if (check == true)
                    {
                        player.transform.position = new Vector2(platforms[i].transform.position.x
                            , platforms[i].transform.position.y + platforms[i].transform.localScale.y / 2    //magic happens
                            + player.transform.localScale.y / 2);
                    }


                }
            }
        }

        platforms = null; //delete all objects inside
    }
}

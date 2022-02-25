using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpControl : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    public GameObject player;

    private Rigidbody2D playerRigid;

    private platformCreator pCreator;

    public GameObject platforms;

    private float tempSpeed;

    private float timeRemaining;

    GameObject[] platformsOnScreen; 

    //public float pointsIncreasedPerPlatform;

    private bool controllable = true;

    void Awake()
    {
        pCreator = platforms.GetComponent<platformCreator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRigid = player.GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        platformsOnScreen = GameObject.FindGameObjectsWithTag("Platforms"); //Getting the platforms constantly so we can speed up the platforms on the screen.

        if (controllable == false)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; //timer
            }
            else
            {
                controllable = fixBoost(tempSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Player collects power-up
    {
        //if (collision.tag == "Player")
        //{
            
        //}
        switch (collision.gameObject.tag) //check tag of powerUp
        {
            case "Invincible":
                Invincibility();
                Destroy(collision.gameObject);
                break;
            case "DoublePoints":
                DoublePoints();
                Destroy(collision.gameObject);
                break;
            case "Boost":
                Boost();
                Destroy(collision.gameObject);
                break;
        }
        
    }
      
    //void Collected(Collider2D collision)
    //{
    //    spriteRenderer.enabled = false;

    //    switch (collision.gameObject.tag) //check tag of powerUp
    //    {
    //        case "Invincible":
    //            Invincibility(collision);
    //            break;
    //        case "DoublePoints":
    //            DoublePoints(collision);
    //            break;
    //        case "Boost":
    //            Boost(collision);
    //            break;
    //    }

    //}

    private void Invincibility()
    {
        Debug.Log("Invincibility!!!");

    }

    private void DoublePoints()
    {
        Debug.Log("DoublePoints!!!");

    }

    private void Boost()
    {
        Debug.Log("Boost!!!");
        player.transform.parent = null;
        playerRigid.constraints = RigidbodyConstraints2D.FreezePosition;
        player.transform.GetComponent<BoxCollider2D>().enabled = false;
        controllable = false;
        timeRemaining = 3;
        tempSpeed = pCreator.current_platform_speed;

        GameObject[] platformsOnScreen = GameObject.FindGameObjectsWithTag("Platforms");

        pCreator.current_platform_speed = tempSpeed * 5;
        pCreator.spawnDelay /= 2;



        foreach (GameObject platform in platformsOnScreen)
        {
            platform.GetComponent<platform>().setSpeed(tempSpeed * 2);
        }
    }

    private bool fixBoost(float dSpeed)
    {

        playerRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        player.transform.GetComponent<BoxCollider2D>().enabled = true;

        //GameObject[] platformsOnScreen = GameObject.FindGameObjectsWithTag("Platforms");
        foreach (GameObject platform in platformsOnScreen)
        {
            platform.GetComponent<platform>().setSpeed(dSpeed);
        }

        pCreator.current_platform_speed = dSpeed;
        pCreator.spawnDelay *= 2;
        return true;
    }

    public bool getControls()
    {
        return controllable;
    }

}

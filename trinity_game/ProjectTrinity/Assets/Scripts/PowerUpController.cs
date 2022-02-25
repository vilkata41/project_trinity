using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public GameObject pGenerator;

    private platformCreator pCreator;

    private Rigidbody2D playerRigid;

    private float timeRemaining;

    private float tempSpeed;

    private bool controllable;


    void Awake()
    {
        pCreator = pGenerator.GetComponent<platformCreator>();
    }

    private void Start()
    {
        controllable = true;
    }

    void Update()
    {
        playerRigid = this.GetComponent<Rigidbody2D>();
        if (!controllable) {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                fixBoost(tempSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag) {
            case "Boost":
                Destroy(collision.gameObject);
                Boost();
                break;
            case "Invincible":
                Destroy(collision.gameObject);
                Invincible();
                break;
            case "DoublePoints":
                Destroy(collision.gameObject);
                DoublePoints();
                break;
        }
    }

    public void Boost() {
        Debug.Log("Boosting NOW!!!!");
        this.transform.parent = null;
        playerRigid.constraints = RigidbodyConstraints2D.FreezePosition;
        this.transform.GetComponent<BoxCollider2D>().enabled = false;
        controllable = false;
        timeRemaining = 3;

        GameObject[] platformsOnScreen = GameObject.FindGameObjectsWithTag("Platforms"); //Getting the platforms constantly so we can speed up the platforms on the screen.
        tempSpeed = pCreator.current_platform_speed;

        pCreator.current_platform_speed = tempSpeed * 5;
        pCreator.spawnDelay /= 5;

        foreach (GameObject platform in platformsOnScreen)
        {
            platform.GetComponent<platform>().setSpeed(tempSpeed * 5);
        }
    }

    public void Invincible() {
        Debug.Log("Invincibility!");
    }

    public void DoublePoints() {
        Debug.Log("Double points!");
    }


    public void fixBoost(float tempSpeed) {
        controllable = true;
        playerRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.transform.GetComponent<BoxCollider2D>().enabled = true;

        GameObject[] platformsOnScreen = GameObject.FindGameObjectsWithTag("Platforms");
        foreach (GameObject platform in platformsOnScreen)
        {
            platform.GetComponent<platform>().setSpeed(tempSpeed);
        }

        pCreator.current_platform_speed = tempSpeed;
        pCreator.spawnDelay *= 5;
    }

    public bool isControllable(){
        return controllable;
    }
}

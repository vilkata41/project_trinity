
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformCreator : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject redPlatformPrefab;
    public GameObject powerUpI;
    public GameObject powerUpD;
    public GameObject powerUpB;

    public float spawnDelay = 1.0f;
    public float speedup = 0.995f;
    public float current_platform_speed = 6.0f;

    private Vector2 screenBounds;
    private Vector2 platformScale;

    private float leftPlatform_leftBound;
    private float leftPlatform_rightBound;
    private float rightPlatform_leftBound;
    private float rightPlatform_rightBound;

    private GameObject[] powerUps;


    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)); //Getting all screen dimensions and scaling the game accordingly
        platformScale = new Vector2(screenBounds.x * 2 / 3, screenBounds.y / 20);
        // The next couple of operations calculate the two halves of the screen with bounds - range where the platforms could generate
        leftPlatform_leftBound = -screenBounds.x + (platformScale.x / 2 + screenBounds.x / 10);
        leftPlatform_rightBound = 0 - (screenBounds.x / 10 + platformScale.x / 2);
        rightPlatform_leftBound = 0 + (screenBounds.x / 10 + platformScale.x / 2);
        rightPlatform_rightBound = screenBounds.x - (platformScale.x / 2 + screenBounds.x / 10);
        // Call the generation coroutine
        StartCoroutine(platformCreation());
        powerUps =  new GameObject[] { powerUpD, powerUpI, powerUpB };
    }

    private void spawnPlatforms() {
        GameObject p1; // Setting up platform gameobjects pre-if statements
        GameObject p2;
        int redChance = Random.Range(1, 6); //20% chance to spawn a red platform
        if (redChance == 1)
        { //if we have a red platform, we can have it either on the left side or on the right side - not both
            int side = Random.Range(1, 3); //left or right - randomizing
            if (side == 1)
            { //left
                p1 = Instantiate(redPlatformPrefab) as GameObject;
                p1.GetComponent<platform>().setRed(); //Setting the bool in platform to true - indication we have a red platform
                p2 = Instantiate(platformPrefab) as GameObject;
            }
            else
            { //right
                p1 = Instantiate(platformPrefab) as GameObject;
                p2 = Instantiate(redPlatformPrefab) as GameObject;
                p2.GetComponent<platform>().setRed(); //Same thing here
            }
        }
        else { // no red platform
            p1 = Instantiate(platformPrefab) as GameObject;
            p2 = Instantiate(platformPrefab) as GameObject;
        }

        GameObject pu = instantiatePowerUp();

        // We set the platforms' sizes and positions accordingly
        p1.transform.localScale = platformScale;
        p2.transform.localScale = platformScale;
        p1.transform.position = new Vector2(Random.Range(leftPlatform_leftBound,leftPlatform_rightBound), screenBounds.y * 1.5f);
        p2.transform.position = new Vector2(Random.Range(rightPlatform_leftBound, rightPlatform_rightBound), screenBounds.y * 1.5f);
        p1.GetComponent<platform>().setSpeed(current_platform_speed);
        p2.GetComponent<platform>().setSpeed(current_platform_speed);

        //Setting the powerups to be able to spawn only on the left or on the right platform
        int leftright = Random.Range(1, 3); // 50% left - 50% right
        if (leftright == 1 && pu != null) {
            pu.transform.position = new Vector2(p1.transform.position.x, p1.transform.position.y + screenBounds.y * 0.1f);
            pu.transform.parent = p1.transform;
        }
        else if (leftright == 2 && pu != null){
            pu.transform.position = new Vector2(p2.transform.position.x, p2.transform.position.y + screenBounds.y * 0.1f);
            pu.transform.parent = p2.transform;
        }
    }

    IEnumerator platformCreation() {
        while (true) {
            yield return new WaitForSeconds(spawnDelay);
            spawnPlatforms();
            spawnDelay *= speedup;
            current_platform_speed /= speedup;
        }
    }

    GameObject instantiatePowerUp() {
        int rand_placement = Random.Range(1,5); // 25% chance to spawn
        int rand_pu = Random.Range(0, 3); // choose the powerup
        //Debug.Log("rand_pu: " + rand_pu);
        GameObject pu = null;
        //Debug.Log("0: " + powerUps[0] + " 1: " + powerUps[1] + " 2: " + powerUps[2]);
        for (int i = 0; i < powerUps.Length; i++) //goes through all powerups
        {
            if (rand_placement == 1 && rand_pu == i) { //find power up on position "rand_pu" in the array
                pu = Instantiate(powerUps[i]) as GameObject;
            }
        }
        return pu;
    }
}

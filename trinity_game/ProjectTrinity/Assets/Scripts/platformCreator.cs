using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformCreator : MonoBehaviour
{
    public GameObject platformPrefab;
    public float spawnDelay = 1.0f;
    public float speedup = 0.995f;
    public float current_platform_speed = 6.0f;
    private Vector2 screenBounds;
    private Vector2 platformScale;
    private float leftPlatform_leftBound;
    private float leftPlatform_rightBound;
    private float rightPlatform_leftBound;
    private float rightPlatform_rightBound;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        platformScale = new Vector2(screenBounds.x * 2 / 3, screenBounds.y / 20);
        leftPlatform_leftBound = -screenBounds.x + (platformScale.x / 2 + screenBounds.x / 10);
        leftPlatform_rightBound = 0 - (screenBounds.x / 10 + platformScale.x / 2);
        rightPlatform_leftBound = 0 + (screenBounds.x / 10 + platformScale.x / 2);
        rightPlatform_rightBound = screenBounds.x - (platformScale.x / 2 + screenBounds.x / 10);
        StartCoroutine(platformCreation());
    }

    private void spawnPlatforms() {
        GameObject p1 = Instantiate(platformPrefab) as GameObject;
        GameObject p2 = Instantiate(platformPrefab) as GameObject;
        p1.transform.localScale = platformScale;
        p2.transform.localScale = platformScale;
        p1.transform.position = new Vector2(Random.Range(leftPlatform_leftBound,leftPlatform_rightBound), screenBounds.y * 1.5f);
        p2.transform.position = new Vector2(Random.Range(rightPlatform_leftBound, rightPlatform_rightBound), screenBounds.y * 1.5f);
        p1.GetComponent<platform>().setSpeed(current_platform_speed);
        p2.GetComponent<platform>().setSpeed(current_platform_speed);
    }

    IEnumerator platformCreation() {
        while (true) {
            yield return new WaitForSeconds(spawnDelay);
            spawnPlatforms();
            spawnDelay *= speedup;
            current_platform_speed /= speedup;
        }
    }
}

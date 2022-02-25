using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    private float speed;
    private Vector2 screenBounds;
    private Camera cam;
    private bool red;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.Translate(new Vector2(0, -1) * speed * Time.deltaTime, Space.Self);
        if (transform.position.y < -screenBounds.y * 2) {
            Destroy(this.gameObject);
        }
    }

    public void setSpeed(float newSpeed) {
        speed = newSpeed;
    }

    public void setRed() {
        red = true;
    }

    public bool isRedPlatform() {
        return red;
    }
}

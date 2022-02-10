using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalPlatformMovement : MonoBehaviour
{
    public float speed = 1.5f;
    private Vector2 screenBounds;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
    }

    void LateUpdate()
    {
        this.transform.Translate(new Vector2(0, -1) * speed * Time.deltaTime, Space.Self);
        if (transform.position.y < -screenBounds.y * 2)
        {
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletScript : MonoBehaviour
{
    private float speed;
    private Vector2 screenBounds;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("StartGame");
        }
        
    }

    void LateUpdate()
    {
        this.transform.Translate(new Vector2(0, -1) * speed * Time.deltaTime, Space.Self);
        if (transform.position.x < -screenBounds.x * 2)
        {
            Destroy(this.gameObject);
        }
    }
}


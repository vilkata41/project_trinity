using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpControl : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision) //Player collects power-up
    {
        if (collision.tag == "Player")
        {
            Collected();
        }
    }
      
    void Collected()
    {
        spriteRenderer.enabled = false;

        switch (spriteRenderer.tag) //check tag of powerUp
        {
            case "Invincible":
                Invincibility();
                break;
            case "DoublePoints":
                DoublePoints();
                break;
            case "Boost":
                Boost();
                break;
        }

    }

    void Invincibility()
    {
        Debug.Log("Invincibility!!!");
    }

    void DoublePoints()
    {
        Debug.Log("DoublePoints!!!");
    }

    void Boost()
    {
        Debug.Log("Boost!!!");
    }

}

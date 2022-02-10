using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickuppableController : MonoBehaviour
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Collected();
        }
    }
      
    void Collected()
    {
        spriteRenderer.enabled = false;
    }
}

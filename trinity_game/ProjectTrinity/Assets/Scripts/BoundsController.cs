using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Player is dead.");
            Application.LoadLevel("StartGame");
            
        }
    
    
    }
}

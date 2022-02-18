using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePerSecond : MonoBehaviour
{
    public Text scoringText;
    public float scoreAmount;
    public float pointsIncreasedPerSecond;



    // Start is called before the first frame update
    void Start()
    {
        scoreAmount = 0f;
        pointsIncreasedPerSecond = 1f;


    }


    // Update is called once per frame
    void Update()
    {
        scoringText.text = (int)scoreAmount + "";
        scoreAmount += pointsIncreasedPerSecond * Time.deltaTime;
    
    }
        
}
       

       

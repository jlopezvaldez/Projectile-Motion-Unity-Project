using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class point_counter : MonoBehaviour
{
    private static int points = 0;
    public Text textpoint;

    // Update is called once per frame
    void Update()
    {
        if (boom.score == 1) 
        {
            points += boom.score;
            boom.score = 0;
        }
        
        textpoint.text = "Points: " + points;
    }
}
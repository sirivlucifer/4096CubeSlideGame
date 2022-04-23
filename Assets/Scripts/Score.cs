using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score = 0;
    public Transform cube;
    public Text scoreText;

    




    
    // Update is called once per frame
    void Update() {   
    scoreText.text = score.ToString();  
        //bool score2= Convert.ToInt32(scoreText.text);

        //make score is not negative
       



    }
}

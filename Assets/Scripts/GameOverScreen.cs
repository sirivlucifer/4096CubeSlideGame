using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointText;

    public void Setup(int score){
        gameObject.SetActive(true);
        pointText.text = score.ToString() + "POINTS";
    }

     public void RestartButton(){
         Score.score = 0;
        SceneManager.LoadScene("Game");
    }
    public void ExitButton(){
        Application.Quit();
    }


}

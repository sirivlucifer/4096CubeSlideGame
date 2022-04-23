using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RedZone : MonoBehaviour
{

    public GameOverScreen GameOverScreen;
    public void GameOver(){
        GameOverScreen.Setup(Score.score);
    
    }

   

    private void OnTriggerStay(Collider other) {
        Cube cube = other.GetComponent<Cube>();
        if(cube != null){
            if(!cube.IsMainCube&&cube.CubeRigidbody.velocity.magnitude< .1f){
                Debug.Log("gameover");
                GameOver();
                }
            }
        }
    }


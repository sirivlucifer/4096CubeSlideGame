using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;
    [SerializeField] private float cubeMaxPosX;
    [Space]
    [SerializeField] private TouchSlider touchSlider;
     private Cube mainCube;

    private bool isPointerDown;
    private bool canMove;
    private Vector3 cubePos;

    private void Start()
    {
       SpawnCube();
       canMove = true;

       //listen to slider events
         touchSlider.OnPointerDownEvent += OnPointerDown;
         touchSlider.OnPointerUpEvent += OnPointerUp;
         touchSlider.OnPointerDragEvent += OnPointerDrag;
    }
    private void Update() {
        if(isPointerDown)
           mainCube.transform.position = Vector3.Lerp(
               mainCube.transform.position,
                cubePos,
                 Time.deltaTime * moveSpeed
                 );
        
    }
    private void MoveCube(){
           
        }
    private void OnPointerDown()
    {
       
        isPointerDown = true;
      
    }
    private void OnPointerUp()
    {
      if(isPointerDown&&canMove){
        isPointerDown = false;
        canMove = false;
        //push cube
        Force();

        //Spawn new cube after 0.3 seconds
        Invoke("SpawnNewCube",0.1f);
        
      }
    }

    void Force(){
        mainCube.CubeRigidbody.AddForce(Vector3.forward * pushForce,ForceMode.Impulse);
    } 

    
    private void SpawnNewCube(){
        mainCube.IsMainCube = false;
        canMove=true;
        SpawnCube();

    }

    private void OnPointerDrag(float xMovement)
    {
        if(isPointerDown){
            //move cube
            cubePos = mainCube.transform.position;
            cubePos.x = xMovement * cubeMaxPosX;
        }
    }
    private void SpawnCube(){
        mainCube = CubeSpawner.Instance.SpawnRandom();
        mainCube.IsMainCube = true;
        //reset cube pos variable
        cubePos = mainCube.transform.position;
    }
    private void OnDestroy()
    {
        //remove listeners
        touchSlider.OnPointerDownEvent -= OnPointerDown;
        touchSlider.OnPointerUpEvent -= OnPointerUp;
        touchSlider.OnPointerDragEvent -= OnPointerDrag;
    }

}

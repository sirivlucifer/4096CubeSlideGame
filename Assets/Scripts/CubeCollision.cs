using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CubeCollision : MonoBehaviour
{
    Cube cube ;
  // public AudioSource audio;

    private void Awake() {
        cube = GetComponent<Cube>();
       // audio = GetComponent<AudioSource>();
    }
           IEnumerator WaitOneSecToDeactivateTrash(Collision collision)
      {
          Debug.Log("hello before time");
          yield return new WaitForSeconds (1f);
          Debug.Log("hello after time");
      }
    public void  OnCollisionEnter(Collision collision) {
        Cube otherCube = collision.gameObject.GetComponent<Cube>();     
        // check if contacted with other cube
        if(otherCube != null && cube.CubeID > otherCube.CubeID){
            //check if both cubes have same number
            if(cube.CubeNumber == otherCube.CubeNumber){
                //Debug.Log("HIT: "+cube.CubeNumber);
                Score.score +=cube.CubeNumber*2;
               Vector3 contactPoint = collision.contacts[0].point; 
               //check if cubes number less than max number in cubespawner:
                if(otherCube.CubeNumber < CubeSpawner.Instance.maxCubeNumber){
                     //spawn a new cube as a result.
                     Cube newCube = CubeSpawner.Instance.Spawn(cube.CubeNumber*2, contactPoint+Vector3.up*.3f); //2 4 8 16 32 64 128 256 512 1024 2048 4096                           
                    //push the new cube up and forward.
                     float pushForce = 2.5f;
                     newCube.CubeRigidbody.AddForce(new Vector3(0, .3f, 1f)*pushForce,ForceMode.Impulse);
                     //add some torque
                     float randomValue = Random.Range(-20f,20f);
                     Vector3 randomDirection = Vector3.one * randomValue;
                     newCube.CubeRigidbody.AddTorque(randomDirection);            
                }
                //the expolison should effect sorrounded cubes too.
                Collider[] surroundedCubes = Physics.OverlapSphere ( contactPoint,1f);
                float expolisonForce = 400f;
                float expolisonRadius = 1.5f;
                foreach(Collider coll in surroundedCubes){
                    if(coll.attachedRigidbody!= null){
                        coll.attachedRigidbody.AddExplosionForce(expolisonForce,contactPoint,expolisonRadius);
                    }
                }
                FX.Instance.PlayCubeExplosionFX(contactPoint,cube.CubeColor);
                //Destrory the two cubes: 
                CubeSpawner.Instance.DestroyCube(cube,3f); //sonradan incele ---------!!!
                CubeSpawner.Instance.DestroyCube(otherCube,3f);
                 
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour
{
    [SerializeField] private ParticleSystem cubeExplosionFX;
    public Material fxMaterial;
    

    ParticleSystem.MainModule cubeExplosionFXMainModule;
  //singleton class for
    public static FX Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start(){
        cubeExplosionFXMainModule = cubeExplosionFX.main;
       
        //create fx material get component.

    }

    public void PlayCubeExplosionFX(Vector3 position,Color color){
         Color newColor;
        ColorUtility.TryParseHtmlString("#FFFF00", out newColor);
        
       // cubeExplosionFXMainModule.startColor = new ParticleSystem.MinMaxGradient(color);
        cubeExplosionFX.GetComponent<ParticleSystemRenderer>().material.SetColor("_Color", newColor);
        cubeExplosionFX.transform.position = position;
        cubeExplosionFX.Play ();
    }

}

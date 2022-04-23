using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
   // Singleton class 
    public static CubeSpawner Instance;

    Queue <Cube> cubeQueue = new Queue<Cube>();
    [SerializeField] private int cubestQueueCapacity = 10;
    [SerializeField] private bool autoQueueGrow = true;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Color[] cubeColors;

    [HideInInspector] public int maxCubeNumber;//in our case 4096

    private int maxPower=12;

    private Vector3 defaultSpawnPosition;

    private void Awake()
    {
        Instance = this;
        defaultSpawnPosition = transform.position;
        maxCubeNumber = (int)Mathf.Pow(2, maxPower);
        InitializeCubeQueue();
    }
   private void InitializeCubeQueue(){
        for (int i = 0; i < cubestQueueCapacity; i++)       
            AddCubeToQueue();
    }
    private void AddCubeToQueue(){
        Cube cube = Instantiate(cubePrefab, defaultSpawnPosition, Quaternion.identity).GetComponent<Cube>();
            cube.gameObject.SetActive(false);
            cube.IsMainCube=false;
            cubeQueue.Enqueue(cube);
    }
    public Cube Spawn(int number, Vector3 position){
        if(cubeQueue.Count == 0){
            if(autoQueueGrow){
                cubestQueueCapacity++;
                AddCubeToQueue();
            }else{
                Debug.Log("[Cubes Queue] Queue is full");
                return null;
            }
        }
        Cube cube = cubeQueue.Dequeue();
        cube.transform.position = position;
        cube.SetNumber(number);
        cube.SetColor(GetColor(number));
        cube.gameObject.SetActive(true);
        return cube;
    }
    public Cube SpawnRandom(){
        return Spawn (GenerateRandomNumber(), defaultSpawnPosition);
    }
    public void DestroyCube(Cube cube, float time){
        cube.CubeRigidbody.velocity = Vector3.zero;
        cube.CubeRigidbody.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.IsMainCube = false;
        cube.gameObject.SetActive(false);
        cubeQueue.Enqueue(cube);

    }


    public int GenerateRandomNumber(){
        return (int)Mathf.Pow(2,Random.Range(1,6)); // 2,4,8,16,32
    }

    private Color GetColor(int number){
        return cubeColors[(int)(Mathf.Log(number)/Mathf.Log(2))-1];
    }
    




}

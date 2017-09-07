using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratorScript : MonoBehaviour {

    GameObject newObject;
    Vector3 lastPosition = new Vector3(10, 0, 0);
    Vector3 offsetVector;

    private float screenWidthInPoints;

    int randomObject;

	public GameObject[] availableObjects;    
	public List<GameObject> objects;
	
	public float objectsMinDistance = 3.0f;    
	public float objectsMaxDistance = 10.0f;
	
	public float objectsMinY = -1.4f;
	public float objectsMaxY = 1.4f;
	
	public float objectsMinRotation = -45.0f;
	public float objectsMaxRotation = 45.0f;

    public float rotation;
    TurtleController turtleController;


    // Use this for initialization
    void Start () {


        InvokeRepeating("AddObject", 0.0f, 2.2f);
        GameObject thePlayer = GameObject.Find("player");
        turtleController = thePlayer.GetComponent<TurtleController>();



    }
	
	// Update is called once per frame
	void Update () {
        float randomY = Random.Range(objectsMinY, objectsMaxY);
        newObject = availableObjects[Random.Range(0, availableObjects.Length)];
        offsetVector = new Vector3(Random.Range(objectsMinDistance, objectsMaxDistance), randomY, 0);
        rotation = Random.Range(objectsMinRotation, objectsMaxRotation);
       
    }



    void AddObject()
    {
        if (turtleController.timeOut == false)
        {
            UnityEngine.GameObject myPrefabInstance = GameObject.Instantiate(newObject, lastPosition + offsetVector, Quaternion.Euler(Vector3.forward * rotation));


            lastPosition = new Vector3(myPrefabInstance.transform.position.x, 0, 0);
        }
    }






}

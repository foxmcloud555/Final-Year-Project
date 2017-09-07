using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObjectOrRoom : MonoBehaviour {

    GameObject player;

    float playerXPos = 0;

    float distanceThreshold = 0;

    // Use this for initialization
    void Start () {
		 player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

       playerXPos = player.transform.position.x;

        distanceThreshold = playerXPos - 20;

      




        if (transform.position.x < distanceThreshold)
        {
            Object.Destroy(this.gameObject);
            
        }
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour {

    public GameObject goodFood;
    public GameObject badFood;
    GameObject foodItem;
    float Timer;


    // Use this for initialization
    void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {

        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            int spriteValue = UnityEngine.Random.Range(0, 2);
            switch (spriteValue)
            {
                case 0:
                  foodItem = Instantiate(goodFood, new Vector3(Random.Range(-6, 6), 5f, 0f), transform.rotation) as GameObject;
                    break;
                case 1:
                    foodItem = Instantiate(badFood, new Vector3(Random.Range(-6, 6), 5f, 0f), transform.rotation) as GameObject;
                    break;


            }
            Timer = 0.5f;
        }


    }
}

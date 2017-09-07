using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadFood : MonoBehaviour {

    public Sprite[] badFoods;
    //public Sprite Wrench;
    //public Sprite Hammer;
    //public Sprite Poop;
    public bool poopCheck = false;
    int spriteValue;

    SpriteRenderer sr;

    // Use this for initialization
    void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        spriteValue = Random.Range(0, 3);
       

                
                sr.sprite = badFoods[spriteValue];
        if (spriteValue == 2)
        {
            tag = "Poop";
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.transform.position.y < -7)
        {
            GameObject.Destroy(gameObject);
        }

    }
}

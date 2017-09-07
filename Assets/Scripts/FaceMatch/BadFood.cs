using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadFood : MonoBehaviour {

    public Sprite Wrench;
    public Sprite Hammer;
    public Sprite Poop;
   
    int spriteValue;

    SpriteRenderer sr;

    // Use this for initialization
    void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        spriteValue = UnityEngine.Random.Range(0, 3);
        switch (spriteValue)
        {
            case 0:
                sr.sprite = Wrench;
                break;
            case 1:
                sr.sprite = Hammer;
                break;
            case 2:
                sr.sprite = Poop;
                break;
           

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodFood : MonoBehaviour {

    public Sprite Apple;
    public Sprite Banana;
    public Sprite Burger;
    public Sprite Donut;
    public Sprite Fries;
    int spriteValue;

    SpriteRenderer sr;

    // Use this for initialization
    void Start () {

        sr = GetComponent<SpriteRenderer>();
        spriteValue = UnityEngine.Random.Range(0, 5);
        switch (spriteValue)
        {
            case 0:
                sr.sprite = Apple;
                break;
            case 1:
                sr.sprite = Banana;
                break;
            case 2:
                sr.sprite = Burger;
                break;
            case 3:
                sr.sprite = Donut;
                break;
            case 4:
                sr.sprite = Fries;
                break;

        }

    }
	
	// Update is called once per frame
	void Update () {

        if (gameObject.transform.position.y < -7)
        {
            GameObject.Destroy(gameObject);
        }
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Affdex;
using UnityEngine.UI;

public class PlayerMouth : ImageResultsListener
{

    public GameObject faceDetect;

    public Sprite faceNotFound;
    public Sprite faceFound;

    SpriteRenderer srDetect;

    public Sprite Closed;
    public Sprite Open;
    public float currentMouthOpen;
    bool mouthOpen;
    int goodFood;
    int badFood;
    int score;
    public Text goodFoodText;
    public Text badFoodText;
    public Text scoreText;
    private Rigidbody2D rb2d;
    private bool gameWon;

    SpriteRenderer sr;

    public override void onFaceFound(float timestamp, int faceId)
    {

        //if (Debug.isDebugBuild) Debug.Log("Found the face");
        srDetect.sprite = faceFound;
    }

    public override void onFaceLost(float timestamp, int faceId)
    {

        currentMouthOpen = 0;
        // if (Debug.isDebugBuild) Debug.Log("Lost the face");
        srDetect.sprite = faceNotFound;
    }

    public override void onImageResults(Dictionary<int, Face> faces)
    {
        if (faces.Count > 0)
        {

            faces[0].Expressions.TryGetValue(Expressions.MouthOpen, out currentMouthOpen);
        }
    }

    // Use this for initialization
    void Start()
    {
        srDetect = faceDetect.GetComponent<SpriteRenderer>();
        sr = GetComponent<SpriteRenderer>();
        goodFoodText = goodFoodText.GetComponent<Text>();
        badFoodText = badFoodText.GetComponent<Text>();
        scoreText = scoreText.GetComponent<Text>();
        gameWon = false;

    }

    // Update is called once per frame
    void Update()
    {

        mouthOpen = currentMouthOpen > 10;

        MoveMouth();
        goodFoodText.text = "Good Foods = " + goodFood.ToString();
        badFoodText.text = "Bad Foods = " + badFood.ToString();
        scoreText.text = "Score = " + score.ToString();



        float movex = Input.GetAxis("Horizontal");
        if (!gameWon)
        {
            if (Input.GetKey("left"))
            {
                gameObject.transform.Translate(Vector3.left / 5);

            }
            else
            {

            }

            if (Input.GetKey("right"))
            {
                gameObject.transform.Translate(Vector3.right / 5);
            }

        }

        if (gameObject.transform.position.x > 10)
        {
            gameObject.transform.position = new Vector3(-10, -4.12f, 0);
        }

        if (gameObject.transform.position.x < -10)
        {
            gameObject.transform.position = new Vector3(10, -4.12f, 0);
        }


        if (score > 20)
        {
            gameWon = true;
        }



    }

    void MoveMouth()
    {

        if (mouthOpen)
        {
            sr.sprite = Open;
        }
        else
        {
            sr.sprite = Closed;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (mouthOpen)
        {
            if (col.gameObject.tag == "GoodFood")
            {
                Destroy(col.gameObject);
                goodFood++;
                score++;

            }
            if (col.gameObject.tag == "BadFood")
            {
                Destroy(col.gameObject);
                badFood++;
                if (score > 0)
                {
                    score--;
                }

            }
        }

    }

    void OnGUI()
    {

    }

    void DisplayRestartButton()
    {
        if (gameWon)
        {
            Rect buttonRect = new Rect(Screen.width * 0.35f, Screen.height * 0.45f, Screen.width * 0.30f, Screen.height * 0.1f);
            if (GUI.Button(buttonRect, "Tap to restart!"))
            {
                Application.LoadLevel(Application.loadedLevelName);
            };
        }



    }
}



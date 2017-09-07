using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Affdex;
using UnityEngine.UI;

public class PlayerMouth : ImageResultsListener
{

    public GameObject faceDetect;
    public Canvas buttonCanvas;
    private Button[] buttons;
    public Sprite faceNotFound;
    public Sprite faceFound;

    SpriteRenderer srDetect;

    public Sprite Closed;
    public Sprite Open;
    public float currentMouthOpen;
    bool mouthOpen;
    int goodFoodCount;
    int badFoodCount;
    int score;
    public Text goodFoodText;
    public Text badFoodText;
    public Text scoreText;
    private Rigidbody2D rb2d;
    private bool gameWon;
    bool poopCheck = false;
    BadFood badFoodScript;
    public AudioClip fartSound;
    public AudioClip goodFoodSound;
    public AudioClip badFoodSound;
    public AudioClip belch;
    private bool belchCheck = false;
    Text[] fullText;
   

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
        if (!gameWon){
            mouthOpen = currentMouthOpen > 10;
        }

        MoveMouth();
        goodFoodText.text = "Good Foods = " + goodFoodCount.ToString();
        badFoodText.text = "Bad Foods = " + badFoodCount.ToString();
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


        if (score >= 10 && belchCheck == false)
        {
            gameWon = true;
            AudioSource.PlayClipAtPoint(belch, transform.position, 1.0f);
            Debug.Log("belch!");
            belchCheck = true;
            mouthOpen = false;

        }

        DisplayRestartButton();

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
                goodFoodCount++;
                score++;
                AudioSource.PlayClipAtPoint(goodFoodSound, transform.position, 1.0f);



            }
            if (col.gameObject.tag == "BadFood")
            {
                Destroy(col.gameObject);
                badFoodCount++;
                if (score > 0)
                {
                    score--;
                }
                AudioSource.PlayClipAtPoint(badFoodSound, transform.position, 1.0f);
            }

            if (col.gameObject.tag == "Poop")
            {
                Destroy(col.gameObject);
                badFoodCount++;
                Debug.Log("poop!");
                if (score > 0)
                {
                    score--;
                }
                AudioSource.PlayClipAtPoint(fartSound, transform.position, 1.0f);
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
            fullText = buttonCanvas.GetComponentsInChildren<Text>(true);
            buttons = buttonCanvas.GetComponentsInChildren<Button>(true);

            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(true);
            }
            foreach (Text text in fullText)
            {
               text.gameObject.SetActive(true);
            }

        }

    }
}



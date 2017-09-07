using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Affdex;
using UnityEngine.UI;

public class FaceMatchGameController : ImageResultsListener
{

    public Sprite[] uiFaceList;
   // public Sprite[] animalFaceList;
   // public Sprite grin;
  //  public Sprite neutral;
   // public Sprite angry;
    public float currentMouthOpen;
    public float currentSmile;
  //  public float currentValence;
    public float currentAnger;
    public float currentSurprise;
    public float currentDisgust;
    public float currentEyeClosure;
    public float currentLipPucker;

    public Canvas gameCanvas;

    

    SpriteRenderer playerSr;
  

    public override void onFaceFound(float timestamp, int faceId)
    {

        if (Debug.isDebugBuild) Debug.Log("Found the face");
    }

    public override void onFaceLost(float timestamp, int faceId)
    {
        currentMouthOpen = 0;
        currentSmile = 0;
        currentEyeClosure = 0;
        currentLipPucker = 0;

        if (Debug.isDebugBuild) Debug.Log("Lost the face");
    }

    public override void onImageResults(Dictionary<int, Face> faces)
    {
        if (faces.Count > 0)
        {
            faces[0].Expressions.TryGetValue(Expressions.MouthOpen, out currentMouthOpen);
            faces[0].Emotions.TryGetValue(Emotions.Joy, out currentSmile  );
            faces[0].Emotions.TryGetValue(Emotions.Anger, out currentAnger);
            faces[0].Expressions.TryGetValue(Expressions.EyeClosure, out currentEyeClosure);
            faces[0].Expressions.TryGetValue(Expressions.LipPucker, out currentLipPucker);

        }
    }

    // Use this for initialization
    void Start () {

        EmotionChecker.currentEmotion = "neutral";
        playerSr = GetComponent<SpriteRenderer>();

      

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("w") || ((currentMouthOpen > 50) && (currentEyeClosure < 50)))
        {

            playerSr.sprite = uiFaceList[0];
            Debug.Log("Lion");

        }
        else
        
        if (Input.GetKeyDown("e") || ((currentEyeClosure > 50) && (currentSmile > 50)))
        {

            playerSr.sprite = uiFaceList[1];
            Debug.Log("monkey ");

        }
        else
        
        if (Input.GetKeyDown("r") || ((currentEyeClosure > 50) && (currentMouthOpen > 50)&&(currentSmile <50)))
        {

            playerSr.sprite = uiFaceList[2];
            Debug.Log("sloth");

        }
        else
        if (currentLipPucker > 50)
        {
            playerSr.sprite = uiFaceList[4];
            
        }
       else playerSr.sprite = uiFaceList[3];
       

    }
}

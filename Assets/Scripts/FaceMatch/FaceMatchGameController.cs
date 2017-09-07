using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Affdex;

public class FaceMatchGameController : ImageResultsListener
{

    public Sprite smile;
    public Sprite grin;
    public Sprite neutral;
    public Sprite angry;
    public float currentMouthOpen;
    public float currentSmile;
    public float currentValence;
    public float currentAnger;
    public float currentSurprise;
    public float currentDisgust;
    public float currentEyeClosure;
    

    SpriteRenderer sr;

    public override void onFaceFound(float timestamp, int faceId)
    {

        if (Debug.isDebugBuild) Debug.Log("Found the face");
    }

    public override void onFaceLost(float timestamp, int faceId)
    {
        currentMouthOpen = 0;
        currentSmile = 0;
        if (Debug.isDebugBuild) Debug.Log("Lost the face");
    }

    public override void onImageResults(Dictionary<int, Face> faces)
    {
        if (faces.Count > 0)
        {
            faces[0].Expressions.TryGetValue(Expressions.MouthOpen, out currentMouthOpen);
            faces[0].Emotions.TryGetValue(Emotions.Joy, out currentSmile  );
        }
    }

    // Use this for initialization
    void Start () {

        EmotionChecker.currentEmotion = "neutral";
        sr = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("w")||(currentSmile > 50))
        {
            Debug.Log("w pressed");
            sr.sprite = smile;
        }
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("e pressed");
            sr.sprite = grin;
        }
        if (Input.GetKeyDown("r"))
        {
            Debug.Log("r pressed");
            sr.sprite = neutral;
        }

    }
}

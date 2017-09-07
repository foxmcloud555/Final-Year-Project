using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceChange : MonoBehaviour {

    public Image animalFaceHolder;

    Image sr;
    
    public Sprite[] uiFaceList;
    private int faceIndex;
    private int FACEMAX;

    // Use this for initialization
    void Start () {
        FACEMAX = uiFaceList.Length;
        faceIndex = 0;
        sr = animalFaceHolder.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        sr.sprite = uiFaceList[faceIndex];
    }

    public void SwitchFace()
    {


        if (faceIndex < FACEMAX - 1)
        {
            faceIndex++;
        }
        else faceIndex = 0;
        
        

    }
}

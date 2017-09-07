using UnityEngine;
using UnityEngine.UI;
using Affdex;
using System.Collections.Generic;


public class TurtleController : ImageResultsListener {


    public GameObject faceDetect;

    public Sprite faceNotFound;
    public Sprite faceFound;

    public Canvas buttonCanvas;
    private Button[] buttons;

    Image image;
    SpriteRenderer sr;

    private bool timeOut = false;

    private float coins;

	public Texture2D coinIconTexture;

	public AudioClip coinCollectSound;

	public AudioSource jetpackAudio;
	
	public AudioSource footStepSound;

	public ParallaxScroll parallax;

    public float timer;

    bool jetpackOn = false;

    public Text timerText;

    public Text coinText;

    public float currentMouthOpen;

    public float jetpackThrust = 20.0f;

    public float playerSpeed = 3.0f;

    public Transform isonFloor;

    public bool onFloor;

    public LayerMask groundCheckLayerMask;

    Animator animator;

    public ParticleSystem jetpack;

    public override void onFaceFound(float timestamp, int faceId)
    {
       
       
       // sr.sprite = faceFound;
        image.sprite = faceFound;
    }

    public override void onFaceLost(float timestamp, int faceId)
    {
        currentMouthOpen = 0;
       
        //sr.sprite = faceNotFound;
        image.sprite = faceNotFound;
    }

    public override void onImageResults(Dictionary<int, Face> faces)
    {
        if (faces.Count > 0)
        {
           faces[0].Expressions.TryGetValue(Expressions.MouthOpen, out currentMouthOpen);
        }
    }


    // Use this for initialization
    void Start () {
        //sr = faceDetect.GetComponent<SpriteRenderer>();
        image = faceDetect.GetComponent<Image>();
        animator = GetComponent<Animator>();
        timerText = timerText.GetComponent<Text>();
        coinText = coinText.GetComponent<Text>();




    }
	
	// Update is called once per frame
	void Update () {
    
        //if (onFloor == true)
        //{
        //    Debug.Log("on the floor");
        //}
        //else Debug.Log("in the air");



    }

	void FixedUpdate () 
	{
        timer = Mathf.Round(timer * 1000.0f) / 1000.0f;
        

        timerText.text = timer.ToString();
        coinText.text = coins.ToString();

        //if (jetpackOn == false && !timeOut && currentMouthOpen > 3 )
        //{
        //    jetpackOn = true;
        //  //  timer += 0.05f;
        //}
       

        //if (jetpackOn == true && !timeOut && currentMouthOpen < 90)
        //{
        //    jetpackOn = false;
        //}

        bool jetpackOn = currentMouthOpen > 3;
        
       // jetpackOn = jetpackOn && !timeOut;
		
		
		
		if (!timeOut)
		{
			Vector2 newVelocity = GetComponent<Rigidbody2D>().velocity;
			newVelocity.x = playerSpeed;
			GetComponent<Rigidbody2D>().velocity = newVelocity;
		}

        if (timer > 20)
        {
            timeOut = true;
            animator.SetBool("timeOut", true);
            DisplayRestartButton();
        }
		
		UpdateonFloorStatus();

        jetpackControl(jetpackOn);

		parallax.offset = transform.position.x;
	} 



    void jetpackControl( bool jetpackOn)
    {
        if (jetpackOn)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jetpackThrust));
            timer += 0.02f;
        }

        ParticleSystem.EmissionModule em = jetpack.emission;
        em.enabled = !onFloor;

        if (jetpack)
        {
            em.rateOverTime = 300f;
        }
        else
        {
            em.rateOverTime = 75f;
        }
       // em.rateOverTime = jetpackOn ? 300.0f : 75.0f;

        footStepSound.enabled = !timeOut && onFloor;

        jetpackAudio.enabled =  !timeOut && !onFloor;
        jetpackAudio.volume = jetpackOn ? 1.0f : 0.5f;
        footStepSound.enabled = !timeOut && onFloor;
        footStepSound.volume = 0.1f;

    }

    void UpdateonFloorStatus()
	{
		//1
		onFloor = Physics2D.OverlapCircle(isonFloor.position, 0.1f, groundCheckLayerMask);
		
		//2
		animator.SetBool("onFloor", onFloor);
	}

	

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("Coins"))
			CollectCoin(collider);
		else
			HitByweapon(collider);
	}
		
	void HitByweapon(Collider2D weaponCollider)
	{
        if (!timeOut)
        {
            weaponCollider.gameObject.GetComponent<AudioSource>().Play();

            if (coins > 10)
            {
                coins -= 10;
            }

            else
            {
                coins = 0;
            }
        }
    }

	void CollectCoin(Collider2D coinCollider)
	{
		coins++;
		
		Destroy(coinCollider.gameObject);
        float volume = 5.0f;

		AudioSource.PlayClipAtPoint(coinCollectSound, transform.position, volume);
	}

	

	void DisplayRestartButton()
	{
        if (timeOut && onFloor)
        {

            buttons = buttonCanvas.GetComponentsInChildren<Button>(true);

            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(true);
            }

        }

    }

	

}

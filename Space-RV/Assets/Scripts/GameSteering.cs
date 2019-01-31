using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class GameSteering : MonoBehaviour
{
	//	GLOBAL VARIABLES
	//	=============================================
	//	Width of half of steering track
	//	(this will be doubled, zero in the centre)
		public float WIDTH = 50.0f;

	//	Width of sweet spot (as above)
		public float SWEET = 1.0f;

	//	Starting speed of needle
		public float SPEED = 0.03f;

	//	Add to speed with each move
		public float ACCEL = 0.01f;

	//	Time required to stay in sweet spot to win
		public float WINTIME = 100;
	//	=============================================

	public float needlePos = 0;
	private float currentSpeed = 0;
	private int direction = 0;
	private int timeInSweetSpot = 0;
	private bool gameOver = false;

	public Slider steerSlider;
	public BridgeRoom bridge;
	public PlayerMovement player;
	public int countMin = 5;
	public int countMax = 20;
	public bool isSteering = false;
	public bool canInteract = false;
	public bool countdownRunning = false;

	public GameObject sliderUI;
	public GameObject pilotUI;
	public GameObject goodText;
	public GameObject steerUI;
	public Image right;
	public Image left;

	public JuicyInteract uiPop;

	private Vector3 ogSizePilot;
	private Vector3 ogSizeSteer;
	private Vector3 ogSizeSlider;
	private Vector3 ogSizeGood;
	private Vector3 closed;

	public GameObject Bridge;
	bool goodCountdown = false;

    void Start()
    {
       // Debug.Log("running");
		currentSpeed = SPEED;
		direction = leftOrRight();

		closed = new Vector3(0,0,0);
		
		ogSizeSlider = sliderUI.transform.localScale;
		ogSizeGood = goodText.transform.localScale;
		ogSizeSteer = steerUI.transform.localScale;

		//pilotUI.transform.localScale = closed;
		sliderUI.transform.localScale = closed;
		goodText.transform.localScale = closed;
		steerUI.transform.localScale = closed;

		goodText.SetActive(false);
		steerUI.SetActive(false);
		

    }

	void LateStart()
	{
		ogSizePilot = bridge.gameObject.GetComponent<BridgeUI>().ogSize;
	}

    void Update()
    {
    	//	Stop running if the game is over;
    	//	this may only be necessary for debugging
    	if (gameOver) 
    	{
    		return;
    	}
        
        if (timeInSweetSpot >= WINTIME && isSteering) 
        {
    //	WIN STATE HERE
        	//Debug.Log("Congratulations!");
        	gameOver = true;
			goodText.SetActive(true);
			uiPop.StartCoroutine(uiPop.UIPop(goodText.transform.localScale, ogSizeGood, goodText));
			uiPop.StartCoroutine(uiPop.UIPop(sliderUI.transform.localScale, closed, sliderUI));
			sliderUI.SetActive(false);	
			player.canMove = true;		
			isSteering = false;	

			StartCoroutine(CountdownNext());
			StartCoroutine(CountdownCloseGood());
        }
        if (Math.Abs(needlePos) > WIDTH) 
        {
    //	LOSE STATE HERE
        	Debug.Log("Bummer you hit like, an asteroid or something");
        	gameOver = true;
        }

		if(Math.Abs(needlePos)>SWEET)
		{
			if(steerUI.activeSelf==false && isSteering == false)
			{
				steerUI.SetActive(true);
				uiPop.StartCoroutine(uiPop.UIPop(steerUI.transform.localScale, ogSizeSteer , steerUI));
			}
			
		}
		if(canInteract)
		{
			//press A to interact
			if(Input.GetButtonDown("Fire1"))
			{
				isSteering = true;
				player.canMove = false;
				canInteract = false;
				uiPop.StartCoroutine(uiPop.UIPop(pilotUI.transform.localScale, closed, pilotUI));
				pilotUI.SetActive(false);
				sliderUI.SetActive(true);
				uiPop.StartCoroutine(uiPop.UIPop(sliderUI.transform.localScale, ogSizeSlider, sliderUI));

				if(steerUI.activeSelf)
				{
					
					uiPop.StartCoroutine(uiPop.UIPop(steerUI.transform.localScale, closed, steerUI));
					steerUI.SetActive(false);
				}

			
			}
			

		}

		if(isSteering)
		{

			listenToControlPad();		

		}

        
    }

    void FixedUpdate()
    {
    	if (!gameOver) 
    	{
	        needlePos += currentSpeed * direction;
			steerSlider.value = needlePos;
    	}

        if (Math.Abs(needlePos) < SWEET) 
        {
        	timeInSweetSpot++;
        }	else {
        	timeInSweetSpot = 0;
        }
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            canInteract = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            canInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            canInteract = false;
        }
    }

    void listenToControlPad()	{
    	float input = Input.GetAxis("Horizontal");

    	if (Math.Abs(input) > 0.1) 
    	{
    		if (input < 0) 
    		{
    			Debug.Log("moving left");
    			direction = -1;
				left.color = Color.green;
			//	right.color = Color.white;
    		}
    		if (input > 0) 
    		{
    			Debug.Log("moving right");
    			direction = 1;
				right.color = Color.green;
			//	left.color = Color.white;
    		}

			else{
				left.color = Color.white;
				right.color = Color.white;
			}
    		currentSpeed += Math.Abs(input) * ACCEL;
    	}

		if(input > 0)
		{
			right.color = Color.green;
			left.color = Color.white;
		}

		if(input <0)
		{
			left.color = Color.green;
			right.color = Color.white;
		}
		if(input == 0)
		{
			left.color = Color.white;
			right.color = Color.white;
		}

		//press b to exit steering
			if(Input.GetButtonDown("Fire2"))
			{
				Debug.Log("Not Steering");
				isSteering = false;
				player.canMove = true;
				canInteract = true;	
				pilotUI.SetActive(true);
				uiPop.StartCoroutine(uiPop.UIPop(pilotUI.transform.localScale, ogSizePilot, pilotUI));
				
				
				uiPop.StartCoroutine(uiPop.UIPop(sliderUI.transform.localScale, closed, sliderUI));
				sliderUI.SetActive(false);			
			}
    }

    int leftOrRight()	{
    	if (UnityEngine.Random.Range(0, 1) == 0) 
    	{
    		return -1;
    	}	else {
    		return 1;
    	}
    }

	IEnumerator CountdownNext()
	{
		if(countdownRunning)
		{
			yield break;
		}

		countdownRunning = true;

		int count = UnityEngine.Random.Range(countMin,countMax);
		yield return new WaitForSeconds(count);
		gameOver = false;
		timeInSweetSpot = 0;
		Debug.Log("Steer Game Starting again");
		currentSpeed = SPEED;
		isSteering = false;
		countdownRunning = false;
	}

	IEnumerator CountdownCloseGood()
	{
		if(goodCountdown)
		{
			yield break;
		}

		goodCountdown = true;
		int count = 2;

		yield return new WaitForSeconds(count);
		
		uiPop.StartCoroutine(uiPop.UIPop(goodText.transform.localScale, closed, goodText));
		goodText.SetActive(false);
		goodCountdown = false;

	}
}

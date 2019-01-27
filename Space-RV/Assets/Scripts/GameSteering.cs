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

    void Start()
    {
       // Debug.Log("running");
		currentSpeed = SPEED;
		direction = leftOrRight();
    }

    void Update()
    {
    	//	Stop running if the game is over;
    	//	this may only be necessary for debugging
    	if (gameOver) 
    	{
    		return;
    	}
        
        if (timeInSweetSpot >= WINTIME) 
        {
    //	WIN STATE HERE
        	//Debug.Log("Congratulations!");
        	gameOver = true;
			StartCoroutine(CountdownNext());
        }
        if (Math.Abs(needlePos) > WIDTH) 
        {
    //	LOSE STATE HERE
        	Debug.Log("Bummer you hit like, an asteroid or something");
        	gameOver = true;
        }
		if(canInteract)
		{
			if(Input.GetButtonDown("Fire1"))
			{
				isSteering = true;
				player.canMove = false;
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
    		}
    		if (input > 0) 
    		{
    			Debug.Log("moving right");
    			direction = 1;
    		}
    		currentSpeed += Math.Abs(input) * ACCEL;
    	}

		//press b to exit steering
			if(Input.GetButtonDown("Fire2"))
			{
				Debug.Log("Not Steering");
				isSteering = false;
				player.canMove = true;				
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
		Debug.Log("Game Starting again");
		currentSpeed = SPEED;
		isSteering = false;
		countdownRunning = false;
	}
}

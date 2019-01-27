using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        Debug.Log("running");
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
        	Debug.Log("Congratulations!");
        	gameOver = true;
        }
        if (Math.Abs(needlePos) > WIDTH) 
        {
    //	LOSE STATE HERE
        	Debug.Log("Bummer you hit like, an asteroid or something");
        	gameOver = true;
        }

        listenToControlPad();
    }

    void FixedUpdate()
    {
    	if (!gameOver) 
    	{
	        needlePos += currentSpeed * direction;
    	}

        if (Math.Abs(needlePos) < SWEET) 
        {
        	timeInSweetSpot++;
        }	else {
        	timeInSweetSpot = 0;
        }
    }

    void listenToControlPad()	{
    	float input = Input.GetAxis("HorizontalL");

    	if (Math.Abs(input) > 0.1) 
    	{
    		if ((input < 0 && direction > 0) || (input > 0 && direction < 0)) 
    		{
    			Debug.Log("switch dir");
    			direction *= -1;
    		}
    		currentSpeed += input * ACCEL;
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
}

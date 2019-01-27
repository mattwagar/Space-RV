using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSequence	 : MonoBehaviour
{
	//	GLOBAL VARIABLES
	//	=================================================
	//	Is the game running
		public bool gameIsOn = false;

	//	Number of control pad arrows
	//	before player gets to see which button to press
		public int ARROWS = 3;

	//	Time to press button
		public int BTN_SPEED = 50;

	//	Kids' happiness meter
		public float HAPPINESS = 100.0f;

	//	Rate of happiness decrease when idle
		public float IDLE_HANDS = 0.01f

	//	Add to kids' happiness on win
		public float HAPPY_UP = 10.0f;

	//	Decrease kids' happiness on loss
		public float HAPPY_DOWN = 20.0f;
	//	=================================================

    int[] sequence = new int[ARROWS + 1];
    bool readyForNextSeq = true;
    bool showLetter = false;
    int currentArrow = 0;
    bool listenToPad = true;
    int buttonTimer = 0;

    void Start()
    {
    	Debug.Log("running");
    }

    void FixedUpdate()	{
    	if (showLetter) 
    	{
    		buttonTimer++;
    	}
    	if (!gameIsOn) 
    	{
    		HAPPINESS -= IDLE_HANDS;
    	}
    }

    void Update()
    {
    	if (Input.GetButtonDown("Fire3"))
    	{
    //	B BUTTON; EXIT GAME
    	}

    	if (!showLetter) 
	    	{
	    	float inputX = Input.GetAxis("Horizontal");
	    	float inputY = Input.GetAxis("Vertical");

	    	//	Turn listening back on when player lets go of control pad
	    	if (&& inputX < 0.1 && inputX > -0.1 && inputY < 0.1 && inputY > -0.1) 
	    	{
	    		listenToPad = true;
	    	}
    	}

    	//	Waiting for the next arrow input
    	if (listenToPad && currentArrow < ARROWS)
    	{
    		//	Check which direction the player pressed
    		checkPad(inputX, inputY);
    	}

    	//	Waiting for button input
    	if (showLetter)
    	{
    		if (buttonTimer >= BTN_SPEED) 
    		{
    			HAPPINESS -= HAPPY_DOWN
    		}
    		//	Send the sequence number, get back the button letter
    		char button = getSequenceButton();

    		//	Check if player is pressing a button
    		char input = checkButtons();

    		//	If a button has been pressed, check it against the sequence
    		if (input != ' ')
    		{
    			//	If it's a match, reset the arrow index to the start, 
    			//	increment the sequence index, 
    			//	prepare to display sequence on next frame
	    		if (button == input) 
	    		{
	    			Debug.Log("yup");
	    			HAPPINESS += HAPPY_UP;
	    			readyForNextSeq = true;
	    		}	else {
	//	CALL LOSE STATE HERE
	    			Debug.Log("no dice, " + input);
	    			HAPPINESS -= HAPPY_DOWN;
	    		}
    		}
    	}

    	//	The readyForNextSeq boolean is so that the Debug Console will only 
    	//	show thre next sequence once; it may not be necessary
    	if (readyForNextSeq) 
    	{
	        displayNext();
    	}
    }

    //	This is the listener to the control pad;
    //	reads the direction pressed then returns a number 0-3
    //	corresponding to the sequence array,
    //	then turns off the listener until the player lets go of the pad
    //	between presses
    checkPad(float x, float y) {
    	int inputValue = -1;
    	if (y > 0.9) 
    	{
	    	inputValue = 2;
    	}
    	if (y < -0.9) 
    	{
	    	inputValue = 0;
    	}
    	if (x < -0.9) 
    	{
	    	inputValue = 3;
    	}
    	if (x > 0.9) 
    	{
	    	inputValue = 1;
    	}

    	//	If pad was not pressed, wait for next frame
    	if (inputValue == -1) 
    	{
	    	return;
    	}	else {
    		listenToPad = false;
			//	If it matches the next on in the sequence, increment the array index
			if (input == sequence[currentArrow]) 
			{
				Debug.Log("correct");
				currentArrow++;
			}	else {
	//	CALL START-OVER STATE HERE
				Debug.Log("wrong");
				currentArrow = 0;
			}

			//	If the arrow sequence is complete, reveal the button
			if (currentArrow == ARROWS)
			{
				showLetter = true;
			}
		}
    }

    //	Listener for the buttons; returns letter of button pressed
    char checkButtons()	{
    	char letter = ' ';
    	if (Input.GetButtonDown("Jump"))
    	{
    		letter = 'Y';
    	}
    	if (Input.GetButtonDown("Fire1"))
    	{
    		letter = 'X';
    	}
    	if (Input.GetButtonDown("Fire2"))
    	{
    		letter = 'A';
    	}
    	return letter;
    }

    //	Returns button letter mapped to 0-3 from sequence array
    char getSequenceButton()	{
    	char letter = ' ';
    	switch (sequence[currentSeq, 3]) 
    	{
    		case 0:
    		  letter = 'A';
    		  break;
    		case 1:
    		  letter = 'X';
    		  break;
    		case 2:
    		  letter = 'Y';
    		  break;
    	}
    	return letter;
    }

    // Traverses arrow portion of current sequence
    void displayNext()	{
    	readyForNextSeq = false;
    	currentArrow = 0;

    	for (int i = 0; i < ARROWS; i++) 
    	{
    		sequence[i] = Random.Range(0, 4);
    	}
    	sequence[ARROWS] = Random.Range(0, 3)

    	//	This is just Debug Console shit;
    	//	Basically, at this point display the arrow sprites on the screen
    	for (int i=0; i<3; i++) 
    	{
    		showArrow(sequence[i]);
    	}


    }

    //	Outputs arrow sequence to Debug Console
    void showArrow(int num)	{
    	char arrow = '-';
    	switch (num) 
    	{
    		case 0:
    		  arrow = '^';
    		  break;
    		case 1:
    		  arrow = '>';
    		  break;
    		case 2:
    		  arrow = 'v';
    		  break;
    		case 3:
    		  arrow = '<';
    		  break;
    	}
    	Debug.Log(arrow);
    }
}

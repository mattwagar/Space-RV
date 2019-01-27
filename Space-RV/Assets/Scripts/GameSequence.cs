using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceScript : MonoBehaviour
{
	//	GLOBAL VARIABLES
	//	===================================================================
	//	Number of sequences player must complete in task
		final int SEQUENCES = 5;

	//	Number of control pad arrows before player gets to see which button
		final int ARROWS = 3;
	//	===================================================================

    int[,] sequences = new int[SEQUENCES, ARROWS + 1];
    bool readyForNextSeq = true;
    bool showLetter = false;
    int currentSeq = 0;
    int currentArrow = 0;
    bool listenToPad = true;

    void Start()
    {
    	//	Fill the array of sequences with random numbers 0-3
    	for (int i = 0; i < SEQUENCES; i++) 
    	{
    		for (int j = 0; j < ARROWS + 1; j++) 
    		{
    			int nextNum = Random.Range(0, 4);
    			sequences[i, j] = nextNum;
    		}
    	}
    }

    void Update()
    {
    	float inputX = Input.GetAxis("Horizontal");
    	float inputY = Input.GetAxis("Vertical");

    	//	Turn listening back on when player lets go of control pad
    	if (inputX < 0.1 && inputX > -0.1 && inputY < 0.1 && inputY > -0.1) 
    	{
    		listenToPad = true;
    	}

    	//	Waiting for the next arrow input
    	if (listenToPad && currentArrow < ARROWS)
    	{
    		//	Check which direction the player pressed
    		int input = checkPad(inputX, inputY);
    		if (input > -1) 
    		{
    			//	If it matches the next on in the sequence, increment the array index
	    		if (input == sequences[currentSeq, currentArrow]) 
	    		{
	    			Debug.Log("correct");
	    			currentArrow++;
	    		}	else {
	//	CALL LOSE STATE HERE
	    			Debug.Log("wrong");
	    		}

	    		//	If the arrow sequence is complete, reveal the button
	    		//	The showLetter boolean is only here to prevent the Debug Console from showing
	    		//	it over and over; it may not be necessary when integrated into the game
	    		if (currentArrow == ARROWS)
	    		{
	    			showLetter = true;
	    		}
    		}
    	}

    	//	Waiting for button input
    	if (currentArrow == ARROWS)
    	{
    		//	Send the sequence number, get back the button letter
    		char button = getSequenceButton();

    		//	See note above re: the showLetter boolen
    		if (showLetter)
    		{
	    		showLetter = false;
		    	Debug.Log("now press " + button);
    		}
    		
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
	    			currentArrow = 0;
	    			currentSeq++;
	    			readyForNextSeq = true;
	    		}	else {
	//	CALL LOSE STATE HERE
	    			Debug.Log("no dice, " + input);
	    		}
    		}

    		//	Once entire sequence array has been traversed, game is won 
    		if (currentSeq == SEQUENCES) 
    		{
	//	CALL WIN STATE HERE
    			Debug.Log("woohoo, you fixed the ship");
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
    int checkPad(float x, float y) {
    	int returnValue = -1;
    	if (y > 0.9) 
    	{
	    	returnValue = 2;
    	}
    	if (y < -0.9) 
    	{
	    	returnValue = 0;
    	}
    	if (x < -0.9) 
    	{
	    	returnValue = 3;
    	}
    	if (x > 0.9) 
    	{
	    	returnValue = 1;
    	}
    	if (returnValue > -1) 
    	{
	    	listenToPad = false;
    	}
    	return returnValue;
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
    	if (Input.GetButtonDown("Fire3"))
    	{
    		letter = 'B';
    	}
    	return letter;
    }

    //	Returns button letter mapped to 0-3 from sequence array
    char getSequenceButton()	{
    	char letter = ' ';
    	switch (sequences[currentSeq, 3]) 
    	{
    		case 0:
    		  letter = 'A';
    		  break;
    		case 1:
    		  letter = 'B';
    		  break;
    		case 2:
    		  letter = 'X';
    		  break;
    		case 3:
    		  letter = 'Y';
    		  break;
    	}
    	return letter;
    }

    // Traverses arrow portion of current sequence
    void displayNext()	{
    	readyForNextSeq = false;

    	for (int i=0; i<3; i++) 
    	{
    		showArrow(sequences[currentSeq, i]);
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

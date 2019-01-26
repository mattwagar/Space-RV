using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceScript : MonoBehaviour
{
    int[,] sequences = new int[5, 4];
    bool readyForNext = true;
    bool showLetter = false;
    int currentSeq = 0;
    int currentInput = 0;
    bool listenToPad = true;

    // Start is called before the first frame update
    void Start()
    {
    	for (int i=0; i<5; i++) 
    	{
    		for (int j=0; j<4; j++) 
    		{
    			int nextNum = Random.Range(0, 4);
    			sequences[i, j] = nextNum;
    		}
    	}
    }

    // Update is called once per frame
    void Update()
    {
    	float inputX = Input.GetAxis("Horizontal");
    	float inputY = Input.GetAxis("Vertical");

    	if (inputX < 0.1 && inputX > -0.1 && inputY < 0.1 && inputY > -0.1) 
    	{
    		listenToPad = true;
    	}

    	if (listenToPad && currentInput < 3)
    	{
    		int input = checkPad(inputX, inputY);
    		if (input > -1) 
    		{
	    		if (input == sequences[currentSeq, currentInput]) 
	    		{
	    			Debug.Log("correct");
	    			currentInput++;
	    		}	else {
	    			Debug.Log("wrong");
	    		}
	    		if (currentInput == 3) 
	    		{
	    			showLetter = true;
	    		}
    		}
    	}

    	if (currentInput == 3) 
    	{
    		char button = displayButton();
    		if (showLetter) 
    		{
	    		showLetter = false;
		    	Debug.Log("now press " + button);
    		}
    		
    		char input = checkButtons();

    		if (input != ' ')
    		{
		    	Debug.Log("letter " + button);
	    		if (button == input) 
	    		{
	    			Debug.Log("yup");
	    			currentInput = 0;
	    			currentSeq++;
	    			readyForNext = true;
	    		}	else {
	    			Debug.Log("no dice, " + input);
	    		}
    		}

    		if (currentSeq == 5) 
    		{
    			Debug.Log("woohoo, you fixed the ship");
    		}
    	}

    	if (readyForNext) 
    	{
	        displayNext();
    	}

    	// if (Input.) 
    	// {
    		
    	// }


    }

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

    char displayButton()	{
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

    void displayNext()	{
    	readyForNext = false;

    	for (int i=0; i<3; i++) 
    	{
    		showArrow(sequences[currentSeq, i]);
    	}


    }

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

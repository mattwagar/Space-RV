using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		public float IDLE_HANDS = 0.02f;

	//	Add to kids' happiness on win
		public float HAPPY_UP = 10.0f;

	//	Decrease kids' happiness on loss
		public float HAPPY_DOWN = 20.0f;
	//	=================================================

	public Sprite up;
	public Sprite down;
	public Sprite right;
	public Sprite left;

	public Image space1;
	public Image space2;
	public Image space3;
	public Image space4;

	public Sprite aButton;
	public Sprite xButton;
	public Sprite yButton;

	public List<Image> spaces = new List<Image>();
	public List<Sprite> directions = new List<Sprite>();

    int[] sequence;
    bool readyForNextSeq = true;
    bool showLetter = false;
    int currentArrow = 0;
    bool listenToPad = true;
    int buttonTimer = 0;

	public bool canInteract;
	public PlayerMovement player;

	public Slider happyBar;

	public GameObject interactPrompt;

    void Start()
    {
	    sequence = new int[ARROWS + 1];
		spaces.Add(space1);
		spaces.Add(space2);
		spaces.Add(space3);
		//spaces.Add(space4);

		directions.Add(up);
		directions.Add(right);
		directions.Add(down);
		directions.Add(left);
    	Debug.Log("sequence game running");
		interactPrompt.SetActive(false);
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
		happyBar.value = HAPPINESS;
		if(canInteract)
		{
			if(gameIsOn==false)
			{
				if(Input.GetButtonDown("Fire1"))
				{
				gameIsOn = true;
				player.canMove = false;

				}
			}
			
		}

    	if (gameIsOn) 
    	{
	    	if (Input.GetButtonDown("Fire2"))
	    	{
    //	B BUTTON; EXIT GAME
	    		//	turn off all contents of Update()
	    		gameIsOn = false;

	    		//	reset all booleans and counters to game start
	    		bool readyForNextSeq = true;
	    		bool showLetter = false;
	    		int currentArrow = 0;
	    		bool listenToPad = true;
	    		int buttonTimer = 0;

	    		//	remove arrows and letters from screen
				for (int i=0; i<3; i++) 
		    	{
					Image currentImage = spaces[i];
					currentImage.enabled = false;
					currentImage.transform.rotation = Quaternion.Euler(0,0,0);
		    	}
				space4.enabled = false;
	    	}

			interactPrompt.SetActive(false);

	    	if (readyForNextSeq) 
	    	{
		        displayNext();
	    	}

	    	if (!showLetter) 
		    	{
		    	float inputX = Input.GetAxis("Horizontal");
		    	float inputY = Input.GetAxis("Vertical");

		    	//	Waiting for the next arrow input
		    	if (listenToPad)
		    	{
		    		//	Check which direction the player pressed
		    		checkPad(inputX, inputY);
		    	} 	
		    	//	Turn listening back on when player lets go of control pad
		    	else if (inputX < 0.1 && inputX > -0.1 && inputY < 0.1 && inputY > -0.1) 
		    	{
		    		listenToPad = true;
		    	}

	    	}	else {
	    		//	Check if time has expired to enter button
	    		if (buttonTimer >= BTN_SPEED) 
	    		{
	    			HAPPINESS -= HAPPY_DOWN;
	    			readyForNextSeq = true;
	    			showLetter = false;
	    			buttonTimer = 0;
					space4.enabled = false;
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
		    		}	else {
		//	CALL LOSE STATE HERE
		    			Debug.Log("no dice, " + input);
		    			HAPPINESS -= HAPPY_DOWN;
		    		}
	    			readyForNextSeq = true;
	    			buttonTimer = 0;
	    			showLetter = false;
					space4.enabled = false;
	    		}
	    	}

	    	if (HAPPINESS > 100) 
	    	{
	    		HAPPINESS = 100;
	    	}	else if (HAPPINESS < 0) 
	    	{
	    		HAPPINESS = 0;
	    	}

			
    	}
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            canInteract = true;
			interactPrompt.SetActive(true);
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
			interactPrompt.SetActive(false);
        }
    }

    //	This is the listener to the control pad;
    //	reads the direction pressed then returns a number 0-3
    //	corresponding to the sequence array,
    //	then turns off the listener until the player lets go of the pad
    //	between presses
    void checkPad(float x, float y) {
    	int inputValue = -1;
    	if (y > 0.9) 
    	{
	    	inputValue = 0;
    	}
    	if (y < -0.9) 
    	{
	    	inputValue = 2;
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
			if (inputValue == sequence[currentArrow]) 
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
			// space4.enabled = false;
			
    	}
    	if (Input.GetButtonDown("Fire3"))
    	{
    		letter = 'X';
			// space4.enabled = false;
			//space4.sprite = xButton;
    	}
    	if (Input.GetButtonDown("Fire1"))
    	{
    		letter = 'A';
			// space4.enabled = false;
			//space4.sprite = aButton;
    	}
    	return letter;
    }

    //	Returns button letter mapped to 0-3 from sequence array
    char getSequenceButton()	{
		for (int i=0; i<3; i++) 
    	{
    		//showArrow(sequence[i]);
			Image currentImage = spaces[i];
			currentImage.enabled = false;

			currentImage.transform.rotation = Quaternion.Euler(0,0,0);
			//currentImage.sprite = directions[sequence[i]]; 
    	}
    	char letter = ' ';
    	switch (sequence[3]) 
    	{
    		case 0:
    		  letter = 'A';
			  space4.enabled = true;
			  space4.sprite = aButton;
    		  break;
    		case 1:
    		  letter = 'X';
			  space4.enabled = true;
			  space4.sprite = xButton;
    		  break;
    		case 2:
    		  letter = 'Y';
			  space4.enabled = true;
			  space4.sprite = yButton;
    		  break;
    	}

//	=============================
//	FOR TESTING - DELETE
if (buttonTimer < 5) 
{
Debug.Log("now press " + letter);
}
//	=============================
    	return letter;
    }

    // Traverses arrow portion of current sequence
    void displayNext()	{
    Debug.Log("new sequence");
    	readyForNextSeq = false;
    	currentArrow = 0;

    	for (int i = 0; i < ARROWS; i++) 
    	{
    		sequence[i] = Random.Range(0, 4);
    	}
    	sequence[ARROWS] = Random.Range(0, 3);

    	//	This is just Debug Console shit;
    	//	Basically, at this point display the arrow sprites on the screen
    	for (int i=0; i<3; i++) 
    	{
    		showArrow(sequence[i]);
			Image currentImage = spaces[i];

			currentImage.transform.Rotate(0,0,(-90* sequence[i]));
			currentImage.enabled = true;
			//currentImage.sprite = directions[sequence[i]]; 
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameShoveling : MonoBehaviour
{
	//	GLOBAL VARIABLES
	//	===================================================
	//	How many frames it takes to dig or dump a shovelful
		public float MOVETIME = 15;

	//	Rate at which the reactor cools when opened
		public float COOLINGRATE = 0.02f;

	//	Energy added by one shovelful of good uranium
		public float GOOD_U = 10.0f;

	//	Energy reduced by one shovelful of bad uranium
		public float BAD_U = 20.0f;

	//	Percentage of bad uranium in the pile
		public int U_RATIO = 30;
	//	===================================================
	//	IMPORTED VALUE - REPLACE
	//	Current furnace level at start of task
		public float furnaceTemp = 50.0f;
	//	===================================================

	//	Constants for the charPosition variable
	static int START = 0;
	static int DIG = 1;
	static int TURN = 2;
	static int TOSS = 3;
	static int ADD = 4;

	int charPosition = START;
	int digTime = 0;
	bool goodUranium = true;
	public bool shovelGameActive = false;

	//object references
	public SpriteRenderer reactorSprite;	
	public SpriteRenderer shovelSprite;
	public bool inReactor = false;

	//public Sprite reactorClosed;
	//public Sprite reactorOpen;

	public GameObject uraniumPile;
	public GameObject reactor;
	public ReactorControl reactorController;
	public PlayerMovement player;
	public GameObject goodSprite;
	public GameObject badSprite;
	public Slider tempGauge;



	//reference UI shit
	public GameObject pressLB;
	public GameObject pressUse;
	public GameObject openReactor;

	public JuicyInteract juicyInteract;

	private Vector3 pressLBSize;
	private Vector3 pressUseSize;
	private Vector3 openReactorSize;

	private Vector3 closed;
	

    void Start()
    {
		//get the original scales of the UI
		pressLBSize = pressLB.transform.localScale;
		pressUseSize = pressUse.transform.localScale;
		openReactorSize = openReactor.transform.localScale;


		closed = new Vector3(0,0,0);

		pressLB.SetActive(false);
		pressUse.SetActive(false);
		openReactor.SetActive(false);

		pressLB.transform.localScale = closed;
		pressUse.transform.localScale = closed;
		openReactor.transform.localScale = closed;
        //Debug.Log("running");
    }

    void Update()
    {
		if (furnaceTemp <= 0) 
		{
	//	LOSE STATE
			furnaceTemp = 0;
			Debug.Log("furnace went out");
			return;
		}

        switch (charPosition) 
        {
        	//	From the START state, wait for a button push to start digging
        	case 0:
			openReactor.SetActive(false);
        	  listenToDig();
			  
        	  break;

        	//	From the DIG state, wait before moving to the next state
        	case 1:
        	  digTime++;
        	  if (digTime >= MOVETIME) 
        	  {
    	  		charPosition = TURN;
    	  		digTime = 0;
        	  }
        	  break;

        	//	Once the shovel is full, wait for another button push
        	case 2:
				
        	  listenToShovel();
        	  break;

        	//	After emptying shovel, wait before returning to START state
        	case 3:
        	  digTime++;
        	  if (digTime >= MOVETIME) 
        	  {
        	  	charPosition = START;
        	  	digTime = 0;
        	  }
        	  break;
        	case 4:
        	  digTime++;
        	  if (digTime >= MOVETIME) 
        	  {
        	  	charPosition = START;
        	  	digTime = 0;
        	  }
        	  break;
        }

		tempGauge.value = furnaceTemp;
    }

    //	Furnace loses 1% per minute while open throughout game
    void FixedUpdate()	{
    	furnaceTemp -= COOLINGRATE;
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			inReactor = true;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			inReactor = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			inReactor = false;
			shovelSprite.enabled = true;
			if(pressLB.activeSelf)
			{
				juicyInteract.StartCoroutine(juicyInteract.UIPop(pressLB.transform.localScale, closed, pressLB));
				pressLB.SetActive(false);
			}
			
		}
	}
    void listenToDig()	{
    	//	On button press, change position and fill shovel
		if(inReactor)
		{
			if(shovelGameActive == false)
			{
				if(pressLB.activeSelf == false)
				{
					pressLB.SetActive(true);
					juicyInteract.StartCoroutine(juicyInteract.UIPop(pressLB.transform.localScale, pressLBSize, pressLB));
				
				}
				
			}
			if (Input.GetButtonDown("LB"))
        	{
				if(shovelGameActive == false)
				{
					shovelGameActive = true;
				}
				juicyInteract.StartCoroutine(juicyInteract.UIPop(pressLB.transform.localScale, closed, pressLB));
				pressLB.SetActive(false);
				
				pressUse.SetActive(true);
				juicyInteract.StartCoroutine(juicyInteract.UIPop(pressLB.transform.localScale, pressUseSize, pressUse));
				charPosition = DIG;
				shovelSprite.enabled=false;
				player.canMove = false;
				player.animator.SetBool("Shoveling", true);
				player.animator.SetTrigger("Dig");


				//	Determine whether uranium is any good
				goodUranium = (Random.Range(0, 100) > U_RATIO);

				Debug.Log("shovelful of U; good stuff: " + goodUranium);
				if(goodUranium)
				{
					goodSprite.SetActive(true);

				}

				else
				{
					badSprite.SetActive(true);
				}
        	}
		}
        
    }

    void listenToShovel()	{

		
    	//	If uranium is put in furnace, change position and empty shovel
    	if (Input.GetButtonDown("Fire3")) 
    	{
			if(reactorController.open)
			{
				charPosition = ADD;

    		//	Good uranium raises temperature, bad lowers it
    		furnaceTemp = goodUranium
    			? furnaceTemp + GOOD_U
    			: furnaceTemp - BAD_U;
				player.animator.SetTrigger("Throw");
				if(goodSprite.activeSelf== true)
				{
					goodSprite.SetActive(false);
				}
				else if(badSprite.activeSelf == true)
				{
					badSprite.SetActive(false);
				}
				juicyInteract.StartCoroutine(juicyInteract.UIPop(pressUse.transform.localScale, closed, pressUse));
				pressUse.SetActive(false);

			}

			else
			{
				Debug.Log("Reactor is closed!");
				juicyInteract.StartCoroutine(juicyInteract.UIPop(openReactor.transform.localScale, openReactorSize, openReactor));
				openReactor.SetActive(true);
			}

    		
		}
    		
		//	Check for goal temperature
    	if (furnaceTemp >= 100)
    	{
    	//	WIN STATE
    		furnaceTemp = 100;
    		Debug.Log("furnace is full");
    		return;
    	}
    	//Debug.Log("furnace temp " + furnaceTemp);
    		
    	//	If uranium is tossed aside, change position and empty shovel
    	if (Input.GetButtonDown("Fire2"))
    	{
    		charPosition = TOSS;
    		Debug.Log("tossed aside");
			shovelGameActive = false;
			player.canMove = true;
			player.animator.SetBool("Shoveling", false);
			player.animator.SetTrigger("Throw");
			if(goodSprite.activeSelf== true)
				{
					goodSprite.SetActive(false);
				}
				else if(badSprite.activeSelf == true)
				{
					badSprite.SetActive(false);
				}
			
			pressUse.SetActive(false);
			if(openReactor.activeSelf)
			{
				juicyInteract.StartCoroutine(juicyInteract.UIPop(openReactor.transform.localScale, closed, openReactor));
				openReactor.SetActive(false);
			}
			
    	}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameShoveling : MonoBehaviour
{
	//	GLOBAL VARIABLES
	//	==============================================
	//	How many frames it takes to dig a shovelful
		public float DIGTIME = 10;

	//	Rate at which the reactor cools when opened
		public float COOLINGRATE = 0.02f;

	//	Energy added by one shovelful of good uranium
		public float GOOD_U = 10.0f;

	//	Energy reduced by one shovelful of bad uranium
		public float BAD_U = 20.0f;

	//	Percentage of bad uranium in the pile
		public int U_RATIO = 30;
	//	==============================================
	//	IMPORTED VALUE - REPLACE
	//	Current furnace level at start of task
		public float furnaceTemp = 50.0f;
	//	==============================================

	static int START = 0;
	static int DIG = 1;
	static int TURN = 2;
	static int TOSS = 3;
	static int ADD = 4;

	int charPosition = START;
	int digTime = 0;
	bool shovelFull = false;
	bool goodUranium = true;

    void Start()
    {
        Debug.Log("running");

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
        	case 0:
        	  listenToDig();
        	  break;
        	case 1:
        	  digTime++;
        	  if (digTime >= DIGTIME) 
        	  {
    	  		charPosition = TURN;
    	  		digTime = 0;
        	  }
        	  break;
        	case 2:
        	  listenToShovel();
        	  break;
        	case 3:
        	  digTime++;
        	  if (digTime >= DIGTIME) 
        	  {
        	  	charPosition = START;
        	  	digTime = 0;
        	  }
        	  break;
        	case 4:
        	  digTime++;
        	  if (digTime >= DIGTIME) 
        	  {
        	  	charPosition = START;
        	  	digTime = 0;
        	  }
        	  break;
        }
    }

    void FixedUpdate()	{
    	furnaceTemp -= COOLINGRATE;
    }

    void listenToDig()	{
        if (Input.GetButtonDown("LB")) 
        {
        	charPosition = DIG;
        	shovelFull = true;
        	goodUranium = (Random.Range(0, 100) > U_RATIO);

        	Debug.Log("shovelful of U; good stuff: " + goodUranium);
        }
    }

    void listenToShovel()	{
    	if (Input.GetButtonDown("LT")) 
    	{
    		charPosition = ADD;
    		shovelFull = false;
    		furnaceTemp = goodUranium
    			? furnaceTemp + GOOD_U
    			: furnaceTemp - BAD_U;
    		if (furnaceTemp >= 100)
    		{
    //	WIN STATE
    			furnaceTemp = 100;
    			Debug.Log("furnace is full");
    			return;
    		}
    		Debug.Log("furnace temp " + furnaceTemp);
    	}

    	if (Input.GetButtonDown("RT")) 
    	{
    		charPosition = TOSS;
    		shovelFull = false;
    		Debug.Log("tossed aside");
    	}
    }
}

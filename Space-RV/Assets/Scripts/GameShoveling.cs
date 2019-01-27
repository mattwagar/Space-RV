using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameShoveling : MonoBehaviour
{
	//	GLOBAL VARIABLES
	//	==============================================
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

	// private bool 

    void Start()
    {
        Debug.Log("running");
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump")) 
        {
        	Debug.Log("Jump");
        }
        if (Input.GetButtonDown("LB")) 
        {
        	Debug.Log("LB");
        }
        if (Input.GetButtonDown("RB")) 
        {
        	Debug.Log("RB");
        }
        if (Input.GetButtonDown("LT")) 
        {
        	Debug.Log("LT");
        }
        if (Input.GetButtonDown("RT")) 
        {
        	Debug.Log("RT");
        }
        if (Input.GetButtonDown("extra5")) 
        {
        	Debug.Log("extra5");
        }
        if (Input.GetButtonDown("extra6")) 
        {
        	Debug.Log("extra6");
        }
    }

    void FixedUpdate()	{
    	furnaceTemp -= COOLINGRATE;
    }
}

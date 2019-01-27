using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameShoveling : MonoBehaviour
{
	//	GLOBAL VARIABLES
	//	==============================================
	//	Rate at which the reactor cools when opened
		public static float COOLINGRATE = 0.02f;

	//	Energy added by one shovelful of good uranium
		public static float GOOD_U = 10.0f;

	//	Energy reduced by one shovelful of bad uranium
		public static float BAD_U = 20.0f;

	//	Percentage of bad uranium in the pile
		public static int U_RATIO = 30;
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
        	Debug.Log("pressed");
        }
    }

    void FixedUpdate()	{
    	furnaceTemp -= COOLINGRATE;
    }
}

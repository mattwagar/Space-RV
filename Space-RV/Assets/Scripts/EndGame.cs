using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class EndGame : MonoBehaviour
{
	//	=================================
	//	Distance from destination, in 50ths of seconds
		public int timeBeforeFinish = 6000;

	//	Win and lose states
		public bool WIN_GAME = false;
		public bool LOSE_GAME = false;
	//	=================================

	public GameSequence kids;
	public GameSteering nav;
	public GameShoveling furnace;

    void FixedUpdate()	{
    	timeBeforeFinish--;
    }

    void Update()
    {
        if (timeBeforeFinish <= 0) 
        {
        	WIN_GAME = true;
        }

        if (kids.HAPPINESS <= 0
        	|| Math.Abs(nav.needlePos) > nav.WIDTH
        	|| furnace.furnaceTemp <= 0
        	) 
        {
        	LOSE_GAME = true;
        }
    }
}

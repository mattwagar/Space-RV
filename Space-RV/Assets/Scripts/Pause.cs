using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
     bool paused = false;
     public GameObject pause;
     public GameSequence gameSequence;
     public GameSteering gameSteering;
     public GameShoveling gameShoveling;
    

    void Start()
    {
        pause.SetActive(false);
    }
 
     void Update()
     {
         if(Input.GetButtonDown("pauseButton"))
             paused = togglePause();

        if(paused)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                paused = togglePause();
            }

            if(Input.GetButtonDown("Fire2"))
            {
                Application.Quit();
            }
        }
     }
     
    /*  void OnGUI()
     {
         if(paused)
         {
             GUILayout.Label("Game is paused!");
             if(GUILayout.Button("Click me to unpause"))
                 paused = togglePause();
         }
     }*/
     
     bool togglePause()
     {
         if(Time.timeScale == 0f)
         {
             Time.timeScale = 1f;
             pause.SetActive(false);
             gameSequence.enabled = true;
             gameSteering.enabled = true;
             gameShoveling.enabled = true;
             return(false);
         }
         else
         {
             Time.timeScale = 0f;
             pause.SetActive(true);
             gameSequence.enabled = false;
             gameSteering.enabled = false;
             gameShoveling.enabled = false;
             return(true);    
         }
     }
 }


﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public GameObject elevatorObj;
    private ElevatorController elevator;
    public SpriteRenderer sprite;
    public Animator animator;
    private int originalLayer;


    public float runSpeed = 40f;
    public bool canCallElevator = false;
    int floor = 0;

    float horizontalMove = 0f;

    bool jump = false;
    bool moving = false;
    bool canRide = false;
    bool dash = false;

    void Start(){
        originalLayer = sprite.sortingOrder;
        Debug.Log("Layer" + originalLayer);
        elevator = elevatorObj.GetComponent<ElevatorController>();

    }



    // Update is called once per frame
    void Update()
    {
        Movement();
        if(canCallElevator)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Calling Elevator");
                elevator.CallElevator(floor);
                canCallElevator = false;
            }
        }

        if(canRide)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                
                //Debug.Log("Calling Elevator");
                elevator.Elevator(floor);
                Debug.Log("Riding Elevator");
            }
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
        jump = false;
        dash = false;
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "FirstFloor")
        {
           // Debug.Log("Can Call Elevator to First Floor");
            floor = 1;
            canCallElevator = true;
        }

        else if(col.gameObject.tag == "SecondFloor")
        {
           // Debug.Log("Can call elevator to Second floor");
            floor = 2;
            canCallElevator = true;
        }
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "ElevatorTrigger")
        {
            canRide = true;
            canCallElevator = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "FirstFloor" || col.gameObject.tag == "SecondFloor")
        {
          //  Debug.Log("Left Elevator Shaft");
            floor = 0;
            canCallElevator = false;
        }

        else if(col.gameObject.tag == "ElevatorTrigger")
        {
            canRide = false;
        }
        
    }

    void Movement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
       // Debug.Log("horizontal: "+ horizontalMove);
       if(Mathf.Abs(horizontalMove) > 0){
           moving = true;
       }
       else{
           moving = false;
       }

       if(moving == true)
       {
           animator.SetBool("Run", true);
       }
       else
       {
           animator.SetBool("Run", false);
                  }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

       // if  (Input.GetButtonDown("Fire1"))
        //{
           // dash = true;
       // }
    }
}

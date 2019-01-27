using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public int currentFloor = 1;
    public Vector2 firstFloorPos;
    public GameObject player;
    private Vector2 playerPos;

    public GameObject secondFloorPos;
    public GameObject elevator;
    public float speed;

    public bool elevatorCalled = false;

    bool ridingElevator = false;
    bool elevatorArrived = false;

   // public bool elevatorCalled;
    // Start is called before the first frame update
    void Start()
    {
        firstFloorPos = transform.position;
    }

    void Update()
    {
      /// if(ridingElevator)
      // {
          // player.transform.position = elevator.transform.position;
       //}

    }
    public void CallElevator(int floor)
    {
        if(elevatorCalled == false)
        {
           // Debug.Log("Calling elevator to "+ floor+"floor");
            StartCoroutine("MoveElevator", floor);            
        }
        
        
    }

    public void Elevator(int floor)
    {
        StartCoroutine("RideElevator",floor);
    }
    IEnumerator MoveElevator(int floor)
    {
        if(elevatorCalled){
            yield break;
        }
        elevatorCalled = true;
       
        int destination = floor;

        float counter = 0;

    //Get the current position of the object to be moved
       // Vector3 startPos = fromPosition.position;

        while (counter < speed)
        {
            
            //on the same floor
            if(destination == currentFloor)
            {
               // Debug.Log("On the same floor as the elevator");
                yield break;              


                
                
           }

            //go to the second floor

            if(destination == 2 && currentFloor == 1)
            {
                counter += Time.deltaTime;
                elevator.transform.position = Vector2.Lerp(elevator.transform.position, secondFloorPos.transform.position,counter/speed);
               // Debug.Log("Arrived 2ND Floor");
                //currentFloor = 2;
                
                
            }

            //go to the first floor 
            else if(destination == 1 && currentFloor == 2)
            {
                counter += Time.deltaTime;
                elevator.transform.position = Vector2.Lerp(elevator.transform.position, firstFloorPos,counter/speed);
              //  Debug.Log("Arrived 1st Floor");
                //currentFloor = 1;
                

            }
            yield return null;
        }
        
        
        elevatorCalled = false;
        //StartCoroutine("Transport");
        
        
        
        
    }

    IEnumerator RideElevator(int floor)
    {
       // Debug.Log("Transporting");
        if(ridingElevator){
            yield break;
        }
        ridingElevator = true;

        float counter = 0;
        

        while (counter < speed)
        {
            counter += Time.deltaTime;
            //on the same floor
            
            //go to the first floor
            player.transform.SetParent(elevator.transform);
            if(floor == 2)
            {
                
                elevator.transform.position = Vector2.Lerp(elevator.transform.position, firstFloorPos,counter/speed);
               // Debug.Log("Arrived 1 Floor");
            }

            //go to the second floor 
            else if(floor == 1)
            {
                elevator.transform.position = Vector2.Lerp(elevator.transform.position, secondFloorPos.transform.position,counter/speed);
                //Debug.Log("Arrived 2 Floor");

            }
            yield return null;
        }

        ridingElevator = false;
        player.transform.parent = null;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private Vector2 closedPos;
    
    public Transform openPos;
    public GameObject door;
    public float speed = 0.2f;
    private bool open = false; 

    private Vector2 velocity = Vector2.zero;

    void Start(){
        closedPos = door.transform.position;
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
           // Debug.Log("Open");
            //open door 
            if(open == false)
            {
                door.transform.position = Vector2.Lerp(closedPos,openPos.position , speed * Time.deltaTime);
                open = true;
            }
                


        }
       
    }

    void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.tag == "Player")
        {
            if(open == true){
                door.transform.position = openPos.position;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
          // Debug.Log("Closed");
           //close door
           if(open == true)
           {
               door.transform.position = Vector2.Lerp(openPos.position,closedPos, speed);
               open = false;
               
           }

        }
    }

    
}

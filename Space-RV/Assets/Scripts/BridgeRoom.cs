using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeRoom : MonoBehaviour
{
    public bool canInteract = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            canInteract = true;
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
        }
    }
    
}

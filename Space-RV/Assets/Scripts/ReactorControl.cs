using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorControl : MonoBehaviour
{
    SpriteRenderer reactor;
    public Sprite reactorOpen;
    public Sprite reactorClosed;

    public bool open;
    // Start is called before the first frame update
    void Start()
    {
        reactor = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Input.GetButtonDown("Fire1"))
            {
                if(open)
                {
                    open = false;
                    reactor.sprite = reactorClosed;

                }

                else
                {
                    open = true;
                    reactor.sprite = reactorOpen;
                }
            }
        }
    }

  //  public void Interact()
   //
    //}
}

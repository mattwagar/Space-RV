using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorControl : MonoBehaviour
{
    SpriteRenderer reactor;
    public Sprite reactorOpen;
    public Sprite reactorClosed;
    public JuicyInteract uiPop;

    public GameObject button;

    private Vector3 ogSize;
    private Vector3 closed;

    public bool open;
    private bool canOpen;
    // Start is called before the first frame update
    void Start()
    {
        reactor = GetComponent<SpriteRenderer>();
        closed = new Vector3(0,0,0);
        ogSize = button.transform.localScale;
        button.transform.localScale = closed;
        button.SetActive(false);
    }  

    void Update()
    {
        if(canOpen == true)
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
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            canOpen = true;
            button.SetActive(true);
            uiPop.StartCoroutine(uiPop.UIPop(button.transform.localScale,ogSize,button));
        }
    }

    

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            canOpen = true;
        }
            
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            canOpen = false;
            uiPop.StartCoroutine(uiPop.UIPop(button.transform.localScale,closed,button));
            button.SetActive(false);
        }

    }

  //  public void Interact()
   //
    //}
}

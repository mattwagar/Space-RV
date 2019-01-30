using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeUI : MonoBehaviour
{
    //bool inBridge = false;
    // Start is called before the first frame update
    public JuicyInteract uiPop;

    public GameObject pilotText;
    private Vector3 closed;
    [HideInInspector]
    public Vector3 ogSize;


    void Start()
    {
        closed = new Vector3(0,0,0);
        ogSize = pilotText.transform.localScale;
        pilotText.transform.localScale = closed;
        pilotText.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
            pilotText.SetActive(true);
            uiPop.StartCoroutine(uiPop.UIPop(pilotText.transform.localScale,ogSize, pilotText));
			//inBridge = true;
		}
	}

    void OnTriggerExit2D(Collider2D other)
    {
       if(other.gameObject.tag == "Player")
		{
            
            uiPop.StartCoroutine(uiPop.UIPop(pilotText.transform.localScale,closed, pilotText));
            pilotText.SetActive(false);
			//inBridge = true;
		} 
    }
}

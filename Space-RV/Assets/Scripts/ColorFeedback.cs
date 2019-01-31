using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorFeedback : MonoBehaviour
{
    Image sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<Image>();
    }

    // Update is called once per frame
   

    public void Negative()
    {
        sprite.color = Color.white;
    }

    public void Positive()
    {
        sprite.color = Color.green;
    }
}

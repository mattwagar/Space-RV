using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuicyInteract : MonoBehaviour
{
    public float time = 0.5f;


    public  IEnumerator UIPop(Vector3 startScale, Vector3 endScale, GameObject target)
    {
        float currentTime = 0.0f;

        for(float t = 0; t < 1; t += Time.deltaTime/time)
        {
            
            target.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            currentTime += Time.deltaTime;
             yield return null;            
        }
       
       // Debug.Log("UI Scale");

        //return null;
    }
}

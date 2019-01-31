using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuicyInteract : MonoBehaviour
{
    public float time = 0.5f;
    public float cameraShakeDuration = 0.5f;
    public float cameraShakeAmount = 0.7f;
     Camera cam;
     Transform camTransform;
     
    void Start()
    {
        cam = Camera.main;
        camTransform = Camera.main.transform;
        
    }

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


    public  IEnumerator CameraShake()
    {// float currentTime = 0.0f;
        Vector3 originalPos = camTransform.transform.localPosition;

        for(float t = 0; t < 1; t += Time.deltaTime/cameraShakeDuration)
        {
            
          camTransform.localPosition = originalPos + Random.insideUnitSphere * cameraShakeAmount;
            yield return null;            
        }
       
       // Debug.Log("UI Scale");

        //return null;
    }

    public IEnumerator SpriteFlash(Image sprite)
    {
        float times = 5;
        for(int i = 0; i < times;i++)
        {
           sprite.color = Color.red;
           yield return new WaitForSeconds(0.05f);
           sprite.color = Color.white;
           yield return new WaitForSeconds(0.05f);


        }
    }
}

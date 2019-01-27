using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_to_main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        if (Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("Main");
            SceneManager.UnloadSceneAsync("Start");
        }
      
    }
}


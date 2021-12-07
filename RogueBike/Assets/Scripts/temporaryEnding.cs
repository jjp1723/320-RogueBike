using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class temporaryEnding : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("Escape key was pressed");
            AkSoundEngine.StopAll();
            AkSoundEngine.PostEvent("Set_State_end", gameObject);
            SceneManager.LoadScene(2);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{


    void Play()
    {
        SceneManager.LoadScene(1);
    }

    void Exit()
    {
        SceneManager.LoadScene(0);
    }

    void Quit()
    {
        Quit();
    }

    

}

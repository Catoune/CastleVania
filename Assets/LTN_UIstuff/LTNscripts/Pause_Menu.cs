using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{

    public bool pausedGame = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (pausedGame)
            {
                Resume();
            }

            else
            {
                Load_Pause();
            }
        }
    }


    public void Load_Pause()
    {
        SceneManager.LoadScene("Pause_Menu", LoadSceneMode.Additive);
        pausedGame = true;
    }

    public void Resume()
    {
        SceneManager.UnloadScene("Pause_Menu");
        pausedGame = false;
    }

    public void quitGame()
    {
        Application.Quit();
    }
}

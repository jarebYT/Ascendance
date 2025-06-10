using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseSettings : MonoBehaviour
{
    //la variable pause
    public static bool pause = false;

    public GameObject pauseMenuUi;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
            {
                Resume();
            }
            else
            {
                MakePause();
            }
        }
    }


    public void Resume(){
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1.0f;
        pause = false;
    }

    void MakePause(){
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        pause = true;

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

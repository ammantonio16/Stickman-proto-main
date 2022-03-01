using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject AreYouSure;
    
    public void Pause()
    {
        PausePanel.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        //Time.timeScale = 1f;
    }

    public void YouSure()
    {
        AreYouSure.SetActive(true);
        PausePanel.SetActive(false);
    }

    public void YouSureNo()
    {
        AreYouSure.SetActive(false);
        PausePanel.SetActive(true);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Title");
    }
}

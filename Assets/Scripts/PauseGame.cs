using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject menuSet; // 메뉴 UI

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGameTime();
            }
        }
    }

    public void PauseGameTime()
    {
        isPaused = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
        menuSet.SetActive(true);    //메뉴 UI 활성화
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        menuSet.SetActive(false);    //메뉴 UI 비활성화
    }
}


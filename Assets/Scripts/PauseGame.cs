using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject menuSet; // �޴� UI

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
        menuSet.SetActive(true);    // �޴� UI Ȱ��ȭ

        // Ŀ���� ���̰� �ϰ� ��� ����
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        menuSet.SetActive(false);    // �޴� UI ��Ȱ��ȭ

        // Ŀ���� ����� ���
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}



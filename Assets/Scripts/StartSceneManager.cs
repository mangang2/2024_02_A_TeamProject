using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    void Start()
    {
        // Time.timeScale�� 1�� �ʱ�ȭ (������ ���������� �帣����)
        Time.timeScale = 1;

        // AudioListener.pause�� false�� �ʱ�ȭ (������� ���������� ����ǵ���)
        AudioListener.pause = false;

        // Ŀ���� ��� �����ϰ� ���̵��� ���� (�ʿ� ��)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}


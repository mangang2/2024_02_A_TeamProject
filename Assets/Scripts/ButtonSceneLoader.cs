using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneLoader : MonoBehaviour
{
    // StartScene���� ��ȯ�ϴ� �޼���
    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}


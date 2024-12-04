using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneLoader : MonoBehaviour
{
    // StartScene으로 전환하는 메서드
    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}


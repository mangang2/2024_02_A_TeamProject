using UnityEngine;

public class Exit : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("���� ����!");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

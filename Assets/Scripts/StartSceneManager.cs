using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    void Start()
    {
        // Time.timeScale을 1로 초기화 (게임이 정상적으로 흐르도록)
        Time.timeScale = 1;

        // AudioListener.pause를 false로 초기화 (오디오가 정상적으로 재생되도록)
        AudioListener.pause = false;

        // 커서를 잠금 해제하고 보이도록 설정 (필요 시)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public UIManager uiManager;
    private bool hasClicked = false;  // 버튼이 눌렸는지 여부를 저장하는 변수

    public void SceneChanges()
    {
        if (hasClicked) return;  // 이미 버튼이 눌렸다면 아무 동작도 하지 않음

        hasClicked = true;  // 버튼이 눌렸음을 기록
        uiManager.FadeAndLoadScene("Main");  // 씬 전환 실행
    }
}


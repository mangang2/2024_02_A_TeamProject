using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public UIManager uiManager;
    private bool hasClicked = false;  // ��ư�� ���ȴ��� ���θ� �����ϴ� ����

    public void SceneChanges()
    {
        if (hasClicked) return;  // �̹� ��ư�� ���ȴٸ� �ƹ� ���۵� ���� ����

        hasClicked = true;  // ��ư�� �������� ���
        uiManager.FadeAndLoadScene("Main");  // �� ��ȯ ����
    }
}


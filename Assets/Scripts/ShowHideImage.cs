using UnityEngine;
using UnityEngine.UI;

public class ShowHideImage : MonoBehaviour
{
    public GameObject imageObject;  // ��Ÿ���ų� ���� �̹��� ������Ʈ

    public void ToggleImageOnClick()
    {
        if (imageObject != null)
        {
            // �̹����� Ȱ��ȭ ���¸� ���
            imageObject.SetActive(!imageObject.activeSelf);
        }
    }
}


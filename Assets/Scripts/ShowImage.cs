using UnityEngine;
using UnityEngine.UI;

public class ShowImage : MonoBehaviour
{
    public GameObject imageObject;  // ��Ÿ�� �̹��� ������Ʈ

    public void ShowImageOnClick()
    {
        // �̹��� Ȱ��ȭ
        if (imageObject != null)
        {
            imageObject.SetActive(true);
        }
    }
}

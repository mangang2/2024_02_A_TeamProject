using UnityEngine;
using UnityEngine.UI;

public class ShowImage : MonoBehaviour
{
    public GameObject imageObject;  // 나타낼 이미지 오브젝트

    public void ShowImageOnClick()
    {
        // 이미지 활성화
        if (imageObject != null)
        {
            imageObject.SetActive(true);
        }
    }
}

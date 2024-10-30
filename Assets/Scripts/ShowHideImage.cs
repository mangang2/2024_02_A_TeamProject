using UnityEngine;
using UnityEngine.UI;

public class ShowHideImage : MonoBehaviour
{
    public GameObject imageObject;  // 나타내거나 숨길 이미지 오브젝트

    public void ToggleImageOnClick()
    {
        if (imageObject != null)
        {
            // 이미지의 활성화 상태를 토글
            imageObject.SetActive(!imageObject.activeSelf);
        }
    }
}


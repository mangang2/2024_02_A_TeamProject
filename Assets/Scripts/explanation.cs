using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explanation : MonoBehaviour
{
    bool IDCardChk = false;
    public void clickIDCard()
    {
        RectTransform rectTran = gameObject.GetComponent<RectTransform>();
        GameObject obj = GameObject.Find("게임설명");
        Vector3 position = obj.transform.localPosition;
        if (IDCardChk == false)
        {
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1200);
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 700);
            position.x = 0;
            position.y = 0;
            obj.transform.localPosition = position;
            IDCardChk = true;
        }
        else
        {
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 470);
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300);
            position.x = -580;
            position.y = 10;
            obj.transform.localPosition = position;
            IDCardChk = false;
        }
    }
}

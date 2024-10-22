using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public UIManager uiManager;  
    public void SceneChanges()
    {
        uiManager.FadeAndLoadScene("Main");  
    }

}


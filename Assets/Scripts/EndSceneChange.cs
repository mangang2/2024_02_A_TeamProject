using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneChange : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("End Scene");
    }
 
}

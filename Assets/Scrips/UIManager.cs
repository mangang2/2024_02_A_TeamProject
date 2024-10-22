using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Image Panel;
    float time = 0f;
    float F_time = 1f;

    public void FadeAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeFlow(sceneName));  
    }

    IEnumerator FadeFlow(string sceneName)
    {
        Panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Panel.color;
       
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        
        SceneManager.LoadScene(sceneName);

        time = 0f;
       
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }

        Panel.gameObject.SetActive(false);
    }
}


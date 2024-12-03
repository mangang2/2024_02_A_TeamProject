using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    // 트리거 충돌이 발생했을 때 실행
    private void OnTriggerEnter(Collider other)
    {
        // "Player" 태그를 가진 오브젝트와 충돌했는지 확인
        if (other.CompareTag("Player"))
        {
            // "End Scene"으로 전환
            SceneManager.LoadScene("End Scene");
        }
    }
}



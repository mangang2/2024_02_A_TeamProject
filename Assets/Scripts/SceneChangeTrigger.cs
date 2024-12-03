using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    // Ʈ���� �浹�� �߻����� �� ����
    private void OnTriggerEnter(Collider other)
    {
        // "Player" �±׸� ���� ������Ʈ�� �浹�ߴ��� Ȯ��
        if (other.CompareTag("Player"))
        {
            // "End Scene"���� ��ȯ
            SceneManager.LoadScene("End Scene");
        }
    }
}



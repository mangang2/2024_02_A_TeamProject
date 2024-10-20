using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // �� ������ ���� �ʿ�

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject ButtonTeleport;  // ButtonTeleport ��ġ (������� ����)
    public GameObject TopTeleport;      // TopTeleport ��ġ
    public GameObject[] mapPrefabs;     // �������� ��ü�� �� �����յ�

    private int swapCount = 0;  // ��ü Ƚ���� �����ϴ� ���� �߰�

    private void OnTriggerEnter(Collider collision)
    {
        // ButtonTeleport �±װ� �ִ� ������Ʈ�� �浹 ��
        if (collision.gameObject.CompareTag("ButtonTeleport"))
        {
            // ���� Hierarchy���� Map ������Ʈ ã��
            GameObject currentMapObject = GameObject.FindWithTag("Map");
            GameObject currentFakeMapObject = GameObject.FindWithTag("FakeMap");

            if (currentMapObject != null) // �ٲ� �κ�
            {
                // �������� �� �������� ����
                int randomIndex = Random.Range(0, mapPrefabs.Length);
                GameObject randomPrefab = mapPrefabs[randomIndex];

                // ���� Map ������Ʈ ����
                Destroy(currentMapObject);

                // ���� �������� Map ��ġ�� ����
                Instantiate(randomPrefab, currentMapObject.transform.position, currentMapObject.transform.rotation);

                Debug.Log($"Map�� ���� ������ '{randomPrefab.name}'�� ��ü�Ǿ����ϴ�.");
            }
            else if (currentFakeMapObject != null) // �ٲ� �κ�
            {
               
                // FakeMap�� �ִ� ���, �ش� ������Ʈ�� ����
                Destroy(currentFakeMapObject);

                // ������ ó������ ���� (���� ���� �ٽ� �ε�)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            // ��ü Ƚ�� ���� �߰�
            swapCount++;

            // ��ü Ƚ���� 5�� �����ϸ� ���� ���� �߰�
            if (swapCount >= 5)
            {
                Debug.Log("���� ����!");
                Application.Quit();  // ���� ����
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;  // �����Ϳ��� ����
#endif
            }

            // �÷��̾ ButtonTeleport ��ġ�� �̵�
            Player.transform.position = ButtonTeleport.transform.position;
        }

        // TopTeleport �±װ� �ִ� ������Ʈ�� �浹 ��
        if (collision.gameObject.CompareTag("TopTeleport"))
        {
            // ���� Hierarchy���� Map ������Ʈ ã��
            GameObject currentMapObject = GameObject.FindWithTag("Map");
            GameObject currentFakeMapObject = GameObject.FindWithTag("FakeMap");

            if (currentMapObject != null) // �ٲ� �κ�
            {
                // ���� Map ������Ʈ ����
                Destroy(currentMapObject);

                // ������ ó������ ���� (���� ���� �ٽ� �ε�)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (currentFakeMapObject != null) // �ٲ� �κ�
            {
                // FakeMap�� �ִ� ���, �ش� ������Ʈ�� �����ϰ� �������� �� �� ����
                Destroy(currentFakeMapObject);

                int randomIndex = Random.Range(0, mapPrefabs.Length);
                GameObject randomPrefab = mapPrefabs[randomIndex];

                // ���� �������� FakeMap ��ġ�� ����
                Instantiate(randomPrefab, currentFakeMapObject.transform.position, currentFakeMapObject.transform.rotation);

                Debug.Log($"FakeMap�� ���� ������ '{randomPrefab.name}'�� ��ü�Ǿ����ϴ�.");
            }

            // �÷��̾ TopTeleport ��ġ�� �̵�
            Player.transform.position = TopTeleport.transform.position;

            // ��ü Ƚ�� ���� �߰�
            swapCount++;

            // ��ü Ƚ���� 5�� �����ϸ� ���� ���� �߰�
            if (swapCount >= 5)
            {
                Debug.Log("���� ����!");
                Application.Quit();  // ���� ����
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;  // �����Ϳ��� ����
#endif
            }


        }
    }
}



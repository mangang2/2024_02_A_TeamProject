using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject ButtonTeleport;  // ButtonTeleport ��ġ 
    public GameObject TopTeleport;      // TopTeleport ��ġ
    public GameObject[] mapPrefabs;     // �������� ��ü�� �� �����յ�

    private int swapCount = 0;  // ��ü Ƚ���� �����ϴ� ���� �߰�
    private int previousIndex = -1;  // ������ ���õ� ���� �ε����� �����ϴ� ����

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("ButtonTeleport"))
        {
            GameObject currentMapObject = GameObject.FindWithTag("Map");
            GameObject currentFakeMapObject = GameObject.FindWithTag("FakeMap");

            if (currentMapObject != null)
            {
                int randomIndex;

                // ���� �ε����� �ٸ� �ε����� ����
                do
                {
                    randomIndex = Random.Range(0, mapPrefabs.Length);
                } while (randomIndex == previousIndex);

                previousIndex = randomIndex;  // ���� �ε��� ������Ʈ
                GameObject randomPrefab = mapPrefabs[randomIndex];

                Destroy(currentMapObject);
                Instantiate(randomPrefab, currentMapObject.transform.position, currentMapObject.transform.rotation);

                Debug.Log($"Map�� ���� ������ '{randomPrefab.name}'�� ��ü�Ǿ����ϴ�.");
                swapCount++;

                if (swapCount >= 5)
                {
                    Debug.Log("���� ����!");
                    Application.Quit();
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                }
            }
            else if (currentFakeMapObject != null)
            {
                Destroy(currentFakeMapObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            Player.transform.position = ButtonTeleport.transform.position;
        }

        if (collision.gameObject.CompareTag("TopTeleport"))
        {
            GameObject currentMapObject = GameObject.FindWithTag("Map");
            GameObject currentFakeMapObject = GameObject.FindWithTag("FakeMap");

            if (currentMapObject != null)
            {
                Destroy(currentMapObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (currentFakeMapObject != null)
            {
                Destroy(currentFakeMapObject);

                int randomIndex;
                do
                {
                    randomIndex = Random.Range(0, mapPrefabs.Length);
                } while (randomIndex == previousIndex);

                previousIndex = randomIndex;  // ���� �ε��� ������Ʈ
                GameObject randomPrefab = mapPrefabs[randomIndex];

                Instantiate(randomPrefab, currentFakeMapObject.transform.position, currentFakeMapObject.transform.rotation);

                Debug.Log($"FakeMap�� ���� ������ '{randomPrefab.name}'�� ��ü�Ǿ����ϴ�.");
                swapCount++;

                if (swapCount >= 5)
                {
                    Debug.Log("���� ����!");
                    Application.Quit();
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                }
            }

            Player.transform.position = TopTeleport.transform.position;
        }
    }
}




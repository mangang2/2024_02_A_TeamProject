using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject ButtonTeleport;
    public GameObject TopTeleport;
    public GameObject[] mapPrefabs;

    private int swapCount = 0;
    private int previousIndex = -1;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("ButtonTeleport"))
        {
            GameObject currentMapObject = GameObject.FindWithTag("Map");
            GameObject currentFakeMapObject = GameObject.FindWithTag("FakeMap");

            if (currentMapObject != null)
            {
                int randomIndex;

                do
                {
                    randomIndex = Random.Range(0, mapPrefabs.Length);
                } while (randomIndex == previousIndex);

                previousIndex = randomIndex;
                GameObject randomPrefab = mapPrefabs[randomIndex];

                Destroy(currentMapObject);
                Instantiate(randomPrefab, currentMapObject.transform.position, currentMapObject.transform.rotation);

                Debug.Log($"Map�� ���� ������ '{randomPrefab.name}'�� ��ü�Ǿ����ϴ�.");
                swapCount++;

                if (swapCount >= 5)
                {
                    Debug.Log("���� ����! StartScene���� �̵��մϴ�.");
                    SceneManager.LoadScene("StartScene");
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

                previousIndex = randomIndex;
                GameObject randomPrefab = mapPrefabs[randomIndex];

                Instantiate(randomPrefab, currentFakeMapObject.transform.position, currentFakeMapObject.transform.rotation);

                Debug.Log($"FakeMap�� ���� ������ '{randomPrefab.name}'�� ��ü�Ǿ����ϴ�.");
                swapCount++;

                if (swapCount >= 5)
                {
                    Debug.Log("���� ����! StartScene���� �̵��մϴ�.");
                    SceneManager.LoadScene("StartScene");
                }
            }

            Player.transform.position = TopTeleport.transform.position;
        }
    }
}




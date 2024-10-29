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

                Debug.Log($"Map이 랜덤 프리팹 '{randomPrefab.name}'로 교체되었습니다.");
                swapCount++;

                if (swapCount >= 5)
                {
                    Debug.Log("게임 종료! StartScene으로 이동합니다.");
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

                Debug.Log($"FakeMap이 랜덤 프리팹 '{randomPrefab.name}'로 교체되었습니다.");
                swapCount++;

                if (swapCount >= 5)
                {
                    Debug.Log("게임 종료! StartScene으로 이동합니다.");
                    SceneManager.LoadScene("StartScene");
                }
            }

            Player.transform.position = TopTeleport.transform.position;
        }
    }
}




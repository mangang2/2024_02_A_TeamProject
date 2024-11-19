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
    public GameObject[] numberPrefabs;  // Number 오브젝트 배열 추가

    private int currentNumberIndex = 0;  // 순차적인 인덱스 관리 변수
    private int swapCount = 0;
    private int previousIndex = -1;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("ButtonTeleport"))
        {
            GameObject currentMapObject = GameObject.FindWithTag("Map");
            GameObject currentFakeMapObject = GameObject.FindWithTag("FakeMap");
            GameObject currentNumberObject = GameObject.FindWithTag("Number");

            // Map 교체
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

                // swapCount가 5에 도달하면 게임 종료
                if (swapCount >= 5)
                {
                    Debug.Log("게임 종료! StartScene으로 이동합니다.");

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    SceneManager.LoadScene("StartScene");
                }

                // swapCount 증가 시 Number 오브젝트 순차적 교체
                if (swapCount > 0 && swapCount <= numberPrefabs.Length)
                {
                    if (currentNumberObject != null)
                    {
                        GameObject newNumberPrefab = numberPrefabs[currentNumberIndex];

                        Destroy(currentNumberObject);  // 기존 Number 오브젝트 삭제
                        Instantiate(newNumberPrefab, currentNumberObject.transform.position, currentNumberObject.transform.rotation);  // 새로운 Number 오브젝트 인스턴스화

                        Debug.Log($"Number가 순차적으로 '{newNumberPrefab.name}'로 변경되었습니다.");

                        currentNumberIndex++;  // 순차적인 인덱스 증가

                        // 순차적으로 끝까지 갔다면 인덱스 리셋
                        if (currentNumberIndex >= numberPrefabs.Length)
                        {
                            currentNumberIndex = 0;  // 다시 처음부터 시작
                        }
                    }
                }
            }
            // FakeMap 교체
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
                swapCount = 0;
            }

            Player.transform.position = ButtonTeleport.transform.position;
        }

        if (collision.gameObject.CompareTag("TopTeleport"))
        {
            GameObject currentMapObject = GameObject.FindWithTag("Map");
            GameObject currentFakeMapObject = GameObject.FindWithTag("FakeMap");
            GameObject currentNumberObject = GameObject.FindWithTag("Number");

            // Map 교체
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
                swapCount = 0;
            }
            // FakeMap 교체
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

                // swapCount가 5에 도달하면 게임 종료
                if (swapCount >= 5)
                {
                    Debug.Log("게임 종료! StartScene으로 이동합니다.");

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    SceneManager.LoadScene("StartScene");
                }

                // swapCount 증가 시 Number 오브젝트 순차적 교체
                if (swapCount > 0 && swapCount <= numberPrefabs.Length)
                {
                    if (currentNumberObject != null)
                    {
                        GameObject newNumberPrefab = numberPrefabs[currentNumberIndex];

                        Destroy(currentNumberObject);  // 기존 Number 오브젝트 삭제
                        Instantiate(newNumberPrefab, currentNumberObject.transform.position, currentNumberObject.transform.rotation);  // 새로운 Number 오브젝트 인스턴스화

                        Debug.Log($"Number가 순차적으로 '{newNumberPrefab.name}'로 변경되었습니다.");

                        currentNumberIndex++;  // 순차적인 인덱스 증가

                        // 순차적으로 끝까지 갔다면 인덱스 리셋
                        if (currentNumberIndex >= numberPrefabs.Length)
                        {
                            currentNumberIndex = 0;  // 다시 처음부터 시작
                        }
                    }
                }
            }

            Player.transform.position = TopTeleport.transform.position;
        }
    }
}


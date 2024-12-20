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
    public GameObject[] numberPrefabs;  // Number 오브젝트 배열
    public GameObject resetPrefab;     // swapCount가 0일 때 생성할 프리팹
    public GameObject endPrefab;

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

                // swapCount가 증가하면 Number 오브젝트 교체
                if (swapCount > 0 && swapCount <= numberPrefabs.Length)
                {
                    if (currentNumberObject != null)
                    {
                        GameObject newNumberPrefab = numberPrefabs[currentNumberIndex];

                        Destroy(currentNumberObject);  // 기존 Number 오브젝트 삭제
                        Instantiate(newNumberPrefab, currentNumberObject.transform.position, currentNumberObject.transform.rotation);  // 새로운 Number 오브젝트 생성

                        Debug.Log($"Number가 순차적으로 '{newNumberPrefab.name}'로 변경되었습니다.");

                        currentNumberIndex++;  // 순차적인 인덱스 증가

                        if (currentNumberIndex >= numberPrefabs.Length)
                        {
                            currentNumberIndex = 0;  // 다시 처음부터 시작
                        }
                    }
                }

                // swapCount가 6에 도달하면 게임 종료
                if (swapCount >= 6)
                {
                    Debug.Log("swapCount가 6에 도달했습니다. Map 또는 FakeMap을 삭제하고 특정 프리팹을 생성합니다.");

                    // 0.5초 대기 후 실행될 코루틴 호출
                    StartCoroutine(HandleGameEndWithDelay(0.5f));
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

                // swapCount가 0이 될 때 특정 프리팹 생성
                if (currentNumberObject != null)
                {
                    Vector3 position = currentNumberObject.transform.position;
                    Quaternion rotation = currentNumberObject.transform.rotation;

                    Destroy(currentNumberObject);  // 기존 Number 오브젝트 삭제
                    Instantiate(resetPrefab, position, rotation);  // 특정 프리팹 생성

                    Debug.Log($"swapCount가 0으로 초기화되어 '{resetPrefab.name}' 프리팹이 생성되었습니다.");

                    currentNumberIndex = 0;  // 순차적인 프리팹도 처음부터 시작
                }
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

                // swapCount가 0이 될 때 특정 프리팹 생성
                if (currentNumberObject != null)
                {
                    Vector3 position = currentNumberObject.transform.position;
                    Quaternion rotation = currentNumberObject.transform.rotation;

                    Destroy(currentNumberObject);  // 기존 Number 오브젝트 삭제
                    Instantiate(resetPrefab, position, rotation);  // 특정 프리팹 생성

                    Debug.Log($"swapCount가 0으로 초기화되어 '{resetPrefab.name}' 프리팹이 생성되었습니다.");

                    currentNumberIndex = 0;  // 순차적인 프리팹도 처음부터 시작
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
                swapCount++;

                // swapCount가 증가하면 Number 오브젝트 교체
                if (swapCount > 0 && swapCount <= numberPrefabs.Length)
                {
                    if (currentNumberObject != null)
                    {
                        GameObject newNumberPrefab = numberPrefabs[currentNumberIndex];

                        Destroy(currentNumberObject);  // 기존 Number 오브젝트 삭제
                        Instantiate(newNumberPrefab, currentNumberObject.transform.position, currentNumberObject.transform.rotation);  // 새로운 Number 오브젝트 생성

                        Debug.Log($"Number가 순차적으로 '{newNumberPrefab.name}'로 변경되었습니다.");

                        currentNumberIndex++;  // 순차적인 인덱스 증가

                        if (currentNumberIndex >= numberPrefabs.Length)
                        {
                            currentNumberIndex = 0;  // 다시 처음부터 시작
                        }
                    }
                }

                // swapCount가 6에 도달하면 게임 종료
                if (swapCount >= 6)
                {
                    Debug.Log("swapCount가 6에 도달했습니다. Map 또는 FakeMap을 삭제하고 특정 프리팹을 생성합니다.");

                    // 0.5초 대기 후 실행될 코루틴 호출
                    StartCoroutine(HandleGameEndWithDelay(0.5f));
                }
            }

            Player.transform.position = TopTeleport.transform.position;
        }
    }

    // 코루틴 메서드
    private IEnumerator HandleGameEndWithDelay(float delay)
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(delay);

        GameObject currentMapObject = GameObject.FindWithTag("Map");
        GameObject currentFakeMapObject = GameObject.FindWithTag("FakeMap");

        // Map 객체가 있을 경우 삭제하고 endPrefab 생성
        if (currentMapObject != null)
        {
            Vector3 position = currentMapObject.transform.position;
            Quaternion rotation = currentMapObject.transform.rotation;

            Destroy(currentMapObject);
            Instantiate(endPrefab, position, rotation);

            Debug.Log($"Map이 삭제되고 '{endPrefab.name}' 프리팹이 생성되었습니다.");
        }
        // FakeMap 객체가 있을 경우 삭제하고 endPrefab 생성
        else if (currentFakeMapObject != null)
        {
            Vector3 position = currentFakeMapObject.transform.position;
            Quaternion rotation = currentFakeMapObject.transform.rotation;

            Destroy(currentFakeMapObject);
            Instantiate(endPrefab, position, rotation);

            Debug.Log($"FakeMap이 삭제되고 '{endPrefab.name}' 프리팹이 생성되었습니다.");
        }
    }
}

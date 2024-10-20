using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // 씬 리셋을 위해 필요

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject ButtonTeleport;  // ButtonTeleport 위치 (사용하지 않음)
    public GameObject TopTeleport;      // TopTeleport 위치
    public GameObject[] mapPrefabs;     // 랜덤으로 교체할 맵 프리팹들

    private int swapCount = 0;  // 교체 횟수를 저장하는 변수 추가

    private void OnTriggerEnter(Collider collision)
    {
        // ButtonTeleport 태그가 있는 오브젝트와 충돌 시
        if (collision.gameObject.CompareTag("ButtonTeleport"))
        {
            // 현재 Hierarchy에서 Map 오브젝트 찾기
            GameObject currentMapObject = GameObject.FindWithTag("Map");
            GameObject currentFakeMapObject = GameObject.FindWithTag("FakeMap");

            if (currentMapObject != null) // 바뀐 부분
            {
                // 랜덤으로 맵 프리팹을 선택
                int randomIndex = Random.Range(0, mapPrefabs.Length);
                GameObject randomPrefab = mapPrefabs[randomIndex];

                // 기존 Map 오브젝트 삭제
                Destroy(currentMapObject);

                // 랜덤 프리팹을 Map 위치에 생성
                Instantiate(randomPrefab, currentMapObject.transform.position, currentMapObject.transform.rotation);

                Debug.Log($"Map이 랜덤 프리팹 '{randomPrefab.name}'로 교체되었습니다.");
            }
            else if (currentFakeMapObject != null) // 바뀐 부분
            {
               
                // FakeMap이 있는 경우, 해당 오브젝트를 삭제
                Destroy(currentFakeMapObject);

                // 게임을 처음부터 시작 (현재 씬을 다시 로드)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            // 교체 횟수 증가 추가
            swapCount++;

            // 교체 횟수가 5에 도달하면 게임 종료 추가
            if (swapCount >= 5)
            {
                Debug.Log("게임 종료!");
                Application.Quit();  // 게임 종료
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;  // 에디터에서 종료
#endif
            }

            // 플레이어를 ButtonTeleport 위치로 이동
            Player.transform.position = ButtonTeleport.transform.position;
        }

        // TopTeleport 태그가 있는 오브젝트와 충돌 시
        if (collision.gameObject.CompareTag("TopTeleport"))
        {
            // 현재 Hierarchy에서 Map 오브젝트 찾기
            GameObject currentMapObject = GameObject.FindWithTag("Map");
            GameObject currentFakeMapObject = GameObject.FindWithTag("FakeMap");

            if (currentMapObject != null) // 바뀐 부분
            {
                // 기존 Map 오브젝트 삭제
                Destroy(currentMapObject);

                // 게임을 처음부터 시작 (현재 씬을 다시 로드)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (currentFakeMapObject != null) // 바뀐 부분
            {
                // FakeMap이 있는 경우, 해당 오브젝트를 삭제하고 랜덤으로 새 맵 생성
                Destroy(currentFakeMapObject);

                int randomIndex = Random.Range(0, mapPrefabs.Length);
                GameObject randomPrefab = mapPrefabs[randomIndex];

                // 랜덤 프리팹을 FakeMap 위치에 생성
                Instantiate(randomPrefab, currentFakeMapObject.transform.position, currentFakeMapObject.transform.rotation);

                Debug.Log($"FakeMap이 랜덤 프리팹 '{randomPrefab.name}'로 교체되었습니다.");
            }

            // 플레이어를 TopTeleport 위치로 이동
            Player.transform.position = TopTeleport.transform.position;

            // 교체 횟수 증가 추가
            swapCount++;

            // 교체 횟수가 5에 도달하면 게임 종료 추가
            if (swapCount >= 5)
            {
                Debug.Log("게임 종료!");
                Application.Quit();  // 게임 종료
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;  // 에디터에서 종료
#endif
            }


        }
    }
}



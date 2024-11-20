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
    public GameObject[] numberPrefabs;  // Number ������Ʈ �迭
    public GameObject resetPrefab;     // swapCount�� 0�� �� ������ ������

    private int currentNumberIndex = 0;  // �������� �ε��� ���� ����
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

            // Map ��ü
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

                // swapCount�� �����ϸ� Number ������Ʈ ��ü
                if (swapCount > 0 && swapCount <= numberPrefabs.Length)
                {
                    if (currentNumberObject != null)
                    {
                        GameObject newNumberPrefab = numberPrefabs[currentNumberIndex];

                        Destroy(currentNumberObject);  // ���� Number ������Ʈ ����
                        Instantiate(newNumberPrefab, currentNumberObject.transform.position, currentNumberObject.transform.rotation);  // ���ο� Number ������Ʈ ����

                        Debug.Log($"Number�� ���������� '{newNumberPrefab.name}'�� ����Ǿ����ϴ�.");

                        currentNumberIndex++;  // �������� �ε��� ����

                        if (currentNumberIndex >= numberPrefabs.Length)
                        {
                            currentNumberIndex = 0;  // �ٽ� ó������ ����
                        }
                    }
                }

                // swapCount�� 6�� �����ϸ� ���� ����
                if (swapCount >= 6)
                {
                    Debug.Log("���� ����! StartScene���� �̵��մϴ�.");

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    SceneManager.LoadScene("StartScene");
                }
            }
            // FakeMap ��ü
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
                swapCount = 0;

                // swapCount�� 0�� �� �� Ư�� ������ ����
                if (currentNumberObject != null)
                {
                    Vector3 position = currentNumberObject.transform.position;
                    Quaternion rotation = currentNumberObject.transform.rotation;

                    Destroy(currentNumberObject);  // ���� Number ������Ʈ ����
                    Instantiate(resetPrefab, position, rotation);  // Ư�� ������ ����

                    Debug.Log($"swapCount�� 0���� �ʱ�ȭ�Ǿ� '{resetPrefab.name}' �������� �����Ǿ����ϴ�.");

                    currentNumberIndex = 0;  // �������� �����յ� ó������ ����
                }
            }

            Player.transform.position = ButtonTeleport.transform.position;
        }

        if (collision.gameObject.CompareTag("TopTeleport"))
        {
            GameObject currentMapObject = GameObject.FindWithTag("Map");
            GameObject currentFakeMapObject = GameObject.FindWithTag("FakeMap");
            GameObject currentNumberObject = GameObject.FindWithTag("Number");

            // Map ��ü
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
                swapCount = 0;

                // swapCount�� 0�� �� �� Ư�� ������ ����
                if (currentNumberObject != null)
                {
                    Vector3 position = currentNumberObject.transform.position;
                    Quaternion rotation = currentNumberObject.transform.rotation;

                    Destroy(currentNumberObject);  // ���� Number ������Ʈ ����
                    Instantiate(resetPrefab, position, rotation);  // Ư�� ������ ����

                    Debug.Log($"swapCount�� 0���� �ʱ�ȭ�Ǿ� '{resetPrefab.name}' �������� �����Ǿ����ϴ�.");

                    currentNumberIndex = 0;  // �������� �����յ� ó������ ����
                }
            }
            // FakeMap ��ü
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

                // swapCount�� �����ϸ� Number ������Ʈ ��ü
                if (swapCount > 0 && swapCount <= numberPrefabs.Length)
                {
                    if (currentNumberObject != null)
                    {
                        GameObject newNumberPrefab = numberPrefabs[currentNumberIndex];

                        Destroy(currentNumberObject);  // ���� Number ������Ʈ ����
                        Instantiate(newNumberPrefab, currentNumberObject.transform.position, currentNumberObject.transform.rotation);  // ���ο� Number ������Ʈ ����

                        Debug.Log($"Number�� ���������� '{newNumberPrefab.name}'�� ����Ǿ����ϴ�.");

                        currentNumberIndex++;  // �������� �ε��� ����

                        if (currentNumberIndex >= numberPrefabs.Length)
                        {
                            currentNumberIndex = 0;  // �ٽ� ó������ ����
                        }
                    }
                }

                // swapCount�� 6�� �����ϸ� ���� ����
                if (swapCount >= 6)
                {
                    Debug.Log("���� ����! StartScene���� �̵��մϴ�.");

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    SceneManager.LoadScene("StartScene");
                }
            }

            Player.transform.position = TopTeleport.transform.position;
        }
    }
}


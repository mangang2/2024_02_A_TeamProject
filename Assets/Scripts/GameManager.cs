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
    public GameObject[] numberPrefabs;  // Number ������Ʈ �迭 �߰�

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

                // swapCount�� 5�� �����ϸ� ���� ����
                if (swapCount >= 5)
                {
                    Debug.Log("���� ����! StartScene���� �̵��մϴ�.");

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    SceneManager.LoadScene("StartScene");
                }

                // swapCount ���� �� Number ������Ʈ ������ ��ü
                if (swapCount > 0 && swapCount <= numberPrefabs.Length)
                {
                    if (currentNumberObject != null)
                    {
                        GameObject newNumberPrefab = numberPrefabs[currentNumberIndex];

                        Destroy(currentNumberObject);  // ���� Number ������Ʈ ����
                        Instantiate(newNumberPrefab, currentNumberObject.transform.position, currentNumberObject.transform.rotation);  // ���ο� Number ������Ʈ �ν��Ͻ�ȭ

                        Debug.Log($"Number�� ���������� '{newNumberPrefab.name}'�� ����Ǿ����ϴ�.");

                        currentNumberIndex++;  // �������� �ε��� ����

                        // ���������� ������ ���ٸ� �ε��� ����
                        if (currentNumberIndex >= numberPrefabs.Length)
                        {
                            currentNumberIndex = 0;  // �ٽ� ó������ ����
                        }
                    }
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

                // swapCount�� 5�� �����ϸ� ���� ����
                if (swapCount >= 5)
                {
                    Debug.Log("���� ����! StartScene���� �̵��մϴ�.");

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    SceneManager.LoadScene("StartScene");
                }

                // swapCount ���� �� Number ������Ʈ ������ ��ü
                if (swapCount > 0 && swapCount <= numberPrefabs.Length)
                {
                    if (currentNumberObject != null)
                    {
                        GameObject newNumberPrefab = numberPrefabs[currentNumberIndex];

                        Destroy(currentNumberObject);  // ���� Number ������Ʈ ����
                        Instantiate(newNumberPrefab, currentNumberObject.transform.position, currentNumberObject.transform.rotation);  // ���ο� Number ������Ʈ �ν��Ͻ�ȭ

                        Debug.Log($"Number�� ���������� '{newNumberPrefab.name}'�� ����Ǿ����ϴ�.");

                        currentNumberIndex++;  // �������� �ε��� ����

                        // ���������� ������ ���ٸ� �ε��� ����
                        if (currentNumberIndex >= numberPrefabs.Length)
                        {
                            currentNumberIndex = 0;  // �ٽ� ó������ ����
                        }
                    }
                }
            }

            Player.transform.position = TopTeleport.transform.position;
        }
    }
}


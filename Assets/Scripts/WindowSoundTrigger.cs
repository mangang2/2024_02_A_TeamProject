using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public AudioSource windSound; // ����� �ҽ��� ������ ����
    public float volumeIncrease = 0.8f; // ���� �������� 0.8�� ���� (�� ū �Ҹ�)

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ Ʈ���ſ� ������
        {
            windSound.volume = Mathf.Min(windSound.volume + volumeIncrease, 1f);
            windSound.Play(); // �ٶ� �Ҹ� ���
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ Ʈ���ſ��� ������
        {
            windSound.Stop(); // �ٶ� �Ҹ� ����
            windSound.volume = windSound.volume - volumeIncrease;
        }
    }
}

// Start is called before the first frame update


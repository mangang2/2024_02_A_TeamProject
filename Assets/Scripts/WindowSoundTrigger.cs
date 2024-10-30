using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public AudioSource windSound; // ����� �ҽ��� ������ ����

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ Ʈ���ſ� ������
        {
            windSound.Play(); // �ٶ� �Ҹ� ���
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ Ʈ���ſ��� ������
        {
            windSound.Stop(); // �ٶ� �Ҹ� ����
        }
    }
}

// Start is called before the first frame update


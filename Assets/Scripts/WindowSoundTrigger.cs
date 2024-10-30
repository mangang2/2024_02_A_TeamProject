using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public AudioSource windSound; // 오디오 소스를 연결할 변수

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 트리거에 들어오면
        {
            windSound.Play(); // 바람 소리 재생
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 트리거에서 나가면
        {
            windSound.Stop(); // 바람 소리 멈춤
        }
    }
}

// Start is called before the first frame update


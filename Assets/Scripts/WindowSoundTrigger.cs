using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public AudioSource windSound; // 오디오 소스를 연결할 변수
    public float volumeIncrease = 0.8f; // 볼륨 증가량을 0.8로 설정 (더 큰 소리)

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 트리거에 들어오면
        {
            windSound.volume = Mathf.Min(windSound.volume + volumeIncrease, 1f);
            windSound.Play(); // 바람 소리 재생
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 트리거에서 나가면
        {
            windSound.Stop(); // 바람 소리 멈춤
            windSound.volume = windSound.volume - volumeIncrease;
        }
    }
}

// Start is called before the first frame update


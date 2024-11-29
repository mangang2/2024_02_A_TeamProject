using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpeakerVolume : MonoBehaviour
{
    // 소리를 재생할 AudioSource 컴포넌트
    private AudioSource audioSource;

    // 플레이어의 위치를 나타내는 Transform
    private Transform player;

    // 소리가 들리는 최대 거리
    public float maxDistance = 10f;

    // 최대 볼륨이 되는 최소 거리
    public float minDistance = 1f;

    // 여러 오디오 클립을 사용하기 위한 배열
    public AudioClip[] audioClips;

    private void Start()
    {
        // AudioSource 컴포넌트 찾기
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource를 찾을 수 없습니다. 스피커에 AudioSource를 추가해주세요.");
            enabled = false;
            return;
        }

        // 플레이어 찾기 (여러 방법 시도)
        FindPlayer();

        if (player == null)
        {
            Debug.LogError("플레이어를 찾을 수 없습니다. 플레이어 오브젝트를 확인해주세요.");
            enabled = false;
            return;
        }

        // 오디오 클립 설정
        SetupAudioClip();
    }

    private void FindPlayer()
    {
        // 방법 1: "Player" 태그로 찾기
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            return;
        }

        // 방법 2: 이름으로 찾기
        playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            return;
        }

        // 방법 3: PlayerController 컴포넌트로 찾기
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            player = playerController.transform;
            return;
        }
    }

    private void SetupAudioClip()
    {
        if (audioClips.Length > 0)
        {
            // 랜덤하게 오디오 클립 선택
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.loop = true; // 반복 재생 설정
        }
        else
        {
            Debug.LogWarning("오디오 클립이 할당되지 않았습니다. Inspector에서 오디오 클립을 할당해주세요.");
        }
    }

    private void Update()
    {
        if (player == null || audioSource.clip == null) return;

        // 플레이어와 스피커 사이의 거리 계산
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= maxDistance)
        {
            // 거리에 따른 볼륨 계산 (0 ~ 1 사이의 값)
            float volume = 1 - Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));

            // 계산된 볼륨을 AudioSource에 적용
            audioSource.volume = volume;

            // 소리가 재생 중이 아니고 볼륨이 0보다 크면 재생 시작
            if (!audioSource.isPlaying && volume > 0)
            {
                audioSource.Play();
            }// 현재 볼륨을 콘솔에 출력 (디버깅 용도)
            Debug.Log($"현재 볼륨: {volume:F2}");
        }
        else
        {
            // 플레이어가 최대 거리를 벗어나면 소리 중지
            audioSource.Stop();
            Debug.Log("소리 중지: 스피커에서 너무 멀어졌습니다.");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioCrossFade : MonoBehaviour
{
    public AudioSource audioSource1;  // 첫 번째 AudioSource
    public AudioSource audioSource2;  // 두 번째 AudioSource
    public AudioClip windClip;        // 바람 소리 클립
    public float fadeDuration = 2.0f; // 페이드 인/아웃 지속 시간
    private bool isPlayingSource1 = true; // 현재 재생 중인 AudioSource를 추적

    void Start()
    {
        // 두 AudioSource에 동일한 AudioClip 설정
        audioSource1.clip = windClip;
        audioSource2.clip = windClip;

        // 처음에는 첫 번째 AudioSource 재생 시작
        audioSource1.Play();

        // Crossfade 실행 주기를 AudioClip의 길이에서 페이드 시간을 뺀 값으로 설정
        InvokeRepeating("Crossfade", windClip.length - fadeDuration, windClip.length);
    }

    void Crossfade()
    {
        if (isPlayingSource1)
        {
            // 두 번째 AudioSource 페이드 인 및 첫 번째 AudioSource 페이드 아웃
            StartCoroutine(FadeIn(audioSource2, fadeDuration));
            StartCoroutine(FadeOut(audioSource1, fadeDuration));
            audioSource2.Play();  // 두 번째 AudioSource 재생
        }
        else
        {
            // 첫 번째 AudioSource 페이드 인 및 두 번째 AudioSource 페이드 아웃
            StartCoroutine(FadeIn(audioSource1, fadeDuration));
            StartCoroutine(FadeOut(audioSource2, fadeDuration));
            audioSource1.Play();  // 첫 번째 AudioSource 재생
        }

        // 현재 재생 중인 AudioSource를 반대로 전환
        isPlayingSource1 = !isPlayingSource1;
    }

    // 페이드 인 (볼륨을 0에서 1로 서서히 증가)
    IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        audioSource.volume = 0f;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            audioSource.volume = Mathf.Lerp(0f, 1f, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 1f;  // 최종적으로 볼륨을 1로 설정
    }

    // 페이드 아웃 (볼륨을 1에서 0으로 서서히 감소)
    IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0f;  // 최종적으로 볼륨을 0으로 설정
        audioSource.Stop();  // 오디오 중지
    }
}

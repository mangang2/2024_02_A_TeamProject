using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioCrossFade : MonoBehaviour
{
    public AudioSource audioSource1;  // ù ��° AudioSource
    public AudioSource audioSource2;  // �� ��° AudioSource
    public AudioClip windClip;        // �ٶ� �Ҹ� Ŭ��
    public float fadeDuration = 2.0f; // ���̵� ��/�ƿ� ���� �ð�
    private bool isPlayingSource1 = true; // ���� ��� ���� AudioSource�� ����

    void Start()
    {
        // �� AudioSource�� ������ AudioClip ����
        audioSource1.clip = windClip;
        audioSource2.clip = windClip;

        // ó������ ù ��° AudioSource ��� ����
        audioSource1.Play();

        // Crossfade ���� �ֱ⸦ AudioClip�� ���̿��� ���̵� �ð��� �� ������ ����
        InvokeRepeating("Crossfade", windClip.length - fadeDuration, windClip.length);
    }

    void Crossfade()
    {
        if (isPlayingSource1)
        {
            // �� ��° AudioSource ���̵� �� �� ù ��° AudioSource ���̵� �ƿ�
            StartCoroutine(FadeIn(audioSource2, fadeDuration));
            StartCoroutine(FadeOut(audioSource1, fadeDuration));
            audioSource2.Play();  // �� ��° AudioSource ���
        }
        else
        {
            // ù ��° AudioSource ���̵� �� �� �� ��° AudioSource ���̵� �ƿ�
            StartCoroutine(FadeIn(audioSource1, fadeDuration));
            StartCoroutine(FadeOut(audioSource2, fadeDuration));
            audioSource1.Play();  // ù ��° AudioSource ���
        }

        // ���� ��� ���� AudioSource�� �ݴ�� ��ȯ
        isPlayingSource1 = !isPlayingSource1;
    }

    // ���̵� �� (������ 0���� 1�� ������ ����)
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

        audioSource.volume = 1f;  // ���������� ������ 1�� ����
    }

    // ���̵� �ƿ� (������ 1���� 0���� ������ ����)
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

        audioSource.volume = 0f;  // ���������� ������ 0���� ����
        audioSource.Stop();  // ����� ����
    }
}

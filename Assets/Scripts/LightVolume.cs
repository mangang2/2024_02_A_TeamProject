using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightVolume : MonoBehaviour
{
    // �Ҹ��� ����� AudioSource ������Ʈ
    private AudioSource audioSource;

    // �÷��̾��� ��ġ�� ��Ÿ���� Transform
    private Transform player;

    // �Ҹ��� �鸮�� �ִ� �Ÿ� (�Ҹ� ������ ����)
    public float maxDistance = 10f; // �Ҹ� ������ 50���� ���
    public float minDistance = 0.5f; // �ִ� ������ �Ǵ� �ּ� �Ÿ� ����

    // ���� ����� Ŭ���� ����ϱ� ���� �迭
    public AudioClip[] audioClips;

    private void Start()
    {
        // AudioSource ������Ʈ ã��
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource�� ã�� �� �����ϴ�. ����Ŀ�� AudioSource�� �߰����ּ���.");
            enabled = false;
            return;
        }

        // �÷��̾� ã�� (���� ��� �õ�)
        FindPlayer();

        if (player == null)
        {
            Debug.LogError("�÷��̾ ã�� �� �����ϴ�. �÷��̾� ������Ʈ�� Ȯ�����ּ���.");
            enabled = false;
            return;
        }

        // ����� Ŭ�� ����
        SetupAudioClip();
    }

    private void FindPlayer()
    {
        // ��� 1: "Player" �±׷� ã��
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            return;
        }

        // ��� 2: �̸����� ã��
        playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            return;
        }

        // ��� 3: PlayerController ������Ʈ�� ã��
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
            // �����ϰ� ����� Ŭ�� ����
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.loop = true; // �ݺ� ��� ����
        }
        else
        {
            Debug.LogWarning("����� Ŭ���� �Ҵ���� �ʾҽ��ϴ�. Inspector���� ����� Ŭ���� �Ҵ����ּ���.");
        }
    }

    private void Update()
    {
        if (player == null || audioSource.clip == null) return;

        // �÷��̾�� ����Ŀ ������ �Ÿ� ���
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= maxDistance)
        {
            // �Ÿ��� ���� ���� ��� (0 ~ 1 ������ ��)
            float volume = 1 - Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));

            // ���� ������ AudioSource�� ����
            audioSource.volume = volume;

            // �Ҹ��� ��� ���� �ƴϰ� ������ 0���� ũ�� ��� ����
            if (!audioSource.isPlaying && volume > 0)
            {
                audioSource.Play();
            }
        }
        else
        {
            // �÷��̾ �ִ� �Ÿ��� ����� �Ҹ� ����
            audioSource.Stop();
        }
    }
}

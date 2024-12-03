using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSceneManager : MonoBehaviour
{
    // Video Player ������Ʈ
    private VideoPlayer videoPlayer;

    private void Start()
    {
        // ������Ʈ�� VideoPlayer ������Ʈ�� �����ɴϴ�.
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer != null)
        {
            // ���� ����� ���� �� �̺�Ʈ ����
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("VideoPlayer ������Ʈ�� ã�� �� �����ϴ�!");
        }

        // Ŀ���� ��� �����ϰ� ���̵��� ����
        UnlockCursor();
    }

    // ���� ���� �� ȣ��Ǵ� �޼���
    private void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("Video Ended! Loading StartScene...");
        SceneManager.LoadScene("StartScene"); // StartScene���� ��ȯ
    }

    private void OnDestroy()
    {
        // �̺�Ʈ ���� (�޸� ���� ����)
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }

    // Ŀ�� ��� ���� �� ���̱�
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None; // Ŀ�� ��� ����
        Cursor.visible = true;                 // Ŀ�� ���̱�
    }
}


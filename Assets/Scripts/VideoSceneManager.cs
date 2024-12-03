using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSceneManager : MonoBehaviour
{
    // Video Player 컴포넌트
    private VideoPlayer videoPlayer;

    private void Start()
    {
        // 오브젝트에 VideoPlayer 컴포넌트를 가져옵니다.
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer != null)
        {
            // 영상 재생이 끝날 때 이벤트 연결
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("VideoPlayer 컴포넌트를 찾을 수 없습니다!");
        }
    }

    // 영상 종료 시 호출되는 메서드
    private void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("Video Ended! Loading StartScene...");
        SceneManager.LoadScene("StartScene"); // StartScene으로 전환
    }

    private void OnDestroy()
    {
        // 이벤트 해제 (메모리 누수 방지)
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}

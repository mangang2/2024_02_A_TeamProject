using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EndSceneImageController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // 비디오 플레이어
    public GameObject imageObject;   // 활성화할 이미지 오브젝트
    public float slideDistance = 100f;  // 이동 거리

    private RectTransform imageRectTransform;  // 이미지 오브젝트의 RectTransform

    void Start()
    {
        // RectTransform 컴포넌트 가져오기
        imageRectTransform = imageObject.GetComponent<RectTransform>();

        // 비디오 플레이어의 이벤트에 함수 연결
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Play();  // 영상 시작
    }

    // 영상이 준비되면 호출되는 함수
    void OnVideoPrepared(VideoPlayer vp)
    {
        // 영상이 시작된 후 1초 뒤에 이미지를 아래로 내려오는 효과와 함께 활성화하고, 그 후 3초 뒤에 비활성화
        StartCoroutine(SlideInAndDeactivateImageAfterDelay(1f, 3f));
    }

    // 1초 뒤에 이미지를 아래로 내려오게 하고, 그 후 3초 뒤에 비활성화하는 코루틴
    IEnumerator SlideInAndDeactivateImageAfterDelay(float activationDelay, float deactivationDelay)
    {
        // 1초 대기
        yield return new WaitForSeconds(activationDelay);

        // 이미지 활성화
        imageObject.SetActive(true);

        // 시작 위치를 화면 밖으로 설정
        Vector2 startPos = new Vector2(imageRectTransform.anchoredPosition.x, imageRectTransform.anchoredPosition.y + slideDistance);
        imageRectTransform.anchoredPosition = startPos;

        // 아래로 내려오는 애니메이션
        float timeElapsed = 0f;
        Vector2 targetPos = new Vector2(imageRectTransform.anchoredPosition.x, imageRectTransform.anchoredPosition.y - slideDistance);

        while (timeElapsed < 1f)
        {
            imageRectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // 최종 위치로 정확히 설정
        imageRectTransform.anchoredPosition = targetPos;

        // 3초 대기
        yield return new WaitForSeconds(deactivationDelay);

        // 이미지 비활성화
        imageObject.SetActive(false);
    }
}

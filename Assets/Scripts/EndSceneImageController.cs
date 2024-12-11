using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EndSceneImageController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // ���� �÷��̾�
    public GameObject imageObject;   // Ȱ��ȭ�� �̹��� ������Ʈ
    public float slideDistance = 100f;  // �̵� �Ÿ�

    private RectTransform imageRectTransform;  // �̹��� ������Ʈ�� RectTransform

    void Start()
    {
        // RectTransform ������Ʈ ��������
        imageRectTransform = imageObject.GetComponent<RectTransform>();

        // ���� �÷��̾��� �̺�Ʈ�� �Լ� ����
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Play();  // ���� ����
    }

    // ������ �غ�Ǹ� ȣ��Ǵ� �Լ�
    void OnVideoPrepared(VideoPlayer vp)
    {
        // ������ ���۵� �� 1�� �ڿ� �̹����� �Ʒ��� �������� ȿ���� �Բ� Ȱ��ȭ�ϰ�, �� �� 3�� �ڿ� ��Ȱ��ȭ
        StartCoroutine(SlideInAndDeactivateImageAfterDelay(1f, 3f));
    }

    // 1�� �ڿ� �̹����� �Ʒ��� �������� �ϰ�, �� �� 3�� �ڿ� ��Ȱ��ȭ�ϴ� �ڷ�ƾ
    IEnumerator SlideInAndDeactivateImageAfterDelay(float activationDelay, float deactivationDelay)
    {
        // 1�� ���
        yield return new WaitForSeconds(activationDelay);

        // �̹��� Ȱ��ȭ
        imageObject.SetActive(true);

        // ���� ��ġ�� ȭ�� ������ ����
        Vector2 startPos = new Vector2(imageRectTransform.anchoredPosition.x, imageRectTransform.anchoredPosition.y + slideDistance);
        imageRectTransform.anchoredPosition = startPos;

        // �Ʒ��� �������� �ִϸ��̼�
        float timeElapsed = 0f;
        Vector2 targetPos = new Vector2(imageRectTransform.anchoredPosition.x, imageRectTransform.anchoredPosition.y - slideDistance);

        while (timeElapsed < 1f)
        {
            imageRectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // ���� ��ġ�� ��Ȯ�� ����
        imageRectTransform.anchoredPosition = targetPos;

        // 3�� ���
        yield return new WaitForSeconds(deactivationDelay);

        // �̹��� ��Ȱ��ȭ
        imageObject.SetActive(false);
    }
}

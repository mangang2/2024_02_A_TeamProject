using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLightAnomaly : MonoBehaviour
{
    // 깜빡일 조명 배열
    public Light[] flickeringLights;

    // 깜빡임 간격
    public float flickerInterval = 0.1f;

    // 이상현상 지속 시간
    public float anomalyDuration = 10f;

    // 이상현상 시작 함수
    public void StartAnomaly()
    {
        StartCoroutine(AnomalySequence());
    }

    // 이상현상 시퀀스 코루틴
    IEnumerator AnomalySequence()
    {
        // 깜빡임 시작
        StartCoroutine(FlickerLights());

        // 지정된 시간 동안 대기
        yield return new WaitForSeconds(anomalyDuration);

        // 깜빡임 정지 및 원래 상태로 복구
        StopAllCoroutines();
        RestoreLights();
    }

    // 조명 깜빡임 코루틴
    IEnumerator FlickerLights()
    {
        while (true)
        {
            foreach (Light light in flickeringLights)
            {
                // 각 조명의 on/off 상태를 토글
                light.enabled = !light.enabled;
            }
            yield return new WaitForSeconds(flickerInterval);
        }
    }

    // 조명을 원래 상태로 복구하는 함수
    void RestoreLights()
    {
        foreach (Light light in flickeringLights)
        {
            light.enabled = true;
        }
    }
}

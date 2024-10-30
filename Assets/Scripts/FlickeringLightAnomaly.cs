using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLightAnomaly : MonoBehaviour
{
    // ������ ���� �迭
    public Light[] flickeringLights;

    // ������ ����
    public float flickerInterval = 0.1f;

    // �̻����� ���� �ð�
    public float anomalyDuration = 10f;

    // �̻����� ���� �Լ�
    public void StartAnomaly()
    {
        StartCoroutine(AnomalySequence());
    }

    // �̻����� ������ �ڷ�ƾ
    IEnumerator AnomalySequence()
    {
        // ������ ����
        StartCoroutine(FlickerLights());

        // ������ �ð� ���� ���
        yield return new WaitForSeconds(anomalyDuration);

        // ������ ���� �� ���� ���·� ����
        StopAllCoroutines();
        RestoreLights();
    }

    // ���� ������ �ڷ�ƾ
    IEnumerator FlickerLights()
    {
        while (true)
        {
            foreach (Light light in flickeringLights)
            {
                // �� ������ on/off ���¸� ���
                light.enabled = !light.enabled;
            }
            yield return new WaitForSeconds(flickerInterval);
        }
    }

    // ������ ���� ���·� �����ϴ� �Լ�
    void RestoreLights()
    {
        foreach (Light light in flickeringLights)
        {
            light.enabled = true;
        }
    }
}

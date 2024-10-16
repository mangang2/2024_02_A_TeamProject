using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light lightSource;        // ������ Light ������Ʈ
    public float minIntensity = 0.0f; // �ּ� ���
    public float maxIntensity = 2.0f; // �ִ� ���
    public float flickerSpeed = 0.05f; // �����Ÿ� �ӵ�

    private float randomizer;

    void Start()
    {
        if (lightSource == null)
        {
            lightSource = GetComponent<Light>();  // Light ������Ʈ �ڵ� ����
        }
    }

    void Update()
    {
        randomizer = Mathf.PerlinNoise(Time.time * flickerSpeed, 0.0f);
        lightSource.intensity = Mathf.Lerp(minIntensity, maxIntensity, randomizer);
    }
}



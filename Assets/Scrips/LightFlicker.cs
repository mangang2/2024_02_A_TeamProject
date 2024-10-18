using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light lightSource;        // 전등의 Light 컴포넌트
    public float minIntensity = 0.0f; // 최소 밝기
    public float maxIntensity = 2.0f; // 최대 밝기
    public float flickerSpeed = 0.05f; // 깜빡거림 속도

    private float randomizer;

    void Start()
    {
        if (lightSource == null)
        {
            lightSource = GetComponent<Light>();  // Light 컴포넌트 자동 연결
        }
    }

    void Update()
    {
        randomizer = Mathf.PerlinNoise(Time.time * flickerSpeed, 0.0f);
        lightSource.intensity = Mathf.Lerp(minIntensity, maxIntensity, randomizer);
    }
}



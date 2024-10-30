using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light flickeringLight;
    public float minIntensity = 0f;
    public float maxIntensity = 1f;
    public float flickerSpeed = 0.1f;

    private float randomIntensity;

    private void Start()
    {
        if (flickeringLight == null)
        {
            flickeringLight = GetComponent<Light>();
        }
        StartCoroutine(FlickerLight());
    }

    private IEnumerator FlickerLight()
    {
        while (true)
        {
            randomIntensity = Random.Range(minIntensity, maxIntensity);
            flickeringLight.intensity = randomIntensity;
            yield return new WaitForSeconds(flickerSpeed);
        }
    }
}


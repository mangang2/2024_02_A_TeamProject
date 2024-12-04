using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider audioSlider;

    public void AudioControl() 
    {
        float sound = audioSlider.value;

        float dB = sound <= 0.0001f ? -80f : Mathf.Log10(Mathf.Clamp(sound, 0.0001f, 1f)) * 20f;
        masterMixer.SetFloat("BGM", dB);
    }

    void Start()
    {
        audioSlider.value = 0.25f; // �����̴� �ʱⰪ (0.25)
        AudioControl(); // �ʱ� ������ �����̴� ���� ����
    }

}






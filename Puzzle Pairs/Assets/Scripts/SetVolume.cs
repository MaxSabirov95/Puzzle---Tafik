using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Text percents;
    public void SetLevel(float sliderValue)
    {
        percents.text = (sliderValue * 100).ToString("f0")+("%");
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue)*20);
    }
}

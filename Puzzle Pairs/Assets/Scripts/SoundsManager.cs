using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public AudioClip[] Sounds;
    //public AudioSource BackgroundMusic;

    private static AudioSource AudioSrc;//--Main Audio source

    void Start()
    {
        BlackBoard.soundsManager = this;
        // BackgroundMusic.Play();
        AudioSrc = GetComponent<AudioSource>();
    }

    public void SoundsList(int SoundNumber)
    {
        AudioSrc.PlayOneShot(Sounds[SoundNumber]);
    }//--Game sounds list
}

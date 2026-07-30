using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource bg_adudio;
    [SerializeField] private AudioSource audioPlayer_wl;
    [SerializeField] private AudioSource audioPlayer_button;
    [SerializeField] private AudioSource audioPlayer_Spin;

    [Header("Clips")]
    [SerializeField] private AudioClip SpinButtonClip;
    [SerializeField] private AudioClip SpinClip;
    [SerializeField] private AudioClip Button;
    [SerializeField] private AudioClip Win_Audio;
    [SerializeField] private AudioClip NormalBg_Audio;

    private bool isForceMuted = false;
    private readonly Dictionary<AudioSource, bool> preFocusMuteState = new Dictionary<AudioSource, bool>();
    private List<AudioSource> allSources;

    private void Awake()
    {
        allSources = new List<AudioSource> { bg_adudio, audioPlayer_wl, audioPlayer_button, audioPlayer_Spin };
        playBgAudio();
        //if (bg_adudio) bg_adudio.Play();
        //audioPlayer_button.clip = clips[clips.Length - 1];
    }

    internal void PlayWLAudio(string type)
    {

        switch (type)
        {

            case "win":
                //index = UnityEngine.Random.Range(1, 2);
                audioPlayer_wl.clip = Win_Audio;
                break;

                //index = 3;

        }
        StopWLAaudio();
        //audioPlayer_wl.clip = clips[index];
        //audioPlayer_wl.loop = true;
        audioPlayer_wl.Play();

    }

    internal void PlaySpinAudio()
    {

        if (audioPlayer_Spin)
        {
            audioPlayer_Spin.clip = SpinClip;

            audioPlayer_Spin.Play();
        }

    }

    internal void StopSpinAudio()
    {

        if (audioPlayer_Spin) audioPlayer_Spin.Stop();

    }

    internal void SetMuteAll(bool forceMute)
    {
        if (forceMute == isForceMuted) return;
        isForceMuted = forceMute;

        foreach (var source in allSources)
        {
            if (source == null) continue;
            if (forceMute)
            {
                preFocusMuteState[source] = source.mute;
                source.mute = true;
            }
            else
            {
                source.mute = preFocusMuteState.TryGetValue(source, out bool prevMuted) ? prevMuted : source.mute;
            }
        }
    }

    internal void playBgAudio()
    {


        //int randomIndex = UnityEngine.Random.Range(0, Bg_Audio.Length);
        if (bg_adudio)
        {
            bg_adudio.clip = NormalBg_Audio;


            bg_adudio.Play();
        }

    }

    internal void PlayButtonAudio(string type = "default")
    {

        if (type == "spin")
            audioPlayer_button.clip = SpinButtonClip;
        else
            audioPlayer_button.clip = Button;

        //StopButtonAudio();
        audioPlayer_button.Play();
        // Invoke("StopButtonAudio", audioPlayer_button.clip.length);

    }

    internal void StopWLAaudio()
    {
        audioPlayer_wl.Stop();
        audioPlayer_wl.loop = false;
    }

    internal void StopButtonAudio()
    {

        audioPlayer_button.Stop();

    }


    internal void StopBgAudio()
    {
        bg_adudio.Stop();

    }


    internal void ToggleMute(float value, string type = "all")
    {
        // An explicit user interaction is proof of live focus - it must always win over a
        // stuck/stale forced-mute from a missed focus-regain event.
        isForceMuted = false;

        switch (type)
        {
            case "bg":
                if(value<0.1)
                bg_adudio.mute = true;
                else{
                bg_adudio.mute = false;
                bg_adudio.volume=value;
                }
                break;
            case "button":
                if(value<0.1){
                    audioPlayer_button.mute=true;
                    audioPlayer_Spin.mute=true;
                }
                else{
                    audioPlayer_button.mute=false;
                    audioPlayer_Spin.mute=false;
                    audioPlayer_Spin.volume=value;
                    audioPlayer_button.volume=value;
                }
                break;
            case "wl":
                if(value<0.1)
                audioPlayer_wl.mute = true;
                else{
                audioPlayer_wl.mute = false;
                audioPlayer_wl.volume = value;


                }
                break;
            case "all":
                if(value<0.1){
                audioPlayer_wl.mute = true;
                bg_adudio.mute = true;
                audioPlayer_button.mute = true;
                }else{
                audioPlayer_wl.mute = false;
                bg_adudio.mute = false;
                audioPlayer_button.mute = false;
                audioPlayer_wl.volume = value;
                bg_adudio.volume = value;
                audioPlayer_button.volume = value;
                }

                break;
        }
    }

}

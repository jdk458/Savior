using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    [Header("Bgm")] public AudioSource bgm_AudioSource; public bool bgm_isStart;
    [Header("기타")] public AudioSource _AudioSource;

    public void Play(string audio_name)
    {
        _AudioSource.clip = GameManager.instance.audioManager.GetAudioClip(audio_name);
        GameManager.instance.audioManager.EnvironVolume_Play(_AudioSource);
    }

    public void BgmPlay()
    {
        GameManager.instance.audioManager.Bgm_Play(bgm_AudioSource);
    }

    private void Start()
    {
        if(bgm_isStart)
            BgmPlay();
    }
}

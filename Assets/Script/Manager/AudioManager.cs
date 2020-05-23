using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    [Range(0,1)] public float entireVolume;
    [Range(0, 1)] public float BgmVolune;
    [Range(0, 1)] public float environVolume;

    [Serializable]
    public struct AudioStruct
    {
        public string audioName;
        public AudioClip audioClip;
    }

    public AudioStruct[] audio_list;

    public AudioClip GetAudioClip(string audioName)
    {
        for (int i = 0; i < audio_list.Length; i++)
        {
            if (audio_list[i].audioName.Contains(audioName))
            {
                return audio_list[i].audioClip;
            }
        }
        return null;
    }

    public void EnvironVolume_Play(AudioSource AS)
    {
        float sound = entireVolume * environVolume;
        AS.volume = sound;
        AS.Play();
    }
    public void Bgm_Play(AudioSource AS)
    {
        float sound = entireVolume * BgmVolune;
        AS.volume = sound;
        AS.Play();
    }
}

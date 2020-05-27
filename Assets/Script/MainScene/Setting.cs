using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Slider entireVolume;
    public Slider bgmVolume;
    public Slider environVolume;

    public Sound_Manager sound_Manager;

    private float entireVnum = 1f;
    private float bgmVnum = 1f;
    private float environVnum = 1f;

    private void Start()
    {
        entireVnum = GameManager.instance.audioManager.entireVolume;
        bgmVnum = GameManager.instance.audioManager.BgmVolume;
        environVnum = GameManager.instance.audioManager.environVolume;
        entireVolume.value = entireVnum;
        bgmVolume.value = bgmVnum;
        environVolume.value = environVnum;
    }
    private void Update()
    {
        EntireVolume();
        BgmVolume();
        EnvironVolume();
    }
    public void EntireVolume()
    {
        GameManager.instance.audioManager.entireVolume = entireVolume.value;
        sound_Manager.bgm_AudioSource.volume = entireVolume.value * bgmVolume.value;
        sound_Manager.bgm_AudioSource.volume = entireVolume.value * environVolume.value;
    }
    public void BgmVolume()
    {
        GameManager.instance.audioManager.BgmVolume = bgmVolume.value;
        sound_Manager.bgm_AudioSource.volume = entireVolume.value * bgmVolume.value;
    }
    public void EnvironVolume()
    {
        GameManager.instance.audioManager.environVolume = environVolume.value;
        sound_Manager.bgm_AudioSource.volume = entireVolume.value * environVolume.value;
    }
}

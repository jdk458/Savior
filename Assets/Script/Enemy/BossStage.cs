using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossStage : MonoBehaviour
{
    public CountdownManager countdown;
    public CameraResolution camerasol;
    Vector3 pos;

    public GameObject bosshpbar;
    public Image boss_hp;

    public GameObject background;

    public Transform player;

    public GameObject wall;

    MonsterController monstercontroller;

    private void Start()
    {
        countdown.time = 300;
        wall.transform.position = player.position;
        background.transform.position = player.position;
        bosshpbar.SetActive(true);
        monstercontroller = this.GetComponent<MonsterController>();
    }

    private void FixedUpdate()
    {
        boss_hp.fillAmount = (float)monstercontroller.current_hp /monstercontroller.hp;
        if (countdown.remainTime > 0 && monstercontroller.current_hp <= 0)
            Victory();
        else if (countdown.remainTime <= 0)
            GameOver();
    }
    void GameOver()
    {

    }
    void Victory()
    {
        SceneManager.LoadScene("MainScene");
    }
}

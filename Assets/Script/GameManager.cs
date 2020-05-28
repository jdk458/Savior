using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UserInfoManager userinfo;
    public AudioManager audioManager;
    public MonsterManager monsterManager;
    void Awake()
    {
        instance = this;
    }

   

}
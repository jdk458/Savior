using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector]
    public UserInfoManager userinfo;
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        userinfo = GetComponent<UserInfoManager>();
    }

}

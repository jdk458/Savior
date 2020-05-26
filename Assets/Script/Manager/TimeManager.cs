using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    void Awake()
    {
        instance = this;
    }

    bool isStop = false;

    public bool GetTime()
    {
        return isStop;
    }

    public void SetTime(bool time)
    {
        isStop = time;
    }
}

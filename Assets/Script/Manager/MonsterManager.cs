using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterManager : MonoBehaviour
{
    [Serializable]
    public struct MonsterStruct
    {
        public string name;
        public Stage stage;
        public MonsterType monsterType;
        [Range(0,1)] public float rigidTime;
        public int atk;
        public int hp;
        public float speed;
    }

    public MonsterStruct[] MonsterList;

    public MonsterStruct GetMonster(string name)
    {
        for (int i = 0; i < MonsterList.Length; i++)
        {
            if (MonsterList[i].name.Contains(name))
            {
                return MonsterList[i];
            }
        }
        MonsterStruct Null = new MonsterStruct();
        Null.name = "";
        return Null;
    }
}

public enum MonsterType
{
    약한객체,
    중간객체,
    강한객체
}

public enum Stage
{
    Stage01,
    Stage02,
    Stage03,
    Stage04,
    Stage05
}

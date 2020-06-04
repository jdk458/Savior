using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoManager : MonoBehaviour
{
    public float point = 100;
    public int research_level = 1;

    public bool hp_research;
    public bool atk_research;
    public bool atkspeed_research;
    public bool speed_research;
    public bool item_research;
    public bool shield_research;
    public bool recovery_research;
    public bool skilldamage_research;
    public bool exp_research;
    public bool point_research;

    public int GetMasicMaxHP()
    {
        if (hp_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }
    public int GetMasicAtk()
    {
        if (atk_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }

    public int GetMasicAtkspeed()
    {
        if (atkspeed_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }

    public int GetMasicSpeed()
    {
        if (speed_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }

    public int GetMasicItemDistance()
    {
        if (item_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }

    public int GetMasicShield()
    {
        if (shield_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }

    public int GetMasicRecovery()
    {
        if (recovery_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }

    public int GetMasicSkillDamage()
    {
        if (skilldamage_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }

    public int GetMasicExp()
    {
        if (exp_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }

    public int GetMasicPoint()
    {
        if (point_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }
}

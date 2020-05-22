using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI2_Manager : MonoBehaviour
{
    public GameObject blackpannel;
    public GameObject skillSelect;

    public void SkillSelect()
    {
        blackpannel.SetActive(true);
        skillSelect.SetActive(true);
        skillSelect.transform.GetChild(0).gameObject.SetActive(true);
        skillSelect.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void SkillSelect_Exit()
    {
        blackpannel.SetActive(false);
        skillSelect.SetActive(false);
    }
}

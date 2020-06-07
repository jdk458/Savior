using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_StageManager : MonoBehaviour
{
    [Header("배경리스트")]
    public GameObject[] background_list;

    int currentStage;

    private void Start()
    {
        currentStage = 1;
    }

    public void NextStage()
    {
        //GameObject[] childList = GetComponentsInChildren<GameObject>(true);
        //if (childList != null)
        //{
        //    for(int i = 0; i<childList.Length; i++)
        //    {
        //        Destroy(childList[i].gameObject);
        //    }
        //}
        background_list[currentStage - 1].SetActive(false);
        currentStage++;
        background_list[currentStage - 1].SetActive(true);
    }
}

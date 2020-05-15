using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    [Header("blackpannel")] public GameObject blackpannel;
    [Header("게임시작")] public GameObject chapter_pannel;
    [Header("연구패널")] public GameObject Research_pannel;

    // 게임시작 
    public void OnClickGameStart_Btn()
    {
        blackpannel.SetActive(true);
        chapter_pannel.SetActive(true);
    }
    public void GameStart_Exit()
    {
        blackpannel.SetActive(false);
        chapter_pannel.SetActive(false);
    }
    public void GameStart()
    {
        SceneManager.LoadScene("Battle");
    }

    // 연구
    public void OnClickResearch()
    {
        blackpannel.SetActive(true);
        Research_pannel.SetActive(true);
    }
    public void Research_Exit()
    {
        blackpannel.SetActive(false);
        Research_pannel.SetActive(false);
    }
}

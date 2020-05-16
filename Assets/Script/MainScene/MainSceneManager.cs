using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    [Header("blackpannel")] public GameObject blackpannel;
    [Header("게임시작")] public GameObject chapter_pannel;
    [Header("캐릭터")] public GameObject character_pannel;
    [Header("연구")] public GameObject research_pannel;
    [Header("환경설정")] public GameObject option_pannel;
    [Header("크레딧")] public GameObject Cradit_pannel;

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

    //케릭터
    public void OnClickCharacter()
    {
        blackpannel.SetActive(true);
        character_pannel.SetActive(true);
    }
    public void CharacterExit()
    {
        blackpannel.SetActive(false);
        character_pannel.SetActive(false);
    }

    // 연구
    public void OnClickResearch()
    {
        blackpannel.SetActive(true);
        research_pannel.SetActive(true);
    }
    public void Research_Exit()
    {
        blackpannel.SetActive(false);
        research_pannel.SetActive(false);
    }

    // 환경설정
    public void OnClickOption()
    {
        blackpannel.SetActive(true);
        option_pannel.SetActive(true);
    }
    public void OptionExit()
    {
        option_pannel.SetActive(false);
        blackpannel.SetActive(false);
    }

    //크레딧
    public void OnClickCradit()
    {
        blackpannel.SetActive(true);
        Cradit_pannel.SetActive(true);
    }
    public void CraditExit()
    {
        blackpannel.SetActive(false);
        Cradit_pannel.SetActive(false);
    }
}

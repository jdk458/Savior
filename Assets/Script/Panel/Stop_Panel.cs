using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stop_Panel : MonoBehaviour
{
    public GameObject Stop_panel;
    public GameObject black_panel;

    private void Awake()
    {
        Stop_panel.SetActive(false);
    }

    public void Pop_Panel()
    {
        //게임멈춤
        Stop_panel.SetActive(true);
        black_panel.SetActive(true);
    }

    public void OnClick_Back()
    {
        //게임멈춤 풀기
        Stop_panel.SetActive(false);
        black_panel.SetActive(false);
    }
    public void OnClick_Out()
    {
        SceneManager.LoadScene("MainScene");
    }
}
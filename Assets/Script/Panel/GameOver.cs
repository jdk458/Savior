using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [Header("blackpannel")] public GameObject blackpannel;
    [Header("캐릭터")] public GameObject player;

    public Transform playertransform;
    private void Awake()
    {
        this.gameObject.SetActive(false);
        blackpannel.SetActive(false);
    }

    public void Show()
    {
        blackpannel.transform.position = playertransform.position;
        gameObject.SetActive(true);
        blackpannel.SetActive(true);
    }

    public void OnClick_Restart()
    {
        SceneManager.LoadScene("Battle");
    }
    public void OnClick_Out()
    {
        SceneManager.LoadScene("MainScene");
    }
}

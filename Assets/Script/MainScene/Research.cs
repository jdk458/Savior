using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Research : MonoBehaviour
{
    [Header("Line")] public Image[] fill_image; 
    [Header("If_Line")] public Image if_fill_image;
    [Header("연구레벨_포인트")] public GameObject Research_Level_Point;
    [Header("설명")] public GameObject explanation;

    bool circle_flag;
    public bool if_fill_flag;

    public void PointDownCircle()
    {
        Research_Level_Point.GetComponent<Image>().DOFade(1, .5F);
        Research_Level_Point.transform.GetChild(0).GetComponent<Text>().DOFade(1, .5F);
        Research_Level_Point.transform.GetChild(1).GetComponent<Text>().DOFade(1, .5F);

        explanation.SetActive(true);
        explanation.transform.localPosition = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y + 150);

        if (!if_fill_flag)
            return;

        if (this.GetComponent<Image>().fillAmount == 1)
            return;
        circle_flag = true;
    }
    public void PointUpCircle()
    {
        Research_Level_Point.GetComponent<Image>().DOFade(0, .5F);
        Research_Level_Point.transform.GetChild(0).GetComponent<Text>().DOFade(0, .5F);
        Research_Level_Point.transform.GetChild(1).GetComponent<Text>().DOFade(0, .5F);

        explanation.SetActive(false);

        if (!if_fill_flag)
            return;

        if ((this.GetComponent<Image>().fillAmount == 0 || this.GetComponent<Image>().fillAmount == 1))
            return;
        circle_flag = false;
    }

    private void FixedUpdate()
    {
        if (circle_flag )
        {
            this.GetComponent<Image>().fillAmount += Time.deltaTime;
        }
        if(!circle_flag)
        {
            this.GetComponent<Image>().fillAmount -= Time.deltaTime;
        }

        if(this.GetComponent<Image>().fillAmount == 1)
        {
            for (int i = 0; i < fill_image.Length; i++)
            {
                if (fill_image[i].fillAmount < 1)
                    fill_image[i].fillAmount += Time.deltaTime;
            }
        }

        if(if_fill_image != null && if_fill_image.fillAmount == 1 && !if_fill_flag)
        {
            if_fill_flag = true;
            Visible();
        }
    }

    void Visible()
    {
        gameObject.transform.GetChild(0).GetComponent<Image>().DOFade(1, 2f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("카메라")]
    public Transform theCam;
    [Header("조이스틱")]
    public Transform joystic_foreground;
    [Header("속도")]
    public float speed;

    bool isRun_flag;

    private void FixedUpdate()
    {
        Vector2 joystic_localpos = joystic_foreground.GetComponent<RectTransform>().localPosition;

        if(joystic_localpos != Vector2.zero)
        {
            // 조이스틱 위치에 따른 플레이어 위치 
            this.transform.position = new Vector2(
            this.transform.position.x + (joystic_localpos.x * Time.fixedDeltaTime * speed),
            this.transform.position.y + (joystic_localpos.y * Time.fixedDeltaTime * speed));

            if (!isRun_flag)   // 달리는 동작 
            {
                isRun_flag = true;
                this.GetComponent<Animator>().SetBool("isRun", true);
            }

            if(joystic_localpos.x < 0)
            {
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, 180, this.transform.rotation.z);
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, 0, this.transform.rotation.z);
            }

            // 카메라 플레이어 따라가기 
            theCam.position = new Vector3(this.transform.position.x, this.transform.position.y, theCam.position.z);
        }
        else
        {
            if (isRun_flag) // 달리는 동작 멈추기 
            {
                isRun_flag = false;
                this.GetComponent<Animator>().SetBool("isRun", false);
            }
        }
    }
}

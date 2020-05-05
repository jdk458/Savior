using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("카메라")]
    public Transform theCam;
    [Header("조이스틱")]
    public Transform joystic_foreground;
    Vector2 joystic_localpos;
    [Header("속도")]
    public float speed;
    public float dash_move;
    [Header("슬래쉬")]
    public GameObject slash;

    List<Transform> enemy_transform_list = new List<Transform>();

    bool isRun_flag;
    bool isAttack_flag;

    private void Update()
    {
        if (!isAttack_flag)
        {
            // 주변에 적 찾기
            RaycastHit2D[] hit_enemy = Physics2D.CircleCastAll(this.transform.position, 1.5f, Vector2.zero);
            if (hit_enemy.Length > 0)
            {
                for (int i = 0; i < hit_enemy.Length; i++)
                {
                    if (hit_enemy[i] && hit_enemy[i].transform.tag == "Enemy")
                    {
                        enemy_transform_list.Add(hit_enemy[i].transform);
                    }
                }
                if (enemy_transform_list.Count > 1)
                {
                    int min = 0;
                    for (int i = 1; i < enemy_transform_list.Count; i++)
                    {
                        if (Vector2.Distance(this.transform.position, enemy_transform_list[min].position) > Vector2.Distance(this.transform.position, enemy_transform_list[i].position))
                        {
                            min = i;
                        }
                    }
                    StartCoroutine(Attack_Coroutine(enemy_transform_list[min].gameObject));
                }
                else if(enemy_transform_list.Count == 1)
                {
                    StartCoroutine(Attack_Coroutine(enemy_transform_list[0].gameObject));
                }
            }
        }
    }

    private void FixedUpdate()
    {
        joystic_localpos = joystic_foreground.GetComponent<RectTransform>().localPosition;

        if (joystic_localpos != Vector2.zero)
        {
            if (!dash_flag) // 대쉬중에는 막아둔다
            {
                // 조이스틱 위치에 따른 플레이어 위치 
                this.transform.position = new Vector2(
                this.transform.position.x + (joystic_localpos.x * Time.fixedDeltaTime * speed),
                this.transform.position.y + (joystic_localpos.y * Time.fixedDeltaTime * speed));
            }


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

    public void OnClick_Dash()
    {
        if (Mathf.Abs(joystic_localpos.x) < 45 && Mathf.Abs(joystic_localpos.y) < 45)
        {
            return;
        }
        StartCoroutine(DashCoroutine());
    }

    bool dash_flag;
    IEnumerator DashCoroutine()
    {
        dash_flag = true;

        if (joystic_localpos.x > 45)
            this.transform.DOMoveX(this.transform.position.x + dash_move, 0.1f).SetEase(Ease.Linear);
        if (joystic_localpos.x < -45)
            this.transform.DOMoveX(this.transform.position.x - dash_move, 0.1f).SetEase(Ease.Linear);
        if (joystic_localpos.y > 45)
            this.transform.DOMoveY(this.transform.position.y + dash_move, 0.1f).SetEase(Ease.Linear);
        if (joystic_localpos.y < -45)
            this.transform.DOMoveY(this.transform.position.y - dash_move, 0.1f).SetEase(Ease.Linear);


        for (int i = 0; i < 5; i++)
        {
            GameObject ghost_obj = ObjectPoolingManager.instance.GetQueue(ObjectKind.ghost);
            ghost_obj.transform.position = this.transform.position;

            if (joystic_localpos.x < 0)
                ghost_obj.transform.rotation = Quaternion.Euler(this.transform.rotation.x, 180, this.transform.rotation.z);
            else
                ghost_obj.transform.rotation = Quaternion.Euler(this.transform.rotation.x, 0, this.transform.rotation.z);

            yield return new WaitForSeconds(0.1f / 5);

        }

        dash_flag = false;
    }

    IEnumerator Attack_Coroutine(GameObject enemy)
    {
        isAttack_flag = true;
        enemy_transform_list.Clear();
        slash.SetActive(true);
        slash.transform.position = enemy.transform.position;

        Vector3 dir = enemy.transform.position - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        slash.transform.rotation = Quaternion.Euler(slash.transform.rotation.x, slash.transform.rotation.y, angle + 90);

        yield return new WaitForSeconds(0.3f);
        slash.SetActive(false);
        isAttack_flag = false;
    }
}

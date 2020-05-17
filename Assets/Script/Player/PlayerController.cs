using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("STATUS")]
    [Range(1, 10)] public float atk;
    [Range(0, 200)] public int max_hp;
    [Range(3, 10)] public float range;
    [Range(0.1f, 3f)] public float atkspeed;
    [Range(0.04f, 0.1f)] public float speed;
    [Range(1.5f, 3f)] public float dash_move;
    [Range(1f, 5f)] public float item_range;

    [HideInInspector]
    public int current_hp;

    [Header("카메라")]
    public Transform theCam;
    [Header("조이스틱")]
    public Transform joystic_foreground;
    Vector2 joystic_localpos;
    [Header("체력 이미지")]
    public Image hp_image;
    [Header("대쉬, 장애물, 스킬아이템 버튼")]
    public GameObject dash_btn;
    public GameObject obstacle_btn;
    public GameObject skill_item_btn;
    GameObject obstacle_obj;
    GameObject skill_item_obj;

    public GameObject ui_canvas;

    [Header("게임오버플레이어위치")] public Transform gameovertransform;
    [Header("게임오버패널")] public GameObject GameOverPanel;

    //플레이어 레벨 업 변수 정보
    [HideInInspector] public int character_lv_speed;
    [HideInInspector] public int character_lv_exp;
    [HideInInspector] public int character_lv_hp;

    [HideInInspector] public int attack_lv_atk;
    [HideInInspector] public int attack_lv_speed;
    [HideInInspector] public int attack_lv_count;

    [HideInInspector] public int skill_lv_atk;
    [HideInInspector] public int skill_lv_cooltime;
    [HideInInspector] public int skill_lv_getcount;

    Rigidbody2D rigidbody2D;
    List<Transform> enemy_transform_list = new List<Transform>();

    bool isRun_flag;

    [HideInInspector]
    public int player_exp = 99;

    [HideInInspector]
    public int player_lv;
    private void Start()
    {
        current_hp = max_hp;
        hp_image.fillAmount = current_hp / max_hp;
        rigidbody2D = GetComponent<Rigidbody2D>();
        player_exp = 99;
    }

    private void FixedUpdate()
    {
        
        joystic_localpos = joystic_foreground.GetComponent<RectTransform>().localPosition;

        if (joystic_localpos != Vector2.zero)
        {
            if (!obstacle_flag)
            {
                if (!dash_flag) // 대쉬중에는 막아둔다
                {
                    // 조이스틱 위치에 따른 플레이어 위치 
                    this.transform.position = new Vector2(
                    this.transform.position.x + (joystic_localpos.x * Time.fixedDeltaTime * speed),
                    this.transform.position.y + (joystic_localpos.y * Time.fixedDeltaTime * speed));

                }

                Vector2 dir = Vector2.zero - joystic_localpos;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f;

                this.GetComponent<Animator>().SetBool("isRun", true);

                if (angle > 315 || angle <= 360 && angle > 0 || angle <= 45)
                {
                    this.GetComponent<Animator>().SetFloat("angle", 0);
                }
                if (angle > 225 && angle <= 315)
                {
                    this.GetComponent<Animator>().SetFloat("angle", .4f);
                }
                if (angle > 135 && angle <= 225)
                {
                    this.GetComponent<Animator>().SetFloat("angle", .7f);
                }
                if (angle > 45 && angle <= 135)
                {
                    this.GetComponent<Animator>().SetFloat("angle", 1f);
                }
            }
        }
        else
        {
            this.GetComponent<Animator>().SetBool("isRun", false);
        }
        //new Vector3(this.transform.position.x, this.transform.position.y, theCam.position.z);
        // 카메라 플레이어 따라가기 
        theCam.position = Vector3.SmoothDamp(theCam.position, new Vector3(this.transform.position.x, this.transform.position.y, theCam.position.z), ref velocity, 0.3f);
    }
    Vector3 velocity = Vector3.zero;
  

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
            GameObject ghost_temp = ObjectPoolingManager.instance.GetQueue(ObjectKind.ghost);
            ghost_temp.transform.position = this.transform.position;
            ghost_temp.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;

            yield return new WaitForSeconds(0.1f / 5);
        }

        dash_flag = false;

    }

    bool obstacle_flag;
    public void OnClick_Obstacle()
    {
        if (obstacle_flag)
            return;

        StartCoroutine(Obstacle_Coroutine());

    }
    IEnumerator Obstacle_Coroutine()
    {
        obstacle_flag = true;
        obstacle_obj.GetComponent<Obstacle>().ObstacleHit();
        yield return new WaitForSeconds(0.7f);
        obstacle_flag = false;
    }


    bool hit_flag;
    public void Hit(int damage, GameObject enemy)
    {
        if (hit_flag)
            return;

        current_hp -= damage;
        hp_image.fillAmount = (float)current_hp / max_hp;

        //죽음
        if(current_hp <= 0)
        {
            Game_Over();
        }
        Vector3 dir = enemy.transform.position - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f;

        float draft = 10f;

        if (angle > 315 || angle <= 360 && angle > 0 || angle <= 45) // right   
            rigidbody2D.velocity = new Vector2(draft, 0);
        if (angle > 225 && angle <= 315) // under
            rigidbody2D.velocity = new Vector2(0, -draft);
        if (angle > 135 && angle <= 225) // left
            rigidbody2D.velocity = new Vector2(-draft, 0);
        if (angle > 45 && angle <= 135) // up
            rigidbody2D.velocity = new Vector2(0, draft);

        StartCoroutine(HitCoroutine());
    }

    IEnumerator HitCoroutine()
    {
        hit_flag = true;
        int countTime = 0;
        this.GetComponent<Animator>().SetTrigger("hit");
        yield return new WaitForSeconds(0.15f);
        rigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.15f);
        while (countTime < 3)
        {
            if (countTime % 2 == 0)
                this.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 90);
            else
                this.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 180);
            yield return new WaitForSeconds(0.2f);
            countTime++;
        }
        this.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

        hit_flag = false;
    }

    public void Hp_Recovery(int up)
    {
        current_hp += up;
        if (max_hp < current_hp)
            current_hp = max_hp;
        hp_image.fillAmount = (float)current_hp / max_hp;
        hp_image.fillAmount = (float)current_hp / max_hp;
    }

    public void Exp_Up(int up)
    {
        player_exp += up;
        if (player_exp >= 100)
        {
            ui_canvas.GetComponent<Level_Up>().Panel_Pop();
            player_exp = 0;
            player_lv++;
        }
    }

    bool skill_item_flag = false;
    public void OnClick_SkillItem()
    {
        if (skill_item_flag)
            return;

        StartCoroutine(Skill_item_Coroutine());

    }
    IEnumerator Skill_item_Coroutine()
    {
        skill_item_flag = true;
        skill_item_obj.GetComponent<Skill_Get>().Get(skill_item_obj);
        yield return new WaitForSeconds(0.7f);
        skill_item_flag = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag.Contains("Obstacle"))
        {
            obstacle_obj = collision.gameObject;
            dash_btn.SetActive(false);
            obstacle_btn.SetActive(true);
        }
    }
    //스킬구슬 충돌
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Contains("Skill_Item"))
        {
            dash_btn.SetActive(false);
            skill_item_btn.SetActive(true);
            skill_item_obj = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag.Contains("Obstacle"))
        {
            dash_btn.SetActive(true);
            obstacle_btn.SetActive(false);
        }

        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag.Contains("Skill_Item"))
        {
            dash_btn.SetActive(true);
            skill_item_btn.SetActive(false);
        }
    }
    void Game_Over()
    {
        GameObject smoke = ObjectPoolingManager.instance.GetQueue(ObjectKind.smoke);
        smoke.transform.position = this.transform.position;
        GameOverPanel.GetComponent<GameOver>().Show();
    }
}

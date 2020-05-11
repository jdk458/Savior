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

    int current_hp;

    [Header("카메라")]
    public Transform theCam;
    [Header("조이스틱")]
    public Transform joystic_foreground;
    Vector2 joystic_localpos;
    [Header("체력 이미지")]
    public Image hp_image;
    [Header("대쉬 및 장애물 버튼")]
    public GameObject dash_btn;
    public GameObject obstacle_btn;
    GameObject obstacle_obj;


    Rigidbody2D rigidbody2D;
    List<Transform> enemy_transform_list = new List<Transform>();

    bool isRun_flag;

    private void Start()
    {
        current_hp = max_hp;
        hp_image.fillAmount = current_hp / max_hp;
        rigidbody2D = GetComponent<Rigidbody2D>();
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
        this.GetComponent<Animator>().SetTrigger("hit");
        yield return new WaitForSeconds(0.15f);
        rigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.15f);
        hit_flag = false;
    }

    public void Hp_Recovery(int up)
    {
        current_hp += up;
        if (max_hp < current_hp)
            current_hp = max_hp;
        hp_image.fillAmount = (float)current_hp / max_hp;
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag.Contains("Obstacle"))
        {
            dash_btn.SetActive(true);
            obstacle_btn.SetActive(false);
        }
    }
}

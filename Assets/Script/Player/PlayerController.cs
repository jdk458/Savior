using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;

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

    [Header("UI")]
    public Text lv_up;
    public Image exp_full;
    [Header("UI2")]
    public UI2_Manager uI2_Manager;

    [Header("오디오")]
    public AudioSource dash_AudioSource;
    public AudioSource levelUp_AudioSource;
    public AudioSource move_AudioSource;

    /// <summary>
    ///  스파인
    /// </summary>
    [Header("스파인")]
    public GameObject idle_down;
    public GameObject down_ver;
    public GameObject ver;
    public GameObject up;
    public GameObject up_ver;


    //플레이어 레벨 업 변수 정보
    [HideInInspector] public int character_lv_hp;
    [HideInInspector] public int character_lv_speed;
    [HideInInspector] public int character_lv_exp;

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
    public int player_exp;

    [HideInInspector]
    public int player_lv;
    private void Start()
    {
        player_exp = 90;
        current_hp = max_hp;
        lv_up.text = "LV" + player_lv;
        hp_image.fillAmount = current_hp / max_hp;
        rigidbody2D = GetComponent<Rigidbody2D>();
        exp_full.fillAmount = (float)player_exp / 10;

        character_lv_hp = 1;
        character_lv_speed = 1;
        character_lv_exp = 1;

        attack_lv_atk = 1;
        attack_lv_speed = 1;
        attack_lv_count = 3;

        skill_lv_atk = 1;
        skill_lv_cooltime = 1;
        skill_lv_getcount = 1;
    }


    private void FixedUpdate()
    {
        if (TimeManager.instance.GetTime())
            return;


        joystic_localpos = joystic_foreground.GetComponent<RectTransform>().localPosition;

        if (joystic_localpos != Vector2.zero)
        {
            if (!move_AudioSource.isPlaying)
                GameManager.instance.audioManager.EnvironVolume_Play(move_AudioSource);

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

                if (angle > 337.5f && angle <= 360 || angle > 0 && angle <= 22.5f) // 오
                    Spine_Ani(AniKind.right);
                if (angle > 22.5f && angle <= 67.5f) // 위_왼
                    Spine_Ani(AniKind.up_right);
                if (angle > 67.5f && angle <= 112.5f) // 위
                    Spine_Ani(AniKind.up);
                if (angle > 112.5f && angle <= 157.5f) // 위_오
                    Spine_Ani(AniKind.up_left);
                if (angle > 157.5f && angle <= 202.5f) // 왼
                    Spine_Ani(AniKind.left);
                if (angle > 202.5f && angle <= 247.5f) // 아래_오른쪽
                    Spine_Ani(AniKind.down_right);
                if (angle > 247.5f && angle <= 292.5f) // 아래 
                    Spine_Ani(AniKind.down);
                if (angle > 292.5f && angle <= 337.5f) // 아래_왼쪽 
                    Spine_Ani(AniKind.down_left);
            }
        }
        else
        {
            // idle
            Spine_Ani(AniKind.idle);

            if (move_AudioSource.isPlaying)
                move_AudioSource.Stop();
        }
        //new Vector3(this.transform.position.x, this.transform.position.y, theCam.position.z);
        // 카메라 플레이어 따라가기 
        theCam.position = Vector3.SmoothDamp(theCam.position, new Vector3(this.transform.position.x, this.transform.position.y, theCam.position.z), ref velocity, 0.3f);
    }
    Vector3 velocity = Vector3.zero;

    string currentAniName;
    string currentSkinName;
    void Spine_Ani(AniKind ani)
    {
      

        string aniName = "";
        switch (ani)
        {
            case AniKind.idle:
                aniName = "character001_idle_down";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = Vector2.zero; down_ver.transform.localPosition = new Vector2(2000, 2000); ver.transform.localPosition = new Vector2(2000, 2000); up.transform.localPosition = new Vector2(2000, 2000); up_ver.transform.localPosition = new Vector2(2000, 2000);
                idle_down.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "character001_idle_down", true);
                currentAniName = "character001_idle_down";

                idle_down.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                break;
            case AniKind.up:
                aniName = "character001_move_up";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = new Vector2(2000, 2000); down_ver.transform.localPosition = new Vector2(2000, 2000); ver.transform.localPosition = new Vector2(2000, 2000); up.transform.localPosition = Vector2.zero; up_ver.transform.localPosition = new Vector2(2000, 2000);

                up.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "character001_move_up", true);
                currentAniName = "character001_move_up";

                up.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                break;
            case AniKind.up_left:
                aniName = "character001_move_upL";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = new Vector2(2000, 2000); down_ver.transform.localPosition = new Vector2(2000, 2000); ver.transform.localPosition = new Vector2(2000, 2000); up.transform.localPosition = new Vector2(2000, 2000); up_ver.transform.localPosition = Vector2.zero;
                up_ver.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0,"character001_move_upL", true);
                currentAniName = "character001_move_upL";
                up_ver.GetComponent<SkeletonAnimation>().Skeleton.SetSkin("character001_upL");
                up_ver.GetComponent<SkeletonAnimation>().skeleton.SetSlotsToSetupPose();
                up_ver.GetComponent<SkeletonAnimation>().LateUpdate();
                currentSkinName = "character001_upL";
                up_ver.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                break;
            case AniKind.up_right:
                aniName = "character001_move_upR";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = new Vector2(2000, 2000); down_ver.transform.localPosition = new Vector2(2000, 2000); ver.transform.localPosition = new Vector2(2000, 2000); up.transform.localPosition = new Vector2(2000, 2000); up_ver.transform.localPosition = Vector2.zero;
                up_ver.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "character001_move_upR", true);
                currentAniName = "character001_move_upR";
                currentSkinName = "character001_upR";
                up_ver.GetComponent<SkeletonAnimation>().Skeleton.SetSkin("character001_upR");
                up_ver.GetComponent<SkeletonAnimation>().skeleton.SetSlotsToSetupPose();
                up_ver.GetComponent<SkeletonAnimation>().LateUpdate();
                up_ver.GetComponent<Transform>().rotation = Quaternion.Euler(0, 180, 0);
                break;
            case AniKind.left:
                aniName = "character001_move_left";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = new Vector2(2000, 2000); down_ver.transform.localPosition = new Vector2(2000, 2000); ver.transform.localPosition = Vector2.zero; up.transform.localPosition = new Vector2(2000, 2000); up_ver.transform.localPosition = new Vector2(2000, 2000);
            
                ver.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "character001_move_left", true);
                currentAniName = "character001_move_left";
                ver.GetComponent<SkeletonAnimation>().Skeleton.SetSkin("character001_left");
                currentSkinName = "character001_left";
                ver.GetComponent<SkeletonAnimation>().skeleton.SetSlotsToSetupPose();
                ver.GetComponent<SkeletonAnimation>().LateUpdate();
                ver.GetComponent<Transform>().rotation = Quaternion.Euler(0, 180, 0);
                break;
            case AniKind.right:
                aniName = "character001_move_right";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = new Vector2(2000, 2000); down_ver.transform.localPosition = new Vector2(2000, 2000); ver.transform.localPosition = Vector2.zero; up.transform.localPosition = new Vector2(2000, 2000); up_ver.transform.localPosition = new Vector2(2000, 2000);
         
                ver.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "character001_move_right", true);
                currentAniName = "character001_move_right";
                ver.GetComponent<SkeletonAnimation>().Skeleton.SetSkin("character001_right");
                currentSkinName = "character001_right";
                ver.GetComponent<SkeletonAnimation>().skeleton.SetSlotsToSetupPose();
                ver.GetComponent<SkeletonAnimation>().LateUpdate();
                ver.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                break;
            case AniKind.down:
                aniName = "character001_move_down";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = Vector2.zero; down_ver.transform.localPosition = new Vector2(2000, 2000); ver.transform.localPosition = new Vector2(2000, 2000); up.transform.localPosition = new Vector2(2000, 2000); up_ver.transform.localPosition = new Vector2(2000, 2000);

                idle_down.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "character001_move_down", true);
                currentAniName = "character001_move_down";

                idle_down.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                break;
            case AniKind.down_left:
                aniName = "character001_move_downleft";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = new Vector2(2000, 2000); down_ver.transform.localPosition = Vector2.zero; ver.transform.localPosition = new Vector2(2000, 2000); up.transform.localPosition = new Vector2(2000, 2000); up_ver.transform.localPosition = new Vector2(2000, 2000);
                down_ver.GetComponent<SkeletonAnimation>().Skeleton.SetSkin("character001_downleft");
                currentSkinName = "character001_downleft";
                down_ver.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0,"character001_move_downleft",true);
                down_ver.GetComponent<SkeletonAnimation>().skeleton.SetSlotsToSetupPose();
                down_ver.GetComponent<SkeletonAnimation>().LateUpdate();
                currentAniName = "character001_move_downleft";

                down_ver.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                break;
            case AniKind.down_right:
                aniName = "character001_move_downright";
                if (aniName == currentAniName)
                    return;
                idle_down.transform.localPosition = new Vector2(2000, 2000); down_ver.transform.localPosition = Vector2.zero; ver.transform.localPosition = new Vector2(2000, 2000); up.transform.localPosition = new Vector2(2000, 2000); up_ver.transform.localPosition = new Vector2(2000, 2000);
                down_ver.GetComponent<SkeletonAnimation>().Skeleton.SetSkin("character001_downright");
                currentSkinName = "character001_downright";
                down_ver.GetComponent<SkeletonAnimation>().AnimationName = aniName;
                down_ver.GetComponent<SkeletonAnimation>().skeleton.SetSlotsToSetupPose();
                down_ver.GetComponent<SkeletonAnimation>().LateUpdate();
                currentAniName = "character001_move_downright";

                down_ver.GetComponent<Transform>().rotation = Quaternion.Euler(0, 180, 0);
                break;
            case AniKind.hit:
                idle_down.SetActive(false); down_ver.SetActive(false); ver.SetActive(false); up.SetActive(false); up_ver.SetActive(false);
                break;
            case AniKind.item:
                idle_down.SetActive(false); down_ver.SetActive(false); ver.SetActive(false); up.SetActive(false); up_ver.SetActive(false);
                break;
            case AniKind.dead:
                idle_down.SetActive(false); down_ver.SetActive(false); ver.SetActive(false); up.SetActive(false); up_ver.SetActive(false);
                break;
            default:
                break;
        }
    }

    enum AniKind
    {
        idle,
        up,
        up_left,
        up_right,
        left,
        right,
        down,
        down_left,
        down_right,
        hit,
        item,
        dead
    }

    public void OnClick_Dash()
    {
        if (TimeManager.instance.GetTime())
            return;


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

        GameManager.instance.audioManager.EnvironVolume_Play(dash_AudioSource);

        if (joystic_localpos.x > 45)
            this.transform.DOMoveX(this.transform.position.x + dash_move, 0.1f).SetEase(Ease.Linear);
        if (joystic_localpos.x < -45)
            this.transform.DOMoveX(this.transform.position.x - dash_move, 0.1f).SetEase(Ease.Linear);
        if (joystic_localpos.y > 45)
            this.transform.DOMoveY(this.transform.position.y + dash_move, 0.1f).SetEase(Ease.Linear);
        if (joystic_localpos.y < -45)
            this.transform.DOMoveY(this.transform.position.y - dash_move, 0.1f).SetEase(Ease.Linear);

        GameObject player_dash = null;


        for (int i = 0; i < 5; i++)
        {

            if (idle_down.transform.localPosition.x == 0)
            {
                player_dash = ObjectPoolingManager.instance.GetQueue(ObjectKind.idle_down);
                player_dash.transform.rotation = idle_down.transform.rotation;
            }
            if (down_ver.transform.localPosition.x == 0)
            {
                player_dash = ObjectPoolingManager.instance.GetQueue(ObjectKind.down_ver);
                player_dash.transform.rotation = down_ver.transform.rotation;
                player_dash.GetComponent<SkeletonAnimation>().Skeleton.SetSkin(currentSkinName);
                player_dash.GetComponent<SkeletonAnimation>().skeleton.SetSlotsToSetupPose();
                player_dash.GetComponent<SkeletonAnimation>().LateUpdate();
            }
            if (ver.transform.localPosition.x == 0)
            {
                player_dash = ObjectPoolingManager.instance.GetQueue(ObjectKind.ver);
                player_dash.transform.rotation = ver.transform.rotation;
                player_dash.GetComponent<SkeletonAnimation>().Skeleton.SetSkin(currentSkinName);
                player_dash.GetComponent<SkeletonAnimation>().skeleton.SetSlotsToSetupPose();
                player_dash.GetComponent<SkeletonAnimation>().LateUpdate();
            }
            if (up.transform.localPosition.x == 0)
            {
                player_dash = ObjectPoolingManager.instance.GetQueue(ObjectKind.up);
                player_dash.transform.rotation = up.transform.rotation;
            }
            if (up_ver.transform.localPosition.x == 0)
            {
                player_dash = ObjectPoolingManager.instance.GetQueue(ObjectKind.up_ver);
                player_dash.transform.rotation = up_ver.transform.rotation;
                player_dash.GetComponent<SkeletonAnimation>().Skeleton.SetSkin(currentSkinName);
                player_dash.GetComponent<SkeletonAnimation>().skeleton.SetSlotsToSetupPose();
                player_dash.GetComponent<SkeletonAnimation>().LateUpdate();
            }

            player_dash.transform.position = this.transform.Find("Spine_Ani").position;
            yield return new WaitForSeconds(0.1f / 5);

        }

        dash_flag = false;

    }

    bool obstacle_flag;
    public void OnClick_Obstacle()
    {
        if (TimeManager.instance.GetTime())
            return;


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
        if (TimeManager.instance.GetTime())
            return;

        if (hit_flag)
            return;

        current_hp -= damage;
        hp_image.fillAmount = (float)current_hp / max_hp;

        //죽음
        if(current_hp <= 0)
        {
           // Game_Over();
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
        idle_down.GetComponent<Animator>().SetBool("hit", true);
        down_ver.GetComponent<Animator>().SetBool("hit", true);
        ver.GetComponent<Animator>().SetBool("hit", true);
        up.GetComponent<Animator>().SetBool("hit", true);
        up_ver.GetComponent<Animator>().SetBool("hit", true);
        yield return new WaitForSeconds(0.15f);
        rigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        idle_down.GetComponent<Animator>().SetBool("hit", false);
        down_ver.GetComponent<Animator>().SetBool("hit", false);
        ver.GetComponent<Animator>().SetBool("hit", false);
        up.GetComponent<Animator>().SetBool("hit", false);
        up_ver.GetComponent<Animator>().SetBool("hit", false);
        hit_flag = false;
    }

    public void Hp_Recovery(int up)
    {
        if (TimeManager.instance.GetTime())
            return;

        current_hp += up;
        if (max_hp < current_hp)
            current_hp = max_hp;
        hp_image.fillAmount = (float)current_hp / max_hp;
        hp_image.fillAmount = (float)current_hp / max_hp;
    }

    public void Exp_Up(int up)
    {
        if (TimeManager.instance.GetTime())
            return;

        player_exp += up;
        if (player_exp >= 10)
        {
            uI2_Manager.SkillSelect();
            player_exp = 0;
            player_lv++;
            lv_up.text = "LV" + player_lv;
            GameManager.instance.audioManager.EnvironVolume_Play(levelUp_AudioSource);
            TimeManager.instance.SetTime(true);
        }
        exp_full.fillAmount = (float)player_exp / 100;
    }

    bool skill_item_flag = false;
    public void OnClick_SkillItem()
    {
        if (TimeManager.instance.GetTime())
            return;

        if (skill_item_flag)
            return;

        StartCoroutine(Skill_item_Coroutine());

    }
    IEnumerator Skill_item_Coroutine()
    {
        skill_item_flag = true;
        string skill_name = skill_item_obj.GetComponent<Skill_Item>().skillname;
        this.GetComponent<PlayerSkill>().Get(skill_name);
        ObjectKind skill_marble_type = obstacle_obj.GetComponent<Obstacle>().marble_type;
        ObjectPoolingManager.instance.InsertQueue(skill_item_obj,skill_marble_type);
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

}

using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [Header("몬스터 정보")]
    public string name;
    public ObjectKind objectKind;
    Stage stage;
    MonsterType monsterType;
    [Range(0, 1)] float rigidTime;
    int atk;
    int hp; int current_hp;
    float speed;
    [Header("플레이어 정보")]
    public Transform player_transform;
    [Header("스파인 네임 정보")]
    SkeletonAnimation skeletonAnimation;
    string currentSpineName;
    public string move;
    public string hit;
    public string attack;

    [Header("오디오")]
    public AudioSource hit_nomal_atk_AudioSource;

    Rigidbody2D rigidbody2D;

    private void OnEnable()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        stage = GameManager.instance.monsterManager.GetMonster(name).stage;
        monsterType = GameManager.instance.monsterManager.GetMonster(name).monsterType;
        rigidTime = GameManager.instance.monsterManager.GetMonster(name).rigidTime;
        atk = GameManager.instance.monsterManager.GetMonster(name).atk;
        hp = GameManager.instance.monsterManager.GetMonster(name).hp;
        speed = GameManager.instance.monsterManager.GetMonster(name).speed;

        rigidbody2D = this.GetComponent<Rigidbody2D>();
        current_hp = hp;
    }

    private void OnDisable()
    {
        hit_flag = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    bool hit_flag;
    private void FixedUpdate()
    {
        if (TimeManager.instance.GetTime())
            return;

        if (!hit_flag && !attack_flag)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, player_transform.position, speed / 120f);

            if (player_transform.position.x - this.transform.position.x > 0)
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                this.transform.rotation = Quaternion.Euler(0, 0, 0);

            if (currentSpineName != move)
            {
                currentSpineName = move;
                skeletonAnimation.AnimationState.SetAnimation(0, move, true);
                skeletonAnimation.timeScale = 2;
            }
        }
    }

    public void Hit(int damage, bool sound_flag = false)
    {
        if (TimeManager.instance.GetTime())
            return;

        current_hp -= damage;
        if (current_hp <= 0)
        {
            GameObject smoke = ObjectPoolingManager.instance.GetQueue(ObjectKind.smoke);
            smoke.transform.position = this.transform.position;
            ObjectPoolingManager.instance.InsertQueue(this.gameObject, objectKind);
        }

        else
        {
            GameObject damage_obj = ObjectPoolingManager.instance.GetQueue(ObjectKind.nomal_damage);
            damage_obj.transform.position = this.transform.position;
            damage_obj.GetComponent<Damage>().DamageSet(damage);

            StartCoroutine(Hit_Coroutine());

            if (currentSpineName != hit) // 스파인
            {
                currentSpineName = hit;
                skeletonAnimation.AnimationState.SetAnimation(0, hit, false);
                skeletonAnimation.timeScale = 1;
            }

            if (sound_flag)
            {
                hit_nomal_atk_AudioSource.Play();
            }
        }
    }
    IEnumerator Hit_Coroutine()
    {
        hit_flag = true;
        yield return new WaitForSeconds(0.3f);
        hit_flag = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rigidbody2D.velocity = Vector2.zero;
    }

    bool attack_flag;
    private void OnCollisionEnter2D(Collision2D collision) // 공격
    {
        if (collision.transform.tag.Contains("Player") && !attack_flag)
        {
            if (TimeManager.instance.GetTime())
                return;

            
            collision.transform.GetComponent<PlayerController>().Hit(atk, gameObject);
            rigidbody2D.velocity = Vector2.zero;
            StartCoroutine(Attack_Coroutine());

            if (currentSpineName != attack)
            {
                currentSpineName = attack;
                skeletonAnimation.AnimationState.SetAnimation(0, attack, false);
                skeletonAnimation.timeScale = 1;
            }
        }
    }

    IEnumerator Attack_Coroutine()
    {
        attack_flag = true;
        yield return new WaitForSeconds(0.3f);
        attack_flag = false;
    }
}

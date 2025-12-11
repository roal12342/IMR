using System;
using UnityEngine;



public class Monster : MonoBehaviour
{

    [SerializeField]
    private MonsterData monsterData;

    private float M_HP;  //체력
    private float M_Damage;  //공격력
    private float M_Speed;  //속도
    private float M_AttackTime;  //공격시간

    [SerializeField]
    private ParticleSystem HitEffect;  //피격효과

    private bool IsDie = false;  // 몬스터 사망여부

    private GameObject PlayerObj;  //플레이어 오브젝트
    private Animator monsterAnimator;  //몬스터 애니메이터
    private AudioSource monsterAudio;  //몬스터 오디오소스

    public event Action Death;

    private bool IsStop = false;
    private float IsStopTime = 2f;

    private float Distance; // 플레이어와 몬스터 거리
    private bool IsAttack = false; // 몬스터가 공격중인지
    private float AttackTime = 2f; // 몬스터 공격 쿨타임

    private void Start()
    {
        monsterAnimator = this.GetComponent<Animator>();

        monsterAudio = this.GetComponent<AudioSource>();

        MonsterSetting(monsterData);
    }


    private void MonsterSetting(MonsterData Data)
    {
        M_AttackTime = Data.MonsterAttackTime;
        M_Damage = Data.MonsterDamage;
        M_Speed = Data.MonsterSpeed;
        M_HP = Data.MonsterHP;
    }

    //플레이어한테 이동하는 함수
    private void MonsterMove()
    {
        if (PlayerObj==null)
        {
            PlayerObj = GameObject.FindGameObjectWithTag("Player");

        }
        else
        {
            if (!IsStop)
            {
                monsterAnimator.SetBool("Move", true);

                Distance = Vector3.Distance(this.transform.position, PlayerObj.transform.position);

                if (Distance >= 2)
                {
                    this.transform.Translate(0, 0, M_Speed * Time.deltaTime);
                }
            }

            this.transform.LookAt(PlayerObj.transform);
        }
        
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GunPlayer hitPlayer = other.gameObject.GetComponent<GunPlayer>();

            if (hitPlayer != null && !IsAttack)
            {
                IsAttack = true;
                monsterAnimator.SetTrigger("Attack");
                StartCoroutine(hitPlayer.PlayerDMG(M_Damage));
            }
        }
    }

    private void MonsterStop()
    {
        if(IsStop)
        {
            IsStopTime -= Time.deltaTime;
            if (IsStopTime <= 0)
            {
                IsStop = false;
                IsStopTime = 2f;
            }
        }
    }

    public void Damage(Vector3 hitpos, float GunDMG)
    {
        if(!IsDie)
        {
            monsterAnimator.SetBool("Move", false);
            IsStop = true;

            HitEffect.transform.position = hitpos;
            HitEffect.Play();

            M_HP -= GunDMG;

            if(M_HP <= 0)
            {
                ShopManager.Coin += 300;
                Death();
                IsDie = true;
                monsterAnimator.SetTrigger("Die");
                Destroy(this.gameObject, 2f);
            }
            else
            {
                monsterAudio.PlayOneShot(monsterData.MonsterHitClip);
            }
        }
    }




    void Update()
    {
        if (!IsDie)
        {

            MonsterMove();
            MonsterStop();

            if (IsAttack)
            {
                AttackTime -= Time.deltaTime;
                if (AttackTime <= 0)
                {
                    IsAttack = false;
                    AttackTime = 2f;
                }
            }
        }
    }
}

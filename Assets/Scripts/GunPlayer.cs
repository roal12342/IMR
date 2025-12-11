using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunPlayer : MonoBehaviour
{
    private AudioSource PlayerAudio;

    [SerializeField]
    private GameObject gunShotBtn;

    [SerializeField]
    private GameObject gunReloadBtn;


    private Gun gun;

    [SerializeField]
    private Text GunText; //총알 텍스트
    [SerializeField]
    private Text HP_Text;  // 체력 텍스트

    [SerializeField]
    private GameObject DMG_Pannel;  //데미지 패널

    [SerializeField]
    private AudioClip DMG_Clip;  // 데미지 소리

    [SerializeField]
    private AudioClip Die_Clip;  // 죽는 소리

    public Image TargetImage; // 타겟 이미지

    private bool IsDie = false; //죽음 판정

    public float HP; //체력

    private GameObject Target; // 조준한 몬스터를 담는 변수
    private Vector3 GunHitPos; // 총 맞은 위치



    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //컴포넌트 참조
        PlayerAudio = this.GetComponent<AudioSource>();

        gunShotBtn.GetComponent<Button>().onClick.AddListener(GunShooter);
        gunReloadBtn.GetComponent<Button>().onClick.AddListener(GunReload);
    }


    public void GunShooter()
    {
        if(!IsDie)
        {
            gun.GunShot();

            Handheld.Vibrate(); //진동효과

            if(Target != null)
            {
                Monster TargetMonster = Target.GetComponent<Monster>();
                TargetMonster.Damage(GunHitPos, gun.GunDamage);
            }

        }


    }

    public void GunReload()
    {
        gun.GunReload();
    }


    public IEnumerator PlayerDMG(float MonsterDMG)
    {
        if(!IsDie)
        {
            HP -= MonsterDMG;

            if(HP<=0)
            {
                IsDie = true;
            }

            DMG_Pannel.SetActive(true); 
            yield return new WaitForSeconds(0.1f);
            DMG_Pannel.SetActive(false);
        }
    }


    private void PlayUI()
    {
        HP_Text.text = "HP : " + HP.ToString();
        GunText.text = gun.MagAmmo + " / " + gun.Ammoremain;
    }

    private void RayTarget()
    {
        //Camera.main => 휴대폰카메라
        Ray GunRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit Gunhit;

        if(Physics.Raycast(GunRay, out Gunhit, Mathf.Infinity))
        {
            if (Gunhit.collider.gameObject.tag == "Monster")
            {
                TargetImage.color = Color.red;
                GunHitPos = Gunhit.point;
                Target = Gunhit.collider.gameObject;
            }
            
        }
        else
        {
            TargetImage.color = Color.white;
            Target = null;
            GunHitPos = Vector3.zero;
        }
    }
    void Update()
    {
        if(gun == null)
        {
            gun = GameObject.FindFirstObjectByType<Gun>();
        }

        if(!IsDie)
        {
            RayTarget();
            PlayUI();
        }
    }
}

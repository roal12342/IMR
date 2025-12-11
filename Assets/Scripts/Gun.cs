using System.Collections;
using UnityEngine;



//1. 총 데이터 받아온다.
//2. 총 발사
//3. 총 재장전
public class Gun : MonoBehaviour
{

    public GunData gunData;
    public int Ammoremain;  //남은 총알
    public int MagAmmo;  //탄창 최대 총알
    public float GunDamage;  //총기 데미지

    private float LastAttackTime; //마지막 총쏜 시점
    private float ReloadTime; //재장전 시간
    private float GunAttackTime; //총기 연사속도

    private AudioSource GunAudio; //총 오디오

    public ParticleSystem FlashEffect; //총구 화염 효과
    public ParticleSystem ShellEffect; //탄피 효과

    private bool IsReady = true; //총기 발사 준비 여부



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GunAudio = this.GetComponent<AudioSource>();
        GunSetting(gunData);
    }

    private void GunSetting(GunData gunData)
    {
        GunDamage = gunData.GunDamage;
        Ammoremain = gunData.Ammoremain;
        MagAmmo = gunData.MagAmmo;
        ReloadTime = gunData.GunReloadTime;
        GunAttackTime = gunData.GunAttackTime;
    }

    //총 쏘는 함수
    public void GunShot()
    {
        if (IsReady)
        {
            if(MagAmmo > 0 && Time.time > LastAttackTime + GunAttackTime)
            {
                //총 발사 이펙트
                FlashEffect.Play();
                ShellEffect.Play();
                //총 소리 재생
                GunAudio.PlayOneShot(gunData.ShotClip);
                //총알 감소
                MagAmmo--;
                LastAttackTime = Time.time;
            }
        }
        
        
    }

    public void GunReload()
    {
        if (IsReady)
        {
            if(Ammoremain > 0 || MagAmmo < gunData.MagAmmo)
            {
                StartCoroutine(GunReloadTime());
            }
        }
    }
    private IEnumerator GunReloadTime()
    {
        IsReady = false;
        GunAudio.PlayOneShot(gunData.ReloadClip);
        yield return new WaitForSeconds(ReloadTime);

        int ammoFill = gunData.MagAmmo - MagAmmo;
        if (ammoFill > Ammoremain)
        {
            ammoFill = Ammoremain;
        }

        MagAmmo += ammoFill;
        Ammoremain -= ammoFill;

        IsReady = true;
    }
}

using UnityEngine;



[CreateAssetMenu(menuName = "ScriptableObjects/GunData", fileName = "GunData")]
public class GunData : ScriptableObject
{
    public AudioClip ShotClip; //발사소리
    public AudioClip ReloadClip;  //장전소리
    public float GunDamage = 10f;  //총기 데미지
    public int Ammoremain = 100;  //남은 총알
    public int MagAmmo = 30;  //탄창 최대 총알
    public float GunAttackTime = 0.12f;  //총기 연사속도
    public float GunReloadTime = 1.8f;  //총기 재장전 속도



}

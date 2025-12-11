using UnityEngine;



[CreateAssetMenu(menuName = "ScriptableObjects/MonsterData", fileName = "MonsterData")]

public class MonsterData : ScriptableObject
{
    public float MonsterHP;
    public float MonsterSpeed;
    public float MonsterDamage;
    public float MonsterAttackTime;

    public AudioClip MonsterDieClip;
    public AudioClip MonsterAttackClip;
    public AudioClip MonsterHitClip;


}

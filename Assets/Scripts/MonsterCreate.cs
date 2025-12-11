using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.InteropServices;



// 각 스테이지마다 다른 몬스터를 생성하는 스크립트




public class MonsterCreate : MonoBehaviour
{
    private bool IsStart = false;
    public static int Stage = 1;

    [SerializeField]
    private Text StageText;

    [SerializeField]
    private Text GameText;

    public List<GameObject> MonsterList = new List<GameObject>();

    private int MonsterCount = 8;

    private bool IsCreate = false;

    [SerializeField]
    private float GameStartTime = 5f;

    [SerializeField]
    private GameObject StartUI;

    [SerializeField]
    private GameObject NextUI;

    [SerializeField]
    private GameObject[] StageMonsters;

    void Start()
    {
        
    }

    public void NextBtn()
    {
        StartUI.SetActive(true);
        IsStart = false;
        IsCreate = false;
        GameStartTime = 5f;
        Stage++;
        NextUI.SetActive(false);
        GameText.text = "곧 게임이 시작됩니다 :" + GameStartTime.ToString("F1");
    }

    void Update()
    {
        if(IsStart)
        {
            GameStartTime -= Time.deltaTime;
            GameText.text = "곧 게임이 시작됩니다 :" + GameStartTime.ToString("F1");
            if (GameStartTime <= 0)
            {
                //IsStart = false;
                StartUI.SetActive(false);
                if(!IsCreate)
                {
                    StageCreateM();
                }
                else
                {
                    if(MonsterList.Count <= 0)
                    {
                        NextUI.SetActive(true);
                    }
                }
            }
        }
        
    }

    private void StageCreateM()
    {
        IsCreate = true;

        for (int i = 0; i < MonsterCount * Stage; i++)
        {
            float Create_X;
            float Create_Z;

            int RandomRate = Random.Range(0, 4);

            if (RandomRate == 0)
            {
                Create_X = Random.Range(-20, -60);
                Create_Z = Random.Range(-20, -60);

                Monster CreateMonster
                    = Instantiate(StageMonsters[Stage - 1].GetComponent<Monster>(), new Vector3(Create_X, -1f, Create_Z), Quaternion.identity);

                MonsterList.Add(CreateMonster.gameObject);

                CreateMonster.Death += () => MonsterList.Remove(CreateMonster.gameObject);

            }
            else if (RandomRate == 1)
            {
                Create_X = Random.Range(20, 60);
                Create_Z = Random.Range(20, 60);
                Monster CreateMonster
                    = Instantiate(StageMonsters[Stage - 1].GetComponent<Monster>(), new Vector3(Create_X, -1f, Create_Z), Quaternion.identity);
                MonsterList.Add(CreateMonster.gameObject);
                CreateMonster.Death += () => MonsterList.Remove(CreateMonster.gameObject);
            }
            else if (RandomRate == 2)
            {
                Create_X = Random.Range(-20, -60);
                Create_Z = Random.Range(20, 60);
                Monster CreateMonster
                    = Instantiate(StageMonsters[Stage - 1].GetComponent<Monster>(), new Vector3(Create_X, -1f, Create_Z), Quaternion.identity);
                MonsterList.Add(CreateMonster.gameObject);
                CreateMonster.Death += () => MonsterList.Remove(CreateMonster.gameObject);
            }
            else if (RandomRate == 3)
            {
                Create_X = Random.Range(20, 60);
                Create_Z = Random.Range(-20, -60);
                Monster CreateMonster
                    = Instantiate(StageMonsters[Stage - 1].GetComponent<Monster>(), new Vector3(Create_X, -1f, Create_Z), Quaternion.identity);
                MonsterList.Add(CreateMonster.gameObject);
                CreateMonster.Death += () => MonsterList.Remove(CreateMonster.gameObject);
            }
        }
    }

    public void GameStartBTN()
    {
        if (!IsStart)
        {
            IsStart = true;
        }
    }
}

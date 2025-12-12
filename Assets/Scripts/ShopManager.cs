using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private GameObject WeaponPannel;

    [SerializeField]
    private GameObject ItemPannel;

    [SerializeField]
    private Text CoinText;
    private AudioSource ShopAudio;

    [SerializeField]
    private AudioClip ShopClip;
    private InvenManager invenManager;

    public static int Coin = 2000;

    void Start()
    {
        CoinText.text = "Coin : " + Coin.ToString();
        invenManager = this.GetComponent<InvenManager>();
        ShopAudio = this.GetComponent<AudioSource>();
    }

    public void PurChase(string ItemName)
    {
        if (ItemName == "assault3")
        {
            if(Coin >= 2000)
            {
                Coin -= 2000;
                ShopAudio.PlayOneShot(ShopClip);
                invenManager.GetItem(ItemName, 1);
                CoinText.text = "Coin : " + Coin.ToString();
                EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
            }
        }
        if (ItemName == "ShotGun")
        {
            if (Coin >= 2500)
            {
                Coin -= 2500;
                ShopAudio.PlayOneShot(ShopClip);
                invenManager.GetItem(ItemName, 2);
                CoinText.text = "Coin : " + Coin.ToString();
                EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
            }
            
        }
        if (ItemName == "G4")
        {
            if (Coin >= 1000)
            {
                Coin -= 1000;
                ShopAudio.PlayOneShot(ShopClip);
                invenManager.GetItem(ItemName, 3);
                CoinText.text = "Coin : " + Coin.ToString();
                EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
            }
        }

        if (ItemName == "Ammo")
        {
            if (Coin >= 1500)
            {
                Coin -= 1500;
                ShopAudio.PlayOneShot(ShopClip);
                invenManager.GetItem(ItemName, 4);
                invenManager.ItemCount[0]++;
                invenManager.ItemCountText[0].text = "X: " + invenManager.ItemCount[0];
            }
        }
        if (ItemName == "HP")
        {
            if (Coin >= 2000)
            {
                Coin -= 2000;
                ShopAudio.PlayOneShot(ShopClip);
                invenManager.GetItem(ItemName, 5);
                invenManager.ItemCount[1]++;
                invenManager.ItemCountText[1].text = "X: " + invenManager.ItemCount[1];
            }
        }
        if (ItemName == "STOP")
        {
            if (Coin >= 2500)
            {
                Coin -= 2500;
                ShopAudio.PlayOneShot(ShopClip);
                invenManager.GetItem(ItemName, 6);
                invenManager.ItemCount[2]++;
                invenManager.ItemCountText[2].text = "X: " + invenManager.ItemCount[2];
            }
        }
    }

    void Update()
    {
        CoinText.text = "Coin : " + Coin.ToString();
    }
}

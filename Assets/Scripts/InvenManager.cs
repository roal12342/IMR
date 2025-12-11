using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvenManager : MonoBehaviour
{

    [SerializeField]
    private Image[] ItemImage;

    [SerializeField]
    private GameObject ChangeUI;

    [SerializeField]
    private GameObject InvenUI;

    [SerializeField]
    private Image ChangeUI_Image;

    private string UseItemName;
    private GameObject ChangeGun;
    private GunPlayer gunPlayer;
    private ShopManager shopManager;

    public static bool IsStop = false;
    private float StopTime = 5f;

    public int[] ItemCount;
    public Text[] ItemCountText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void GetItem(string ItemName, int ItemNumber)
    {
        Sprite LoadImage = Resources.Load<Sprite>(ItemName);
        ItemImage[ItemNumber].sprite = LoadImage;
    }

    public void ItemUse()
    {
        if(EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite != null)
        {
            ChangeUI.SetActive(true);
            ChangeUI_Image.sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
            UseItemName = ChangeUI_Image.sprite.name;
        }
    }

    public void ChangeItem()
    {
        GameObject hasGun = GameObject.FindGameObjectWithTag("Gun");
        Destroy(hasGun);
        ChangeGun = Resources.Load<GameObject>(UseItemName+"Obj");
        ChangeGun = Instantiate(ChangeGun);

        ChangeGun.transform.parent = Camera.main.transform;

        if(UseItemName == "assault3")
        {
            ChangeGun.transform.localPosition = new Vector3(0.22f, -0.83f, 0.8f);
            ChangeGun.transform.localRotation = Quaternion.identity;
        }
        else if(UseItemName == "ShotGun")
        {
            ChangeGun.transform.localPosition = new Vector3(0.32f, -0.81f, 2.64f);
            ChangeGun.transform.localRotation = Quaternion.identity;

        }
        else if(UseItemName == "G4")
        {
            ChangeGun.transform.localPosition = new Vector3(0.06f, - 0.14f, 0.4f);
            ChangeGun.transform.localRotation = Quaternion.identity;

        }
        else if(UseItemName == "M16")
        {
            ChangeGun.transform.localPosition = new Vector3(0.2f, -0.8f, 1.5f);
            ChangeGun.transform.localRotation = Quaternion.identity;

        }
        ChangeUI.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

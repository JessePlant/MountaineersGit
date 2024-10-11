using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SherpaShopKeeper : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shopkeeperDialogUI;
    public Button ARbuy;
    public Button exitBTN;
    public Button ARequipBTN;
    public Button LSRbuy;
    public Button LSRequipBTN;
    public Button SHequip;
    public Button SHbuy;
    public GameObject buyPanelUI;
    public GameObject exitPanelUI;
    public AttackController activeGun;

    [SerializeField]
    Interface guninfo;
    public Canvas shopCanvas;
    public float arPrice = 1000;

    void Start()
    {
        CheckPurchased();
    }

    void Update()
    {}

    void CheckPurchased(){
        if(SaveManager.Instance.assaultRifle){
            ARbuy.gameObject.SetActive(false);
            ARequipBTN.gameObject.SetActive(true);
        }
        else{
            ARbuy.gameObject.SetActive(true);
            ARequipBTN.gameObject.SetActive(false);
            ARbuy.interactable = SaveManager.Instance.money >= arPrice;
            /*
            TextMeshProUGUI arText = ARbuy.gameObject.GetComponent<TextMeshProUGUI>();
            arText.text = "$" + arPrice;
            */
        }
        /*
        if(SaveManager.Instance.LazerRifle){
            LSRbuy.gameObject.SetActive(false);
            LSRequipBTN.gameObject.SetActive(true);
        }
        else{
            LSRbuy.gameObject.SetActive(true);
            LSRequipBTN.gameObject.SetActive(false);
        }
        */
    }
    public void BuyGun() 
    {
        SaveManager.Instance.money -= 1000;
        SaveManager.Instance.assaultRifle = true;
        SaveManager.Instance.Save();
        CheckPurchased();
    }

    public void equipGun()
    {
        // Check which button is clicked to equip.
        activeGun.currentGun = guninfo.guns[0];
        shopCanvas.gameObject.SetActive(false);
    }
}

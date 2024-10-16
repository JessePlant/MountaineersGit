using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject bulletPrefab;
    public float lastTime = 0; // Remember to implement the cooldown
    public CameraController cameraController;
    public Vector3 mousePos;
    public Vector3 worldPos;
    public int ShotsFired;
    public TextMeshProUGUI reloadingText;
    public Canvas inGameUI;
    public Guns currentGun;
    public SherpaShopKeeper SherpaShopKeeper;
    public float pulseSpeed = 1f;
    public List<float> cooldownTimes;
    public bool isReloading;
    public Vector3 instantiatePos;
    
    void Start()
    {
        ShotsFired = 0;
        inGameUI.gameObject.SetActive(true);
        reloadingText.gameObject.SetActive(false);
        isReloading = false;
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGun!=null && !SherpaShopKeeper.isInShop && !isReloading)
        {
        Reload();
        Shoot(cameraController);
        }
    }
    public void Shoot(CameraController cameraController)
    {
        if (cameraController.onGert)
        {
            if (Input.GetMouseButtonDown(0) && !isReloading && !IngameMenuManager.inMenu)
            {
                ShotsFired++;
                print("Shooting" + ShotsFired);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.Log("Mouse Position:"+ mousePos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    print("Gert Pos"+cameraController.target1.transform.position);   
                    worldPos = hit.point;
                    instantiatePos = new Vector3(cameraController.target1.transform.position.x, cameraController.target1.transform.position.y+0.4f,cameraController.target1.transform.position.z);
                    print("Instantiate Pos"+ instantiatePos);
                    print("Hit Pos"+ worldPos);
                    Instantiate(bulletPrefab, instantiatePos, cameraController.target1.transform.rotation);
                }
                else
                {
                    Debug.Log("No hit");
                }
            }
        }
    }
    void Reload()
    {
        if(ShotsFired >= currentGun.clipSize){
            //Reload
            isReloading = true;
            StartCoroutine(ReloadCoroutine());
        }
    }
    IEnumerator ReloadCoroutine()
    {
        reloadingText.gameObject.SetActive(true);
        StartCoroutine(PulseText());
        yield return new WaitForSeconds(currentGun.reloadSpeed);
        reloadingText.gameObject.SetActive(false);
        isReloading = false;
        ShotsFired = 0;
    }
    IEnumerator PulseText()
    {
        while (reloadingText.gameObject.activeSelf)
        {
            // Pulsing effect by scaling the text
            reloadingText.transform.localScale = Vector3.one * (1 + Mathf.PingPong(Time.time * pulseSpeed, 0.5f));
            yield return null; // Wait for the next frame
        }
    }
}

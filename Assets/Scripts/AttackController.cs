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
    void Start()
    {
        ShotsFired = 0;
        inGameUI.gameObject.SetActive(true);
        reloadingText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGun!=null){
        print(currentGun.name);
        Reload();
        Shoot(cameraController);
        }
    }
    public void Shoot(CameraController camera)
    {
        print("camera test"+ camera.onGert);

        if (camera.onGert)
        {
            print("Camera on Gert");
            if (Input.GetMouseButtonDown(0))
            {
                ShotsFired++;
                print("Shooting" + ShotsFired);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.Log("Mouse Position:"+ mousePos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    worldPos = hit.point;
                    print(worldPos);
                    Instantiate(bulletPrefab, camera.target1.transform.position, Quaternion.identity);
                }
            }
        }
    }
    void Reload()
    {
        if(ShotsFired >= currentGun.clipSize){
            //Reload
            StartCoroutine(ReloadCoroutine());
        }
    }
    IEnumerator ReloadCoroutine()
    {
        reloadingText.gameObject.SetActive(true);
        StartCoroutine(PulseText());
        yield return new WaitForSeconds(currentGun.reloadSpeed);
        reloadingText.gameObject.SetActive(false);
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

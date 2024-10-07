using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject bulletPrefab;
    public float cooldown = 0.1f;
    public float speed = 3.5f;
    public float lastTime = 0; // Remember to implement the cooldown
    public CameraController cameraController;
    public Vector3 mousePos;
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {
        Shoot(cameraController);
    }
    public void Shoot(CameraController camera)
    {
        if (camera.onGert)

        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePos = Input.mousePosition;
                RaycastHit hit;
                if (Physics.Raycast(cameraController.target1.transform.position, cameraController.target1.transform.forward, out hit))
                {
                    print(hit);
                }
                Instantiate(bulletPrefab, camera.target1.transform.position, Quaternion.identity);
                
            }
        }
    }


}

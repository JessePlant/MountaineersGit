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
    public Vector3 worldPos;
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
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.Log("Mouse Position:"+ mousePos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    worldPos = hit.point;
                    print(worldPos);
                }
                Instantiate(bulletPrefab, camera.target1.transform.position, Quaternion.identity);
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera cam;
    public CameraController cameraController;
    public AttackController attackController;
    public float speed = 3.5f;
    public bool isMoving = false;
    Vector3 mousePos;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        attackController = GameObject.Find("AttackController").GetComponent<AttackController>();
        dir = calcDirection(cameraController);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir*speed*Time.deltaTime);
    }
    public Vector2 calcDirection(CameraController camera)
    {
        
        Vector3 direction = new Vector3(attackController.worldPos.x - camera.target1.transform.position.x, attackController.worldPos.y - camera.target1.transform.position.y,camera.target1.transform.position.z);
        isMoving = true;
        return direction.normalized;
    }


}

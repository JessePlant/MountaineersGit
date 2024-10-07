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
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        attackController = GameObject.Find("AttackController").GetComponent<AttackController>();
        mousePos = Input.mousePosition;
        Debug.Log("MousePosition"+ mousePos);
        Debug.Log("Bullet Position"+ transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir*speed*Time.deltaTime);
    }
    public Vector2 calcDirection(CameraController camera)
    {
        
        Vector2 direction = new Vector2(attackController.mousePos.x - camera.target1.transform.position.x, attackController.mousePos.y - camera.target1.transform.position.y);
        isMoving = true;
        return direction.normalized;
    }


}

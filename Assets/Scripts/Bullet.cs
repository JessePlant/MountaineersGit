using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    private Camera cam;
    public CameraController cameraController;
    public AttackController attackController;
    public Transform gert;
    //public float speed = 3.5f;
    public bool isMoving = false;
    Vector3 mousePos;
    Vector3 dir;
    public Vector3 enemyPos;
    //public float damage = 10;
    public bool didHit; 
    EnemyBehaviour e;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        gert = GameObject.Find("Gert").GetComponent<Transform>();
        attackController = GameObject.Find("AttackController").GetComponent<AttackController>();
        dir = calcDirection(cameraController);
        dir.z = gert.transform.position.z;
        print("Direction: "+dir);
        didHit = hitEnemy(gert);
    }

    // Update is called once per frame
    void Update()
    { 
        if(Vector3.Distance(gert.position, transform.position)>10 || transform.position == enemyPos)
        {
            Destroy(gameObject);
        }
        transform.Translate(dir*attackController.currentGun.speed*Time.deltaTime);
    }

    public Vector2 calcDirection(CameraController camera)
    {
        
        Vector3 direction = new Vector3(attackController.worldPos.x - camera.target1.transform.position.x, attackController.worldPos.y - camera.target1.transform.position.y,camera.target1.transform.position.z);
        isMoving = true;
        return direction.normalized;
    }


    bool hitEnemy(Transform player)
    {
        RaycastHit hit;
        if(Physics.Raycast(player.position,dir, out hit))
        {
            e  = hit.transform.GetComponent<EnemyBehaviour>();
            if(e != null)
            {
                print("Taking Damage");
                return true;
            }
        }
        return false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if(didHit && collision.gameObject.tag == "Enemy"){
            e.TakeDamage(attackController.currentGun.attackDamage);
            Destroy(gameObject);
        }   
    }
}

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
    //public float attackController.currentGun.speed = 3.5f;
    public bool isMoving = false;
    Vector3 mousePos;
    Vector3 dir;
    public Vector3 enemyPos;
    //public float damage = 10;
    public bool didHit; 
    EnemyBehaviour e;
    public GameObject relativeToPlayer;
    Vector3 faceNormal;
    Vector2 direction2D;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        gert = GameObject.Find("Gert").GetComponent<Transform>();
        attackController = GameObject.Find("AttackController").GetComponent<AttackController>();
        relativeToPlayer = GameObject.Find("Player");
        dir = calcDirection(cameraController);
        print("Camera thinks"+ cameraController.target1.transform.position);
        print(gert.transform.position);
        print("Direction: "+dir);
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
        Vector2 direction;
        if(!cameraController.Front){
            print("using Other");
            direction = new Vector2(attackController.worldPos.z - camera.target1.transform.position.z, attackController.worldPos.y - camera.target1.transform.position.y);
        }
        else{
            print("using Normal");
        direction = new Vector2(attackController.worldPos.x - camera.target1.transform.position.x, attackController.worldPos.y - camera.target1.transform.position.y);
        }
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
        print("Collided");
        print("Tag"+collision.gameObject.tag);
        if(collision.gameObject.tag.Equals("Enemy")){
            print("Hit Enemy");
            e = collision.gameObject.GetComponent<EnemyBehaviour>();
            e.TakeDamage(attackController.currentGun.attackDamage);
            Destroy(gameObject);
        }   
    }
}

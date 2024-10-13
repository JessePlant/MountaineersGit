using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public Transform Gert;

    public Transform Emily;
    public float moveSpeed;
    public float timeBetweenAttacks = 2f;
    public bool alreadyAttacked;
    public float attackRange;
    public bool inAttackRange;
    public LayerMask playerLayer;
    public AttackController attackController;
    public CameraController cameraController;
    PhysicalState GertState;
    PhysicalState EmilyState;
    public Transform target;
    int RandomTarget;

    [Header("Health Stuff")]
    public Slider healthBar;
    public Vector3 healthBarOffset= new Vector3(0,1,0);
    public float maxHealth = 30f;
    public float currentHealth = 30f;
    public bool canMove = true;
    void Awake(){
        Gert = GameObject.Find("Gert").GetComponent<Transform>();
        Emily = GameObject.Find("Emily").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        GertState = GameObject.Find("Gert").GetComponent<PhysicalState>();
        EmilyState = GameObject.Find("Emily").GetComponent<PhysicalState>();
        attackController = GameObject.Find("AttackController").GetComponent<AttackController>();
        agent.autoTraverseOffMeshLink = true;
        attackRange = 1.5f;
        alreadyAttacked = false;
        RandomTarget = Random.Range(0, 2);
        if(RandomTarget == 0){
            target = Gert;
            Debug.Log("Current Target is Gert");
        }
        else{
            target = Emily;
             Debug.Log("Current Target is Emily");
        }
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

   
    // Update is called once per frame
    void Update()
    {
        if(!SherpaShopKeeper.isInShop && canMove)
        {
        healthBar.transform.position = transform.position + healthBarOffset;
        healthBar.transform.LookAt(Camera.main.transform); 
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if(!inAttackRange) Chase();
        else Attack();
        }
    }
    public void Chase()
    {
        agent.SetDestination(target.position);
    }

    public void Attack()
    {
        Debug.Log("Going to Attack");
        agent.SetDestination(transform.position);
        Debug.Log("Already Attacked "+alreadyAttacked);       
        if(!alreadyAttacked){
            Debug.Log("Attack Commencing");
            alreadyAttacked = true;
            // healthManager.Playerdmg();
            if(RandomTarget == 0 )
            {
                GertState.Damage(10);
            }
            else
            {
                EmilyState.Damage(10);
            }
            StartCoroutine(ResetAttack());
        }
    }

    IEnumerator ResetAttack()
    {
        Debug.Log("Coroutine started, waiting for: " + timeBetweenAttacks + " seconds");
        yield return new WaitForSecondsRealtime(3f);
        Debug.Log("Starting Coroutine");
        alreadyAttacked = false;
    }
    
    public void TakeDamage(float damage){
        currentHealth -= damage;
        print("Current Health"+currentHealth);
        print(currentHealth);
        if(currentHealth <= 0){
            Destroy(gameObject);
            SaveManager.Instance.money += 5;
            SaveManager.Instance.Save();
        }
        else{
            healthBar.value = currentHealth;
            StartCoroutine(shellShock());
        }
    }
    IEnumerator shellShock()
    {
        canMove = false;
        if(attackController.currentGun.name == "Lazer Rifle"){
            yield return new WaitForSecondsRealtime(1);
        }
        else{
            yield return new WaitForSecondsRealtime(0.5f);
        }
        canMove = true;
    }
}

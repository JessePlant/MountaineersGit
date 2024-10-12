using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting;

public class EnemyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public Transform Gert;
    public float moveSpeed;
    public float timeBetweenAttacks = 5f;
    public HealthManager healthManager;
    public bool alreadyAttacked;
    public float attackRange;
    public bool inAttackRange;
    public LayerMask playerLayer;
    public float enemyHealth = 30f;

    void Start(){
        Gert = GameObject.Find("Gert").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = true;
        attackRange = 1f;
        alreadyAttacked = false;
    }

   
    // Update is called once per frame
    void Update()
    {
        
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if(!inAttackRange) Chase();
        else Attack();
    }
    public void Chase(){
        agent.SetDestination(Gert.position);
    }

    public void Attack(){
        Debug.Log("Going to Attack");
        agent.SetDestination(transform.position);
        transform.LookAt(Gert);
        Debug.Log("Already Attacked "+alreadyAttacked);       
        if(!alreadyAttacked){
            Debug.Log("Attack Commencing");
            alreadyAttacked = true;
            healthManager.Playerdmg();
            StartCoroutine(ResetAttack());
        }
    }
    IEnumerator ResetAttack(){
    Debug.Log("Coroutine started, waiting for: " + timeBetweenAttacks + " seconds");
        yield return new WaitForSecondsRealtime(timeBetweenAttacks);
        Debug.Log("Starting Coroutine");
        alreadyAttacked = false;
    }
    
    public void TakeDamage(float damage){
        enemyHealth -= damage;
        print(enemyHealth);
        if(enemyHealth <= 0){
            Destroy(gameObject);
            SaveManager.Instance.money += 5;
        }
        else{
            agent.SetDestination(transform.position);
        }
    }
}

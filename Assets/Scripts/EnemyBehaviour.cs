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
    public float timeBetweenAttacks;
    public HealthManager healthManager;
    bool alreadyAttacked;
    public float attackRange;
    public bool inAttackRange;
    public LayerMask playerLayer;

    void Start(){
        Gert = GameObject.Find("Gert").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = true;
        healthManager = GameObject.Find("HealthManager").GetComponent<HealthManager>();
        attackRange = 1f;
    }

   
    // Update is called once per frame
    void Update()
    {
        
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if(!inAttackRange) Chase();
        else Attack();
    }
    public void Chase(){
        Debug.Log("Chasing");
        agent.SetDestination(Gert.position);
    }

    public void Attack(){
        Debug.Log("Attacking");
        agent.SetDestination(transform.position);
        transform.LookAt(Gert);
        if(!alreadyAttacked){
            healthManager.Playerdmg("Gert");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    void ResetAttack(){
        alreadyAttacked = false;
    }
}

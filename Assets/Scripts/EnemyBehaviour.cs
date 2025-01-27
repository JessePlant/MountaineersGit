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
    public Vector3 healthBarOffset = new Vector3(0, 1, 0);
    public float maxHealth = 30f;
    public float currentHealth = 30f;
    public bool canMove = true;
    public IngameMenuManager ingameMenuManager;
    public GameObject animator;
    Animator anim;
    void Awake() {
        if (transform.position.y < 0) {
            Destroy(gameObject);
        }
        Gert = GameObject.Find("Gert").GetComponent<Transform>();
        Emily = GameObject.Find("Emily").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        GertState = GameObject.Find("Gert").GetComponent<PhysicalState>();
        EmilyState = GameObject.Find("Emily").GetComponent<PhysicalState>();
        attackController = GameObject.Find("AttackController").GetComponent<AttackController>();
        anim = animator.GetComponent<Animator>();
        ingameMenuManager = GameObject.Find("inGameGUI").GetComponent<IngameMenuManager>();
        attackRange = 1.5f;
        alreadyAttacked = false;
        RandomTarget = Random.Range(0, 2);
        if (RandomTarget == 0) {
            target = Gert;
            Debug.Log("Current Target is Gert");
        }
        else {
            target = Emily;
            Debug.Log("Current Target is Emily");
        }
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        agent.updateRotation = false;
        transform.rotation = Quaternion.Euler(new Vector3(4.001f, -0.107f, -2.003f));
    }


    // Update is called once per frame
    void Update()
    {
        if (!SherpaShopKeeper.isInShop && canMove)
        {
            healthBar.transform.position = transform.position + healthBarOffset;
            healthBar.transform.LookAt(Camera.main.transform);
            inAttackRange = false;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, playerLayer);
            if (hitColliders.Length > 0)
            {
                int playersInRange = 0;
                foreach (Collider hitCollider in hitColliders)
                {
                    if (hitCollider.CompareTag("Player"))
                    {
                        playersInRange++;
                    }
                    if (hitCollider.name == target.GetComponent<Collider>().name)
                    {
                        inAttackRange = true;
                    }
                }
                if (!inAttackRange && playersInRange > 0)
                {
                    target = (target == Gert) ? Emily : Gert;
                }
            }

            if (!inAttackRange) Chase();
            else Attack();
        }
    }
    public void Chase()
    {
        anim.speed = 1;
        agent.SetDestination(target.position);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        print("Current Health" + currentHealth);
        print(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            SaveManager.Instance.money += 5;
            SaveManager.Instance.Save();
        }
        else
        {
            healthBar.value = currentHealth;
            StartCoroutine(shellShock());
        }
    }

    public void Attack()
    {

        Debug.Log("Going to Attack");
        agent.SetDestination(transform.position);
        Debug.Log("Already Attacked " + alreadyAttacked);
        if (!alreadyAttacked)
        {
            Debug.Log("Attack Commencing");
            alreadyAttacked = true;
            anim.speed = 0.5f;
            // healthManager.Playerdmg();
            if (target == Gert)
            {
                if (RandomTarget == 0)
                {
                    GertState.Damage(10);
                    ingameMenuManager.PlayerHitFeedback();

                }
                else
                {
                    EmilyState.Damage(10);
                    ingameMenuManager.PlayerHitFeedback();
                }
                StartCoroutine(ResetAttack());
            }
        }
    }

    IEnumerator ResetAttack()
    {
        anim.speed = 0f;
        Debug.Log("Coroutine started, waiting for: " + timeBetweenAttacks + " seconds");
        yield return new WaitForSecondsRealtime(3f);
        Debug.Log("Starting Coroutine");
        alreadyAttacked = false;
    }

        
    IEnumerator shellShock()
    {
        canMove = false;
        if (attackController.currentGun.name == "Lazer Rifle") {
            yield return new WaitForSecondsRealtime(1);
        }
        else {
            yield return new WaitForSecondsRealtime(0.5f);
        }
        canMove = true;
    }
    
}

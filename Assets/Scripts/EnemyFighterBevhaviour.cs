using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFighterBehaviour : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] Transform enemyRotationObject;
    [SerializeField] GameObject alertNotification;
    [SerializeField] GameObject attackAreaNotification;

    [Header("Behaviour stats")]
    [SerializeField] float detectionRadius;
    [SerializeField] float attackRadius;

    [Header("Attack stats")]
    [SerializeField] float fireRate;
    [SerializeField] float attackWindUp;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackDuration;
    [SerializeField] int attackDamage;

    private bool targetingPlayer;
    private bool attackingPlayer;
    private bool inAttack;
    private bool inWindUp;
    private float attackCooldownTimer;
    private float attackTimer;
    Vector3 attackDirection;
    private Transform player;
    private NavMeshAgent navAgent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        navAgent = GetComponent<NavMeshAgent>();
        targetingPlayer = false;
        alertNotification.SetActive(false);
        attackAreaNotification.SetActive(false);
        attackCooldownTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckRange();

        if (targetingPlayer && !inAttack && !inWindUp)
        {
            MoveToPlayer();

            if(!inAttack || !inWindUp)
                RotateToPlayer();

            if(attackingPlayer)
                Attack();
        }

        if (inAttack)
        {
            transform.position += attackDirection * attackSpeed * Time.deltaTime;
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                inAttack = false;
                attackCooldownTimer = fireRate;
                navAgent.isStopped = false;
            }
                
        }

        if (inWindUp)
        {
            attackAreaNotification.SetActive(true);
        }
        else
        {
            attackAreaNotification.SetActive(false);
        }

        if (inAttack)
        {
            navAgent.radius = 0.01f;
        }
        else
        {
            navAgent.radius = 1f;
        }
    }

    private void MoveToPlayer()
    {
        navAgent.SetDestination(player.position);
    }

    private void CheckRange()
    {
        float playerDistance = Vector3.Distance(transform.position, player.position);
        if (playerDistance <= detectionRadius)
        {
            if (targetingPlayer == false)
                StartCoroutine(AlertDetection());

            targetingPlayer = true;

            if(playerDistance <= attackRadius)
            {
                attackingPlayer = true;
            }
        }
        else
        {
            targetingPlayer = false;
            attackingPlayer = false;
            navAgent.ResetPath();
        }
    }

    private IEnumerator AlertDetection()
    {
        alertNotification.SetActive(true);
        yield return new WaitForSeconds(1f);
        alertNotification.SetActive(false);
    }

    private void RotateToPlayer()
    {
        //Reason we use rotation object is so that the world UI (Healthbar) will not
        //rotate with the player and remain static

        Vector3 flatPositon = new Vector3(player.position.x, enemyRotationObject.position.y, player.position.z);
        enemyRotationObject.LookAt(flatPositon);
    }

    private void Attack()
    {
        attackCooldownTimer -= Time.deltaTime;

        if (attackCooldownTimer > 0)
            return;

        StartCoroutine(AttackProcess());

    }

    private IEnumerator AttackProcess()
    {
        navAgent.isStopped = true;
        inWindUp = true;
        attackDirection = enemyRotationObject.forward;
        yield return new WaitForSeconds(attackWindUp);
        attackTimer = attackDuration;
        inWindUp = false;
        inAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && inAttack)
        {
            Debug.Log("Rammed");
            other.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

}

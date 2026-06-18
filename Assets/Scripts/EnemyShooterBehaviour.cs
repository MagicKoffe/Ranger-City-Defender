using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShooterBehaviour : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] Transform shootingPoint;
    [SerializeField] Transform enemyRotationObject;
    [SerializeField] GameObject playerBullet;
    [SerializeField] GameObject alertNotification;

    [Header("Behaviour stats")]
    [SerializeField] float detectionRadius;

    [Header("Shooting stats")]
    [SerializeField] float fireRate;
    [SerializeField] float bulletSpeed;
    [SerializeField] int bulletDamage;

    private bool targetingPlayer;
    private float shootTimer;
    private Transform player; 
    private NavMeshAgent navAgent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        navAgent = GetComponent<NavMeshAgent>();
        targetingPlayer = false;
        alertNotification.SetActive(false);
        shootTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckRange();

        if (targetingPlayer)
        {
            MoveToPlayer();
            RotateToPlayer();
            Shoot();
        }
    }

    private void MoveToPlayer()
    {
        navAgent.SetDestination(player.position);
    }

    private void CheckRange()
    {
        float playerDistance = Vector3.Distance(transform.position, player.position);
        if(playerDistance <= detectionRadius)
        {
            if (targetingPlayer == false)
                StartCoroutine(AlertDetection());

            targetingPlayer = true;
        }
        else
        {
            targetingPlayer = false;
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

        Vector3 flatPositon = new Vector3(player.position.x, 1.5f, player.position.z);
        enemyRotationObject.LookAt(flatPositon);
    }

    private void Shoot()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer > 0)
            return;

        shootTimer = fireRate;
        GameObject newBullet = Instantiate(playerBullet, shootingPoint.position, Quaternion.identity);
        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
        newBullet.GetComponent<EnemyBullet>().setDamage(bulletDamage);
        bulletRb.AddForce(bulletSpeed * shootingPoint.forward, ForceMode.VelocityChange);
    }
}

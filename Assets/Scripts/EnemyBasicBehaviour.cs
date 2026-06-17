using System;
using UnityEngine;

public class EnemyBasicBehaviour : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] Transform shootingPoint;
    [SerializeField] Transform enemyRotationObject;
    [SerializeField] Transform enemyGunObject;
    [SerializeField] GameObject playerBullet;

    [Header("Shooting stats")]
    [SerializeField] float fireRate;
    [SerializeField] float bulletSpeed;
    [SerializeField] int bulletDamage;

    private float shootTimer;
    private Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        shootTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        RotateToPlayer();
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

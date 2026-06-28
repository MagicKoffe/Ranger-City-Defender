using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] Transform shootingPoint;
    [SerializeField] GameObject playerBullet;

    [Header("Shooting stats")]
    [SerializeField] float fireRate;
    [SerializeField] float bulletSpeed;
    [SerializeField] int bulletDamage;

    private float shootTimer;
    private bool gameIsPaused;
    private bool playerIsDead;

    private void OnEnable()
    {
        PauseMenuManager.togglePauseEvent += PausedToggle;
        PlayerHealth.OnPlayerDeath += playerDied;
    }

    private void OnDisable()
    {
        PauseMenuManager.togglePauseEvent -= PausedToggle;
        PlayerHealth.OnPlayerDeath -= playerDied;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootTimer = 0;
        playerIsDead = false;
        gameIsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;  
    }

    private void Shoot()
    {
        if (shootTimer > 0)
            return;

        shootTimer = fireRate;
        GameObject newBullet = Instantiate(playerBullet, shootingPoint.position, Quaternion.identity);
        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
        newBullet.GetComponent<BulletBehaviour>().setDamage(bulletDamage);
        bulletRb.AddForce(bulletSpeed * shootingPoint.forward,ForceMode.VelocityChange);
    }

    private void PausedToggle(bool isPaused)
    {
        gameIsPaused = isPaused;

    }

    private void playerDied()
    {
        playerIsDead = true;
    }

    //--------- INPUT ------------
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (gameIsPaused || playerIsDead)
            return;

        if(context.started)
            Shoot();
    }

    
}

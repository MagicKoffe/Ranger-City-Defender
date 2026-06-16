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

    private float shootTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootTimer = 0;
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

        bulletRb.AddForce(bulletSpeed * shootingPoint.forward,ForceMode.VelocityChange);
    }

    //--------- INPUT ------------
    public void OnShoot(InputAction.CallbackContext context)
    {
        //Debug.Log($"Player shoot: {context.performed}");
        if(context.started)
            Shoot();
    }
}

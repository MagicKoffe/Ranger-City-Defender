using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private int damage;
    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    public void setDamage(int dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Bullet")
            return;

        Debug.Log($"Object hit: {other.gameObject.name}");
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}

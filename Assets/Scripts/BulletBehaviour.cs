using UnityEngine;

public class BulletBehaviour : MonoBehaviour
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
        if (other.tag == "Player" || other.tag == "Bullet")
            return;

        Debug.Log($"Object hit: {other.gameObject.name}");
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}

using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Bullet")
            return;

        Destroy(gameObject);
    }
}

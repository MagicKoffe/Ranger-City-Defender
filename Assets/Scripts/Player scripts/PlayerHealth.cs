using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHealth;
    private int currentHealth;

    [Header("Visuals")]
    [SerializeField] Image healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        updateHealthBar();
    }

    private void updateHealthBar()
    {
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        updateHealthBar();

        if (currentHealth <= 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        //Game over
    }
}

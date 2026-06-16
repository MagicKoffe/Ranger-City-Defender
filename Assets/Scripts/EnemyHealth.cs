using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
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
        Debug.Log($"{gameObject.name} took {dmg} damage.");
        currentHealth -= dmg;
        updateHealthBar();

        if(currentHealth <= 0)
        {
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        gameObject.GetComponent<HealthBar>().SetMaxHeath(maxHealth);
    }

    public void TakeDamage(int damage) // Получение урона
    {
        currentHealth -= damage;
        gameObject.GetComponent<HealthBar>().SetHealth(currentHealth);

        if (currentHealth <= 0) // Рестарт уровни, если здоровье игрока достигает нуля
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void TakeHeal(int heal)
    {
        if (currentHealth + heal > 100)
            currentHealth = 100;
        else
            currentHealth += heal;
        gameObject.GetComponent<HealthBar>().SetHealth(currentHealth);
    }
}

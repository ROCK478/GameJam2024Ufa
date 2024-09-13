using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private float _maxHealth;
    private float _currentHealth;
    public bool IsDetected = false;
    public bool _lookRight;

    private void Awake()
    {
        _maxHealth = 100;
        _currentHealth = _maxHealth;
    }



    public void TakeDamage(int damage) // Получение урона
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Destroy(this.gameObject);
        };
    }
}

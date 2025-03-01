using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour // Любой враг
{
    [SerializeField]private float _maxHealth; //Максимальное здоровье
    public float _currentHealth; //Текущее здоровье
    public bool IsDetected = false; //Переменная обнаружение
    public bool _lookRight; //Переменная для определения направления

    private void Start()
    {
        _currentHealth = _maxHealth; // С начала текущее здоровье стало максимальным
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

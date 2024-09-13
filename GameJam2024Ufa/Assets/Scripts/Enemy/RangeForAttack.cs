using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeForAttack : MonoBehaviour
{
    private GameObject _hero;
    private bool _inRange = false;
    [NonSerialized] private bool _attack  = true;
    [Range(0f, 10f)] public float TimerDuration; // Задержка между ударами
    [NonSerialized] public float TimeForAttack;

    private void Awake()
    {
        _hero = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (_inRange && _attack)
        {
            _hero.GetComponent<Player>().TakeDamage(transform.parent.gameObject.GetComponent<Melee>()._damage);
            TimeForAttack = TimerDuration;
        }
        Timer();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _inRange = false;
        }
    }
    private void Timer()
    {
        if (TimeForAttack > 0)
        {
            _attack = false;
            TimeForAttack -= Time.deltaTime;
            if (TimeForAttack <= 0)
            {
                _attack = true;
            }
        }
    }
}

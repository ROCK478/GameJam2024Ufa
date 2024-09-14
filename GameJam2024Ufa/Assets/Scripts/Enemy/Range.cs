using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : Enemy
{
    [SerializeField] private GameObject _bulletPrephab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _timeLifeBullet;
    public Transform _firePoint;
    [SerializeField] private int _damageBullet;
    private GameObject _hero;
    private Rigidbody2D _rb;
    private bool _canShoot = true;
    [Range(0f, 10f)] public float TimerDuration; // Задержка между ударами
    [NonSerialized] public float TimeForAttack;
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _hero = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bool RightForPerson = (transform.position.x - _hero.transform.position.x > 0) ? true : false;
        if (IsDetected && RightForPerson)
        {
            if (_lookRight == true)
            {
                transform.localScale *= new Vector2(-1, 1);
                _lookRight = !_lookRight;
            }
            if (_canShoot)
            {
                animator.SetBool("isAtack", true);
                //Shoot();
                TimeForAttack = TimerDuration;
            }
            Timer();

        }
        else if (IsDetected && !RightForPerson)
        {
            if (_lookRight == false)
            {
                transform.localScale *= new Vector2(-1, 1);
                _lookRight = !_lookRight;
            }
            if (_canShoot)
            {
                animator.SetBool("isAtack", true);
                //Shoot();
                TimeForAttack = TimerDuration;
            }
            Timer();

        }
        else
        {
            animator.SetBool("isAtack", false);
        }
    }

    public void Shoot()
    {
        GameObject Bullet = Instantiate(_bulletPrephab, _firePoint.position, _firePoint.rotation);
        Rigidbody2D BulletRB = Bullet.GetComponent<Rigidbody2D>();
        Bullet.AddComponent<Bullet>();
        Bullet BulletScrpipt = Bullet.GetComponent<Bullet>();
        BulletScrpipt.BulletDamage = _damageBullet;
        if (_lookRight)
        {
            BulletRB.velocity = new Vector2(_bulletSpeed, 0);
        }
        else
        {
            BulletRB.velocity = new Vector2(_bulletSpeed * -1, 0);
        }
        Destroy(Bullet, _timeLifeBullet);
    }

    private void Timer()
    {
        if (TimeForAttack > 0)
        {
            _canShoot = false;
            TimeForAttack -= Time.deltaTime;
            if (TimeForAttack <= 0)
            {
                _canShoot = true;
            }
        }
    }




}

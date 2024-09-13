using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.Burst.CompilerServices;

public class Player : MonoBehaviour
{
    [Header("Настройки передвижения")]
    [SerializeField] private float _moveSpeed;
    private bool _lookRight = true;
    private Rigidbody2D _rb;
    private Animator _animator;

    [Header("Настройки прыжка")]
    [SerializeField] private float _jumpForce;
    public bool _isGround;
    [SerializeField] private float _rayDistance = 0.6f; //Расстояние для поиска земли для недоступности множественных прыжков


    [Header("Настройки стрельбы")]
    [SerializeField] private GameObject _bulletPrephab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _timeLifeBullet;
    private Transform _firePoint;
    private int _damage = 20;

    [Header("Настройки здоровья")]
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;

    [Header("Настройки тихой ходьбы")]
    [NonSerialized]public bool IsStealth = false;
    [SerializeField] private float _stealthMoveSpeed;



    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _firePoint = GameObject.Find("FirePoint").transform;
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
    }
    private void Update()
    {
        Move();
        Flip();
        Shoot();
        Jump();
        StealthMove();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (IsStealth)
        {
            _rb.velocity = (new Vector2(Input.GetAxis("Horizontal") * _stealthMoveSpeed, _rb.velocity.y));
        }
        else
        {
            _rb.velocity = (new Vector2(Input.GetAxis("Horizontal") * _moveSpeed, _rb.velocity.y));
        }
        _animator.SetFloat("xVelocity", Math.Abs(_rb.velocity.x));
    }

    private void Flip()
    {
        if ((_lookRight && (Input.GetAxis("Horizontal") < 0)) || (!_lookRight && (Input.GetAxis("Horizontal") > 0)))
        {
            gameObject.
            transform.localScale *= new Vector2(-1, 1);
            _lookRight = !_lookRight;
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject Bullet = Instantiate(_bulletPrephab, _firePoint.position, _firePoint.rotation);
            Rigidbody2D BulletRB = Bullet.GetComponent<Rigidbody2D>();
            Bullet.AddComponent<Bullet>();
            Bullet BulletScrpipt = Bullet.GetComponent<Bullet>();
            BulletScrpipt.BulletDamage = _damage;
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
    }

    private void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(_rb.position, Vector2.down, _rayDistance, LayerMask.GetMask("Ground")); //У объектов нужно указать слой "Ground"
        if (hit.collider != null)
        {
            _isGround = true;
        }
        else
        {
            _isGround = false;
            _animator.SetBool("isJump", _isGround);
        }
        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            _rb.AddForce(Vector2.up.normalized * _jumpForce, ForceMode2D.Impulse);
            _animator.SetBool("isJump", _isGround);
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void StealthMove()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            IsStealth = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsStealth = false;
        }
    }
}

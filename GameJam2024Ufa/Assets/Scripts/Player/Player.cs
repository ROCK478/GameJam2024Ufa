using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.Burst.CompilerServices;
using UnityEditor;

public class Player : MonoBehaviour
{
    public HealthBar healthBar;
    [Header("Настройки передвижения")]
    [SerializeField] private float _moveSpeed;
    private bool _lookRight = true;
    private Rigidbody2D _rb;
    private Animator _animator;

    [Header("Настройки прыжка")]
    [SerializeField] private float _jumpForce;
    public bool _isGround;
    [SerializeField] private float _rayDistance = 0.6f; //Расстояние для поиска земли для недоступности множественных прыжков

    [Header("Настройки здоровья")]
    [SerializeField] private int _maxHealth = 4;
    private int _currentHealth;

    [Header("Настройки тихой ходьбы")]
    [NonSerialized]public bool IsStealth = false;
    [SerializeField] private float _stealthMoveSpeed;

    [Header("Настройки ближнего боя")]
    [SerializeField] private GameObject PlayerRangeForAttack;
    [SerializeField] private float _timeAttack;
    [SerializeField] public int DamageMelee;

    [Header("Настройки подката")]
    private bool _isDashing;
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashDuration;

    private SpriteRenderer _sr;



    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        PlayerRangeForAttack = transform.Find("PlayerRangeForAttack").gameObject;
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
    }
    private void Update()
    {
        if (DialogManager.Stop == false)
        {
            Flip();
            Jump();
            StealthMove();
            if (Input.GetKeyDown(KeyCode.C) && !_isDashing)
            {
                StartCoroutine(Dash());
            }
        }
    }
    private void FixedUpdate()
    {
        if (DialogManager.Stop == false)
        {
            Move();
        }
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
            gameObject.transform.localScale *= new Vector2(-1, 1);
            _lookRight = !_lookRight;
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
        //healthBar.SetHealth(_currentHealth);
        if (_currentHealth <= 0)
        {
            SceneManager.LoadScene("Lvl 3");
        }
    }

    private void StealthMove()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            IsStealth = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && Input.GetAxis("Horizontal") != 0)
        {
            IsStealth = false;
        }
    }

    private void MeleeAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayerRangeForAttack.SetActive(true);
            Invoke("PlayerRangeForAttack.SetActive(false)", _timeAttack);
        }
    }

    private IEnumerator Dash()
    {
        _isDashing = true;
        _rb.velocity = new Vector2(_dashForce, _rb.velocity.y);
        yield return new WaitForSeconds(_dashDuration);
        _rb.velocity = Vector2.zero;
        _isDashing = false;
    }
}

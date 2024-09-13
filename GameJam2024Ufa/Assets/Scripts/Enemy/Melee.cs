using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Enemy
{
    private GameObject _hero;
    [SerializeField] private float _speed;
    public int _damage = 20;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _hero = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (IsDetected && transform.position.x - _hero.transform.position.x > 0)
        {
            _rb.velocity = (new Vector2(-_speed, _rb.velocity.y));
            transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        }
        else if (IsDetected && transform.position.x - _hero.transform.position.x < 0)
        {
            _rb.velocity = (new Vector2(_speed, _rb.velocity.y));
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }
}

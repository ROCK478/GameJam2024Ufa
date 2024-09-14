using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Enemy
{
    private GameObject _hero;
    [SerializeField] private float _speed;
    public int _damage = 1;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _hero = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        bool RightForPerson = (transform.position.x - _hero.transform.position.x > 0) ? true : false;
        if (IsDetected && RightForPerson)
        {
            _rb.velocity = (new Vector2(-_speed, _rb.velocity.y));
            if (_lookRight == true)
            {
                transform.localScale *= new Vector2(-1, 1);
                _lookRight = !_lookRight;
            }
        }
        else if (IsDetected && !RightForPerson)
        {
            _rb.velocity = (new Vector2(_speed, _rb.velocity.y));
            if (_lookRight == false)
            {
                transform.localScale *= new Vector2(-1, 1);
                _lookRight = !_lookRight;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeForAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(transform.parent.GetComponent<Player>().DamageMelee);
        }
    }

 
}

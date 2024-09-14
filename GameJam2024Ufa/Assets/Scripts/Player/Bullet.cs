using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int BulletDamage = 1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(BulletDamage);
        }
        if (collision != null)
        {
            Destroy(this.gameObject);
        }
    }
}

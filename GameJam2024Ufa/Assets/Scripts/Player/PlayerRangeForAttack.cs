using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeForAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) //Проверка триггера
    {
        if (other.gameObject.tag == "Enemy") //Если нашли противника
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(transform.parent.GetComponent<Player>().DamageMelee); // Вызываем получение урона у противника
        }
    }

 
}

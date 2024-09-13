using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetected : MonoBehaviour
{
	private GameObject _hero;
	
    private void Awake()
	{
		_hero = GameObject.FindGameObjectWithTag("Player");
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
            bool RightForPerson = (transform.parent.gameObject.transform.position.x - _hero.transform.position.x > 0) ? true : false;
			if (RightForPerson == true && _hero.GetComponent<Player>().IsStealth && transform.parent.gameObject.GetComponent<Enemy>()._lookRight == true)
			{
				transform.parent.gameObject.GetComponent<Enemy>().IsDetected = false;

            }
			else if(RightForPerson == false && _hero.GetComponent<Player>().IsStealth && transform.parent.gameObject.GetComponent<Enemy>()._lookRight == false)
			{
                transform.parent.gameObject.GetComponent<Enemy>().IsDetected = false;
            }
			else
			{
                transform.parent.gameObject.GetComponent<Enemy>().IsDetected = true;
            }
		}
	}
}

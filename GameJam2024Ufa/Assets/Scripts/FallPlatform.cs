using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            yield return new WaitForSeconds(0.5f);
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            Debug.Log("dfsf");
        }
    }

}

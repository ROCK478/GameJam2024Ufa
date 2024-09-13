using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowNPC : MonoBehaviour
{
    public Transform player;
    Vector2 Target;

    public float trackingSpeed = 1.5f;

    void Update()
    {
        if(player)
        {
            Vector2 currentPosition = Vector2.Lerp(transform.position, Target, trackingSpeed * Time.deltaTime);
            transform.position = currentPosition;

            Target = new Vector2(player.transform.position.x, player.transform.position.y);
        }
    }
}

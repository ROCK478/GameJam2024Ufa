using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] float parallaxStrength = 1f;

    public Transform followingTarget;
    public bool disableVerticalParalax;

    private Vector3 _targetPreviousPosition;

    void Start()
    {
        if(!followingTarget)
        {
            followingTarget = Camera.main.transform;
        }

        _targetPreviousPosition = followingTarget.position;
    }
                                                                                
    void Update()
    {
        var delta = followingTarget.position - _targetPreviousPosition;
        if (disableVerticalParalax)
        {
            delta.y = 0f;
        }

        _targetPreviousPosition = followingTarget.position;

        transform.position += delta * parallaxStrength;
    }
}

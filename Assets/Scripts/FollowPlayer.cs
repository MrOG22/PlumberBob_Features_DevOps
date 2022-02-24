using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform followThis;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    void Update()
    {
        transform.position = followThis.transform.position + offset;
    }
}

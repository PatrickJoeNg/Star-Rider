using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] Transform target;

    public float moveSpeed = 0.4f;

    public Vector3 offset;
     Vector3 camOffset = new Vector3(0f, 0f, -8f);

    void LateUpdate()
    {
       Vector3 camLocal = transform.localPosition;

        Vector3 camPosition = target.localPosition;

    }
}

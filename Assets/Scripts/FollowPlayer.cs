using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Vector2 limits = new Vector2(5,5);
    [SerializeField] Transform followTarget;
    [SerializeField] Vector3 offset = Vector3.zero;
    void Update()
    {
        transform.localPosition = offset;
        transform.localPosition = new Vector3(followTarget.localPosition.x, followTarget.localPosition.y, transform.localPosition.z);
    }


    void LateUpdate()
    {
        float clampedX = Mathf.Clamp(transform.localPosition.x, -limits.x, limits.x);
        float clampedY = Mathf.Clamp(transform.localPosition.y, -limits.y, limits.y);

        transform.localPosition = new Vector3(clampedX, clampedY, transform.localPosition.z);
    }
}

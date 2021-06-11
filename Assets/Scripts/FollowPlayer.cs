using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] GameObject player;

    [SerializeField] Vector2 limits = new Vector2(0f, 0f);

    private Vector3 offset = new Vector3(0, 0, -8f);

    public float smoothTime = .3f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        // need to manual set x and y pos
        float rawXPos = transform.localPosition.x;
        float rawYPos = transform.localPosition.y;

        // Debug.Log("X position: " + rawXPos + "\nY position: " + rawYPos);

        // clamped x and y position for camera using vector 2
        float clampedXPos = Mathf.Clamp(rawXPos, -limits.x, limits.x);
        float clampedYPos = Mathf.Clamp(rawYPos, -limits.y, limits.y);

        // pseudo new transform.localPosition for camera lol
        Vector3 newLocalPos = new Vector3(clampedXPos, clampedYPos, 0f);

        //taking player object's position and offset for main camera.
        Vector3 camToPlayer = player.transform.localPosition + offset;

        transform.localPosition = Vector3.SmoothDamp(newLocalPos + offset, camToPlayer, ref velocity, smoothTime);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General Links")]
    [SerializeField] Transform aimReticle;

    [SerializeField] RectTransform largeReticle;
    [SerializeField] RectTransform smallReticle;

    [SerializeField] Camera mainCamera;
    [SerializeField] Camera hoodCamera;

    [Header("General Parameters")]

    [Space]
    [Tooltip("Adjust Ship Effective movement range")]
    public float xRange = 15f;
    public float yRange = 13f;

    [Tooltip("Adjust Movement Speed")]
    public float moveSpeed = 5f;

    [Header("Ship Rotation Effect")]
    [Tooltip("Adjust how the ship rotates as it moves")]
    [SerializeField] float posPitchfactor = -3f;
    [SerializeField] float controlPitchFactor = -2f;
    [SerializeField] float posYawFactor = -3f;
    [SerializeField] float ctrlRollFactor = -10f;


    float xMove, yMove;
    float pitch, yaw, roll;
    [SerializeField] float reticleSpeed = .5f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
        ProcessRotation();
    }

    void ProcessRotation()
    {
        float pitchFromPos = transform.localPosition.y * posPitchfactor;
        float pitchControl = yMove * controlPitchFactor;

        float yawFromPos = transform.localPosition.x * posYawFactor;

        float pitch = pitchFromPos + pitchControl;
        float yaw = yawFromPos;
        float roll = xMove * ctrlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessMovement()
    {
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");

        float xOffset = xMove * Time.deltaTime * moveSpeed;
        float yOffset = yMove * Time.deltaTime * moveSpeed;

        float newXPos = transform.localPosition.x + xOffset;
        float newYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(newXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);

        largeReticle.transform.position = Camera.main.WorldToScreenPoint(aimReticle.transform.position);
        smallReticle.transform.position = Camera.main.WorldToScreenPoint(aimReticle.transform.position);
    }
}

using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General Links")]
    [SerializeField] Transform shipCrosshair;
    [SerializeField] RectTransform outerCrosshair;
    [SerializeField] RectTransform innerCrosshair;
    [SerializeField] CinemachineDollyCart dolly;
    [SerializeField] GameObject[] lasers;
    [Header("General Parameters")]

    [Space]
    [Tooltip("Adjust Ship Effective movement range")]
    public float xRange = 15f;
    public float yRange = 13f;

    [Tooltip("Adjust Movement Speed")]
    public float moveSpeed = 5f;
    public float boostSpeed = 10f;
    public float maxSpeed = 65f;

    [Header("Ship Rotation Effect")]
    [Tooltip("Adjust how the ship rotates as it moves")]
    [SerializeField] float posPitchfactor = -3f;
    [SerializeField] float controlPitchFactor = -2f;
    [SerializeField] float posYawFactor = -3f;
    [SerializeField] float ctrlRollFactor = -10f;

    float xMove, yMove;
    float pitch, yaw, roll;

    [SerializeField] float reticleSpeed = .5f;

    float timeHeld = 0f;


    // Start is called before the first frame update
    void Start()
    {
        SetMovementSpeed(moveSpeed);
    }


    // Update is called once per frame
    void Update()
    {
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");

        ProcessRotation();
        ProcessMovement();
        ShootingLaser();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ProcessBoost(true);
            Debug.Log("Pressing space");
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            ProcessBoost(false);
            Debug.Log
                ("space not pressed");
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ProcessBrake(true);
            Debug.Log("Pressing Left Ctrl");
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ProcessBrake(false);
            Debug.Log("Pressing Left Ctrl");
        }
    }

    void ShootingLaser()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            BurstFire(true);
            Debug.Log("Shooting laser");
        }
        else { BurstFire(false); }
        //if (Input.GetKey(KeyCode.K))
        //{         
        //    timeHeld += Time.deltaTime;
        //    if (timeHeld >= 1.5f)
        //    {
        //        Debug.Log("Charging");
        //    }
        //}
    }

    void ProcessMovement()
    {
        float rawXPos = transform.localPosition.x + xMove;
        float rawYPos = transform.localPosition.y + yMove;

        float xOffset = xMove * Time.deltaTime * moveSpeed;
        float yOffset = yMove * Time.deltaTime * moveSpeed;

        float newXPos = transform.localPosition.x + xOffset;
        float newYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(newXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);

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

    private void SetMovementSpeed(float speed)
    {
        dolly.m_Speed = speed;
    }


    void ProcessBoost(bool state)
    {
        if (state){
            dolly.m_Speed = moveSpeed * boostSpeed;
        }
        else { 
            dolly.m_Speed = moveSpeed; 
        }
    }

    void ProcessBrake(bool state)
    {
        if (state)
        {
            dolly.m_Speed = moveSpeed /2 ;
        }
        else
        {
            dolly.m_Speed = moveSpeed;
        }
    }

    void BurstFire(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionMod = laser.GetComponent<ParticleSystem>().emission;
            emissionMod.enabled = isActive;
        }
    }
    
}

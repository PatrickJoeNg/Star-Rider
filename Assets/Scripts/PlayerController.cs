using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform aimReticle;
    
    private float xMove;
    private float yMove;

    [Header("Movement Multipliers")]
    [SerializeField] float xMultiplier = .5f;
    [SerializeField] float yMultiplier = .5f;

    public float xRange = 15f;
    public float yRange = 15f;

    [Header("Pitch Multipliers")]
    public float pitchMultiplier = 2f;
    public float pitchControlMultiplier = 2f;

    [Header("Yaw Multipliers")]
    public float yawMultiplier = 2f;
    private float yawControlMultiplier = 2f;

    [Header("Roll Multipliers")]
    public float rollMultiplier = 5f;

    public float resetSpeed = 1f;
    Quaternion defaultRotation;

    private float pitch;
    private float yaw;
    private float roll;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        ProcessMovement();
        MoveRotation();
    }
    private void MoveRotation()
    {

        //pitch controls
        float verticalPitch = transform.localPosition.y * pitchMultiplier;
        float vertPitchControl = yMove * pitchControlMultiplier;

        pitch = verticalPitch + vertPitchControl;

        //yaw controls
        //float yawHorizontal = transform.localPosition.y * yawMultiplier;
        //float yawControl = xMove * yawControlMultiplier;
        yaw = transform.localPosition.x * yawMultiplier;

        // roll control
        roll = transform.localPosition.x * rollMultiplier;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

        aimReticle.transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private void ProcessMovement()
    {
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");

        float xOffset = xMove * Time.deltaTime * xMultiplier;
        float yOffset = yMove * Time.deltaTime * yMultiplier;

        float newXPos = transform.localPosition.x + xOffset;    
        float newYPos = transform.localPosition.y + yOffset;

        float clampXPos = Mathf.Clamp(newXPos, -xRange, xRange);
        float clampYPos = Mathf.Clamp(newYPos, -yRange, yRange);

        Debug.Log("Moving left or right " + newXPos);
        Debug.Log("Moving up or down "  + newYPos);

        transform.localPosition = new Vector3(clampXPos, clampYPos, transform.localPosition.z );    
    }
    
    void ResetRotation()
    {
        //Vector3 defaultRotation = transform.rotation;

    }
}

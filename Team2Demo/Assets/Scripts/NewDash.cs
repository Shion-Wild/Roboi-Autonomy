using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDash : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private PlayerMovementUpdated pm;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.E;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovementUpdated>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey))
        {
            Dash();
        }
    }

    private void Dash()
    {
        Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;

        rb.AddForce(forceToApply, ForceMode.Impulse);

    }
}
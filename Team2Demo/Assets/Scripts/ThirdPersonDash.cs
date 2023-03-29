using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonDash : MonoBehaviour
{
    ThirdPersonMovement moveScript;

    public float dashSpeed;
    public float dashTime;


    void Start()
    {
        moveScript = GetComponent<ThirdPersonMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            moveScript.controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);

            yield return null;
        }
    }
}

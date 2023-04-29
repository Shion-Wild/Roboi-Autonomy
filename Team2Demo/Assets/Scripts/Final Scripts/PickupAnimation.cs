using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAnimation : MonoBehaviour
{
    float rotationSpeed = 50f;
    float bounceHeight = .50f;
    float bounceSpeed = 2f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        // Rotate the cube around its own axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Calculate the bounce offset using a sine wave
        float bounceOffset = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;

        // Set the new position of the cube with the bounce offset
        transform.position = startPos + Vector3.up * bounceOffset;
    }
}

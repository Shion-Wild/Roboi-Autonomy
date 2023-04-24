using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject target1; // First target game object
    public GameObject target2; // Second target game object

    private int targetsDestroyed; // Number of targets destroyed

    void Start()
    {
        targetsDestroyed = 0; // Initialize targets destroyed to 0
    }

    void Update()
    {
        // Check if all three targets are destroyed
        if (target1 == null && target2 == null)
        {
            Debug.Log("Boss defeated!"); // Display message when boss is defeated
        }
    }

    // Called when a target is destroyed
    public void TargetDestroyed()
    {
        targetsDestroyed++; // Increment targets destroyed counter

        // Check if all three targets are destroyed
        if (targetsDestroyed == 2)
        {
            Debug.Log("All targets destroyed!"); // Display message when all targets are destroyed
            // Perform additional actions to defeat the boss here
        }
    }
}

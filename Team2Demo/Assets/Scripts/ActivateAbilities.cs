using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAbilities : MonoBehaviour
{
    GrenadeThrower empAbility;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            empAbility = other.GetComponent<GrenadeThrower>();
            empAbility.enabled = true;
            Destroy(gameObject);
        }
    }
}

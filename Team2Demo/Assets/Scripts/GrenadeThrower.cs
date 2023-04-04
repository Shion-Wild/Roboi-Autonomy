using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public float throwForce = 40f;
    public GameObject grenadePrefab;
    Vector3 empOrigin;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowGrenade();
        }
    }

    public void ThrowGrenade()
    {
        empOrigin = transform.forward * 1;
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}

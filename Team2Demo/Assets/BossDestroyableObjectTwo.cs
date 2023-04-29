using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDestroyableObjectTwo : MonoBehaviour
{
    [SerializeField] BossController bossController;

    // Variables to hold the materials for the object
    public Material defaultMaterial;
    public Material destroyedMaterial;

    // Reference to the object's renderer component
    private Renderer objectRenderer;

    // Reference to the object's collider component
    private Collider objectCollider;

    void Start()
    {
        // Get the object's renderer component
        objectRenderer = GetComponent<Renderer>();

        // Get the object's collider component
        objectCollider = GetComponent<Collider>();
    }

    public void ChangeLightsDamage()
    {
        // Change the material of the object to the destroyed material
        objectRenderer.material = destroyedMaterial;

        // Disable the object's collider so it cannot be interacted with
        objectCollider.enabled = false;

        // Set the emissive color of the object to red
        objectRenderer.material.SetColor("_EmissiveColor", Color.red);

        bossController.DisabledConsoleTwo();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public Material targetMaterial;
    public Color baseEmissionColor;
    public Color newEmissionColor;


    void Start()
    {
        SetEmissionColorAtStart();
    }

    void SetEmissionColorAtStart()
    {
        if (targetMaterial == null)
        {
            return;
        }

        // Set the base emission color at start
        targetMaterial.SetColor("_EmissionColor", baseEmissionColor);
        // Enable emission on the material
        targetMaterial.EnableKeyword("_EMISSION");
        // Update the material to apply the changes
        targetMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
    }
    public void EMPDestroy()
    {
        if (targetMaterial == null)
        {
            Debug.LogError("Target Material is not assigned in the ChangeEmissionColor script!");
            return;
        }

        // Set the new emission color
        targetMaterial.SetColor("_EmissionColor", newEmissionColor);
        // Enable emission on the material
        targetMaterial.EnableKeyword("_EMISSION");
        // Update the material to apply the changes
        targetMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
    }

  
}

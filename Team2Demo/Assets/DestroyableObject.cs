using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public Material targetMaterial;
    public Color newEmissionColor;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulb : MonoBehaviour
{
    private Renderer bulbRenderer; // Renderer component of the light bulb
    private Material[] originalMaterials; // Array to store original materials
    private bool greenMaterialAdded = false; // Flag to track whether green material is added

    private void Start()
    {
        bulbRenderer = GetComponent<Renderer>();

        // Store the original materials of the light bulb
        originalMaterials = bulbRenderer.sharedMaterials;
    }

    public void ReplaceMaterial(Material material)
    {
        // Check if the Renderer component is available
        if (bulbRenderer == null)
        {
            bulbRenderer = GetComponent<Renderer>();
        }

        // Make a copy of the current materials array
        Material[] materials = bulbRenderer.sharedMaterials;

        // Replace the material at index 1 with greenMaterial
        if (materials.Length > 1)
        {
            materials[1] = material;
        }
        else
        {
            Debug.LogError("Not enough materials on the object to replace Element 1");
        }

        // Assign the updated materials array back to the Renderer
        bulbRenderer.sharedMaterials = materials;
    }


    public void AddMaterial(Material material)
    {
        if (!greenMaterialAdded)
        {
            Material[] newMaterials = new Material[originalMaterials.Length + 1];
            System.Array.Copy(originalMaterials, newMaterials, originalMaterials.Length);
            newMaterials[newMaterials.Length - 1] = material;

            bulbRenderer.sharedMaterials = newMaterials;
            greenMaterialAdded = true;
        }
    }
}



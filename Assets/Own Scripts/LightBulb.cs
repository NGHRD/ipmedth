using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulb : MonoBehaviour
{
    public Material greenMaterial; // The green material to add

    private Renderer bulbRenderer; // Renderer component of the light bulb
    private Material[] originalMaterials; // Array to store original materials
    private bool greenMaterialAdded = false; // Flag to track whether green material is added

    private void Start()
    {
        bulbRenderer = GetComponent<Renderer>();

        // Store the original materials of the light bulb
        originalMaterials = bulbRenderer.sharedMaterials;
    }

    public void AddGreenMaterial(Material material)
    {
        if (!greenMaterialAdded)
        {
            Material[] newMaterials = new Material[originalMaterials.Length + 1];
            System.Array.Copy(originalMaterials, newMaterials, originalMaterials.Length);
            newMaterials[newMaterials.Length - 1] = greenMaterial;

            bulbRenderer.sharedMaterials = newMaterials;
            greenMaterialAdded = true;
        }
    }
}



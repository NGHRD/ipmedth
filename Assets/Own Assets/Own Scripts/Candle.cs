using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    public GameObject candleLight;
    public Material litMaterial; // Reference to the lit material
    private bool isLit = false;
    private MeshRenderer meshRenderer;
    public SpotlightController spotlightController;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        if (candleLight == null)
        {
            candleLight = GetComponentInChildren<Light>().gameObject;
        }

        candleLight.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lighter") && !isLit)
        {
            turnOnCandle();
        }

        if (spotlightController.targetIndex == 9 && other.CompareTag("Hands") && isLit)
        {
            turnOffCandle();
        }
    }

    private void turnOnCandle()
    {
        isLit = true;

        // Apply the lit material to the mesh renderer
        if (meshRenderer != null && litMaterial != null)
        {
            var materials = new List<Material>(meshRenderer.materials);
            materials.Add(litMaterial);
            meshRenderer.materials = materials.ToArray();
            candleLight.SetActive(true);
            Debug.Log("Lamp gaat aan");
        }

        CandleManager.Instance.CheckAllCandlesLit();
    }

    private void turnOffCandle()
    {
        isLit = false;

        // Remove the last material from the mesh renderer
        if (meshRenderer != null && litMaterial != null)
        {
            var materials = new List<Material>(meshRenderer.materials);

            if (materials.Count > 0)
            {
                materials.RemoveAt(materials.Count - 1); // Remove the last material
                meshRenderer.materials = materials.ToArray();
            }

            candleLight.SetActive(false);
            Debug.Log("is uit nu");
        }

        CandleManager.Instance.CheckAllCandlesUnLit();
    }



    public bool IsLit()
    {
        return isLit;
    }
}
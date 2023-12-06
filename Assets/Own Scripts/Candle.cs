using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    public GameObject candleLight;
    public Material litMaterial; // Reference to the lit material
    private bool isLit = false;
    private MeshRenderer meshRenderer;

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
            Interact();
        }
    }

    private void Interact()
    {
        isLit = true;

        // Apply the lit material to the mesh renderer
        if (meshRenderer != null && litMaterial != null)
        {
            var materials = new List<Material>(meshRenderer.materials);
            materials.Add(litMaterial);
            meshRenderer.materials = materials.ToArray();
            candleLight.SetActive(true);
        }

        CandleManager.Instance.CheckAllCandlesLit();
    }

    public bool IsLit()
    {
        return isLit;
    }
}

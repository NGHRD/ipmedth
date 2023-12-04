using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AutoScaleMaterial : MonoBehaviour
{
    public Vector2 scaleMultiplier = new Vector2(1f, 1f); // Added scale multiplier

    private Vector3 originalScale;
    private Vector2 originalTextureScale;
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        // Store the original scale of the object
        originalScale = transform.localScale;

        // Store the original texture scale from the material
        if (objectRenderer.material.mainTexture != null)
        {
            originalTextureScale = objectRenderer.material.mainTextureScale;
        }

        UpdateTextureScale();
    }

    private void UpdateTextureScale()
    {
        // Calculate the current scale factor relative to the original scale
        Vector3 scaleChangeFactor = new Vector3(
            transform.localScale.x / originalScale.x,
            transform.localScale.y / originalScale.y,
            transform.localScale.z / originalScale.z
        );

        // Adjust the texture scale to maintain the aspect ratio and apply the scale multiplier
        if (objectRenderer.material.mainTexture != null)
        {
            Vector2 newTextureScale = new Vector2(
                (originalTextureScale.x / scaleChangeFactor.x) * scaleMultiplier.x,
                (originalTextureScale.y / scaleChangeFactor.y) * scaleMultiplier.y
            );
            objectRenderer.material.mainTextureScale = newTextureScale;
        }
    }
}

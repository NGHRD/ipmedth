using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

public class SpotlightController : MonoBehaviour
{
    [System.Serializable]
    public class TransformEventPair
    {
        public List<Transform> targets;
        public UnityEvent currentEvent;
        public Material flowMaterial;
        public bool replaceAllMaterials = false; // Flag to determine whether to replace all materials
        public bool addFlowMaterial = true; // Flag to determine whether to add the flow material

        // Add other parameters or functions as needed
    }

    public List<TransformEventPair> targetObjects;

    private Transform currentTarget;
    private Dictionary<MeshRenderer, List<Material>> originalMaterials = new Dictionary<MeshRenderer, List<Material>>();

    private void Start()
    {
        if (targetObjects.Count == 0)
        {
            Debug.LogError("No target objects assigned!");
        }

        // Initialize the current target
        if (targetObjects.Count > 0)
        {
            currentTarget = targetObjects[0].targets[0];
        }

        // Cache the original materials for all targets
        CacheOriginalMaterials();

        // Apply initial flow materials
        ApplyFlowMaterials(currentTarget);
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            // Check if the current target has an associated event and invoke it
            UnityEvent currentEvent = targetObjects.Find(item => item.targets.Contains(currentTarget))?.currentEvent;
            if (currentEvent != null)
            {
                currentEvent.Invoke();
            }
        }
    }

    public void SwitchToNextTarget()
    {
        if (targetObjects.Count > 0)
        {
            // Switch to the next target in the list
            int targetIndex = (targetObjects.FindIndex(item => item.targets.Contains(currentTarget)) + 1) % targetObjects.Count;
            int subTargetIndex = targetObjects[targetIndex].targets.IndexOf(currentTarget);
            subTargetIndex = (subTargetIndex + 1) % targetObjects[targetIndex].targets.Count;
            Transform nextTarget = targetObjects[targetIndex].targets[subTargetIndex];

            // Apply or replace flow materials based on the flags
            if (targetObjects[targetIndex].replaceAllMaterials)
            {
                ReplaceAllMaterials(nextTarget);
            }
            else
            {
                // Remove added flow materials from the current target
                RemoveAddedFlowMaterials(currentTarget);
            }

            // Apply the updated flow materials to the next target
            ApplyFlowMaterials(nextTarget);

            // Update the current target
            currentTarget = nextTarget;
        }
    }

    private void CacheOriginalMaterials()
    {
        foreach (var targetObject in targetObjects)
        {
            foreach (var target in targetObject.targets)
            {
                MeshRenderer targetRenderer = target.GetComponent<MeshRenderer>();
                if (targetRenderer != null && !originalMaterials.ContainsKey(targetRenderer))
                {
                    originalMaterials.Add(targetRenderer, targetRenderer.materials.ToList());
                }
            }
        }
    }

    private void RemoveAddedFlowMaterials(Transform target)
    {
        if (target != null)
        {
            foreach (var targetObject in targetObjects)
            {
                if (targetObject.targets.Contains(target) && targetObject.addFlowMaterial)
                {
                    foreach (var t in targetObject.targets)
                    {
                        MeshRenderer targetRenderer = t.GetComponent<MeshRenderer>();
                        if (targetRenderer != null && originalMaterials.TryGetValue(targetRenderer, out var originalMats))
                        {
                            // Remove only the added flow materials
                            targetRenderer.materials = originalMats.Except(new[] { targetObject.flowMaterial }).ToArray();
                        }
                    }
                }
            }
        }
    }

    private void ApplyFlowMaterials(Transform target)
    {
        if (target != null)
        {
            MeshRenderer targetRenderer = target.GetComponent<MeshRenderer>();
            if (targetRenderer != null && originalMaterials.TryGetValue(targetRenderer, out var originalMats))
            {
                // Add the flow materials without replacing existing materials
                if (targetObjects.Find(item => item.targets.Contains(target))?.addFlowMaterial == true)
                {
                    targetRenderer.materials = originalMats.Concat(new[] { targetObjects.Find(item => item.targets.Contains(target))?.flowMaterial }).ToArray();
                }
            }
        }
    }

    private void ReplaceAllMaterials(Transform target)
    {
        if (target != null)
        {
            foreach (var targetObject in targetObjects)
            {
                if (targetObject.targets.Contains(target) && targetObject.addFlowMaterial)
                {
                    foreach (var t in targetObject.targets)
                    {
                        MeshRenderer targetRenderer = t.GetComponent<MeshRenderer>();
                        if (targetRenderer != null)
                        {
                            // Replace all original materials with the flow material
                            targetRenderer.materials = new Material[] { targetObject.flowMaterial };
                        }
                    }
                }
            }
        }
    }
}

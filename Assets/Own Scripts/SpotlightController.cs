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

    public Transform currentTarget;
    private Transform currentFlowTarget;
    private Dictionary<MeshRenderer, List<Material>> originalMaterials = new Dictionary<MeshRenderer, List<Material>>();
    private Dictionary<MeshRenderer, List<Material>> originalFlowMaterials = new Dictionary<MeshRenderer, List<Material>>();

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
            currentFlowTarget = targetObjects[0].targets[1];
        }

        // Cache the original materials for all targets and flow targets
        CacheOriginalMaterials();

        // Apply initial flow materials
        ApplyFlowMaterials(currentFlowTarget);
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
            Transform nextFlowTarget = targetObjects[targetIndex].targets[subTargetIndex + 1];
            // Apply or replace flow materials based on the flags
            if (targetObjects[targetIndex].replaceAllMaterials)
            {
                ReplaceAllMaterials(nextFlowTarget);
            }
            else
            {
                // Remove added flow materials from the current target
                RemoveAddedFlowMaterials(currentFlowTarget);
            }

            // Apply the updated flow materials to the next target
            ApplyFlowMaterials(nextFlowTarget);

            // Update the current target and flow target
            currentTarget = nextTarget;
            Debug.Log(targetIndex);
            currentFlowTarget = nextFlowTarget;
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

            // Cache the original flow materials for flow targets
            if (targetObject.addFlowMaterial)
            {
                foreach (var target in targetObject.targets)
                {
                    MeshRenderer targetRenderer = target.GetComponent<MeshRenderer>();
                    if (targetRenderer != null && !originalFlowMaterials.ContainsKey(targetRenderer))
                    {
                        originalFlowMaterials.Add(targetRenderer, targetRenderer.materials.ToList());
                    }
                }
            }
        }
    }

    private void RemoveAddedFlowMaterials(Transform target)
    {
        if (target != null)
        {
            MeshRenderer targetRenderer = target.GetComponent<MeshRenderer>();
            if (targetRenderer != null && originalFlowMaterials.TryGetValue(targetRenderer, out var originalFlowMats))
            {
                // Remove only the added flow materials
                targetRenderer.materials = originalFlowMats.ToArray();
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

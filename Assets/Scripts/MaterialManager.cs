using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable))] // Ensures there's an interactable component
public class VRMaterialManager : MonoBehaviour
{
    [Header("Materials")]
    public Material idleMaterial;
    public Material hoveringMaterial;
    public Material clickedMaterial;

    [Header("References")]
    public SkinnedMeshRenderer bodyRenderer; // Reference to the child's SkinnedMeshRenderer

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();

        // Assign idle material initially
        if (bodyRenderer != null && idleMaterial != null)
            bodyRenderer.material = idleMaterial;
        else
            Debug.LogWarning("VRMaterialManager: Missing bodyRenderer or idleMaterial reference.");

        // Subscribe to XR interaction events
        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);
        interactable.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        if (interactable != null)
        {
            interactable.hoverEntered.RemoveListener(OnHoverEntered);
            interactable.hoverExited.RemoveListener(OnHoverExited);
            interactable.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    // XR Events
    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        Hovering();
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        Idle();
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        OnClick();
    }

    // Material functions
    public void Hovering()
    {
        if (bodyRenderer != null && hoveringMaterial != null)
            bodyRenderer.material = hoveringMaterial;
    }

    public void OnClick()
    {
        if (bodyRenderer != null && clickedMaterial != null)
            bodyRenderer.material = clickedMaterial;
    }

    public void Idle()
    {
        if (bodyRenderer != null && idleMaterial != null)
            bodyRenderer.material = idleMaterial;
    }
}

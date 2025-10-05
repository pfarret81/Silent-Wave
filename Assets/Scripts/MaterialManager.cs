using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    [Header("Materials")]
    public Material idleMaterial;
    public Material hoveringMaterial;
    public Material clickedMaterial;

    [Header("References")]
    public SkinnedMeshRenderer bodyRenderer; // Reference to the child's SkinnedMeshRenderer

    void Awake()
    {
        if (bodyRenderer != null && idleMaterial != null)
            bodyRenderer.material = idleMaterial;
        else
            Debug.LogWarning("MaterialApplier: Missing bodyRenderer or idleMaterial reference.");
    }

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            OnClick();

        if (Input.GetKeyDown(KeyCode.T))
            Idle();

        if (Input.GetKeyDown(KeyCode.Y))
            Hovering();
    }
}
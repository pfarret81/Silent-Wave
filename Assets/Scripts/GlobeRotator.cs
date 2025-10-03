using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GlobeRotator : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    [Header("Rotation Settings")]
    public float rotationMultiplier = 1.5f;   // sensibilité rotation
    public float inertiaFactor = 1.5f;        // amplification de l’inertie
    public float damping = 2f;                // ralentissement

    private Transform interactorTransform;
    private Quaternion lastRotation;
    private Vector3 angularVelocity;
    private bool isGrabbed = false;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        interactorTransform = args.interactorObject.transform;
        lastRotation = interactorTransform.rotation;
        angularVelocity = Vector3.zero;
        isGrabbed = true;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        interactorTransform = null;
        isGrabbed = false;

        // On amplifie un peu la vélocité pour l’inertie
        angularVelocity *= inertiaFactor;
    }

    void Update()
    {
        if (isGrabbed && interactorTransform != null)
        {
            // Calculer la différence de rotation entre frames
            Quaternion deltaRot = interactorTransform.rotation * Quaternion.Inverse(lastRotation);

            // Convertir en axe/angle
            deltaRot.ToAngleAxis(out float angle, out Vector3 axis);
            if (angle > 180f) angle -= 360f;

            // Appliquer rotation à la Terre
            transform.Rotate(axis, -angle * rotationMultiplier, Space.World);

            // Stocker vitesse angulaire pour inertie
            angularVelocity = axis * -angle * rotationMultiplier / Time.deltaTime;

            lastRotation = interactorTransform.rotation;
        }
        else
        {
            // Appliquer l’inertie après lâcher
            if (angularVelocity.sqrMagnitude > 0.001f)
            {
                transform.Rotate(angularVelocity * Time.deltaTime, Space.World);
                angularVelocity = Vector3.Lerp(angularVelocity, Vector3.zero, Time.deltaTime * damping);
            }
        }
    }
}

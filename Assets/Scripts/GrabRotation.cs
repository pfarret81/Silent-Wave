using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabRotate : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    [Header("Rotation Settings")]
    public float rotationSensitivity = 5f;   // Sensibilité du geste
    public float inertiaFactor = 2f;        // Multiplicateur de l’inertie
    public float damping = 2f;              // Plus haut = ralentit plus vite

    private Transform interactorTransform;
    private Vector3 lastPos;
    private Vector3 angularVelocity;
    private bool isGrabbed = false;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        interactorTransform = args.interactorObject.transform;
        lastPos = interactorTransform.position;
        isGrabbed = true;
        angularVelocity = Vector3.zero;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        interactorTransform = null;
        isGrabbed = false;

        // Amplifier l’inertie au moment du lâcher
        angularVelocity *= inertiaFactor;
    }

    void Update()
    {
        if (isGrabbed && interactorTransform != null)
        {
            // Calcul déplacement du contrôleur
            Vector3 delta = interactorTransform.position - lastPos;

            // Utilisation du vecteur "horizontal" (droite/gauche) pour rotation intuitive
            float rotationAmountX = delta.x * rotationSensitivity * 100f;
            float rotationAmountY = -delta.y * rotationSensitivity * 100f;

            // Appliquer rotation intuitive (globe qu’on fait tourner à la main)
            transform.Rotate(Vector3.up, -rotationAmountX, Space.World);     // gauche/droite
            transform.Rotate(Vector3.right, rotationAmountY, Space.World);   // haut/bas

            // Sauvegarder la vélocité angulaire (plusieurs axes possibles)
            angularVelocity = new Vector3(rotationAmountY, -rotationAmountX, 0f);

            lastPos = interactorTransform.position;
        }
        else
        {
            // Appliquer l’inertie quand relâché
            if (angularVelocity.sqrMagnitude > 0.001f)
            {
                transform.Rotate(angularVelocity * Time.deltaTime, Space.World);
                angularVelocity = Vector3.Lerp(angularVelocity, Vector3.zero, Time.deltaTime * damping);
            }
        }
    }
}

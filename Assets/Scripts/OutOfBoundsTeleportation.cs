using UnityEngine;

public class OutOfBoundsTeleportation : MonoBehaviour
{
    public Transform destinationTransform;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Nique");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Point de TP: " +  destinationTransform.position);
            Debug.Log("Position du joueur avant: " +  other.transform.position);
            other.transform.position = destinationTransform.position;
            Debug.Log("Position du joueur après: " + other.transform.position);
        }
    }
}

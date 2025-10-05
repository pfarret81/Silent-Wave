using UnityEngine;

public class OutOfBoundsTeleportaiton : MonoBehaviour
{
    public Transform Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.position.y <= -9) {
            Player.position = new Vector3(15f, 10f, 82f);
        }
    }
}
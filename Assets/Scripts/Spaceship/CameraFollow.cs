using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0,-0.60f,6f);
    [SerializeField] private Transform player;
    [SerializeField] private float time;
    [SerializeField] private float distanceAhead;

    void Update()
    {
        Follow(player.position);
        LookAtPlayer(player.position);
    }
    private void Follow(Vector3 shipPosition)
    {
        Vector3 posToBe = shipPosition - offset;
        transform.position = Vector3.Lerp(transform.position,posToBe,2f);
    }
    private void LookAtPlayer(Vector3 shipPosition)
    {
        // float xRotation = ship.eulerAngles.x;
        // transform.eulerAngles = new Vector3(xRotation,0,0);
        // transform.LookAt(ship);

        Vector3 lookAhead = shipPosition + (player.forward * distanceAhead);
        Quaternion targetRotation = Quaternion.LookRotation(lookAhead - transform.position,player.up);
        float xRotation = targetRotation.eulerAngles.x;
        transform.eulerAngles = new Vector3(xRotation,0,0);
        // transform.eulerAngles = 
    }
}

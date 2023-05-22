using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform connection; //where we want to move
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 position = other.transform.position; //get object's current position
        position.x = this.connection.position.x;
        position.y = this.connection.position.y;
        other.transform.position = position;
    }
}

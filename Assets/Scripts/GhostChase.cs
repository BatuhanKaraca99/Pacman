using UnityEngine;

public class GhostChase : GhostBehaviour
{
    private void OnDisable() //if this script disabled
    {
        this.ghost.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>(); //reference

        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            //path finding
            Vector2 direction = Vector2.zero; //shortest direction
            float minDistance = float.MaxValue; //distance

            foreach(Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            this.ghost.movement.SetDirection(direction);
        }
    }
}

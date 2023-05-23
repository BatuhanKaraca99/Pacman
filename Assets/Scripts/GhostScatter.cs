using UnityEngine;

public class GhostScatter : GhostBehaviour
{
    private void OnDisable() //if this script disabled
    {
        this.ghost.chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       Node node = other.GetComponent<Node>(); //reference
       
        if(node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections[index] == -this.ghost.movement.direction && node.availableDirections.Count > 1)
            {
                index++; //change

                if(index >= node.availableDirections.Count)
                {
                    index = 0; //to prevent out of bound
                }
            }

            this.ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }
}

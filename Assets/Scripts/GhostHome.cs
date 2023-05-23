using System.Collections;
using UnityEngine;

public class GhostHome : GhostBehaviour
{
    public Transform inside;
    public Transform outside;

    private void OnEnable() //for security
    {
        StopAllCoroutines(); 
    }

    private void OnDisable()
    {
        if (gameObject.activeInHierarchy) //if object is active
        {
            StartCoroutine(ExitTransition());
        }
    }

    //move up-down while waiting
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            this.ghost.movement.SetDirection(-this.ghost.movement.direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector2.up, true); //forced up
        this.ghost.movement.rigidbody.isKinematic = true; //disable physics
        this.ghost.movement.enabled = false;

        //animation
        Vector3 position = this.transform.position;

        float duration = 0.5f;
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsed / duration); //move towards with percentage
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed+=Time.deltaTime;
            yield return null; //wait frame and continue left off
        }

        elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(this.inside.position, this.outside.position, elapsed / duration); //move towards with percentage
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null; //wait frame and continue left off
        }

        this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true); //return, go to right or left
        this.ghost.movement.rigidbody.isKinematic = false; 
        this.ghost.movement.enabled = true;
    }
}

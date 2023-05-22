using UnityEngine;

public class Ghost : MonoBehaviour
{
    //for each ghost
    public Movement movement { get; private set; }
    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }
    public GhostBehaviour initialBehaviour;
    public Transform target; //to chase or escape from(pacman)

    public int points = 200; //we can change it,because it is public

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.home = GetComponent<GhostHome>();
        this.scatter = GetComponent<GhostScatter>();
        this.chase = GetComponent<GhostChase>();
        this.frightened = GetComponent<GhostFrightened>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable(); //only 1 behaviour running

        if(this.home != this.initialBehaviour)
        {
            this.home.Disable();
        }

        if (this.initialBehaviour != null)
        {
            this.initialBehaviour.Enable();    
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this); //passing the ghost who is eaten
            } else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}

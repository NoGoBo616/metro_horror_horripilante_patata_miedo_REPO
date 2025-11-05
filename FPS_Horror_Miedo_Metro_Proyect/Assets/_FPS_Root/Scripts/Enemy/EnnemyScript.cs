using UnityEngine;

public class EnnemyScript : MonoBehaviour
{
    public float speed = 1f;
    public float chaseSpeed = 20f;
    public Transform pointA;
    public Transform pointB;
    public Transform player;
    public Transform rayOrigin;
    public PC_Controller controller;

    Rigidbody rb;
    public bool chasingPlayer = false;
    public Animator anim;

    public Vector3 currentTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentTarget = pointA.position;
    }

    void FixedUpdate()
    {
        if (player == null || rayOrigin == null) return;

        // Si está persiguiendo al jugador
        if (chasingPlayer)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        // Raycast para detectar al jugador
        Vector3 origin = rayOrigin.position;
        Vector3 direction = rayOrigin.forward;

        // Dibuja el raycast en el editor
        Debug.DrawRay(origin, direction * 10f, Color.red);

        if (Physics.Raycast(origin, direction, out RaycastHit hit, 10f))
        {
            if (hit.transform == player)
            {
                anim.SetTrigger("Attack");
                chasingPlayer = true;
            }
        }
    }

    void Patrol()
    {
        Vector3 dir = (currentTarget - transform.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
        transform.LookAt(currentTarget);

        if (Vector3.Distance(transform.position, currentTarget) < 0.5f)
        {
            currentTarget = currentTarget == pointA.position ? pointB.position : pointA.position;
        }
    }

    public void Hide()
    {
        if (chasingPlayer)
        {
            chasingPlayer = false;
            currentTarget = pointA.position;
            anim.SetTrigger("Hide");
        }
    }

    void ChasePlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + dir * chaseSpeed * Time.fixedDeltaTime);
        transform.LookAt(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("scream");
            controller.Jumpscare();
        } 
    }

    //Dibuja un gizmo permanente en el editor para ver el raycast incluso sin ejecutar el juego
    private void OnDrawGizmos()
    {
        if (rayOrigin != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + rayOrigin.forward * 10f);
        }
    }
}

using System.Collections;
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

    private Rigidbody rb;
    public bool chasingPlayer = false;
    public Animator anim;

    public Vector3 currentTarget;
    private bool isTurning = false; // Evita moverse mientras gira, pero no bloquea el raycast

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentTarget = pointA.position;
    }

    void FixedUpdate()
    {
        if (player == null || rayOrigin == null) return;

        // Siempre lanzar el raycast, incluso si está girando
        DoRaycast();

        // No moverse mientras gira
        if (isTurning) return;

        if (chasingPlayer)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void DoRaycast()
    {
        Vector3 origin = rayOrigin.position;
        Vector3 direction = rayOrigin.forward;

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
            // Cambiar entre puntos A y B
            currentTarget = currentTarget == pointA.position ? pointB.position : pointA.position;

            // Iniciar rotación lenta de 180° aleatoria
            float randomRotation = Random.Range(0, 2) == 0 ? 180f : -180f;
            StartCoroutine(SmoothTurn(randomRotation, 0.5f)); // Gira en 0.5 segundos
        }
    }

    IEnumerator SmoothTurn(float angle, float duration)
    {
        isTurning = true;

        Quaternion startRot = transform.rotation;
        Quaternion endRot = transform.rotation * Quaternion.Euler(0f, angle, 0f);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / duration);
            elapsed += Time.deltaTime;

            // Permite que el raycast funcione incluso durante la rotación
            DoRaycast();

            yield return null;
        }

        transform.rotation = endRot;
        isTurning = false;
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

    private void OnDrawGizmos()
    {
        if (rayOrigin != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + rayOrigin.forward * 10f);
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PC_Controller : MonoBehaviour
{
    [Header("Movement & Look")]
    public GameObject camHolder;
    public float speed = 5f;
    public float sensitiviti = 0.1f;

    //Object References
    Rigidbody playerRb;

    //Input Variables
    Vector2 moveInput;
    Vector2 lookInput;
    float lookRotation;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Debug.DrawRay(camHolder.transform.position, camHolder.transform.forward * 3f, Color.red);
    }

    //Movimiento

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 movimiento = new Vector3(moveInput.x, 0, moveInput.y) * speed * Time.deltaTime;
        transform.Translate(movimiento);
    }

    //Camara

    void CameraLook()
    {
        transform.Rotate(Vector3.up * lookInput.x * sensitiviti);
        lookRotation += (-lookInput.y * sensitiviti);
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);
        camHolder.transform.localEulerAngles = new Vector3(lookRotation, 0, 0);
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        lookInput = ctx.ReadValue<Vector2>();
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PC_Controller : MonoBehaviour
{
    [Header("Movement & Look")]
    public GameObject camHolder;
    public GameObject pasword;
    public float speed = 5f;
    public float sensitiviti = 0.1f;
    public bool panel;
    public bool panelAct;
    public EnnemyScript ennemyScript;

    [Header("UI")]
    public GameObject e;
    public GameObject barraTexto;
    public GameObject jumpscare;
    public int sceneToLoad;

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
        //Raycast

        Ray ray = new Ray(camHolder.transform.position, camHolder.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f))
        {
            if (hit.collider.CompareTag("panel"))
            {
                panel = true;
                e.gameObject.SetActive(true);
            }
            else
            {
                panel = false;
                e.gameObject.SetActive(false);
            }
        }
        else
        {
            panel = false;
            e.gameObject.SetActive(false);
        }

        //Barra de texto

        if (panelAct)
        {
            barraTexto.gameObject.SetActive(true);
        }
        else
        {
            barraTexto.gameObject.SetActive(false);
        }
    }
    
    //Movimiento

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (panelAct) return;
        Vector3 movimiento = new Vector3(moveInput.x, 0, moveInput.y) * speed * Time.deltaTime;
        transform.Translate(movimiento);
    }

    //Camara

    void CameraLook()
    {
        if (panelAct) return;
        transform.Rotate(Vector3.up * lookInput.x * sensitiviti);
        lookRotation += (-lookInput.y * sensitiviti);
        lookRotation = Mathf.Clamp(lookRotation, -90f, 90f);

        camHolder.transform.localEulerAngles = new Vector3(lookRotation, 0f, 0f);
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    //Input

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        lookInput = ctx.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (panel)
        {
            panelAct = !panelAct;
        }
    }

    //Trigger

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hide"))
        {
            Debug.Log("hide");
            ennemyScript.Hide();
        }
    }

    public void Jumpscare()
    {
        StartCoroutine(Scream());
    }

    //IE Numerators

    public IEnumerator Scream()
    {
        jumpscare.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneToLoad);
        ennemyScript.Hide();
        jumpscare.SetActive(false);
        yield return null;
    }
}

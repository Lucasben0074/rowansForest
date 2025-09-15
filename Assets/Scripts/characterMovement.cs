using UnityEngine;

public class characterMovement : MonoBehaviour
{

    [Header("Movement stats")]
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float crouchSpeed = 1f;
    [SerializeField] private float sprintSpeed = 10f;

    [Header("Deteccion de suelo")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float radioSphereCheck = 0.1f;
    private bool isGrounded;

    //atributos de logica
    private float startVelocity;
    private float moveH, moveV;
    private Vector3 playerMovement;
    private Vector3 playerMovementX;
    private Vector3 playerMovementZ;
    private Quaternion rotationMovement;
    private Vector3 normalizedDirection;
    private Rigidbody rb;
    private Animator anim;
    private bool isCrouched = false;
    

    [Header("Control de la camara")]
    [SerializeField] Transform playerCamera;

   
    void Start()
    {
        startVelocity = playerSpeed;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        
        // logica e inputs para movimiento de jugador. 
        moveH = Input.GetAxisRaw("Horizontal");
        moveV = Input.GetAxisRaw("Vertical");
        playerMovement = moveV * playerCamera.forward + moveH * playerCamera.right;
        if (playerMovement.magnitude > 0.1f)
        {
            anim.SetBool("playerMovement", true);
        }
        else
        {
            anim.SetBool("playerMovement",false);
        }

        //Logica de salto con un checkSphere.
        isGrounded = Physics.CheckSphere(transform.position, radioSphereCheck, groundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            anim.SetTrigger("jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }


        //movimiento sigilo + sprint, prioridades.
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouched = !isCrouched;
            anim.SetBool("crouched", isCrouched);
        }
        if (!isCrouched && Input.GetKey(KeyCode.LeftShift) && playerMovement.magnitude > 0.1f)
        {
            anim.SetBool("sprint", true);
            playerSpeed = sprintSpeed;
        }
        else
        {
            anim.SetBool("sprint", false);
            playerSpeed = isCrouched ? crouchSpeed : startVelocity;
        }

    }

    private void FixedUpdate()
    {
        //movimiento con fisicas, normalizado e interpolacion para movimiento suave
        if (playerMovement.magnitude > 0.1f)
        {
            normalizedDirection = playerMovement.normalized;
            normalizedDirection.y = 0;
            rb.MovePosition(rb.position + normalizedDirection * playerSpeed * Time.fixedDeltaTime);
            rotationMovement = Quaternion.LookRotation(normalizedDirection);           
            Quaternion smoothRotation = Quaternion.RotateTowards(rb.rotation, rotationMovement, rotationSpeed * Time.fixedDeltaTime);  
            rb.MoveRotation(smoothRotation);
        }
    }
}

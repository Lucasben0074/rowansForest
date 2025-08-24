using UnityEngine;

public class characterMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotationSpeed = 360f;

    private float moveH, moveV;
    private Vector3 playerMovement;
    private Vector3 playerMovementX;
    private Vector3 playerMovementZ;
    private Quaternion rotationMovement;
    private Vector3 normalizedDirection;
    private Rigidbody rb;
    private Animator anim;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Inputs y animaciones de movimiento

        moveH = Input.GetAxisRaw("Horizontal");
        moveV = Input.GetAxisRaw("Vertical");

        playerMovementX = new Vector3(moveH, 0f, 0f);
        playerMovementZ = new Vector3(0f, 0f, moveV);
        playerMovement = playerMovementX + playerMovementZ;

        if (playerMovement.magnitude > 0.1f )
        {
            anim.SetBool("playerMovement", true);
        }
        else
        {
            anim.SetBool("playerMovement",false);
        }

        // Input y animacion de Sprint

        if (Input.GetKey(KeyCode.LeftShift) && playerMovement.magnitude > 0.1f)
        {
            anim.SetBool("sprint", true);
            playerSpeed = 10f;
        }
        else
        {
            anim.SetBool("sprint", false);
            playerSpeed = 5f;
        }

        // Input y animacion de salto

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    private void FixedUpdate()
    {
        if (playerMovement.magnitude > 0.1f)
        {
            normalizedDirection = playerMovement.normalized;   
            rb.MovePosition(rb.position + normalizedDirection * playerSpeed * Time.fixedDeltaTime);
            rotationMovement = Quaternion.LookRotation(normalizedDirection);
            Quaternion smoothRotation = Quaternion.RotateTowards(rb.rotation, rotationMovement, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(smoothRotation);
        }
    }
}

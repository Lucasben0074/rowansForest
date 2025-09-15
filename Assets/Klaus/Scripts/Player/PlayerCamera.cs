using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform playerCameraTarget;
    [SerializeField] Transform playerCamera;

    [SerializeField] private float cameraSensitivity = 45f;

    private float mouseX;

    private InputAction lookAction;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start()
    {
        lookAction = InputSystem.actions.FindAction("Look");
    }

    void LateUpdate()
    {
        CameraPositionHandler();
    }

    private void CameraPositionHandler()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        mouseX += lookInput.x * Time.deltaTime * cameraSensitivity;

        playerCameraTarget.rotation = Quaternion.Euler(20, mouseX, 0);

    }
}

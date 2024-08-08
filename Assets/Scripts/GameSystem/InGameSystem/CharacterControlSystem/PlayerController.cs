using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Player movement speed
    public float rotationSpeed = 100f; // Rotation speed
    public Transform cameraTransform; // Camera Transform
    public float gravity = -9.81f; // Gravity

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float currentYRotation = 0f; // Current Y-axis rotation angle
    private float verticalVelocity = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            characterController = gameObject.AddComponent<CharacterController>();
        }

        Cursor.lockState = CursorLockMode.None; // Unlock cursor
        cameraTransform.localRotation = Quaternion.Euler(0f, 90f, 0f); // Set camera default angle Y=90
        currentYRotation = transform.eulerAngles.y;
    }

    void Update()
    {
        HandleRotation();
        HandleMovementInput();
        ApplyMovement();
    }

    void HandleRotation()
    {
        float rotateY = 0f;

        if (Input.GetKey(KeyCode.Q))
        {
            rotateY = -rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotateY = rotationSpeed * Time.deltaTime;
        }

        currentYRotation += rotateY;
        currentYRotation = Mathf.Clamp(currentYRotation, -90f, 90f); // Limit rotation angle between -90 and 90 degrees

        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
    }

    void HandleMovementInput()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Get camera forward and right direction
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ignore Y axis direction
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Calculate movement direction based on camera direction
        moveDirection = (forward * moveZ + right * moveX).normalized * moveSpeed;
    }

    void ApplyMovement()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = 0f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 velocity = moveDirection + Vector3.up * verticalVelocity;
        characterController.Move(velocity * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EndPoint"))
        {
            Time.timeScale = 0f;
        }
    }
}
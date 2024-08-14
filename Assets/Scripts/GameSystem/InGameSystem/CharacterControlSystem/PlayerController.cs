using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    public float rotationSpeed = 100f;
    public Transform cameraTransform;
    public float gravity = -9.81f;

    public AudioSource footstepAudioSource;
    public AudioClip footstepClip;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float currentYRotation = 0f;
    private float verticalVelocity = 0f;
    private bool isRunning = false;
    private bool isWalking = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            characterController = gameObject.AddComponent<CharacterController>();
        }

        cameraTransform.localRotation = Quaternion.Euler(0f, 90f, 0f);
        currentYRotation = transform.eulerAngles.y;

        if (footstepAudioSource == null)
        {
            Debug.LogError("Footstep Audiosource Not Set.");
        }
    }

    void Update()
    {
        HandleRotation();
        HandleMovementInput();
        ApplyMovement();
        HandleFootsteps();
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
        currentYRotation = Mathf.Clamp(currentYRotation, -120f, 120f);

        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
    }

    void HandleMovementInput()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        float speed = isRunning ? runSpeed : moveSpeed;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * moveZ + right * moveX).normalized * speed;

        isWalking = (moveX != 0 || moveZ != 0);
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

    void HandleFootsteps()
    {
        if (isWalking && characterController.isGrounded)
        {
            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.clip = footstepClip;
                footstepAudioSource.pitch = isRunning ? 1.5f : 1f;
                footstepAudioSource.Play();
            }
        }
        else
        {
            if (footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Pause();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EndPoint"))
        {
            Time.timeScale = 0f;
        }
    }
}
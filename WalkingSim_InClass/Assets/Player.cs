using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Control State")]
    public bool canLook = true;
    public bool canMove = true;
    public bool canInteractInput = true;
    public BoxCarryInteractable carriedBox;
    public MealCarryInteractable carriedMeal;
    [Header("State")]
    public bool uiMode = false;
    [Header("UI")]

    public Image reticleImage;
    public InteractHintUI interactHintUI;

    [Header("Movement")]
    public float walkSpeed = 5;
    public float runSpeed = 9;
    public float jumpHeight = 5;

    public Transform cameraTransform;
    public float lookSensativity = 1f;

    private CharacterController gg;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float verticalVelocity;
    private float gravity = -20f;
    private float pitch;


    private GameObject currentTarget;

    private bool interactPressed;

    public static event Action<NPCData> OnDialogueRequested;
    private Interactable currentInteractable;


    private bool isRunning;
    private bool isJumping;

    void Awake()
    {
        gg = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (reticleImage != null)
            reticleImage.color = new Color(0, 0, 0, .7f);

        if (interactHintUI != null)
            interactHintUI.HideHint();
    }


    private void Update()
    {
        CheckInteract();

        if (!uiMode)
        {
            HandleLook();
            HandleMovement();
            HandleInteract();
        }
    }

    private void HandleLook()
    {
        float yaw = lookInput.x * lookSensativity;

        float pitchDelta = lookInput.y * lookSensativity;

        transform.Rotate(Vector3.up * yaw);

        pitch -= pitchDelta;
        pitch = Mathf.Clamp(pitch, -90, 90);

        cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }

    private void HandleMovement()
    {
        bool grounded = gg.isGrounded;
        Debug.Log("is grounded: " +  grounded);

        if (grounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        float currentSpeed = walkSpeed;

        if (isRunning)
        {
            currentSpeed = runSpeed;
        }
        else if (!isRunning)
        {
            currentSpeed = walkSpeed;
        }

        Vector3 move = transform.right * moveInput.x * currentSpeed + transform.forward * moveInput.y * currentSpeed;

        if (isJumping && grounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
        {
            isJumping = false;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = Vector3.up * verticalVelocity;

        gg.Move((move + velocity) * Time.deltaTime);


    }
    void CheckInteract()
    {
        currentInteractable = null;

        if (reticleImage != null)
            reticleImage.color = new Color(0, 0, 0, .7f);

        if (interactHintUI != null)
            interactHintUI.HideHint();

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            currentInteractable = hit.collider.GetComponentInParent<Interactable>();

            if (currentInteractable != null)
            {
                if (reticleImage != null)
                    reticleImage.color = Color.red;

                PhoneInteractable phone = currentInteractable as PhoneInteractable;
                if (phone != null && phone.CanShowInteractHint())
                {
                    if (interactHintUI != null)
                        interactHintUI.ShowHint("Press E to Answer");
                }

            }

            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 3, Color.blue);
        }
    }

    void HandleInteract()
    {
        //if the player did not press interact this frame do nothing
        if (!interactPressed) return;
        //consume the input so one click only triggers one interactions
        //this changes next frame
        interactPressed = false;
        if (currentInteractable == null) return;
        currentInteractable.Interact(this);

    }


    public void OnMove(InputAction.CallbackContext context)
    {
        if (!canMove)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!canLook)
        {
            lookInput = Vector2.zero;
            return;
        }

        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!canMove) return;

        if (context.performed) isJumping = true;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (!canMove)
        {
            isRunning = false;
            return;
        }

        isRunning = context.ReadValueAsButton();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (uiMode) return;

        if (context.performed) interactPressed = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("gg Collided with: " + hit.gameObject.name);
    }
    public void RequestDialogue(NPCData nPCData)
    {
        OnDialogueRequested?.Invoke(nPCData);
    }


    public void SetControlEnabled(bool enabled)
    {
        canLook = enabled;
        canMove = enabled;
        canInteractInput = enabled;

        if (!enabled)
        {
            moveInput = Vector2.zero;
            lookInput = Vector2.zero;
            isRunning = false;
            isJumping = false;
            interactPressed = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void SetUIMode(bool value)
    {
        uiMode = value;

        if (uiMode)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void ResetVerticalVelocity()
    {
        verticalVelocity = 0f;
        isJumping = false;
    }

    public void ResetLookRotation()
    {
        pitch = 0f;

        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}

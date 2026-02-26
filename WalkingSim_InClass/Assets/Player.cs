using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
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
    public Image reticleImage;
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

        reticleImage = GameObject.Find("Reticle").GetComponent<Image>();
        reticleImage.color = new Color(r: 0, g: 0, b: 0, a: .7f);
    }


    private void Update()
    {
        HandleLook();
        HandleMovement();
        CheckInteract();
        HandleInteract();
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
        //reset reticle image to normal color first
        if (reticleImage != null) reticleImage.color = new Color(0, 0, 0, .7f);
        //make a ray that goes straight out of the camera(center of screen)
        //players eyesight
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        //RaycastHit hit;
        //asking unity if it hit something within 3 units
        //hit stores what we hit like the collider
        //bool didHit = Physics.Raycast(ray, out hit, 3);
        //if (!didHit) return;//if we didn't hit anything start here
        //if we hit something tagged interactable
        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            currentInteractable = hit.collider.GetComponentInParent<Interactable>();
            if (currentInteractable != null && reticleImage != null)
            {
                reticleImage.color = Color.red;
                Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 3, Color.blue);
            }
            else
            {
                Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 3, Color.blue);
            }
            
        }

        Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 3, Color.blue);
    }

    void HandleInteract()
    {
        //if the player did not press interact this frame do nothing
        if (!interactPressed) return;
        //consume the input so one click only triggers one interactions
        //this changes next frame
        interactPressed = false;
        if (currentTarget == null) return;
        currentInteractable.Interact(this);

    }


    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed) isJumping = true;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        isRunning = context.ReadValueAsButton();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed) interactPressed = true; 
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("gg Collided with: " + hit.gameObject.name);
    }
    public void RequestDialogue(NPCData nPCData)
    {
        OnDialogueRequested?.Invoke(nPCData);
    }
}

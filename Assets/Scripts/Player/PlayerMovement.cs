using UnityEngine;
using UnityEngine.InputSystem; // Import the new Input System namespace

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController controller;
    private InputSystem_Actions inputActions; // Reference to your generated input actions class
    private Vector2 moveInput;
    public PlayerEnergy playerEnergy; // Assign in inspector or via script

    // Public property to access moveInput
    public Vector2 MoveInput => moveInput;

    // Returns true if the player is currently moving
    public bool IsMoving()
    {
        return moveInput.sqrMagnitude > 0.01f;
    }

    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float speedMultiplier = 1f;
        if (playerEnergy != null)
        {
            float energyPercent = playerEnergy.GetEnergyPercent();
            if (energyPercent < 0.3f)
                speedMultiplier = 0.5f; // Half speed below 30% energy
        }

        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);

        if (direction.magnitude > 1)
            direction = direction.normalized;

        controller.SimpleMove(direction * moveSpeed * speedMultiplier);
    }
}

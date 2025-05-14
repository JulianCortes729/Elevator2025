using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset InputActions;
    private InputAction m_jumpAction;
    private InputAction m_moveAction;
    private Rigidbody rb;
    private Vector2 m_moveInput;
    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        
        m_jumpAction = InputSystem.actions.FindAction("Jump");
        m_moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate(){
        Walking();
    }

    void Update(){
        m_moveInput = m_moveAction.ReadValue<Vector2>();

        if(m_jumpAction.WasPressedThisFrame()){
            Jump();
        }
    }

    public void Jump(){
        rb.AddForceAtPosition(new Vector3(0f,5f,0f), Vector3.up, ForceMode.Impulse);
    }
    private void Walking(){
       rb.MovePosition(rb.position + new Vector3(m_moveInput.x, 0f, m_moveInput.y) * moveSpeed * Time.fixedDeltaTime);
    }
    
}

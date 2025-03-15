using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    // Vector de dirección del movimiento (input del jugador)
    Vector2 moveVector;

    [SerializeField] float speed = 10f;// Velocidad máxima horizontal

    [SerializeField] float acceleration = 20f;// Tasa de aceleración/suavizado

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Método llamado por el sistema de Input de Unity (Evento de Input)
    public void InputPlayer(InputAction.CallbackContext _context){
        moveVector = _context.ReadValue<Vector2>();
    }


    // Lógica de física (se ejecuta en intervalos fijos)
    private void FixedUpdate()
    {
        // 1. Calcular velocidad objetivo en eje X
        float targetVelocityX = moveVector.x * speed;

        // 2. Suavizar transición entre velocidad actual y objetivo
        float smoothedVelocityX = Mathf.Lerp(
            rb.linearVelocity.x,
            targetVelocityX,
            acceleration * Time.fixedDeltaTime
        );
               
        // 3. Aplicar nueva velocidad manteniendo velocidad en Y (gravedad/saltos)
        rb.linearVelocity = new Vector3(smoothedVelocityX, rb.linearVelocity.y, 0f);
    }

}

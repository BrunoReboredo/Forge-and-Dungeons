using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    private Vector3 moveDirection;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W)) moveZ = 1f;  // Arriba
        if (Input.GetKey(KeyCode.S)) moveZ = -1f; // Abajo
        if (Input.GetKey(KeyCode.A)) moveX = -1f; // Izquierda
        if (Input.GetKey(KeyCode.D)) moveX = 1f;  // Derecha

        moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}

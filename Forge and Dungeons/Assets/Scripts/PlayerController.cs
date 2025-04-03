using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f; // Velocidad de movimiento
    [SerializeField] GameObject capsulePrefab; // Prefab del "palo"
    [SerializeField] float capsuleDistance = 1f; // Distancia del palo respecto al jugador
    private GameObject capsuleInstance; // Instancia del palo
    private Vector3 moveDirection;

    private void Start()
    {
        // Instanciamos el palo y lo rotamos en horizontal
        if (capsulePrefab != null)
        {
            capsuleInstance = Instantiate(capsulePrefab, transform.position + transform.forward * capsuleDistance, Quaternion.identity);
            capsuleInstance.transform.SetParent(transform); // Lo hacemos hijo del jugador
            capsuleInstance.transform.rotation = Quaternion.Euler(0, 0, 90); // Gira la cápsula en horizontal
        }
    }

    private void Update()
    {
        HandleMovement();
        UpdateCapsulePosition();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void HandleMovement()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W)) moveZ = 1f;  // Adelante
        if (Input.GetKey(KeyCode.S)) moveZ = -1f; // Atrás
        if (Input.GetKey(KeyCode.A)) moveX = -1f; // Izquierda
        if (Input.GetKey(KeyCode.D)) moveX = 1f;  // Derecha

        moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        if (moveDirection != Vector3.zero)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            transform.forward = moveDirection; // Hace que el personaje mire hacia donde se mueve
        }
    }

    private void UpdateCapsulePosition()
    {
        if (capsuleInstance != null)
        {
            // Posiciona el palo delante del jugador
            capsuleInstance.transform.position = transform.position + transform.forward * capsuleDistance;
            capsuleInstance.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90); // Mantiene el palo en horizontal
        }
    }
}


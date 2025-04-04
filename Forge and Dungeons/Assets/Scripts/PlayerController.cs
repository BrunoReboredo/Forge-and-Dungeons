using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 200f;
    [SerializeField] GameObject capsulePrefab; // El "palo"
    [SerializeField] float capsuleDistance = 1f; // Distancia delante del jugador

    private Vector3 moveDirection;
    private GameObject capsuleInstance;

    private void Start()
    {
        if (capsulePrefab != null)
        {
            capsuleInstance = Instantiate(capsulePrefab, transform.position + transform.forward * capsuleDistance, Quaternion.identity);
            capsuleInstance.transform.SetParent(transform); // El palo sigue al jugador
            capsuleInstance.transform.rotation = Quaternion.Euler(0, 0, 90); // Poner en horizontal (ajustar si necesario)
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleMouseRotation();
        UpdateCapsulePosition();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void HandleMovement()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W)) moveZ = 1f;
        if (Input.GetKey(KeyCode.S)) moveZ = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void HandleMouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);
    }

    private void UpdateCapsulePosition()
    {
        if (capsuleInstance != null)
        {
            capsuleInstance.transform.position = transform.position + transform.forward * capsuleDistance;
            capsuleInstance.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90); // Mantenerlo en horizontal
        }
    }
}




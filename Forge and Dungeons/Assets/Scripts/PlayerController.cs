using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 200f;
    [SerializeField] float capsuleDistance = 10f; // Distancia delante del jugador
    [SerializeField] GameObject lanternPrefab; // Prefab de la linterna

    private Vector3 moveDirection;
    private GameObject capsuleInstance;
    private GameObject lanternInstance;
    private bool lanternOn = false;

    private void Start()
    {

    }

    private void Update()
    {
        HandleMovement();
        HandleMouseRotation();
        UpdateCapsulePosition();
        HandleLanternToggle();
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

    private void HandleLanternToggle()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("se ha pulsado la letra I para la luz");
            if (lanternInstance == null)
            {
                // Instanciamos la linterna por primera vez
                lanternInstance = Instantiate(lanternPrefab, transform);
                lanternInstance.transform.localPosition = new Vector3(0, 0, 1f);
                lanternOn = true;
            }
            else
            {
                // Activamos/desactivamos la linterna
                lanternOn = !lanternOn;
                lanternInstance.SetActive(lanternOn);
            }
        }
    }
}




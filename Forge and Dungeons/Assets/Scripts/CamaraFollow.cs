using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // Referencia al jugador
    [SerializeField] private Vector3 offset;  // Offset de la cámara


    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player")?.transform;
        }
    }

    private void LateUpdate()
    {
        // Mantiene la posición de la cámara sin rotar con el jugador
        transform.position = player.position + offset;
    }
}

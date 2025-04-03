using System.Collections;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float attackRange = 3f; // Rango de ataque
    public float attackAngle = 90f; // Ángulo de la zona de ataque
    public float damage = 10f; // Daño del ataque
    public GameObject arrowPrefab; // Prefab de la flecha PLACEHOLDER

    private Transform playerTransform;
    private Camera playerCamera;
    private Collider[] hitEnemies;
    private GameObject arrowInstance; // Instancia de la flecha en el juego PLACEHOLDER

    private void Start()
    {
        playerTransform = transform;
        playerCamera = Camera.main;

        // Instanciar la flecha y colocarla un poco adelante y sobre el jugador PLACEHOLDER
        arrowInstance = Instantiate(arrowPrefab, playerTransform.position + Vector3.up * 25f, Quaternion.identity);
        arrowInstance.transform.SetParent(playerTransform); // Mantener la flecha fija al jugador PLACEHOLDER
    }

    private void Update()
    {
        // Actualizar la rotación de la flecha según la dirección en la que mira el jugador
        Vector3 forwardDirection = playerCamera.transform.forward;

        // Mover la flecha un poco adelante del jugador en la dirección en la que está mirando
        arrowInstance.transform.position = playerTransform.position + forwardDirection * 7.5f + Vector3.up * 7.5f;

        // Rotar la flecha para que apunte en la misma dirección que el jugador
        arrowInstance.transform.rotation = Quaternion.LookRotation(forwardDirection);

        // Si el jugador hace clic izquierdo
        if (Input.GetMouseButtonDown(0)) // Clic izquierdo del mouse
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        // Obtener la dirección en la que está mirando el jugador
        Vector3 forwardDirection = playerCamera.transform.forward;

        // Detectar el área de ataque
        Vector3 attackDirection = playerTransform.forward;

        // Comprobar si el jugador está mirando dentro de la zona de ataque
        if (Vector3.Angle(forwardDirection, attackDirection) < attackAngle / 2)
        {
            // Realizar la animación (aún no implementada)
            // animator.SetTrigger("Attack");

            // Obtener todos los enemigos dentro del rango del área de ataque
            hitEnemies = Physics.OverlapSphere(playerTransform.position, attackRange);

            foreach (var enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy")) // Comprobar si el objeto es un enemigo
                {
                    // Activar el daño al enemigo
                    // Log como placeholder
                    Debug.Log("Daño infligido a: " + enemy.name);
                    // enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
                }
            }
        }
    }
}

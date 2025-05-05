using System.Collections;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] float attackRadius = 1.2f;
    [SerializeField] float attackDamage = 10f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform attackPoint;



    private bool canAttack = true;

    void Update()
    {
        /* if (Input.GetMouseButtonDown(0)) // 0 = botón izquierdo del ratón
         {
             Debug.Log("¡Botón izquierdo del ratón presionado!");
         }*/


    }

    void PerformAttack()
    {
        canAttack = false;

        // Detecta enemigos en el área de golpe
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRadius, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }

        // Cooldown
        Invoke(nameof(ResetAttack), 0.3f);
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}

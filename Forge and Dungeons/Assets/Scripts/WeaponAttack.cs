using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    [SerializeField] WeaponStats stats;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] GameObject weaponHolder;

    [Header("Modelos de armas")]
    [SerializeField] private GameObject swordModelPrefab;
    [SerializeField] private GameObject spearModelPrefab;
    [SerializeField] private GameObject axeModelPrefab;
    [SerializeField] private GameObject hammerModelPrefab;
    private float attackCooldown = 0f;
    private GameObject currentWeaponModel;

    void Start()
    {
        EquipWeaponModel();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && attackCooldown <= 0f)
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log($"Atacando con {stats.weaponType} de {stats.materialType}");

        switch (stats.weaponType)
        {
            case WeaponType.Sword:
                PerformMeleeAttack(stats.range, stats.GetDamage());
                attackCooldown = 1f / stats.attackRate;
                break;

            case WeaponType.Spear:
                PerformMeleeAttack(stats.range + 1f, stats.GetDamage() * 0.9f);
                attackCooldown = 1f / stats.attackRate;
                break;

            case WeaponType.Axe:
                PerformMeleeAttack(stats.range * 0.8f, stats.GetDamage() * 1.5f);
                attackCooldown = 1.5f;
                break;

            case WeaponType.Hammer:
                PerformMeleeAttack(stats.range * 0.8f, stats.GetDamage() * 1.3f);
                attackCooldown = 0.8f;
                break;
        }
    }

    void PerformMeleeAttack(float range, float damage)
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, range, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(damage);
        }
    }

    void EquipWeaponModel()
    {
        // Borra el modelo anterior si existe
        if (currentWeaponModel != null)
        {
            Destroy(currentWeaponModel);
        }

        GameObject modelToEquip = null;

        switch (stats.weaponType)
        {
            case WeaponType.Sword:
                modelToEquip = swordModelPrefab;
                break;
            case WeaponType.Spear:
                modelToEquip = spearModelPrefab;
                break;
            case WeaponType.Axe:
                modelToEquip = axeModelPrefab;
                break;
            case WeaponType.Hammer:
                modelToEquip = hammerModelPrefab;
                break;
        }

        if (modelToEquip != null && weaponHolder != null)
        {
            // Instancia el modelo sin parent
            currentWeaponModel = Instantiate(modelToEquip, weaponHolder.transform.position, weaponHolder.transform.rotation);
            // Luego, asigna el WeaponHolder como su padre
            currentWeaponModel.transform.SetParent(weaponHolder.transform);
        }
        else
        {
            Debug.LogWarning("Falta asignar el modelo o el weaponHolder.");
        }
    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, stats.range);
    }
}
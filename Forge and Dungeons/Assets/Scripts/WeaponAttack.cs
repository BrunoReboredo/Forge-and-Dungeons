using UnityEngine;
using System.Collections.Generic;
using System;

public class WeaponAttack : MonoBehaviour
{
    public WeaponStatsSO stats;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] GameObject weaponHolder;
    [Header("Prefabs de armas (combinaciones tipo/material)")]
    [SerializeField] private List<WeaponModelEntry> weaponPrefabs;


    private float attackCooldown = 0f;
    private GameObject currentWeaponModel;

    void Start()
    {
        WeaponInstance weapon = weaponHolder.GetComponentInChildren<WeaponInstance>();

        if (weapon != null && weapon.stats != null)
        {
            stats = weapon.stats;
            EquipWeaponModel();
        }

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

        switch (stats.weaponType)
        {
            case WeaponType.Sword:
                PerformMeleeAttack(stats.range, stats.baseDamage);
                attackCooldown = 1f / stats.attackRate;
                break;

            case WeaponType.Spear:
                PerformMeleeAttack(stats.range + 1f, stats.baseDamage * 0.9f);
                attackCooldown = 1f / stats.attackRate;
                break;

            case WeaponType.Axe:
                PerformMeleeAttack(stats.range * 0.8f, stats.baseDamage * 1.5f);
                attackCooldown = 1.5f;
                break;

            case WeaponType.Hammer:
                PerformMeleeAttack(stats.range * 0.8f, stats.baseDamage * 1.3f);
                attackCooldown = 0.8f;
                break;
        }

    }


    void PerformMeleeAttack(float range, float damage)
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, range);

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {

                EnemyHealth health = collider.GetComponent<EnemyHealth>();
                if (health == null)
                    health = collider.GetComponentInParent<EnemyHealth>();

                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }
        }
    }


    public void EquipWeaponModel()
    {
        if (currentWeaponModel != null)
            Destroy(currentWeaponModel);

        GameObject modelToEquip = null;

        foreach (var entry in weaponPrefabs)
        {
            if (entry.weaponType == stats.weaponType && entry.materialType == stats.materialType)
            {
                modelToEquip = entry.prefab;
                break;
            }
        }

        if (modelToEquip != null && weaponHolder != null)
        {
            currentWeaponModel = Instantiate(modelToEquip, weaponHolder.transform.position, weaponHolder.transform.rotation);
            currentWeaponModel.transform.SetParent(weaponHolder.transform);
        }
    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, stats.range);
    }

    public void SetWeapon(WeaponStatsSO newStats)
    {
        stats = newStats;
        EquipWeaponModel();
    }


    [System.Serializable]
    public class WeaponModelEntry
    {
        public WeaponType weaponType;
        public MaterialType materialType;
        public GameObject prefab;
    }

    public class WeaponInstance : MonoBehaviour
    {
        public WeaponStatsSO stats;
    }

}
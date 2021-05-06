using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : Enemy
{
    [SerializeField] private GameObject bulletPrefab;
    private Transform shootPoint;

    protected override void Attack()
    {
        base.Attack();
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
    }

    private void Trigger()
    {
        Shoot();
    }
}

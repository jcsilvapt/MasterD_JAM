using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : Enemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;

    protected override void Attack()
    {
        base.Attack();
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().SetRotation(-Vector3.right);
    }

    private void Trigger()
    {
        Shoot();
    }
}

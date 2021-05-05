using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : Enemy
{
    protected override void Attack()
    {
        base.Attack();
        Shoot();
    }

    private void Shoot()
    {

    }
}

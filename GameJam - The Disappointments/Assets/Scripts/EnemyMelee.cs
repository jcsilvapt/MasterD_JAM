using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    [SerializeField] private BoxCollider swordCollider;

    private void AttackOn()
    {
        swordCollider.enabled = true;
    }

    private void AttackOff()
    {
        swordCollider.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : Enemy, IDamage {
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private AudioSource aSource;

    public void TakeDamage() {
        Die();
    }

    protected override void Attack() {
        base.Attack();
    }

    private void Shoot() {
        shootPoint.LookAt(GameObject.FindGameObjectWithTag("Player").transform.Find("Target").transform.position);
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        aSource.Play();
    }

    private void Trigger() {
        Shoot();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Animator Reference
    protected Animator animator;

    //Flag indicating Enemy's Alive
    protected bool isAlive;

    //Attack cooldown and reload
    protected float cooldown;
    [SerializeField] protected float cooldownTime;

    //Flag indicating Player's on range
    protected bool isAlert;

    //Player Reference
    private Transform player;

    private void Start()
    {
        //Get Reference
        animator = GetComponent<Animator>();

        //Set Flags
        isAlive = true;
        isAlert = false;
    }

    private void Update()
    {
        if(cooldown <= 0 && isAlive && isAlert)
        {
            if(player != null)
            {
                if(player.transform.position.x <= transform.position.x)
                {
                    transform.eulerAngles = new Vector3(0, -90f, 0);

                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 90f, 0);
                }

                Attack();
            }
        }

        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    protected virtual void Attack()
    {
        animator.Play("Attack");
        cooldown = cooldownTime;
    }

    public void Die()
    {
        isAlive = false;
        animator.Play("Die");
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = false;
    }

    public void SetEnemy(Transform playerUnit)
    {
        player = playerUnit;
    }

    public void SetAlert(bool alertStatus)
    {
        isAlert = alertStatus;
    }
}

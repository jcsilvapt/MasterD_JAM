using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float bulletSpeed;

    private Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = rotation * bulletSpeed;
    }

    public void SetRotation(Vector3 direction)
    {
        rotation = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerController>().Die();
            Destroy(gameObject);
        }

        if(other.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }
}

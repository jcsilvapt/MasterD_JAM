using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour {

    [SerializeField] float velocity = 1;

    bool hasBeenShoot = false;

    Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();

        
    }

    private void Update() {
        if(!hasBeenShoot) {
            rb.AddForce(Vector3.forward * velocity, ForceMode.Impulse);
            hasBeenShoot = !hasBeenShoot;
        }
    }



}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour {
    private Rigidbody rb;

    [SerializeField] private Vector3 sizeOnThrow;
    [Header("Sounds")]
    [SerializeField] private AudioClip gearFlying;
    [SerializeField] private AudioClip gearHit;
    private Vector3 defaultLocation;
    private Quaternion defaultRotation;
    private Vector3 defaultScale;

    private AudioSource aSource;


    private bool isActive;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        aSource = GetComponent<AudioSource>();

        defaultLocation = transform.localPosition;
        defaultRotation = transform.localRotation;
        defaultScale = transform.localScale;
        /*
        isActive = true;
        transform.eulerAngles = Vector3.zero;
        transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        */
    }

    private void FixedUpdate() {
        if (isActive) {
            transform.eulerAngles += new Vector3(0, 2f, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") {
            if (other.tag == "Enemy") {
                other.GetComponent<IDamage>().TakeDamage();
                aSource.clip = gearHit;
                aSource.Play();
            }
            if (other.tag == "Floor") {
                rb.isKinematic = true;
                isActive = false;
            }
        }
    }

    public void SetBigSize() {
        isActive = true;
        transform.eulerAngles = Vector3.zero;
        transform.localScale = sizeOnThrow;
        aSource.clip = gearFlying;
        aSource.Play();
    }

    public void SetDefaultSize() {
        transform.localPosition = defaultLocation;
        transform.localRotation = defaultRotation;
        transform.localScale = defaultScale;
    }
}

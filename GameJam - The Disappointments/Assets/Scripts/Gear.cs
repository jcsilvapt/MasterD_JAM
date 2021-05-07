using System.Collections;
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


    [SerializeField] private bool isActive;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        aSource = GetComponent<AudioSource>();

        defaultLocation = transform.localPosition;
        defaultRotation = transform.localRotation;
        defaultScale = transform.localScale;
    }

    private void FixedUpdate() {
        if (isActive) {
            transform.eulerAngles += new Vector3(0, 2f, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (isActive) {
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
                if(other.tag == "Fall") {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().RecallGear();
                }
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
        isActive = false;
        transform.localPosition = defaultLocation;
        transform.localRotation = defaultRotation;
        transform.localScale = defaultScale;
    }

    public void SetActiveGear() {
        isActive = true;
    }

}

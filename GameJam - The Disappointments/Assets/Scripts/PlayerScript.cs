using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    [Range(1, 10f)]
    [SerializeField] float movementSpeed = 5;

    [Range(1, 5)]
    [SerializeField] float mouseSpeed = 1;

    [Range(100, 200)]
    [SerializeField] float forceOnBullet = 100;
    [SerializeField] GameObject shootFrom;
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject curvePoint;
    [SerializeField] GameObject particleSystem;

    [SerializeField] bool hasShoot = false;

    GameObject bullet;

    bool isGrounded = false;
    bool isToReturn = false;

    Rigidbody rb;

    private float time = 0.0f;
    private Vector3 bulletLastPosition;

    float x, y;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {

        x = Input.GetAxisRaw("Horizontal");
        if (x != 0) {
            rb.velocity = (transform.forward * x) * movementSpeed;
        }
    }

    private void Update() {
        if (!hasShoot) {
            if (Input.GetMouseButtonDown(0)) {
                ShootBullet();
            }
        } else {
            if (Input.GetMouseButtonDown(0)) {
                TeleportToBullet();
            }
            if (Input.GetMouseButtonDown(1)) {
                ReturnBullet();
            }
        }

        if (isToReturn) {
            if (time < 1.0f) {
                bullet.transform.position = getBQCPoint(time, bulletLastPosition, curvePoint.transform.position, transform.position);
                time += Time.deltaTime;
            }
            if (Vector3.Distance(transform.position, bullet.transform.position) < 1.25f) {
                Destroy(bullet);
                isToReturn = false;
                hasShoot = false;
            }
        }
    }

    void TeleportToBullet() {

        GameObject playerPosition = Instantiate(particleSystem, transform);
        GameObject bulletPosition = Instantiate(particleSystem, bullet.transform);
        playerPosition.transform.parent = null;
        playerPosition.GetComponentsInChildren<ParticleSystem>().Initialize();
        bulletPosition.transform.parent = null;
        bulletPosition.GetComponentsInChildren<ParticleSystem>().Initialize();
        Destroy(bullet);
        Destroy(playerPosition, 5f);
        Destroy(bulletPosition, 5f);
        hasShoot = false;
        transform.position = new Vector3(0, bullet.transform.position.y + 1f, bullet.transform.position.z);
    }

    void ShootBullet() {
        hasShoot = true;
        bullet = Instantiate(prefab, shootFrom.transform.position, shootFrom.transform.rotation) as GameObject;
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(bullet.transform.forward * forceOnBullet, ForceMode.Impulse);
    }

    void ReturnBullet() {
        time = 0.0f;
        bulletLastPosition = bullet.transform.position;

        isToReturn = true;
        //bullet.GetComponent<Rigidbody>().isKinematic = true;
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;



    }

    public float GetMouseSpeed() {
        return mouseSpeed;
    }

    // Bezier Quadratic Curve formula
    // Learn more:
    // https://en.wikipedia.org/wiki/B%C3%A9zier_curve
    Vector3 getBQCPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2) {
        // "t" is always between 0 and 1, so "u" is other side of t
        // If "t" is 1, then "u" is 0
        float u = 1 - t;
        // "t" square
        float tt = t * t;
        // "u" square
        float uu = u * u;
        // this is the formula in one line
        // (u^2 * p0) + (2 * u * t * p1) + (t^2 * p2)
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;
    }
}

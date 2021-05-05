using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    private Rigidbody rb;

    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        isActive = true;
        transform.eulerAngles = Vector3.zero;
        transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transform.eulerAngles += new Vector3(0, 2f, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            rb.isKinematic = true;
            isActive = false;
        }
    }
    /*{
        if(other.tag != "Player")
        {
            rb.isKinematic = true;
            isActive = false;
        }
    }
    */
}

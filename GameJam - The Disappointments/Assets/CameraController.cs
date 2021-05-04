using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float minYPosition = 4;
    [SerializeField] Vector3 currentPosition;

    private void Start() {
        currentPosition = new Vector3(transform.position.x, minYPosition, transform.position.z);
    }

    private void Update() {
        var mousePos = Input.mousePosition;

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, transform.position.y - 2, transform.position.y +8), transform.position.z);
    }

}

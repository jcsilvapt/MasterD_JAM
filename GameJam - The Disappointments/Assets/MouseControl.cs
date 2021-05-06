using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour {

    public GameObject player;

    Vector3 lookingAt;

    private void Update() {
        var mousePas = Input.mousePosition;

        lookingAt = Camera.main.ScreenToWorldPoint(new Vector3(mousePas.x, mousePas.y, 5));

        transform.LookAt(lookingAt);
    }

    public Vector3 GetMousePosition() {
        return lookingAt;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour {

    void Update() {
        var mousePos = Input.mousePosition;

        transform.LookAt(Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 20)));
    }

    Quaternion XLookRotation(Vector3 right, Vector3 up = default(Vector3)) {
        if (up == default(Vector3)) {
            up = Vector3.up;
        }

        Quaternion rightToFoward = Quaternion.Euler(0f, -90f, 0f);
        Quaternion forwardToTarget = Quaternion.LookRotation(right, up);

        return forwardToTarget * rightToFoward;
    }

}

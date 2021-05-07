using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallEnd : MonoBehaviour {


    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            GameManager.PlayerLose(false);
        }
    }

}

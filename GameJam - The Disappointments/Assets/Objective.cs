using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {


    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            GameManager.PlayerWin();
        }
    }

}

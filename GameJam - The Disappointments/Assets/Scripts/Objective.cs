using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {

    [SerializeField] bool isFinalMap = false;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (!isFinalMap) {
                GameManager.PlayerWin();
            } else {
                //TODO: Fazer END GAME
            }
        }
    }

}

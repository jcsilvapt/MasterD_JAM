using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {

    [SerializeField] bool isFinalMap = false;
    [SerializeField] Transform FinalAnimation;

    private bool hasEnded = false;

    private void Update() {
        if (hasEnded) {
            transform.position += Vector3.up * 5f * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (!isFinalMap) {
                GameManager.PlayerWin();
            } else {
                hasEnded = true;
                StartCoroutine(gameEnded());
            }
        }
    }


    IEnumerator gameEnded() {

        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MeshRenderer>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        GameManager.PlayerWonTheGame();
    }
}

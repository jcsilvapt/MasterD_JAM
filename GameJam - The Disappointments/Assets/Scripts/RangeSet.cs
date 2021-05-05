using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSet : MonoBehaviour
{
    private Enemy unit;

    private void Start()
    {
        unit = transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            unit.SetAlert(true);
            unit.SetEnemy(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            unit.SetAlert(false);
            unit.SetEnemy(null);
        }
    }

}

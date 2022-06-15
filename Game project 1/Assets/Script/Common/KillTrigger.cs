using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other != null) {
            Debug.Log("Someone Fall in the void");
            if (other.gameObject.CompareTag("Player")) {
                other.GetComponent<PlayerHearth>().MakeDead();
            }
        }
    }
}

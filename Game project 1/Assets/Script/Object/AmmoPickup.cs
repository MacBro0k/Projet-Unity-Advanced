using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public GameObject PickupSound;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            // other.GetComponent<PlayerInventory>().AddRocket(1);
            other.GetComponent<PlayerInventory>().ChargeLaser(100);
            Instantiate(PickupSound);
        }
    }
}
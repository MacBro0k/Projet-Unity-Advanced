using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPickup : MonoBehaviour
{
    public int amount;
    //public GameObject PickupSound;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && other.GetComponent<PlayerInventory>().Rocket < other.GetComponent<PlayerInventory>().MaxRocket) {
            other.GetComponent<PlayerInventory>().AddRocket(amount);
            Destroy(gameObject.transform.root.gameObject);
            //Instantiate(PickupSound);
        }
    }
}

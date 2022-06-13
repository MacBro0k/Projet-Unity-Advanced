using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealObjectControler : MonoBehaviour
{
    public GameObject HealSound;
    public int Amount;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.GetComponent<PlayerInventory>().Life < collision.GetComponent<PlayerInventory>().MaxLife) { // Check if pleyer is not full life
            collision.GetComponent<PlayerInventory>().Heal(Amount);
            Instantiate(HealSound);
            Destroy(gameObject.transform.root.gameObject);
        } 
    }
}

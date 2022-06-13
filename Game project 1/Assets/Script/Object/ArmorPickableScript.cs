using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPickableScript : MonoBehaviour
{
    public GameObject PickupSound;
    public int Amount;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.GetComponent<PlayerInventory>().Armor < collision.GetComponent<PlayerInventory>().MaxArmor) { // Nous recuperons le tag de l'objet en colision 
            collision.GetComponent<PlayerInventory>().AddArmor(Amount);
            Instantiate(PickupSound); // transform.position nous permet de recuperer la position de l'objet actuel 
            Destroy(gameObject.transform.root.gameObject); 
        } 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealObjectControler : MonoBehaviour
{
    //public GameObject HealSound;
    public int Amount;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.GetComponent<PlayerInventory>().Life < collision.GetComponent<PlayerInventory>().MaxLife) { // Nous recuperons le tag de l'objet en colision 
            Destroy(gameObject.transform.root.gameObject); // On detruit la hierarchie complete de l'objet rubis 
            collision.GetComponent<PlayerInventory>().Heal(Amount);
            //Instantiate(HealSound, transform.position, Quaternion.identity); // transform.position nous permet de recuperer la position de l'objet actuel 
        } 
    }
}

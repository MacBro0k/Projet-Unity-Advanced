using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int Explosion Damage;


    void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.tag == "Enemy"){
            
        }
    }
}
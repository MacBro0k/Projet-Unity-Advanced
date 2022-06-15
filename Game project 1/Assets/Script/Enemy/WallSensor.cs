using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSensor : MonoBehaviour
{
    public GameObject Entity;
    // Rotate root entity when is front a wall
    void OnTriggerEnter2D (Collider2D col){
        if(col.tag == "Wall" || col.tag == "BreakableWall"){
            Entity.GetComponent<EnemyControler>().Rotate();
        }
    }
}

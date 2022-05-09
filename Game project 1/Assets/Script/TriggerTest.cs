using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    Rigidbody2D playerRB;
    BoxCollider2D testtriggerTR;
    // Start is called before the first frame update
    void Start()
    {
        testtriggerTR = GetComponent<BoxCollider2D>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        if (testtriggerTR.gameObject.tag == "Player")
        {
            Debug.Log("Le joueur entre dans le collider");
        }
        Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Le joueur sort du collider");
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Le joueur reste dans le collider");
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 15;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject,8f);
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Enemy"){
            hit.GetComponent<EnemyControler>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (hit.tag == "Wall") {
            Destroy(gameObject);
        }
        else if (hit.tag == "BreakableWall"){
            hit.GetComponent<ObstacleControler>().Break();
            Destroy(gameObject);
        }

    }

}

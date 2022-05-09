using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;

    PrototypeHeroControler target;
    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection = (GameObject.FindObjectOfType<PrototypeHeroControler>().transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Player"){
            hit.GetComponent<PlayerInventory>().TakeDamage(damage);
            Destroy(gameObject,1);
        }
        else if (hit.tag == "Wall") {
            Destroy(gameObject);
        }

    }
}

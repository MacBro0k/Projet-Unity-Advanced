using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    public GameObject bullet;
    public GameObject Sensor;
    public Transform groundDetection;
    GameObject Player;
    public GameObject ShootSound;


    public float PatrolSpeed;
    public float RayDistance;
    public float MaxDist;
    public float CacDist;
    public float FireRate = 3f;
    public int MaxLife;
    public bool IsPatroling = true;
    public bool Guard;
    public bool Stand;

    int Life;
    float nextFire;
    bool IsVisible = false;
    bool moving = false;
    bool IsDead = false;

    private Animator m_animator;
    private bool movingRight = true;

    void Start(){
        m_animator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        nextFire = Time.time;
        Life = MaxLife;
    }

    void Update(){
        m_animator.SetBool("Moving", moving);
        IsVisible = Sensor.GetComponent<PlayerSensor>().canSeePlayer;
        if(IsPatroling){
            Guard = false;
            Stand = false;
        }
        else if(Guard || Stand){
            IsPatroling = false;
        }
        if(IsPatroling && IsVisible){
            IsPatroling = false;
        }
        else if(!IsPatroling && !IsVisible && !Guard){
            IsPatroling = true;
        }
        if(!IsDead){
            if(IsPatroling) {
                moving = true;
                transform.Translate(Vector2.right * PatrolSpeed * Time.deltaTime);      
                RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, RayDistance);
                if(groundInfo.collider == false){
                    if(movingRight == true){
                        transform.Rotate(0f,180f,0f);
                        movingRight = false;
                    }else{
                        transform.Rotate(0f,180f,0f);
                        movingRight = true;
                    }
                }
            }else{
                if(Vector2.Distance(transform.position, Player.transform.position) >= MaxDist){
                    if(!Stand){
                        moving = true;                
                        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, PatrolSpeed * Time.deltaTime);
                    }
                }
                else if(Vector2.Distance(transform.position, Player.transform.position) <= MaxDist){
                    moving = false;
                    if(Vector2.Distance(transform.position, Player.transform.position) <= CacDist){
                        if (Time.time > nextFire){
                            nextFire = Time.time + FireRate;
                            m_animator.SetTrigger("Attack");
                            Player.GetComponent<PlayerInventory>().TakeDamage(2);
                            Player.GetComponent<Rigidbody2D>().AddForce((Player.transform.position - transform.position) * 25f);
                        }
                    }else{
                        if (Time.time > nextFire){
                            nextFire = Time.time + FireRate;
                            m_animator.SetTrigger("Shoot");
                            Instantiate(bullet,transform.position,Quaternion.identity);
                            Instantiate(ShootSound);
                        }
                    }
                }
            }
        }

    }




    public void TakeDamage(int Damage){
        if(!IsDead){
            Life -= Damage;
            if(Life <= 0){
                m_animator.SetTrigger("Death");
                IsDead = true;
            }
            m_animator.SetTrigger("Hit");

        }
    }

    public void Die(){
        Destroy(gameObject);
    }

    void OnTriggerEnter (Collider col){
        if(col.tag == "Wall"){
            if(movingRight == true){
                transform.Rotate(0f,180f,0f);
                movingRight = false;
            }else{
                transform.Rotate(0f,180f,0f);
                movingRight = true;
            }
        }
    }
}

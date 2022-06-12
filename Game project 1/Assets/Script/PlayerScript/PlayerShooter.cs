using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShooter : MonoBehaviour
{

    public Transform firepoint;
    public GameObject Bullet;
    public GameObject BlastEffect;
    public GameObject BlastSound;
    public GameObject Rocket;
    public GameObject LaserImpactEffect;

    public enum SecondaryAttackList{
        Rocket, 
        Laser
    }
    public SecondaryAttackList SecondaryAttack;
    
    public int LaserDamage;
    public float ColdownTime = 1f;

    private float endColdownTime;
    private bool m_canShoot = true;
    private Animator m_animator;

    private GameObject m_Player;

    void Start(){
        m_Player = transform.root.gameObject;
        try{
            m_animator = GetComponent<Animator>();      
        }
        catch(Exception e){
            Debug.Log("no Animator");
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_canShoot = m_Player.GetComponent<PrototypeHeroControler>().m_canShoot;

        if(m_canShoot && Time.time >= endColdownTime){
            if (Input.GetButtonDown("Fire1")){
                if(m_animator){
                    m_animator.SetBool("IsCharging", true);
                }else{
                    Shoot();
                    Instantiate(BlastSound);
                }
            }
            else if (Input.GetButtonDown("Fire2")){
                if( SecondaryAttack == SecondaryAttackList.Rocket){
                    if(m_Player.GetComponent<PlayerInventory>().Rocket != 0){
                        m_Player.GetComponent<PlayerInventory>().RemoveRocket(1);
                        endColdownTime = Time.time + ColdownTime;
                        Instantiate(Rocket, firepoint.position, firepoint.rotation);
                    }

                }
                else if (SecondaryAttack == SecondaryAttackList.Laser){
                    //if(m_Player.GetComponent<PlayerInventory>().Laser == 0){
                        RaycastHit2D HitInfo = Physics2D.Raycast(firepoint.position, firepoint.right);

                        if(HitInfo){
                            EnemyControler enemy = HitInfo.transform.GetComponent<EnemyControler>();
                            if (enemy != null){
                                enemy.TakeDamage(LaserDamage);
                            }else{
                                Destroy(enemy);
                            }
                            DoorActivator Door = HitInfo.transform.GetComponent<DoorActivator>();
                            if (Door != null){
                                Door.Activate();
                            }else{
                                Destroy(Door);
                            }

                            Instantiate(LaserImpactEffect, HitInfo.point, Quaternion.identity);
                        }
                    //}
                }
                else{
                    Debug.LogWarning("No secondary_attack Selected");
                }
            }

        }
    }

    void Shoot (){
        endColdownTime = Time.time + ColdownTime;
        Instantiate(Bullet, firepoint.position, firepoint.rotation);
        Instantiate(BlastEffect, firepoint.position, firepoint.rotation).transform.SetParent(gameObject.transform);
        if(m_animator){
            m_animator.SetBool("IsCharging", false);
        }
    }

}

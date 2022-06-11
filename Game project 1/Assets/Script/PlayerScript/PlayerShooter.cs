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
    
    public float ColdownTime = 1f;

    private float endColdownTime;
    private bool m_canShoot = true;
    private Animator m_animator;

    void Start(){
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
        m_canShoot = transform.root.gameObject.GetComponent<PrototypeHeroControler>().m_canShoot;

        if(m_canShoot && Time.time >= endColdownTime){
            if (Input.GetButtonDown("Fire1")){
                if(m_animator){
                    m_animator.SetBool("IsCharging", true);
                }else{
                    Shoot();
                    Instantiate(BlastSound);
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

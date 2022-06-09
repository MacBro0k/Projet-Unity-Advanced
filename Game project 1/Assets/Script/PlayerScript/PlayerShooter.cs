using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShooter : MonoBehaviour
{

    public Transform firepoint;
    public GameObject Bullet;
    public GameObject BlastEffect;
    
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
        if(m_canShoot){
            if (Input.GetButtonDown("Fire1")){
                if(m_animator)
                    m_animator.SetTrigger("ShootingB");
                Shoot();
            }
        }
    }

    void Shoot (){
        Instantiate(Bullet, firepoint.position, firepoint.rotation);
        Instantiate(BlastEffect, firepoint.position, firepoint.rotation).transform.SetParent(gameObject.transform);
    }

    void CantShoot(){
        m_canShoot = !m_canShoot;
    }
}

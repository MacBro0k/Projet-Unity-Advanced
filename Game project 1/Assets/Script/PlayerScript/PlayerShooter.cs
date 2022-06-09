using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{

    public Transform firepoint;
    public GameObject bullet;
    
    public bool canShoot = true;
    private Animator m_animator;

    void Start(){
        m_animator = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        if(canShoot){
            if (Input.GetButtonDown("Fire1")){
                m_animator.SetTrigger("ShootingB");
                Shoot();
            }
        }
    }

    void Shoot (){
        Instantiate(bullet, firepoint.position, firepoint.rotation);
    }

    void CantShoot(){
        canShoot = !canShoot;
    }
}

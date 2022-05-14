using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int Life {  get; private set; }

    // Munition 
    public float Laser {  get; private set; } // Charge max 100%
    public int Rocket {  get; private set; }
    public int Grenade {  get; private set; }

    public int MaxLife;
    public int MaxRocket;
    public int MaxGrenade;

    private Animator m_animator;

    // Interface Image
    public Image lifebar;
    public Image laserbar;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        Life = MaxLife;
    }

    // Enleve des PV et joue l'animation "hit" ou "Die"
    public void TakeDamage(int Damage){
        Life -= Damage;
        lifebar.fillAmount = Mathf.Round((1/(float)MaxLife)*(float)Life*10000)/10000;
        if(Life <= 0){
            m_animator.SetTrigger("Death");
        }else{
            m_animator.SetTrigger("Hit");
        }
    }

    // Soigne le joueur
    public void Heal(int Amount){
        for(int i = 0; i <= Amount; i++){
            if(Life != MaxLife){
                    Life += 1;
            }else{
                break;
            }
        }
        lifebar.fillAmount = Mathf.Round(((1/(float)MaxLife)*(float)Life)*10000)/10000;
    }

    // Décgharge le laser
    public void UnchargeLaser(float Amount){
        Laser -= Amount;
        laserbar.fillAmount = Mathf.Round((0.01*Laser)*10000)/10000;
    }

    // Charge le laser 
    public void ChargeLaser(float Amount){
        for(int i = 0; i <= Amount; i+=0.1){
            if(Laser != 100){
                    Laser += 0.1;
            }else{
                break;
            }
        }
        laserbar.fillAmount = Mathf.Round((0.01*Laser)*10000)/10000;
    }

    public void 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{

    public int              Life {  get; private set; }
    public int              Armor {  get; private set; }

    // Munition 
    public float            Laser {  get; private set; } // Charge max 100%
    public int              Rocket {  get; private set; }
    public int              Grenade {  get; private set; }

    private bool            invincible = false;

    public int              MaxLife;
    public int              MaxArmor;

    public int              MaxRocket;
    public int              MaxGrenade;

    private Animator        m_animator;

    // Interface Image
    public Image lifebar;
    public Image laserbar;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        Life = MaxLife;
    }

    //////////////////////////////////////////////////////////////////////////
    //                 Gestion de la vie et de l'armure                     //
    //////////////////////////////////////////////////////////////////////////

    // Enleve des PV et joue l'animation "hit" ou "Die"
    public void TakeDamage(int Damage){
        if(!invincible){
            if(Armor != 0){
                if(Damage <= Armor){
                    Armor -= Damage;
                    return;
                }else{
                    Damage = Damage - Armor;
                    Armor = 0; 
                }
            }
            Life -= Damage;
            lifebar.fillAmount = Mathf.Round((1/(float)MaxLife)*(float)Life*10000)/10000;
            if(Life <= 0){
                m_animator.SetTrigger("Death");
            }else{
                m_animator.SetTrigger("Hit");
            }
        }
    }

    // Soigne le joueur
    public void Heal(int Amount){
        for(int i = 0; i <= Amount; i++){
            if(Life == MaxLife)
                break;
            Life++;
        }
        lifebar.fillAmount = Mathf.Round(((1/(float)MaxLife)*(float)Life)*10000)/10000;
    }

    // Ajoute de l'armure aux joueur
    public void AddArmor(int Amount){
        for(int i = 0; i <= Amount; i++){
            if(Armor == MaxArmor)
                break;
            Armor++;
        }
    }

    //////////////////////////////////////////////////////////////////////////
    //                      Gestion des munitions                           //
    //////////////////////////////////////////////////////////////////////////

    // Décgharge le laser
    public void UnchargeLaser(float Amount){
        Laser -= Amount;
        laserbar.fillAmount = Mathf.Round(((float)0.01*Laser)*10000)/10000;
    }

    // Charge le laser 
    public void ChargeLaser(float Amount){
        for(float i = 0; i <= Amount; i += (float)0.1){
            if(Laser != 100)
                break;
            Laser += (float)0.1;
        }
        laserbar.fillAmount = Mathf.Round(((float)0.01*Laser)*10000)/10000;
    }

    // Ajoute des Rockets dans l'inventaire du joueur
    public void AddRocket(int Amount){
        for(int i = 0; i<=Amount; i++){
            if(Rocket == MaxRocket)
                break;
            Rocket++;
        }
    }

    // Retire des Rockets dans l'inventaire du joueur
    public void RemoveRocket(int Amount){
        for(int i = 0; i<= Amount; i++){
            if(Rocket == 0)
                break;
            Rocket--;
        }
    }

    // Ajoute des Grenades dans l'inventaire du joueur
    public void AddGrenade(int Amount){
        for(int i = 0; i<=Amount; i++){
            if(Grenade == MaxGrenade)
                break;
            Grenade++;
        }
    }

    // Retire des Grenades dans l'inventaire du joueur
    public void RemoveGrenade(int Amount){
        for(int i = 0; i<= Amount; i++){
            if(Grenade == 0)
                break;
            Grenade--;
        }
    }

    public void Invincible(){
        invincible = !invincible;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int Life {  get; private set; }
    public int MaxLife;

    private Animator m_animator;
    public Image lifebar;
    void Start()
    {
        m_animator = GetComponent<Animator>();
        Life = MaxLife;
    }

    public void TakeDamage(int Damage){
        Life -= Damage;
        lifebar.fillAmount = Mathf.Round((1/(float)MaxLife)*(float)Life*10000)/10000;
        if(Life <= 0){
            m_animator.SetTrigger("Death");
        }else{
            m_animator.SetTrigger("Hit");
        }
    }

    public void Heal(int Amount){
        for(int i = 0; i <= Amount; i++){
            if(Life != MaxLife){
                    Life += 1;
            }else{
                break;
            }
        }
        lifebar.fillAmount = Mathf.Round((1/(float)MaxLife)*(float)Life*10000)/10000;
    }
}

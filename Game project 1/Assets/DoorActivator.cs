using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivator : MonoBehaviour
{

    public Animator DoorAnimator;

    private Animator ActivatorAnimator;

    void Start(){
        ActivatorAnimator = GetComponent<Animator>();
    }


    // Open the door thanks to Animator
    public void Activate()
    {
        DoorAnimator.SetBool("Opening",true);
        ActivatorAnimator.SetBool("Activated",true);
    }
}

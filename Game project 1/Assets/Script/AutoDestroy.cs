using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float aliveTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, aliveTime); // gameObject represente l'objet sur lequel le script est atach� Start se lancera des que l'object est instantie
    }
}

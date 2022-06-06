using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour
{

    public GameObject Player;

    private void FixedUpdate()
    {
        Vector3 Difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Difference.Normalize();

        float RotationZ = Mathf.Atan2(Difference.y, Difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, RotationZ);

        if (RotationZ < -90 || RotationZ > 90)
        {
            if(Player.transform.eulerAngles.y == 0){
                transform.localRotation = Quaternion.Euler(180,0,-RotationZ);
                Player.GetComponent<PrototypeHeroControler>().Flip();
            } 
            else if (Player.transform.eulerAngles.y == 180) 
            {
                transform.localRotation = Quaternion.Euler(180, 180, -RotationZ);
                //Player.transform.Rotate(0f,180f,0f);
            }
        }
        else{
            if(Player.transform.eulerAngles.y == 180){
                Player.GetComponent<PrototypeHeroControler>().Flip();
                //Player.transform.Rotate(0f,180f,0f);
            }
        }

    }
}

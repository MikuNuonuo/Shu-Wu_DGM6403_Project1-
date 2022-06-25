using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartadd: MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Healthbar1>().health += 1;
            
        }
        }

    private void Destroy(bool v)
    {
        throw new NotImplementedException();
    }
}
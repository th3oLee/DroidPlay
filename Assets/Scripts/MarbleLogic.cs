using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleLogic : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Goal")
        {
            GameControl.instance.Victory();
        }

        if (other.gameObject.transform.parent != null && other.gameObject.transform.parent.name == "DefeatBar")
        {
            GameControl.instance.GameOver();
        }

    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTilting : MonoBehaviour
{
    public float tiltAngle = 20f;
    public float smooth = 5.0f;

    void Update()
    {
        if (!GameControl.instance.victory && !GameControl.instance.gameOver)
        {
            float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
            float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

            Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
        
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        }
    }
}

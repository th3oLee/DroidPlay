using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTilting : MonoBehaviour
{
    public float tiltAngle;
    public float smooth = 5.0f;

    private float tiltAroundX;
    private float tiltAroundZ;

    void Update()
    {
        if (!GameControl.instance.victory && !GameControl.instance.gameOver)
        {
            if (GameControl.instance.playingRemote.name != null)
            {
                tiltAngle = 50f;
                
                tiltAroundZ = - GameControl.instance.playingRemote.accelerometter.x * tiltAngle;
                tiltAroundX = GameControl.instance.playingRemote.accelerometter.y * tiltAngle;
                
                Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
                transform.rotation = target;
            }
            else
            {
                tiltAngle = 20f;
                
                tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
                tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
                
                Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
                Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            }
            
        }
    }
}

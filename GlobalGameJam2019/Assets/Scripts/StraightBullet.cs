using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : Bullet
{

    void Start()
    {
        rb2D.velocity = -transform.right * speed;
    }

}

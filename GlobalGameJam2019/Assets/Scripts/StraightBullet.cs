using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : Bullet
{

    void Update()
    {
        Vector3 moveVect = new Vector3(speed * Time.deltaTime, 0, 0);
        tf.Translate(moveVect, Space.Self);
    }
}

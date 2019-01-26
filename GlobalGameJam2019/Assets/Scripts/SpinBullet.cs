using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBullet : Bullet
{
    public float spin = (float)1;

    void Update()
    {
        Vector3 moveVect = new Vector3(speed * Time.deltaTime, 0, 0);

        tf.transform.Rotate(0, 0, spin);
        tf.Translate(moveVect, Space.World);
    }
}

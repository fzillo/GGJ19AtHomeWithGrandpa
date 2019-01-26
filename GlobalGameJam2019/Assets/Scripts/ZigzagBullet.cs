using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigzagBullet : Bullet
{

    public float timeTillFlip = 3;
    [SerializeField] private float localTime;
    [SerializeField] private int upDown = 1;



    // Update is called once per frame
    void Update()
    {
        localTime += Time.deltaTime;

        if (localTime > timeTillFlip)
        {
            localTime = 0;
            upDown *= -1;
            //tf.Rotate(180, 0, 0);
        }

        Vector3 moveVect = new Vector3(speed * Time.deltaTime, upDown * speed * Time.deltaTime, 0);
        tf.Translate(moveVect, Space.Self);
    }
}

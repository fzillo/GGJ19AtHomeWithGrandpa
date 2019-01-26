using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBullet : MonoBehaviour
{

    //public Rigidbody2D rb2D;
    public float speed = 10;
    public Transform rotationPointParent;

    void Start()
    {
        //rb2D.velocity = -transform.right * speed;
    }
    // Update is called once per frame
    void Update()
    {
        rotationPointParent.transform.Rotate(0, 0, 1);

    }
}

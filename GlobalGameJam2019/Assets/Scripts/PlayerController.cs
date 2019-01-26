using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public Rigidbody2D playerRB;
    public float speed = 10;

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

       Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
        playerRB.AddForce (movement * speed,ForceMode2D.Impulse);
    }
}

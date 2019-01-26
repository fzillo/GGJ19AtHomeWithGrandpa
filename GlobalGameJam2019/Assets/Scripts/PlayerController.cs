using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public Transform player;
    public float speed = 10;
	public Vector2 movement;
	private bool InJump = false;
	private bool Jumped = false;
	private bool DoubleJump = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
		if (player.y <= 0)
			Jumped = DoubleJump = false;

		if (Input.GetAxisRaw ("Vertical") == 0)
			InJump = false;
		if(Input.GetAxisRaw("Vertical") == 1 && !InJump)
		{
			InJump = true;
			if (DoubleJump)
				movement = new Vector2 (moveHorizontal, 0);
			else if (Jumped) {
				DoubleJump = true;
				movement = new Vector2 (moveHorizontal, 0);
			}
		
        player.rigidbody.AddForce (movement * speed, ForceMode2D.Impulse);

    }
}

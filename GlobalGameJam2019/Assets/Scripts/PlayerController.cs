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
	private Animator animator;

	void Start(){
		animator = player.GetComponent<Animator>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
		if(moveHorizontal > 0)
			moveHorizontal = speed;
		if(moveHorizontal < 0)
			moveHorizontal = -speed;
		if(moveHorizontal == 0)
			animator.SetBool("Running", false);
		else
			animator.SetBool("Running", true);

		if (Mathf.Abs(player.position.y) <= 0.01)
			Jumped = DoubleJump = false;
		
		if(Jumped)
			moveHorizontal = moveHorizontal/2;

		if (Input.GetAxisRaw ("Vertical") == 0)
			InJump = false;
		if(Input.GetAxisRaw("Vertical") == 1 && !InJump)
		{
			InJump = true;
			if (DoubleJump)
				movement = new Vector2 (moveHorizontal, 0);
			else if (Jumped) {
				DoubleJump = true;
				player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				movement = new Vector2 (moveHorizontal, 13);
			}
			else{
				Jumped = true;
				player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				movement = new Vector2 (moveHorizontal, 12);
			}
		}
		else movement = new Vector2 (moveHorizontal, 0);
        player.GetComponent<Rigidbody2D>().AddForce (movement, ForceMode2D.Impulse);
    }
}

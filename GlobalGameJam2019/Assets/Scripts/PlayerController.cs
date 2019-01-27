using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D rigidBody;

    public float speed = 10;
    public Vector2 movement;

    private bool InJump = false;
    private bool Jumped = false;
    private bool DoubleJump = false;
	private Animator animator;

	void Start(){
		animator = player.GetComponent<Animator>();
	}

    private AudioManager audioManager;

    private void Start() {
        audioManager = GetComponent<AudioManager>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private int _frameCount;
    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (moveHorizontal != 0) {
            if (moveHorizontal > 0)
                moveHorizontal = speed;
            if (moveHorizontal < 0)
                moveHorizontal = -speed;
		if(moveHorizontal == 0)
			animator.SetBool("Running", false);
		else
			animator.SetBool("Running", true);

            if (_frameCount % 30 == 0) // alle 30 frames step sound abspielen
                audioManager.PlayPitchRandom("step", 0.1f);
        }

        if (Mathf.Abs(transform.position.y) <= 0.01)
            Jumped = DoubleJump = false;

        if (Jumped) {
            moveHorizontal = moveHorizontal / 2;
            audioManager.Play("jump");
        }

        if (Input.GetAxisRaw("Vertical") == 0)
            InJump = false;

        if (Input.GetAxisRaw("Vertical") == 1 && !InJump) {
            InJump = true;
            if (DoubleJump) {
                movement = new Vector2(moveHorizontal, 0);
                audioManager.Play("jump_double");
            } else if (Jumped) {
                DoubleJump = true;
                rigidBody.velocity = Vector2.zero;
                movement = new Vector2(moveHorizontal, 13);
            } else {
                Jumped = true;
                rigidBody.velocity = Vector2.zero;
                movement = new Vector2(moveHorizontal, 12);
            }
        } else {
            movement = new Vector2(moveHorizontal, 0);
        }

        rigidBody.AddForce(movement, ForceMode2D.Impulse);
    }
}

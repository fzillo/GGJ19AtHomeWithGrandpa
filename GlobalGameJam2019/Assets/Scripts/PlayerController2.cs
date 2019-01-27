using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour {
    public static PlayerController2 instance;
    [HideInInspector] public bool facingRight = true;
    public float moveSpeed = 0.2f;
    public Vector2 movement;
	public float spriteToBottomDist = 2.0f;

    public Transform player;
    public Lifemeter lifemeterInstance;
    public AudioManager audioManager;

    public bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;
    private bool InJump = false;
    private bool Jumped = false;
    private bool DoubleJump = false;

    public GameObject shield;
    public GameObject shout;

    // Use this for initialization
    void Awake() {
        instance = this;
        anim = player.GetComponent<Animator>();
        rb2d = player.GetComponent<Rigidbody2D>();
    }

    void Start() {
        audioManager = AudioManager.instance;
    }

    // Update is called once per frame
    void Update() {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.down, 5, 1 << LayerMask.NameToLayer("Ground"));
		float distanceToGround = player.transform.position.y - hit.point.y;
        if (hit.collider != null)
			grounded = (distanceToGround < spriteToBottomDist+0.2f) && (distanceToGround > spriteToBottomDist);
        else
            grounded = false;

        //if(anim.GetBool("Running") == false)
        //	player.position += 2.5f * Time.deltaTime * Vector3.left;
        if (!grounded) {
			if(5f * Time.deltaTime < Mathf.Abs(distanceToGround - spriteToBottomDist))
				player.position += 5f * Time.deltaTime * Vector3.down;
			else
				player.position += (distanceToGround - spriteToBottomDist) * Vector3.down;
			
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("FastFall"))
				if(5f * Time.deltaTime < Mathf.Abs(distanceToGround - spriteToBottomDist))
					player.position += 5f * Time.deltaTime * Vector3.down;
				else
					player.position += (distanceToGround - spriteToBottomDist) * Vector3.down;
        }

	}

    public void effectShout()
    {
        StartCoroutine(shouter());
    }
    public float getAngryScore()
    {
        return lifemeterInstance.getCurrentLife();
        anim.SetBool("Grounded", grounded);
    }


    private float _frameCount;
    private bool _airborn;
    void FixedUpdate() {
        _frameCount++;
        if (_frameCount % 10 == 0) // TODO: von animation frame abhängig machen
            audioManager.PlayPitchRandom("step", 0.002f);

        float h = Input.GetAxis("Horizontal");
        //anim.SetFloat("Speed", Mathf.Abs(h));
        h = Mathf.Clamp(h, -moveSpeed, moveSpeed);
        /*if(h > 0)
			h = moveSpeed;
		if(h < 0)
			h = -moveSpeed;*/

        if (grounded) {
            Jumped = DoubleJump = false;
            if (_airborn) {
                audioManager.PlayPitchRandom("jump_landing", 0.05f);
            }
            _airborn = false;
        }

        if (Jumped)
            h = h / 2;

        if (Input.GetAxisRaw("Vertical") == 0) {
            InJump = false;
        }
        if (Input.GetAxisRaw("Vertical") == 1 && !InJump) {
            _airborn = true;
            InJump = true;

            if (DoubleJump)
                movement = new Vector2(h, 0);
            else if (Jumped) {
                DoubleJump = true;
                anim.SetTrigger("Jump");
                audioManager.PlayPitchRandom("jump_double", 0.005f);

                //rb2d.velocity = Vector2.zero;
                movement = new Vector2(h, 10);
            } else {
                Jumped = true;
                anim.SetTrigger("Jump");
                audioManager.PlayPitchRandom("jump", 0.005f);

                //rb2d.velocity = Vector2.zero;
                movement = new Vector2(h, 10);
            }
        } else movement = new Vector2(h, 0);
        rb2d.AddForce(movement, ForceMode2D.Impulse);

        /*
                if (h * rb2d.velocity.x < maxSpeed)
                    rb2d.AddForce(Vector2.right * h * moveForce);

                if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
                    rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        */
        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();
        else if (h == 0 && !facingRight)
            Flip();

        if (h == 0) {
            anim.SetBool("Running", false);
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        } else {
            anim.SetBool("Running", true);
        }
    }


	public void effectShield()
    {
        StartCoroutine(shielder());
    }

    IEnumerator shielder() {
        shield.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        shield.SetActive(false);
    }

    IEnumerator shouter() {
        shout.SetActive(true);
        audioManager.PlayInSequencePitchRandom("player_shout", 3);
        for (int i = 0; i < 10; i++) {
            shout.SetActive(false);
            yield return new WaitForSecondsRealtime(0.1f);
            shout.SetActive(true);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        shout.SetActive(false);
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = player.transform.localScale;
        theScale.x *= -1;
        player.transform.localScale = theScale;

        audioManager.PlayPitch("step", 0.5f);
    }
}

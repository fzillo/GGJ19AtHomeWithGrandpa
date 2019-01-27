using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public static PlayerController2 instance;
    [HideInInspector] public bool facingRight = true;
    public float moveSpeed = 0.2f;
    public Vector2 movement;

    public Transform player;
    public Lifemeter lifemeterInstance;

    public bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;
    private bool InJump = false;
    private bool Jumped = false;
    private bool DoubleJump = false;

    public GameObject shield;
    public GameObject shout;

    // Use this for initialization
    void Awake()
    {
        PlayerController2.instance = this;
        anim = player.GetComponent<Animator>();
        rb2d = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.down, 2, 1 << LayerMask.NameToLayer("Ground"));

        if (hit.collider != null)
            grounded = (player.transform.position.y - hit.point.y < 2.5) &&
                (player.transform.position.y - hit.point.y > 0.4);
        else
            grounded = false;

        //if(anim.GetBool("Running") == false)
        //	player.position += 2.5f * Time.deltaTime * Vector3.left;
        if (!grounded)
        {
            player.position += 5f * Time.deltaTime * Vector3.down;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("FastFall"))
                player.position += 5f * Time.deltaTime * Vector3.down;
        }
        anim.SetBool("Grounded", grounded);
    }

    public void effectShield()
    {
        StartCoroutine(shielder());
    }

    public void effectShout()
    {
        StartCoroutine(shouter());
    }
    public float getAngryScore()
    {
        // TODO: retrieve it!!!!
        return 0.5f;
    }



    IEnumerator shielder()
    {
        //Rotate 90 deg
        shield.active = true;

        //Wait for 4 seconds
        yield return new WaitForSecondsRealtime(4);

        //Rotate 40 deg
        shield.active = false;
    }

    IEnumerator shouter()
    {
        //Rotate 90 deg
        shout.active = true;

		for(int i = 0; i < 10; i++)
		{
			shout.active = false;
        	yield return new WaitForSecondsRealtime(0.1f);
			shout.active = true;
        	yield return new WaitForSecondsRealtime(0.1f);
		}

        //Rotate 40 deg
        shout.active = false;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(h));
        h = Mathf.Clamp(h, -moveSpeed, moveSpeed);
        /*if(h > 0)
			h = moveSpeed;
		if(h < 0)
			h = -moveSpeed;*/

        if (grounded)
            Jumped = DoubleJump = false;
        if (Jumped)
            h = h / 2;

        if (Input.GetAxisRaw("Vertical") == 0)
            InJump = false;
        if (Input.GetAxisRaw("Vertical") == 1 && !InJump)
        {
            InJump = true;
            if (DoubleJump)
                movement = new Vector2(h, 0);
            else if (Jumped)
            {
                DoubleJump = true;
                anim.SetTrigger("Jump");
                //rb2d.velocity = Vector2.zero;
                movement = new Vector2(h, 15);
            }
            else
            {
                Jumped = true;
                anim.SetTrigger("Jump");
                //rb2d.velocity = Vector2.zero;
                movement = new Vector2(h, 14);
            }
        }
        else movement = new Vector2(h, 0);
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

        if (h == 0)
        {
            anim.SetBool("Running", false);
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        else
            anim.SetBool("Running", true);
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = player.transform.localScale;
        theScale.x *= -1;
        player.transform.localScale = theScale;
    }
}

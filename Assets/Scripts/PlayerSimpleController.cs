using UnityEngine;
using System.Collections;

public class PlayerSimpleController : MonoBehaviour 
{
	public float maxSpeed = 10f;
	bool facingRight = true;

	Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	//will be used mainly for animation to tell
	//when it shouldn't be 'falling' anymore.
	public LayerMask whatIsGround;

	public float jumpForce = 700f;

	// Use this for initialization
	void Start () 
	{
		//for use using animations
		anim = GetComponent<Animator>();
	}

	//physics stuff
	void FixedUpdate()
	{
		//used when using animation for jumping/falling
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		//Animator.SetBool("Ground", grounded);

		//anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

		//makes it so you cannot turn mid jump
		if (!grounded)
		{
			return;
		}

		float move = Input.GetAxis("Horizontal");

		//anim.SetFloat("Speed", Mathf.Abs(move));

		GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if(move > 0 && !facingRight)
		{
			Flip();
		}
		else if(move < 0 && facingRight)
		{
			Flip();
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if(grounded && Input.GetKeyDown(KeyCode.Space))
		{
			//for when we have an animation
			//anim.SetBook("Ground", false);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
		}
	}

	void Flip()
	{
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
	}
}

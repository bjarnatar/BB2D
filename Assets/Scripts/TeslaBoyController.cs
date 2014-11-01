using UnityEngine;
using System.Collections;

public class TeslaBoyController : MonoBehaviour
{
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float topSpeed = 10.0f;
	public float jumpForce = 10.0f;
	public Transform[] groundCheck;
	public float groundedDistance = 0.05f;
	public float lookAheadDistance = 0.05f;

	private Animator animator;
	private bool facingRight = true;

	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator>();
		if (animator == null)
		{
			Debug.LogError("TeslaBoyController needs an Animator component!");
		}
	}

	void Update()
	{
		// Vertical movement / Jump
		if (IsGrounded() && Input.GetButtonDown("Jump"))
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
		}
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		animator.SetFloat("Speed", Mathf.Abs (horizontalInput));

		bool grounded = IsGrounded();
		bool wallAhead = IsWallAhead();

		if (!grounded && wallAhead)
		{ // Set input to 0 to not push into the wall
			horizontalInput = 0;
		}

		// If speed is greater than 0 AND NOT facingRight
		if (horizontalInput > 0 && !facingRight)
		{
			Flip();
		}
		else if (horizontalInput < 0 && facingRight)
		{
			Flip();
		}

		// Horizontal movement
		float topSpeedAdjusted = topSpeed;
		MovingPlatform mp = GetMovingPlatformStandingOn();
		if (mp)
		{
			float moveDir = facingRight ? 1 : -1;
			if (Mathf.Abs(horizontalInput) < 0.05f)
			{ // No input - Limit top speed to platform's speed
				topSpeedAdjusted = Mathf.Abs(mp.GetPlatformVelocity().x);
			}
			else
			{ // Limit top speed to previous top speed plus platform's speed
				topSpeedAdjusted += mp.GetPlatformVelocity().x * moveDir;
			}
		}

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(horizontalInput * rigidbody2D.velocity.x < topSpeedAdjusted)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * horizontalInput * moveForce);
		
		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > topSpeedAdjusted)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * topSpeedAdjusted, rigidbody2D.velocity.y);

		Debug.Log ("Velocity: " + rigidbody2D.velocity.ToString());
	}
	
	public void Die()
	{
		Application.LoadLevel(0);
	}

	void Flip()
	{
		// facingRight set to NOT facingRight 
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	bool IsGrounded()
	{
		for (int i = 0 ; i < groundCheck.Length ; ++i)
		{
			Vector2 start = new Vector2(groundCheck[i].position.x, groundCheck[i].position.y);
			Vector2 end = start + Vector2.up * -groundedDistance;
			RaycastHit2D rh = Physics2D.Linecast(start, end);

			if (rh.collider)
			{
				return true;
			}
		}
		return false;
	}

	bool IsWallAhead()
	{
		float movementDirection = facingRight ? 1.0f : -1.0f;
		Vector2 start = rigidbody2D.position + new Vector2((collider2D.bounds.extents.x * movementDirection), 0);
		//Debug.Log ("Dist: " + (start-rigidbody2D.position).ToString());
		RaycastHit2D rcHit = Physics2D.Raycast (start, Vector2.right * movementDirection, lookAheadDistance, 1 << 8);
		
		if (rcHit)
		{
			return true;
		}
		return false;
	}

	MovingPlatform GetMovingPlatformStandingOn()
	{
		for (int i = 0 ; i < groundCheck.Length ; ++i)
		{
			Vector2 start = new Vector2(groundCheck[i].position.x, groundCheck[i].position.y);
			Vector2 end = start + Vector2.up * -groundedDistance;
			RaycastHit2D rh = Physics2D.Linecast(start, end);
			
			if (rh.collider && rh.collider.GetComponent<MovingPlatform>())
			{
				return rh.collider.GetComponent<MovingPlatform>();
			}
		}
		return null;
	}

}

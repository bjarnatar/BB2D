using UnityEngine;
using System.Collections;

public class TeslaBoyController : MonoBehaviour
{
	public float topSpeed = 10.0f;

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

	// Update is called once per frame
	void FixedUpdate ()
	{
		float speed = Input.GetAxis("Horizontal") * topSpeed;

		// If speed is greater than 0 AND NOT facingRight
		if (speed > 0 && !facingRight)
		{
			Flip();
		}
		else if (speed < 0 && facingRight)
		{
			Flip();
		}


		rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);


		//rigidbody2D.MovePosition(transform.position + new Vector3(speed, rigidbody2D.velocity.y, 0) * Time.deltaTime);

		speed = Mathf.Abs(speed);
		animator.SetFloat("Speed", speed);

	}

	void Flip()
	{
		// facingRight set to NOT facingRight 
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}

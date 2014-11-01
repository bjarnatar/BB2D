using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public float movementSpeed = 5.0f;
	public bool ignoreDrops = false;

	public int movementDirection = 1;
	public float lookDownDistance = 0.6f;
	public float lookAheadDistance = 0.2f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate()
	{
		if (IsWallAhead() || (!ignoreDrops && IsDropAhead()))
		{
			Flip();
		}
		rigidbody2D.velocity = new Vector2(movementSpeed * movementDirection, rigidbody2D.velocity.y);
		
	}

	void Flip()
	{
		movementDirection *= -1;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	bool IsDropAhead()
	{
		Vector2 start = rigidbody2D.position + new Vector2((collider2D.bounds.extents.x * movementDirection), 0);
		RaycastHit2D rcHit = Physics2D.Raycast (start, Vector2.up * -1, lookDownDistance, 1 << 8);

		if (rcHit)
		{
			return false;
		}
		return true;
	}

	bool IsWallAhead()
	{
		Vector2 start = rigidbody2D.position + new Vector2((collider2D.bounds.extents.x * movementDirection), 0);
		RaycastHit2D rcHit = Physics2D.Raycast (start, Vector2.right * movementDirection, lookAheadDistance, 1 << 8);

		if (rcHit)
		{
			return true;
		}
		return false;
	}


}

using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
	public float range = 5;
	public float speed = 2;

	// TODO Don't show in editor
	public Vector2 platformDelta;

	private Vector2 originalPosition;

	// Use this for initialization
	void Start ()
	{
		originalPosition = transform.position;
		platformDelta = Vector2.zero;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Vector2 newPos = new Vector2(originalPosition.x + range * Mathf.Sin (Time.time * speed), originalPosition.y);
		platformDelta = newPos - rigidbody2D.position;
		rigidbody2D.MovePosition(newPos);

		Vector2 velocity = platformDelta / Time.deltaTime;
	}

	public Vector2 GetPlatformVelocity()
	{
		return platformDelta / Time.deltaTime;
	}
}

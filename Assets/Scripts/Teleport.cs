using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
	public Transform destination;
	public Camera destinationCamera;
	public bool killSpeed = false;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			if (destination)
			{
				other.rigidbody2D.position = destination.position;
			}

			if (killSpeed)
			{
				other.rigidbody2D.velocity = Vector2.zero;
			}

			if (destinationCamera)
			{
				Camera.main.gameObject.SetActive(false);
				destinationCamera.gameObject.SetActive(true);
			}
		}
	}
}

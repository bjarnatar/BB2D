using UnityEngine;
using System.Collections;

public class MagnetPower : MonoBehaviour
{
	public float range = 3;
	public float force = 10;
	public bool affectSelf = false;
	public LayerMask excludeLayers;

	private Rigidbody2D myRigidBody;

	// Use this for initialization
	void Start ()
	{
		// Check if this object or its parent has a rigidbody2d and assign it to myRigidBody
		myRigidBody = GetComponent<Rigidbody2D>();
		if (myRigidBody == null)
		{
			myRigidBody = GetComponentInParent<Rigidbody2D>();
		}
	}
	
	void FixedUpdate ()
	{
		if (Input.GetButton("Repel"))
		{
			Rigidbody2D[] rigidbodies = GameObject.FindObjectsOfType<Rigidbody2D>();
			foreach(Rigidbody2D rb in rigidbodies)
			{
				// Only do the magnetism if target object is not in the exclude layer
				if (((1 << rb.gameObject.layer) & excludeLayers.value) == 0)
				{
					float distance = (rb.position - (Vector2)transform.position).magnitude;

					if (distance < range && rb != myRigidBody)
					{
						Vector2 forceVector = (Vector2)(rb.transform.position - transform.position);
						// Apply force to the other rigidbody
						rb.AddForceAtPosition(forceVector * force, transform.position);
						//rb.AddForce (forceVector * force);
						if (affectSelf)
						{
							rigidbody2D.AddForce(forceVector * -force, ForceMode2D.Force);
						}
					}
				}
			}
		}

		if (Input.GetButton("Attract"))
		{
			Rigidbody2D[] rigidbodies = GameObject.FindObjectsOfType<Rigidbody2D>();
			foreach(Rigidbody2D rb in rigidbodies)
			{
				// Only do the magnetism if target object is not in the exclude layer
				if (((1 << rb.gameObject.layer) & excludeLayers.value) == 0)
				{
					float distance = (rb.position - (Vector2)transform.position).magnitude;
				
					if (distance < range && rb != myRigidBody)
					{
						Vector2 forceVector = (Vector2)(rb.transform.position - transform.position);
						// Apply force to the other rigidbody
						rb.AddForceAtPosition(forceVector * -force, transform.position);

						if (affectSelf)
						{
							rigidbody2D.AddForce(forceVector * force, ForceMode2D.Force);
						}
					}
				}
			}
		}
	}
}














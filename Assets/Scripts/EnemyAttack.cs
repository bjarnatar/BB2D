using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
	public int killScore = 10; // Score this baddie gives when killed
	public float deathJumpForce = 5.0f;
	public float deathGravityMultiplier = 5.0f;
	public float deathSecondsToDestroy = 2.0f;

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			// If the bottom of the player collider is higher than the center of the enemy collider, the player wins
			if (collision.collider.bounds.min.y > collider2D.bounds.center.y)
			{ // Player wins, I die
				//collision.gameObject.SendMessage("AddScore", killScore);
				StartCoroutine("Die");
			}
			else
			{
				collision.gameObject.SendMessage("Die");
			}
		}		
	}

	IEnumerator Die()
	{
		collider2D.enabled = false;
		rigidbody2D.velocity = Vector2.up * deathJumpForce;
		rigidbody2D.gravityScale = deathGravityMultiplier;
		yield return new WaitForSeconds(deathSecondsToDestroy);
		Destroy(gameObject);
	}
}

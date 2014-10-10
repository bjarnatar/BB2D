using UnityEngine;
using System.Collections;

public class KillField : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		// TODO check that the colliding object is the player
		if (other.tag == "Player")
		{
			GameObject.Destroy(other.gameObject);
		}
	}
}

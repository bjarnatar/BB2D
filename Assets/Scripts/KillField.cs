using UnityEngine;
using System.Collections;

public class KillField : MonoBehaviour 
{
	public bool shouldKillEnemy = false;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" || (shouldKillEnemy && other.tag == "Enemy"))
		{
			other.SendMessage("Die");
		}
	}
}

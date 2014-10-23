using UnityEngine;
using System.Collections;

public class KillField : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			//GameObject.Destroy(other.gameObject);
			Application.LoadLevel(0);
		}
	}
}

using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
	public string loadLevelName;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Application.LoadLevel(loadLevelName);
		}
	}
}

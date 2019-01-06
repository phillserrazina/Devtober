using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_VisionRange : MonoBehaviour {

	// VARIABLES

	public float suspicionIncreaseRate;
	public float suspicionDecreaseRate;

	public float suspicion = 0.0f;

	private Player player;

	// EXECUTION FUNCTIONS

	private void Awake()
	{
		player = FindObjectOfType<Player> ();
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "Player")
		{
			if (player.isCarryingBody)
			{
				Debug.Log ("SUSPICION INCREASING!!");
				suspicion += suspicionIncreaseRate;
			}
		}
	}

	// METHODS
}

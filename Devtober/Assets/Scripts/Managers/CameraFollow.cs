using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	// VARIABLES

	public Transform objectToFollow;
	public GameObject targetCam;

	private Player player;


	// EXECUTION FUNCTIONS

	private void Awake()
	{
		player = FindObjectOfType<Player> ();
	}

	private void FixedUpdate()
	{
		FollowPlayer ();
	}


	// METHODS

	private void FollowPlayer()
	{
		Vector3 offset = new Vector3 (0.0f, 0.0f, -10.0f);
		Vector3 pos = player.transform.position + offset;
		this.transform.position = pos;

		if (Input.GetKey (KeyCode.F)) 
		{
			FollowObject ();
			targetCam.SetActive (true);
		} 
		else
			targetCam.SetActive (false);
	}

	private void FollowObject()
	{
		if (objectToFollow != null) 
		{
			Vector3 offset = new Vector3 (0.0f, 0.0f, -10.0f);
			Vector3 pos = objectToFollow.position + offset;
			this.transform.position = pos;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	// VARIABLES

	[Header("Stats")]
	public float movementSpeed = 5.0f;
	public int money = 0;
	public bool isCarryingBody = false;

	private bool isSprinting = false;
	private bool isLookingLeft = true;
	private static NPC bodyBeingCarried;

	private Rigidbody rb;
	private Animator animator;
	private GameManager gameManager;


	// EXECUTION FUNCTIONS

	private void Awake () 
	{
		rb = gameObject.GetComponent<Rigidbody> ();
		animator = GetComponentInChildren<Animator> ();
		gameManager = FindObjectOfType<GameManager> ();
	}

	private void Update () 
	{
		DropBody ();
		UpdateAnimations ();
	}

	private void FixedUpdate ()
	{
		MovementHandler ();	
	}

	private void OnTriggerStay (Collider col)
	{
		// If an NPC is in range
		if (col.tag == "NPC")
		{
			// Get the NPC script
			NPC npc = col.GetComponent<NPC> ();

			// If Player presses the Action Key
			if (Input.GetKeyUp (KeyCode.E))
			{
				// If the NPC is alive
				if (npc.isAlive)
					// Kill them
					npc.isAlive = false;

				// If the NPC is already dead AND the Player is
				// not carrying a body yet
				else if (!npc.isAlive && !this.isCarryingBody)
				{
					// The player is now carrying a body
					this.isCarryingBody = true;
					// It is carrying the NPC in range
					bodyBeingCarried = npc;
					// Tell the NPC that it got picked up
					npc.gotPickedUp = true;
				}
			}
		}
	}


	private void OnDestroy()
	{
		PlayerPrefs.SetInt ("PlayerBodyCarry", GameManager.BoolToInt (isCarryingBody));
	}

	// METHODS

	private void MovementHandler()
	{
		// If the Player presses D
		if (Input.GetKey (KeyCode.D)) 
		{
			// Make the player walk
			rb.velocity = Vector3.right * movementSpeed * Time.fixedDeltaTime;

			// If the Player presses Right or Left Shift
			if (Input.GetKey (KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.RightShift)) 
			{
				// Make the Player run
				rb.velocity = Vector3.right * (movementSpeed * 2) * Time.fixedDeltaTime;
				// Tell Player that it is now sprinting
				this.isSprinting = true;
			} 
			else
				// Tell Player that it is not sprinting anymore
				this.isSprinting = false;

			// Tell Player that it is looking to the right (Not left)
			isLookingLeft = false;
		}

		// If the Player presses A
		else if (Input.GetKey (KeyCode.A)) 
		{
			// Make the player walk
			rb.velocity = Vector3.left * movementSpeed * Time.fixedDeltaTime;

			// If the Player presses Right or Left Shift
			if (Input.GetKey (KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.RightShift)) 
			{
				// Make the Player run
				rb.velocity = Vector3.left * (movementSpeed * 2) * Time.fixedDeltaTime;
				// Tell Player that it is now sprinting
				this.isSprinting = true;
			} 
			else
				// Tell Player that it is not sprinting anymore
				this.isSprinting = false;

			// Tell Player that it is looking to the left
			isLookingLeft = true;
		} 

		// If the Player is not pressing either A or D
		else
			// Make the Player stop
			rb.velocity = Vector3.zero;
	}

	private void DropBody()
	{
		// If the Player is carrying a body AND presses Q
		if (this.isCarryingBody && Input.GetKeyUp (KeyCode.Q))
		{
			// The Player is not carrying a body anymore
			this.isCarryingBody = false;

			// Get the direction of where to drop the body
			int xDirection = -1;
			xDirection = this.isLookingLeft ? xDirection : -xDirection;

			Vector3 bodyOffset = new Vector3 (xDirection, 0, -2);

			// Instantiate the body
			GameObject go = Instantiate (gameManager.npcPrefab, this.transform.position + bodyOffset, gameManager.npcPrefab.transform.rotation);

			// Get the NPC component
			NPC newNpc = go.GetComponent<NPC> ();

			// Give the NPC body its' new information
			newNpc.npcID = bodyBeingCarried.npcID;
			newNpc.isAlive = false;
			newNpc.gotPickedUp = false;
			newNpc.deadSprite = bodyBeingCarried.deadSprite;

			newNpc.gameObject.GetComponentInChildren<Animator> ().Play ("NPC_Dead");
		}
	}

	private void UpdateAnimations()
	{
		// Update all the information values
		animator.SetFloat ("Speed", Mathf.Abs (rb.velocity.x));
		animator.SetBool ("CarryingBody", this.isCarryingBody);
		animator.SetBool ("Sprinting", this.isSprinting);

		// Rotate the Player depending on the direction
		// they're facing at the moment
		if (this.isLookingLeft)
			this.transform.rotation = Quaternion.LookRotation (Vector3.back);
		else
			this.transform.rotation = Quaternion.LookRotation (Vector3.forward);
	}

	/*
	public void ResetVariables()
	{
		isCarryingBody = false;
		PlayerPrefs.SetInt ("PlayerBodyCarry", GameManager.BoolToInt (false));
	}
	*/
}

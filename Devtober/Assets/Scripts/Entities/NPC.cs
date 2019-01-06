using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour {

	// VARIABLES

	public int npcID;
	public bool isAlive = true;
	public bool gotPickedUp = false;
	public Sprite aliveSprite;
	public Sprite deadSprite;
	public string currentSceneName = "MainRoad";

	[Header("Patrol Variables")]
	public float movementSpeed;
	public GameObject moveSpotPrefab;

	private float waitTime;
	public float startWaitTime;

	public float minX;
	public float maxX;

	private bool isLookingLeft = false;
	private bool isLookingFront = false;
	private float startPoseChangeTime = 3;
	private float poseChangeTime;

	private Transform moveSpot;
	private Animator animator;
	private Rigidbody rb;
	private NPC_VisionRange visionRange;


	// EXECUTION FUNCTIONS

	private void Awake()
	{
		animator = gameObject.GetComponentInChildren<Animator> ();
		rb = gameObject.GetComponent<Rigidbody> ();
		visionRange = gameObject.GetComponentInChildren<NPC_VisionRange> ();

		if (!this.isAlive)
			GetComponentInChildren<SpriteRenderer> ().sprite = deadSprite;
	}

	private void Start()
	{
		GameObject go = Instantiate (moveSpotPrefab, moveSpotPrefab.transform.position, Quaternion.identity);
		moveSpot = go.transform;

		waitTime = startWaitTime;
		poseChangeTime = startPoseChangeTime;
		moveSpot.position = new Vector2 (Random.Range (minX, maxX), moveSpot.position.y);
	}

	private void Update()
	{
		if (this.gotPickedUp)
			Destroy (gameObject);

		UpdateInfo ();
	}

	private void FixedUpdate()
	{
		/*
		if (this.isAlive)
			GetComponentInChildren<SpriteRenderer> ().sprite = aliveSprite;
		else
			GetComponentInChildren<SpriteRenderer> ().sprite = deadSprite;
		
		else
		{
			if (!(SceneManager.GetActiveScene ().name == currentSceneName))
				Destroy (gameObject);
		}
		*/
		
		if (this.isAlive)
			Patrol ();
		else
			rb.velocity = Vector3.zero;
	}


	// METHODS

	private void Patrol()
	{
		if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
		{
			rb.velocity = Vector3.zero;

			if (poseChangeTime <= 0) 
			{
				int choice = Random.Range (0, 2);

				if (choice == 0) {
					this.isLookingFront = true;
					this.isLookingLeft = false;
					Debug.Log ("I'm looking to the front!");
				} else if (choice == 1) {
					this.isLookingFront = false;
					this.isLookingLeft = true;
					Debug.Log ("I'm looking to the left!");
				} else if (choice == 2) {
					this.isLookingFront = false;
					this.isLookingLeft = false;
					Debug.Log ("I'm looking to the right!");
				}

				poseChangeTime = startPoseChangeTime;

			} 
			else
				poseChangeTime -= Time.fixedDeltaTime;

			if (waitTime <= 0) 
			{
				moveSpot.position = new Vector2 (Random.Range (minX, maxX), moveSpot.position.y);
				waitTime = startWaitTime + Random.Range(-5, 5);
			} 
			else
				waitTime -= Time.fixedDeltaTime;
		}
		else
		{
			if (moveSpot.position.x < this.transform.position.x) 
			{
				rb.velocity = Vector3.left * movementSpeed * Time.fixedDeltaTime;
				this.isLookingLeft = true;
				this.isLookingFront = false;
				Debug.Log ("I'm looking to the left!");
			}
			else 
			{
				rb.velocity = Vector3.right * movementSpeed * Time.fixedDeltaTime;
				this.isLookingLeft = false;
				this.isLookingFront = false;
				Debug.Log ("I'm looking to the right!");
			}
		}
	}

	private void UpdateInfo()
	{
		animator.SetFloat ("Speed", Mathf.Abs(rb.velocity.x));
		animator.SetBool ("Alive", this.isAlive);
		animator.SetBool ("IsLookingLeft", this.isLookingLeft);
		animator.SetBool ("IsLookingFront", this.isLookingFront);

		if (this.isLookingLeft)
			this.transform.rotation = Quaternion.LookRotation (Vector3.back);
		else
			this.transform.rotation = Quaternion.LookRotation (Vector3.forward);

		if (this.isLookingFront)
			visionRange.enabled = false;
		else
			visionRange.enabled = true;
	}

	/*
	#region ignore
	private void OnDestroy()
	{
		PlayerPrefs.SetInt ("NPC" + npcID + " Condition", GameManager.BoolToInt (isAlive));
		PlayerPrefs.SetInt ("NPC" + npcID + " State", GameManager.BoolToInt (gotPickedUp));
		PlayerPrefs.SetString ("NPC" + npcID + " Current Scene", SceneManager.GetActiveScene().name);
	}

	// METHODS

	public void ResetVariables()
	{
		this.isAlive = true;
		this.gotPickedUp = false;
		PlayerPrefs.SetInt ("NPC" + npcID + " Condition", GameManager.BoolToInt (true));
		PlayerPrefs.SetInt ("NPC" + npcID + " State", GameManager.BoolToInt (false));
		PlayerPrefs.SetString ("NPC " + npcID + " Current Scene", "MainRoad");
	}
	#endregion
	*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

	// VARIABLES

	public enum TriggerTypes
	{
		KEY,
		INSTANT
	}

	public enum CameraTypes
	{
		STATIC,
		FOLLOW
	}

	public TriggerTypes triggerType;
	public CameraTypes cameraTypeInThisEnvironment;
	public Transform cameraPosition;
	public string sceneToChance;
	public int doorToGo;
	public int doorID;
	public Vector3 offset;

	private Player player;
	private Door[] doorArray;

	// EXECUTION FUNCTIONS

	private void Awake()
	{
		player = GameObject.FindObjectOfType<Player> ();
		doorArray = GameObject.FindObjectsOfType<Door> ();
	}

	private void OnTriggerEnter()
	{
		if (this.triggerType == TriggerTypes.INSTANT)
		{
//			GameManager.instance.currentDoorID = this.doorID;
//			GameManager.instance.ChangeScene (sceneToChance);

			for (int i = 0; i < doorArray.Length; i++)
			{
				if (doorArray[i].GetComponent<Door>().doorID == doorToGo)
				{
					player.transform.position = doorArray [i].transform.position - doorArray[i].offset;

					if (doorArray [i].cameraTypeInThisEnvironment == CameraTypes.STATIC) 
					{
						FindObjectOfType<CameraFollow> ().enabled = false;
						FindObjectOfType<CameraFollow> ().gameObject.transform.position = doorArray [i].cameraPosition.position;
					}
					else if (doorArray [i].cameraTypeInThisEnvironment == CameraTypes.FOLLOW)
						FindObjectOfType<CameraFollow> ().enabled = true;
				}
			}
		}
	}

	private void OnTriggerStay()
	{
		if (this.triggerType == TriggerTypes.KEY)
		{
			if (Input.GetKeyDown (KeyCode.W))
			{
//				GameManager.instance.currentDoorID = this.doorID;
//				GameManager.instance.ChangeScene (sceneToChance);

				for (int i = 0; i < doorArray.Length; i++)
				{
					if (doorArray[i].GetComponent<Door>().doorID == doorToGo)
					{
						player.transform.position = doorArray [i].transform.position - doorArray[i].offset;

						if (doorArray [i].cameraTypeInThisEnvironment == CameraTypes.STATIC) 
						{
							FindObjectOfType<CameraFollow> ().enabled = false;
							FindObjectOfType<CameraFollow> ().gameObject.transform.position = doorArray [i].cameraPosition.position;
						}
						else if (doorArray [i].cameraTypeInThisEnvironment == CameraTypes.FOLLOW)
							FindObjectOfType<CameraFollow> ().enabled = true;
					}
				}
			}
		}
	}
}

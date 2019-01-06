using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// VARIABLES

	public static GameManager instance;
	public GameObject npcPrefab;

	private Door[] doorArray;
	private NPC[] npcArray;
	[HideInInspector] public int currentDoorID;
	private Player player;

	// EXECUTION FUNCTIONS

	private void Awake()
	{
		player = FindObjectOfType<Player> ();

		if (instance == null)
		{
			DontDestroyOnLoad (gameObject);
			instance = this;
		}
		else if (instance != this)
			Destroy (gameObject);

//		SceneManager.activeSceneChanged += OnSceneChangeActions;
	}


	// METHODS

	public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}

	/*
	private void OnSceneChangeActions(Scene current, Scene next)
	{
		#region Get Objects

		player = GameObject.FindObjectOfType<Player> ();
		doorArray = GameObject.FindObjectsOfType<Door> ();
		npcArray = GameObject.FindObjectsOfType<NPC>();

		#endregion

		#region Send Player To Correct Door

		for (int i = 0; i < doorArray.Length; i++)
		{
			if (doorArray[i].GetComponent<Door>().doorID == currentDoorID)
			{
				player.transform.position = doorArray [i].transform.position - doorArray[i].offset;
			}
		}

		#endregion

		#region Update Variables

		player.isCarryingBody = IntToBool (PlayerPrefs.GetInt("PlayerBodyCarry") );

		for (int i = 0; i < npcArray.Length; i++)
		{
			npcArray[i].isAlive = IntToBool (PlayerPrefs.GetInt ("NPC" + npcArray[i].npcID + " Condition"));
			npcArray[i].gotPickedUp = IntToBool (PlayerPrefs.GetInt ("NPC" + npcArray[i].npcID + " State"));
			npcArray[i].currentSceneName = PlayerPrefs.GetString("NPC" + npcArray[i].npcID + " Current Scene");
		}

		#endregion
	}
	*/

	#region Helper Functions

	public static bool IntToBool (int i)
	{
		if (i == 0)
			return false;
		else
			return true;
	}

	public static int BoolToInt (bool b)
	{
		if (b == true)
			return 1;
		else
			return 0;
	}

	/*
	public void ResetVariables()
	{
		player.ResetVariables ();

		for (int i = 0; i < npcArray.Length; i++)
		{
			npcArray [i].ResetVariables ();
		}
	}
	*/
	#endregion
}

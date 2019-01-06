using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyCounter : MonoBehaviour {

	// VARIABLES

	public int bodyCount = 0;
	public Text bodyCountText;

	// EXECUTION FUNCTIONS

	private void Update()
	{
		bodyCountText.text = string.Format ("Body Count: {0}", bodyCount);
	}

	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "NPC")
		{
			Destroy (col.gameObject);
			bodyCount++;
		}
	}

	// METHODS


}

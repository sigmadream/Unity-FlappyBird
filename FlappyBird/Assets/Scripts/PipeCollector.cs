using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollector : MonoBehaviour
{	
	private GameObject[] pipeHolders;
	private float pipeDistance = 2.5f;
	private float lastPipesX;

	// for vertical placement of pipes
	private float pipeMinY = -1.5f;
	private float pipeMaxY = 2.4f;


	void Awake ()
	{
		// Get all the pipeholders
		pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");

		// Loop through PipeHolders to randomly place the pipes on their Y pos
		foreach (GameObject ph in pipeHolders) {

			// Store transform.position to manipulate pipeholder
			Vector3 temp = ph.transform.position;

			// Randomize Y position of this pipeholder
			temp.y = Random.Range(pipeMinY, pipeMaxY);

			// Move pipeholder into position
			ph.transform.position = temp;

		}

		// Assign current last pipeX
		lastPipesX = pipeHolders[0].transform.position.x;

		// Loop through pipe X locations to find the largest one.
		foreach (GameObject ph in pipeHolders) {
			// Check if this value is bigger. If yes then store its value, otherwise leave value the same
			// variable = (condition) ? true : false
			lastPipesX = (ph.transform.position.x > lastPipesX) ? ph.transform.position.x : lastPipesX;
		}

	}

	void OnTriggerEnter2D(Collider2D collider) 
	{

		//Debug.Log(name + " collided with " + collider);

		if (collider.tag == "PipeHolder") {

			// get the position of the bg, preparing to move it
			Vector3 temp = collider.transform.position;

			// now move it to the end
			temp.x = lastPipesX + pipeDistance;

			// Randomize Y position of this pipeholder
			temp.y = Random.Range(pipeMinY, pipeMaxY);

			// Assign and move the PipeHolder
			collider.transform.position = temp;

			// update newly added panel to be last X position
			lastPipesX = temp.x;
		}
	}

}

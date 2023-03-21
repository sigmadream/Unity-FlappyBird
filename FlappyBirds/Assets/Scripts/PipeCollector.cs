using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollector : MonoBehaviour {


	private GameObject[] pipeHolders;
	private float pipeDistance = 2.5f;

	private float lastPipesX;

	// for vertical placement of pipes
	private float pipeMinY = -1.5f;
	private float pipeMaxY = 2.4f;


	void Awake ()
	{
		pipeHolders = GameObject.FindGameObjectsWithTag ("Pipeholder");
		foreach (GameObject ph in pipeHolders) {
			Vector3 temp = ph.transform.position;
			temp.y = Random.Range(pipeMinY, pipeMaxY);
			ph.transform.position = temp;

		}
		lastPipesX = pipeHolders[0].transform.position.x;
		foreach (GameObject ph in pipeHolders) {
			lastPipesX = (ph.transform.position.x > lastPipesX) ? ph.transform.position.x : lastPipesX;
		}
	}

	void OnTriggerEnter2D(Collider2D collider) 
	{
		if (collider.tag == "Pipeholder") {
			Vector3 temp = collider.transform.position;
			temp.x = lastPipesX + pipeDistance;
			temp.y = Random.Range(pipeMinY, pipeMaxY);
			collider.transform.position = temp;
			lastPipesX = temp.x;
		}
	}
}

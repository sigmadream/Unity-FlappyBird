﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public static float offsetX = 7f;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (BirdScript.instance != null && BirdScript.instance.isAlive) {
			MoveTheCamera ();
		}
	}

	void MoveTheCamera ()
	{
		Vector3 temp = transform.position;
		temp.x = BirdScript.instance.GetPositionX () + offsetX;
		transform.position = temp;
	}
}

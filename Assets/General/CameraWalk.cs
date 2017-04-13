using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWalk : MonoBehaviour 
{
	public float speed;

	private void Update () 
	{
		if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
			MoveCamera ();	
	}

	private void MoveCamera()
	{
		//TODO: check for + -  in new Vector3 of Input.
		Vector3 inputVector = new Vector3 (Input.GetAxis ("Vertical"), 0f, -Input.GetAxis ("Horizontal"));
		inputVector = transform.InverseTransformDirection (inputVector);
		inputVector = inputVector.normalized * speed;
		transform.Translate (inputVector * Time.deltaTime);
	}
}

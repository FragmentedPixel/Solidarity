using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
	public float panSpeed = 20f;
	public float zoomSpeed = 100f;

	[Header("Clamping")]
	public Vector2 clamp;
	public float minY, maxY;


	private void Update()
	{
		if(Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Mouse ScrollWheel") != 0f)
			MoveCamera();	
	}

	private void MoveCamera()
	{
		float x = Input.GetAxis ("Vertical");
		float y = Input.GetAxis ("Mouse ScrollWheel");
		float z = Input.GetAxis ("Horizontal");

		x = Mathf.Clamp (x, -clamp.x, clamp.x);
		y = Mathf.Clamp (y, minY, maxY);
		z = Mathf.Clamp (z, -clamp.y, clamp.y);

		Vector3 pos = Vector3.zero;
		pos.x += panSpeed * x * Time.deltaTime;
		pos.y += zoomSpeed * y * Time.deltaTime;
		pos.z += panSpeed * z * Time.deltaTime;

		transform.position += pos;
	}
}

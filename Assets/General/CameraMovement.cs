using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
	public float cameraSpeed = 5f;
	public int boundsLimit = 100;

	private int screenWidth;
	private int screenHeight;

	private Vector3 offset;

	void Start () 
	{
		screenHeight = Screen.height;
		screenWidth = Screen.width;
	}

	void LateUpdate () 
	{
		CheckBounds();
	}

	private void CheckBounds()
	{
		if (Input.mousePosition.x > screenWidth - boundsLimit)
			transform.position -= new Vector3 (0, 0, cameraSpeed * Time.deltaTime);

		if (Input.mousePosition.x < 0 + boundsLimit)
			transform.position += new Vector3 (0, 0, cameraSpeed * Time.deltaTime);

		if (Input.mousePosition.y > screenHeight - boundsLimit)
			transform.position += new Vector3 (cameraSpeed * Time.deltaTime, 0, 0);

		if (Input.mousePosition.y < 0 + boundsLimit)
			transform.position -= new Vector3 (cameraSpeed * Time.deltaTime, 0, 0);
	}
}

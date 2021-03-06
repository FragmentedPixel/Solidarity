﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour 
{
	public string[] lines;
	public Text tutorialText;
	public Canvas playerCanvas;

    private Canvas tutorialCanvas;
	private int index = 0;
	private bool writing;

	private void Start()
	{
        tutorialCanvas = GetComponent<Canvas>();
        tutorialCanvas.enabled = true;
		playerCanvas.enabled = false;
		Time.timeScale = 1f;
		PlaceNewLine ();
	}

	private void Update()
	{
		if ((Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Space)) && !writing)
			PlaceNewLine ();
	}

	private void PlaceNewLine()
	{
		if (index < lines.Length)
			StartCoroutine (WriteLineCR (lines[index]));
		else 
		{
			tutorialCanvas.enabled = false;
			playerCanvas.enabled = true;
			Time.timeScale = 1f;
		}

		index++;
	}

	private IEnumerator WriteLineCR(string newText)
	{
		yield return null;

		writing = true;
		tutorialText.text = string.Empty;

		int textIndex = 0;

		while (textIndex < newText.Length) 
		{
			if (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Space))
				break;

			tutorialText.text += newText [textIndex];
			textIndex++;
			yield return null;
		}

		writing = false;
		tutorialText.text = newText;
		yield break;
	}
}

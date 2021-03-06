﻿using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour {
	public bool stopMovement = false;
	public int points = 0;
	public int victory = 0;
	public int enemiesAlive = 0;

	public float victoryTimer = 0;

	private LevelDataScript levelData;
	private GameObject player;
	private AudioSource aud;

	void Start()
	{
		aud = GetComponent<AudioSource>();
		levelData = GameObject.Find("LevelData").GetComponent<LevelDataScript>();
		player = GameObject.Find("Player");
	}

	void Update()
	{
		if(aud.time >= levelData.songLoopEnd)
		{
			aud.time = levelData.songLoopStart;
		}

		victoryTimer -= Time.deltaTime;

		if(victory == 0 && victoryTimer <= 0)
		{
			Application.LoadLevel("_LevelSelect");
		}

		if(enemiesAlive > 0)
		{
			if(this.transform.position.x > levelData.cameraStopArray[levelData.cameraStopIndex] - 128)
			{
				stopMovement = true;
				levelData.cameraStopIndex += 1;
			}
		}
		else
		{
			if(this.transform.position.x >= levelData.levelLength - 129)
			{
				stopMovement = true;
			}
			else
			{
				stopMovement = false;
			}
		}
		
		if(!stopMovement)
		{
			float dist = (transform.position - Camera.main.transform.position).z;
			float threshold = Camera.main.ViewportToWorldPoint(new Vector3(.6f, 0, dist)).x;
			if(player.transform.position.x > threshold)
			{
				PlayerScript playerScript = player.GetComponent<PlayerScript>();
				if(playerScript.deltaX > 0)
				{
					Vector3 position = transform.position;
					position.x += playerScript.deltaX;
					transform.position = position;
				}
			}
		}
	}

	void OnGUI()
	{
		GUI.Label(new Rect(230, 45, 100, 20), points.ToString());

		if(victory == 0)
		{
			GUIStyle style = new GUIStyle();
			style.normal.textColor = Color.green;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = 100;
			GUI.Label(new Rect(256, 230, 256, 230), "VICTORY", style);
		}
	}
}

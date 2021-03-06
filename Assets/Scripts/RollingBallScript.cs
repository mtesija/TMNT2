﻿using UnityEngine;
using System.Collections;

public class RollingBallScript : MonoBehaviour {

	public float yPos = 0;

	private float deltaX = 0;
	private float deltaY = 0;
	private float deltaJump = 0;

	public float jumpPos = 0;
	public float jumpVelocity = 0;
	private float jumpAccel = -15f;

	private GameObject ballShadow;

	void Start() 
	{
		ballShadow = transform.Find("RollingBallShadow").gameObject;
		yPos = transform.position.y;
	}

	public void setValues(float delx, float dely)
	{
		deltaX = delx;
		deltaY = dely;
	}

	void Update() 
	{
		deltaJump = 0;

		if(jumpPos >= 0)
		{
			jumpVelocity += jumpAccel * Time.deltaTime;
			deltaJump += jumpVelocity;
		}
		
		if(jumpPos < 0)
		{
			jumpVelocity = 3.5f;
			jumpPos = 0;
		}
		
		jumpPos += deltaJump;
		
		//Using Z axis for Isometric effect
		float z = yPos + deltaY;

		if(z < 0)
		{
			GameObject.Destroy(this.gameObject);
		}
		
		yPos = z;
		
		float y = z + jumpPos + deltaJump;
		
		float x = transform.position.x + deltaX;

		transform.position = new Vector3(x, y, z);

		if(ballShadow)
		{
			Vector3 position = ballShadow.transform.position;
			position.y = z - 3;
			ballShadow.transform.position = position;
		}
	}
}

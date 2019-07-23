using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMouseAim : MonoBehaviour // unedited script to shoot a ball, must be edited for purpose of game
{
	float startBallSpeed=1.5f;// assigns the initial speed value of the ball

	Rigidbody2D BallBody;// variable allowing forces to act on the ball
	Vector2 mousPos;
	Vector2 BallPos;

	void FixedUpdate(){

		mousPos = Camera.main.ScreenToWorldPoint (Input.mousePosition); //gets position of mouse in the game world
		BallPos = gameObject.transform.position;// changes the balls position per fixed frame
		mousPos = mousPos - BallPos; // calculate the mouses position on the screen relative to the ball
		mousPos = mousPos.normalized; // prevents the magnitude of the balls force from being influenced by the distance of the ball to the mouse cursor, while keeping the same direection 

		if (Input.GetMouseButton (0)) {// only executes when the left mouse button has been clicked and click count is 0

			BallBody=gameObject.GetComponent<Rigidbody2D>();
			BallBody.AddForce (mousPos*startBallSpeed); //applies force and direction to the ball

		}

	}
}

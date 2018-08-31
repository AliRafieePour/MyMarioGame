using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	//we re about dooing camera tracking 
	//we re about to follow our main object mario
	//we define the speed our camera goes toward our main object mario 
	//min and max defines our camera focous 
	//


public class CameraMove : MonoBehaviour {

	// Put Mario here to track his movement
	public Transform cameraTarget;

	// How quickly the camera moves towards Manny
	public float cameraSpeed;

	// Min and max X and Y movements
	public float minX;
	public float minY;
	public float maxX;
	public float maxY;

	// Update used when dealing with Rigid Bodies
	void FixedUpdate(){

		// Make sure the camera has a target
		if (cameraTarget != null) {

			// Lerp smoothes movement from the starting position
			// to the targets position 

			var newPos = Vector2.Lerp (transform.position,
				cameraTarget.position,
				Time.deltaTime * cameraSpeed);

			// Define the cameras new postion
			var vect3 = new Vector3 (newPos.x, newPos.y, -10f);

			// Clamp gets the cameras x position and clamps
			// it between the min and max value
			var clampX = Mathf.Clamp (vect3.x, minX, maxX);
			var clampY = Mathf.Clamp (vect3.y, minY, maxY);

			// Move the camera
			transform.position = new Vector3(clampX, clampY, -10f);
		}
	}
}

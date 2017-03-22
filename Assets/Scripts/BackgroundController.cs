using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class controls the vertical offset of the background, i.e. making it scroll
 */
public class BackgroundController : MonoBehaviour {

	private float offsetY; // The vertical offset for the background

	/**
	 * Initialization
	 */
	void Start () {
		offsetY = 0;
	}
				
	/**
	 * Update is called once per frame
	 */
	void Update () {
		OffsetBackground ();
	}

	/**
	 * Offset the background
	 */
	private void OffsetBackground () {
		// Get the current speed
		float currentSpeed = SpeedController.GetRealSpeed ();

		// Divide by 10 to go from world units to texture units
		currentSpeed /= 10f;

		// Adjust the offset
		offsetY += currentSpeed * Time.deltaTime;

		// Offset the background
		GetComponent<Renderer> ().material.mainTextureOffset = new Vector2 (0, offsetY);
	}
}
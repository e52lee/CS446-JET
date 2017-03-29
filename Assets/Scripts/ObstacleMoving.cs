using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class controls the moving obstacles. All speed variables are in real world units.
 * Call GetTranslationSpeed to get the actual speed of the object on the screen.
 * 
 * NOTES:
 * 
 * - I tried implementing variation in the speed of objects, but it became difficult
 * to ensure that obstacles didn't collide with each other or that there is always an
 * available lane for the user. As such, all obstacles currently move at the same speed.
 * 
 * - Obstacles must move slower than the user possibly can (MIN_SPEED in CarController.cs)
 */
public class ObstacleMoving : MonoBehaviour {

	private const float SPEED = 40f;     // The speed of the obstacle
	private const float OFFSCREEN = -7f; // y position when obstacle has moved offscreen

	/**
	 * Initialization
	 */
	void Start () {
		// Do nothing
	}

	/**
	 * Update is called once per frame
	 */
	void Update () {
		// Move the obstacle
		transform.Translate(new Vector3(0, 1, 0) * GetTranslationSpeed () * Time.deltaTime);

		// Destroy obstacle when it has moved offscreen
		if (transform.position.y < OFFSCREEN) {
			Object.Destroy (this.gameObject);
		}
	}

	/**
	 * Get the actual speed of obstacles on the screen -- i.e. the value that will
	 * be used in transform.Translate
	 *
	 * @return The speed of the object
	 */
	public static float GetTranslationSpeed () {
		float realSpeed = SPEED / Globals.SPEED_TRANSLATION;
		float playerSpeed = SpeedController.GetRealSpeed ();

		// Speed is relative to the player
		return playerSpeed - realSpeed;
	}
}
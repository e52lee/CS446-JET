using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class controls the car and allows the user to switch lanes by swiping left or right
 */
public class CarController : MonoBehaviour {

	private const float LANE_CHANGE_SPEED = 7.5f;   // The speed at which the car changes lanes

	private float targetPositionX; // Target horizontal position

	/**
	 * Initialization
	 */
	void Start () {
		targetPositionX = transform.position.x;
	}

	/**
	 * Update is called once per frame
	 */
	void Update () {
		UpdateTargetPosition ();
		MoveToTargetPosition ();
	}

	/**
	 * Update the target position if a horizontal swipe has occurred
	 */
	private void UpdateTargetPosition () {
		Swipe swipeDirection = TouchController.GetSwipeDirection ();

		if (swipeDirection == Swipe.Left) {
			targetPositionX = Globals.LEFT_LANE_POSITION;
		} else if (swipeDirection == Swipe.Right) {
			targetPositionX = Globals.RIGHT_LANE_POSITION;
		}
	}

	/**
	 * Move the car to its target position if it isn't already there
	 */
	private void MoveToTargetPosition () {
		Vector3 position = transform.position;

		if (position.x > targetPositionX) {
			position.x = Mathf.Max (position.x - LANE_CHANGE_SPEED * Time.deltaTime, Globals.LEFT_LANE_POSITION);
		} else if (position.x < targetPositionX) {
			position.x = Mathf.Min (position.x + LANE_CHANGE_SPEED * Time.deltaTime, Globals.RIGHT_LANE_POSITION);
		}

		transform.position = new Vector3 (position.x, position.y, position.z);
	}

	/**
	 * React to collision
	 *
	 * @param col The collision object
	 */
	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "ObstacleTag") {
			Object.Destroy (this.gameObject); // TODO: Replace with call to some GameOver method
			GameOver();
		}
	}

	void GameOver () {
		Application.LoadLevel("GameOver");
		//Application.LoadLevel(Application.loadedLevel);
	}
}
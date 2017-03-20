using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class controls the car and allows the user to switch lanes by swiping left or right
 */
public class CarController : MonoBehaviour {

	private const float LANE_CHANGE_SPEED = 7.5f;   // The speed at which the car changes lanes
	private const float LEFT_LANE_POSITION = -0.6f; // The position of the car in the left lane
	private const float RIGHT_LANE_POSITION = 0.9f; // The position of the car in the right lane

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
			targetPositionX = LEFT_LANE_POSITION;
		} else if (swipeDirection == Swipe.Right) {
			targetPositionX = RIGHT_LANE_POSITION;
		}
	}

	/**
	 * Move the car to its target position if it isn't already there
	 */
	private void MoveToTargetPosition () {
		Vector3 position = transform.position;

		if (position.x > targetPositionX) {
			position.x = Mathf.Max (position.x - LANE_CHANGE_SPEED * Time.deltaTime, LEFT_LANE_POSITION);
		} else if (position.x < targetPositionX) {
			position.x = Mathf.Min (position.x + LANE_CHANGE_SPEED * Time.deltaTime, RIGHT_LANE_POSITION);
		}

		transform.position = new Vector3 (position.x, position.y, position.z);
	}
}
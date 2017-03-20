using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class controls the speed and allows the user to increase or decrease the speed
 * by swiping up or down, respectively. For convenience, the speed calculations within
 * this class will use the "display" speed, i.e. what will be displayed in the UI.
 * However, classes that need to use the speed for in-game operations, such as scrolling
 * the background, will require the "real" speed, which will be a floating point number,
 * likely somewhere between 0 and 2. They can get this number by calling getRealSpeed.
 * The display speed can be acquired by calling getDisplaySpeed.
 */
public class SpeedController : MonoBehaviour {

	private const int START_SPEED = 50; // The initial speed
	private const int INCREMENT = 10;   // Amount to increment speed upon swipe
	private const int MIN_SPEED = 10;   // The minimum speed
	private const int MAX_SPEED = 140;  // The maximum speed

	private const float ACCELERATION = 0.5f; // The acceleration
	private const float TRANSLATION = 78f;   // Divide speed by this to get "real" speed

	private static float currentSpeed; // The current speed
	private static float targetSpeed;  // The target speed

	/**
	 * Initialization
	 */
	void Start () {
		currentSpeed = targetSpeed = START_SPEED;
	}

	/**
	 * Update is called once per frame
	 */
	void Update () {
		UpdateTargetSpeed ();
		AccelerateToTargetSpeed ();
	}

	/**
	 * Get the current "display" speed -- This is what will be displayed in the UI
	 * 
	 * @return The current display speed
	 */
	public static float GetDisplaySpeed() {
		return currentSpeed;
	}

	/**
	 * Get the current "real" speed -- This is the number used for internal operations,
	 * such as scrolling the background or moving obstacles
	 * 
	 * @return The current real speed
	 */
	public static float GetRealSpeed () {
		return currentSpeed / TRANSLATION;
	}

	/**
	 * Update the target speed if a vertical swipe has occurred
	 */
	private void UpdateTargetSpeed () {
		Swipe swipeDirection = TouchController.GetSwipeDirection ();

		if (swipeDirection == Swipe.Up) {
			targetSpeed = Mathf.Min(targetSpeed + INCREMENT, MAX_SPEED);
		} else if (swipeDirection == Swipe.Down) {
			targetSpeed = Mathf.Max(targetSpeed - INCREMENT, MIN_SPEED);
		}
	}

	/**
	 * Accelerate to the target speed if not already there
	 */
	private void AccelerateToTargetSpeed () {
		currentSpeed += (targetSpeed - currentSpeed) * ACCELERATION * Time.deltaTime;
	}
}
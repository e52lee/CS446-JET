using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Enumeration for the four swipe directions -- we don't care about diagonal
 */
public enum Swipe { None, Up, Down, Left, Right };

/**
 * This class continuously checks for a swipe from the user. When one occurs, the
 * static variable TouchController.swipeDirection is set to one of the Swipe values
 * enumerated above.
 *
 * Note that this class was inspired by a post on the Unity forums:
 * https://forum.unity3d.com/threads/swipe-in-all-directions-touch-and-mouse.165416/#post-1516893
 */
public class TouchController : MonoBehaviour {

	private const float MIN_SWIPE_LENGTH = 100f; // Minimum length to be considered a swipe

	private Vector2 touchStart; // Where the user begins a swipe
	private Vector2 touchEnd;   // Where the user ends a swipe

	private static Swipe swipeDirection; // The direction of the swipe

	/**
	 * Initialization
	 */
	void Start () {
		swipeDirection = Swipe.None;
	}

	/**
	 * Update is called once per frame
	 */
	void Update () {
		CheckForSwipe();
	}

	/**
	 * Get the swipe direction
	 * 
	 * @return The swipe direction -- see the Swipe enum above
	 */
	public static Swipe GetSwipeDirection () {
		return swipeDirection;
	}

	/**
	 * Check if a swipe has occurred and, if so, update swipeDirection
	 */
	private void CheckForSwipe () {
		Vector2 swipeVector;

		// Assume no swipe before checking
		swipeDirection = Swipe.None;

		// Detect user beginning swipe/tap
		if (Input.GetMouseButtonDown(0)) {
			touchStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		}

		// Detect user ending swipe/tap
		if (Input.GetMouseButtonUp(0)) {
			touchEnd = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			swipeVector = new Vector3(touchEnd.x - touchStart.x, touchEnd.y - touchStart.y);

			// Ignore taps
			if (swipeVector.magnitude < MIN_SWIPE_LENGTH) {
				swipeDirection = Swipe.None;
				return;
			}

			// Normalize vector
			swipeVector.Normalize();

			// Determine direction
			if (swipeVector.y > 0 && swipeVector.x > -0.5f && swipeVector.x < 0.5f) {
				swipeDirection = Swipe.Up;
			} else if (swipeVector.y < 0 && swipeVector.x > -0.5f && swipeVector.x < 0.5f) {
				swipeDirection = Swipe.Down;
			} else if (swipeVector.x < 0 && swipeVector.y > -0.5f && swipeVector.y < 0.5f) {
				swipeDirection = Swipe.Left;
			} else if (swipeVector.x > 0 && swipeVector.y > -0.5f && swipeVector.y < 0.5f) {
				swipeDirection = Swipe.Right;
			}
		}
	}
}
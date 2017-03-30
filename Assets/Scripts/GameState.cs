using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class handles the score and speed limit
 */
public class GameState : MonoBehaviour {

	public static int RIGHT_LANE_OVER_THRESH = 5;  // Max speed over limit before losing points
	public static int RIGHT_LANE_UNDER_THRESH = 5; // Max speed under limit before losing points
	public static int LEFT_LANE_OVER_THRESH = 10;  // Max speed over limit before losing points
	public static int LEFT_LANE_UNDER_THRESH = 0; // Max speed under limit before losing points

	public const int START_LIMIT = 50; // Starting speed limit
	public const int MIN_LIMIT = 50;   // Minimum speed limit
	public const int MAX_LIMIT = 100;  // Maximum speed limit

	public const int MIN_DELAY = 10; // Minimum time to wait before changing limit
	public const int MAX_DELAY = 15; // Maximum time to wait before changing limit

	public static int score;      // The player's score
	public static int highScore;  // The player's high score
	public static int speedLimit; // The speed limit
	public static bool gameOver;  // Whether the game is over

	private float limitTimer;

	/**
	 * Use this for initialization
	 */
	void Start () {
		score = 0;
		highScore = 0;
		speedLimit = START_LIMIT;
		gameOver = false;

		if (PlayerPrefs.HasKey("HighScore")) {
			highScore = PlayerPrefs.GetInt("HighScore");
		}

		limitTimer = Random.Range (MIN_DELAY, MAX_DELAY);

		InvokeRepeating("UpdateScore", 1f, 1f);
	}
	
	/**
	 * Update is called once per frame
	 */
	void Update () {
		limitTimer -= Time.deltaTime;

		if (limitTimer <= 0) {
			// Change the speed limit
			ChangeSpeedLimit ();

			// Reset the timer to a random delay
			limitTimer = Random.Range (MIN_DELAY, MAX_DELAY);
		}
	}

	/**
	 * Check if user is within the speed limit threshold
	 *
	 * @return True if the user is within the speed limit threshold, false otherwise
	 */
	public static bool UserIsWithinLimit () {
		int speed = Mathf.RoundToInt (SpeedController.GetDisplaySpeed ());

		GameObject Car = GameObject.Find ("Car");
		CarController carController = Car.GetComponent<CarController> ();

		if ((carController.targetPositionX == Globals.LEFT_LANE_POSITION) &&
			(speed < speedLimit - LEFT_LANE_UNDER_THRESH || speed > speedLimit + LEFT_LANE_OVER_THRESH)) {
			return false;
		}
		else if ((carController.targetPositionX == Globals.RIGHT_LANE_POSITION) &&
			(speed < speedLimit - RIGHT_LANE_UNDER_THRESH || speed > speedLimit + RIGHT_LANE_OVER_THRESH)) {
			return false;
		}

		return true;
	}

	/**
	 * Set gameOver to true
	 */
	public static void ActivateGameOver (){
		gameOver = true;
	}

	/**
	 * Change the speed limit
	 */
	private void ChangeSpeedLimit () {
		if (gameOver) return;

		// Do not change the speed limit by more than 20
		int lowerBound = Mathf.Max (MIN_LIMIT, speedLimit - 20);
		int upperBound = Mathf.Min (MAX_LIMIT, speedLimit + 20);

		// Make sure new speed limit is different
		int tempLimit = speedLimit;
		while (tempLimit == speedLimit) {
			tempLimit = Random.Range(lowerBound, upperBound);
			tempLimit = Mathf.RoundToInt(tempLimit / 10) * 10; // Round to nearest 10
		}

		speedLimit = tempLimit;
	}

	/**
	 * Update the player's score
	 */
	private void UpdateScore () {
		if (!gameOver) {
			if (UserIsWithinLimit ()) {
				score += 1;
			} else if (score > 0) {
				score -= 1;
			}
		}

		if (score > highScore) {
			PlayerPrefs.SetInt("HighScore", score);
		}
	}
}
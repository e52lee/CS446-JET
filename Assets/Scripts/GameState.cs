using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class handles the score and speed limit
 */
public class GameState : MonoBehaviour {

	public const int START_LIMIT = 50; // Starting speed limit
	public const int MIN_LIMIT = 50;   // Minimum speed limit
	public const int MAX_LIMIT = 120;  // Maximum speed limit

	public static int score;      // The player's score
	public static int highScore;  // The player's high score
	public static int speedLimit; // The speed limit
	public static bool gameOver;  // Whether the game is over

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

		InvokeRepeating("UpdateScore", 1f, 1f);

		int interval = Random.Range(5, 8);
		InvokeRepeating("ChangeSpeedLimit", interval, interval);
	}
	
	/**
	 * Update is called once per frame
	 */
	void Update () {
		// Do nothing
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
		// Do not change the speed limit by more than 30
		int lowerBound = Mathf.Max (MIN_LIMIT, speedLimit - 30);
		int upperBound = Mathf.Min (MAX_LIMIT, speedLimit + 30);

		if (!gameOver) {
			speedLimit = Random.Range(lowerBound, upperBound);
			speedLimit = Mathf.RoundToInt(speedLimit / 10) * 10; // Round to nearest 10
		}
	}

	/**
	 * Update the player's score
	 */
	private void UpdateScore () {
		if (!gameOver) {
			int speed = Mathf.RoundToInt (SpeedController.GetDisplaySpeed ());

			if (speed > speedLimit) {
				if (score > 0) {
					score -= 1;
				}
			} else {
				score += 1;
			}
		}

		if (score > highScore) {
			PlayerPrefs.SetInt("HighScore", score);
		}
	}
}
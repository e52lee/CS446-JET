using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * This class handles the UI
 */
public class Display : MonoBehaviour {

	public Text scoreText;
	public Text highScoreText;
	public Text speedText;
	public Text speedTextLabel;
	public Text speedLimitText;

	private bool speedWarning;
	
	/**
	 * Initialization
	 */
	void Start () {
		int highScore = 0;

		if (PlayerPrefs.HasKey("HighScore")) {
			highScore = PlayerPrefs.GetInt("HighScore");
		}

		highScoreText.text = "High Score: " + highScore;

		speedWarning = false;
	}
	
	/**
	 * Update is called once per frame
	 */
	void Update () {
		int score = GameState.score;
		int speed = Mathf.RoundToInt (SpeedController.GetDisplaySpeed ());
		int speedLimit = GameState.speedLimit;

		if (speedWarning && GameState.UserIsWithinLimit ()) {
			speedTextLabel.color = new Color(0, 0, 0);
			speedText.color = new Color(0, 0, 0);
			speedWarning = false;
		} else if (!speedWarning && !GameState.UserIsWithinLimit ()) {
			speedTextLabel.color = new Color(1, 0, 0);
			speedText.color = new Color(1, 0, 0);
			speedWarning = true;
		}

		scoreText.text = "Score: " + score;
		speedText.text = "" + speed;
		speedLimitText.text = "" + speedLimit;
	}
}
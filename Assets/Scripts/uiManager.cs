using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * This class handles the UI
 */
public class uiManager : MonoBehaviour {

	public Text scoreText;
	public Text highScoreText;
	public Text speedText;
	public Text speedLimitText;
	
	/**
	 * Initialization
	 */
	void Start () {
		int highScore = 0;

		if (PlayerPrefs.HasKey("HighScore")) {
			highScore = PlayerPrefs.GetInt("HighScore");
		}

		highScoreText.text = "High Score: " + highScore;
	}
	
	/**
	 * Update is called once per frame
	 */
	void Update () {
		int score = GameState.score;
		int speed = Mathf.RoundToInt (SpeedController.GetDisplaySpeed ());
		int speedLimit = GameState.speedLimit;

		scoreText.text = "Score: " + score;
		speedText.text = "Speed: " + speed;
		speedLimitText.text = "Speed Limit \n        " + speedLimit;
	}
}
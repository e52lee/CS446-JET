using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/**
 * This class handles the text on the Game Over screen
 */
public class GameOverScreen : MonoBehaviour {

	public Text scoreText;
	public Text highScoreText;
	public Text newHighScoreText;

	/**
	 * Use this for initialization
	 */
	void Start () {
		int score = PlayerPrefs.GetInt("Score", 0);
		int highScore = PlayerPrefs.GetInt("HighScore", 0);

		if (score > highScore) {
			highScore = score;
			newHighScoreText.text = "New High Score!";

			// Update the high score on record
			PlayerPrefs.SetInt("HighScore", score);
		}

		scoreText.text += "" + score;
		highScoreText.text += "" + highScore;
	}
	
	/**
	 * Update is called once per frame
	 */
	void Update () {
		// Do nothing
	}
}
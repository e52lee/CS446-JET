using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class uiManager : MonoBehaviour {

	// Use this for initialization
	public Text scoreText;
	public Text hiScoreText;
	bool gameOver;
	int score;
	int hiScore;

	
	/**
	 * Initialization
	 */
	void Start () {
		gameOver = false;
		score = 0;
		if(!PlayerPrefs.HasKey("HighScore") != null){
			hiScore =  PlayerPrefs.GetInt("HighScore");
		}
		else {
			hiScore = 0;
		}
		InvokeRepeating("scoreUpdate", 1.0f, 0.5f);
	}
	
	/**
	 * Update is called once per frame
	 */
	void Update () {
		scoreText.text = "Score: " + score;
		hiScoreText.text = "Score: " + hiScore;
	}

	void scoreUpdate () {
		if(!gameOver){
			score += 1;
		}
		if(score > hiScore){
			hiScore = score;
			PlayerPrefs.SetInt("HighScore", hiScore);
		}
	}

	public void gameOverActivated(){
		gameOver = true;
	}
}

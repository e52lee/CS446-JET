using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class uiManager : MonoBehaviour {

	// Use this for initialization
	public Text scoreText;
	public Text hiScoreText;
	public Text speedText;
	public Text speedLimitText;
	bool gameOver;
	int score;
	int hiScore;
	int speed;
	int speedLimit;
	int interval;
	
	/**
	 * Initialization
	 */
	void Start () {
		gameOver = false;
		score = 0;
		speed = 0;
		
		if(!PlayerPrefs.HasKey("HighScore") != null){
			hiScore =  PlayerPrefs.GetInt("HighScore");
			hiScoreText.text = "High Score: " + hiScore;
		}
		else {
			//hiScore = 0;
			hiScoreText.text = "High Score: 0";
		}
		InvokeRepeating("scoreUpdate", 1.0f, 0.5f);
		
		interval = Random.Range(5, 10);
		InvokeRepeating("limitUpdate", 0, interval);
		
		speed = (int) GameObject.Find("SpeedController").GetComponent<SpeedController>().GetDisplaySpeed();
	}
	
	/**
	 * Update is called once per frame
	 */
	void Update () {
		scoreText.text = "Score: " + score;
		speed = (int) GameObject.Find("SpeedController").GetComponent<SpeedController>().GetDisplaySpeed();
		speedText.text = "Speed: " + speed;
		//hiScoreText.text = "High Score: " + hiScore;
		
		speedLimitText.text = "Speed Limit \n        " + speedLimit;
	}
	
	void limitUpdate () {
		if(!gameOver){
			speedLimit = Random.Range(5, 12);
			speedLimit = speedLimit * 10;
		}
	}
	
	void scoreUpdate () {
		if(!gameOver){
			speed = (int) GameObject.Find("SpeedController").GetComponent<SpeedController>().GetDisplaySpeed();
			if (speed > speedLimit){
				if (score > 0) {
					score -= 1;
				}
			} else {
				score += 1;
			}
		}
		if(score > hiScore){
			//hiScore = score;
			PlayerPrefs.SetInt("HighScore", score);
		}
	}

	public void gameOverActivated(){
		gameOver = true;

		//PlayerPrefs.DeleteAll();
	}
}

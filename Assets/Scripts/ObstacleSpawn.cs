using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class is responsible for randomly spawning obstacles
 */
public class ObstacleSpawn : MonoBehaviour {

	private const float BASE_DISTANCE = 10f;    // Base distance between obstacles
	private const float DISTANCE_VARIANCE = 3f; // The amount that the distance can vary

	private float timer; // Time until next spawn

	public GameObject[] obstacles; // Array of obstacle objects -- set in the inspector

	/**
	 * Initialization
	 */
	void Start () {
		timer = GetRandDelay();
	}

	/**
	 * Update is called once per frame
	 */
	void Update () {
		timer -= Time.deltaTime;

		if (timer <= 0) {
			// Create a random obstacle
			Instantiate (GetRandObstacle (), GetRandPosition (), transform.rotation);

			// Reset the timer to a random delay
			timer = GetRandDelay ();
		}
	}

	/**
	 * Get a random obstacle from the array "obstacles" -- set in the inspector
	 *
	 * @return The obstacle
	 */
	private GameObject GetRandObstacle () {
		return obstacles[Random.Range (0, obstacles.Length)];
	}

	/**
	 * Get a delay time that ensures that obstacles spawn within a certain distance
	 * of one another (BASE_DISTANCE +/- DISTANCE_VARIANCE)
	 *
	 * @return The delay time
	 */
	private float GetRandDelay () {
		float obstacleSpeed = ObstacleMoving.GetTranslationSpeed ();
		float desiredDistance = BASE_DISTANCE + Random.Range (-DISTANCE_VARIANCE, DISTANCE_VARIANCE);

		// time = distance / speed
		return desiredDistance / obstacleSpeed;
	}

	/**
	 * Get a random position -- The x position will be either the left or right lane
	 * and the y and z positions will be the y and z of the ObstacleSpawn object
	 *
	 * @return The position vector
	 */
	private Vector3 GetRandPosition () {
		float xPos;

		// Randomly choose a lane
		if (Random.Range (0, 2) == 0) {
			xPos = Globals.LEFT_LANE_POSITION;
		} else {
			xPos = Globals.RIGHT_LANE_POSITION;
		}

		return new Vector3 (xPos, transform.position.y, transform.position.z);
	}
}
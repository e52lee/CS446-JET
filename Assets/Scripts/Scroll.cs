using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

	public float speed;
	Vector2 offset;
	Vector3 position;
	public float minSwipeDistY;
	private Vector2 startPos;
	public float touchStart = 0f;

	// Use this for initialization
	void Start () {
		speed = 0.2f;
	}
				
	// Update is called once per frame
	void Update (){

		if (Input.GetMouseButtonDown (0)) {
			touchStart = Input.mousePosition.y;
		}
		if (Input.GetMouseButtonUp (0)) {
			float delta = Input.mousePosition.y - touchStart;
			if (delta < -50f) {
				if (speed <= 2f) {
					speed = speed * 3 / 2;

				}
				//move up
			}

			if (delta > 50f) {
				speed = speed / 3 * 2;
				//move down
			}	
		}

		offset = new Vector2 (0, Time.time * speed);

		GetComponent<Renderer> ().material.mainTextureOffset = offset;
	}
}







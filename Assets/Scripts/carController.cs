using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour {

	public float carSpeed;
	Vector3 position;

	public float minSwipeDistY;
	public float minSwipeDistX;	
	private Vector2 startPos;
	public float touchStart = 0f;

	void Start () {
		position = transform.position;
	}

	// Update is called once per frame
	void Update () {
		/*position.x += Input.GetAxis ("Horizontal") * carSpeed * Time.deltaTime;
		transform.position = position;*/


		if(Input.GetMouseButtonDown(0)){
			touchStart = Input.mousePosition.x;
		}
		if(Input.GetMouseButtonUp(0)){
			float delta = Input.mousePosition.x - touchStart;
			if(delta<-50f){
				transform.position = new Vector3 (position.x - 3.5f * carSpeed * Time.deltaTime,
					position.y, position.z);
				//move right
			}

			else if(delta > 50f){
				transform.position = new Vector3 (position.x + 3.5f * carSpeed * Time.deltaTime,
					position.y, position.z);
				//move left
			}	
		}

		/*if(Vector3.Distance (transform.position, position)> 4.1f){
			if(transform.position.x > position.x){
				transform.position = new Vector3 (transform.position.x - carSpeed,
					transform.position.y, transform.position.z);
			}
			else {
				transform.position = new Vector3 (transform.position.x + carSpeed,
					transform.position.y, transform.position.z);
			}
			if(Vector3.Distance (transform.position, position) <= .4f){
				transform.position = position;
			}
		}*/
	}
}

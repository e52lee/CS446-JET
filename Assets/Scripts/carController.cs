using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour {

	public float carSpeed;
	Vector3 position;

	public float minSwipeDistY;
	public float minSwipeDistX;	
	private Vector2 startPos;

	void Start () {
		position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		position.x += Input.GetAxis ("Horizontal") * carSpeed * Time.deltaTime;
		transform.position = position;

		if (Input.touchCount > 0){
			Touch touch = Input.touches[0];
			switch (touch.phase) 
			{	
			case TouchPhase.Began:
				startPos = touch.position;
				break;
			
			case TouchPhase.Ended:

				float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
				
				if (swipeDistHorizontal > minSwipeDistX) 	
				{		
					float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
							
					if (swipeValue > 0){//right swipe		
						position.x += 5f * carSpeed * Time.deltaTime;
						transform.position = position;	
					}
					else if (swipeValue < 0){//left swipe	
						position.x -= 5f * carSpeed * Time.deltaTime;
						transform.position = position;
					}	
				}
				break;
			}
		}
	}
}

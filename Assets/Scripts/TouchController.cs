using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

	float touchStart = 0f;
	Vector3 cameraDestination;
	public float cameraSpeed = 0.1f;
	// Use this for initialization
	void Start () {
		cameraDestination = Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			touchStart = Input.mousePosition.x;
		}
		if(Input.GetMouseButtonUp(0)){
			float delta = Input.mousePosition.x - touchStart;
			if(delta<-50f){
				cameraDestination = new Vector3 (Camera.main.transform.position.x + 6f,
					Camera.main.transform.position.y, Camera.main.transform.position.z);
				//move the camera right
			}

			else if(delta > 50f){
				cameraDestination = new Vector3 (Camera.main.transform.position.x - 6f,
					Camera.main.transform.position.y, Camera.main.transform.position.z);
				//move the camera left
			}	
		}
		if(Vector3.Distance (Camera.main.transform.position, cameraDestination)> 4.1f){
			if(Camera.main.transform.position.x > cameraDestination.x){
				Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x - cameraSpeed,
					Camera.main.transform.position.y, Camera.main.transform.position.z);
			}
			else {
				Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x + cameraSpeed,
					Camera.main.transform.position.y, Camera.main.transform.position.z);
			}
			if(Vector3.Distance (Camera.main.transform.position, cameraDestination) <= .4f){
				Camera.main.transform.position = cameraDestination;
			}
		}
	}
}

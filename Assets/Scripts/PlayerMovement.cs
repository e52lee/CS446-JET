using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Screen.width and Screen.height

public class PlayerMovement : MonoBehaviour {
	
	void Update () 
	{
		if(Input.touchCount == 1) 
		{
			transform.Translate (Input.touches[0].deltaPosition.x * 0.01f, Input.touches[0].deltaPosition.y * 0.01f, 0);
		}
	}
}

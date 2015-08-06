using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	//Inspector values
	[SerializeField] float camMoveSpeed;

	private Vector2 moveIn = Vector2.zero;

	public void Init(){
		print ("Screen size: " + Screen.width + ", " + Screen.height);	

		//5 size --> height: 5*2*32 pixels = 320 pixels
		//camSize * 2 * 32 * n = height
		//camSize = height / (2 * 32 * n)
		//(n: pixel multiplier) n = 2

		Camera.main.orthographicSize = Screen.height / (2f * 32f);
	}

	public void GUpdate(){
		moveIn.x = Input.GetAxis("Horizontal");
		moveIn.y = Input.GetAxis("Vertical");
	}

	public void GFixedUpdate(){
		Camera.main.transform.Translate(moveIn * camMoveSpeed);
	}
}

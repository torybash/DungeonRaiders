using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	//Inspector values
	[SerializeField] float camMoveSpeed;

	private Vector2 moveIn = Vector2.zero;

	public void GUpdate(){
		moveIn.x = Input.GetAxis("Horizontal");
		moveIn.y = Input.GetAxis("Vertical");
	}

	public void GFixedUpdate(){
		Camera.main.transform.Translate(moveIn * camMoveSpeed);
	}
}

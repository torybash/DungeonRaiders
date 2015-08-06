using UnityEngine;
using System.Collections;

public class WispController : MonoBehaviour {

	//Prefabs
	[SerializeField] Transform wispPrefab;

	//Refs
	private Transform wispT;

	//Variables 
	private Vector2 wispGroundedPos = Vector2.zero;

	//Controller ref
	private GameController gameCtrl;


	void Awake(){
		gameCtrl = GetComponent<GameController>();
	}


	public void GUpdate()
	{
		if (Input.GetMouseButton(0))
		{
			if (wispT == null){
				wispT = Instantiate(wispPrefab);
			}

			wispT.position = Camera.main.ScreenToWorldPoint(Input.mousePosition).withZ(0);

//			wispGroundedPos

			RaycastHit2D hit = Physics2D.Raycast(wispT.position, -Vector2.up); //TODO RAYCAST SHOULD ONLY TARGET FLOOR
			if (hit.collider != null) { 
//				Debug.Log("Raycasthit: " + hit.collider + ", ypos: " + (hit.collider.transform.position.y + 1.0f));
				wispGroundedPos = new Vector2(wispT.position.x, hit.collider.transform.position.y + 1.0f);
			}
		}
		

	}

	
	public Vector2 GetWispPos(){
		if (wispT == null) return Vector2.zero;
		return wispT.position;
	}

	public Vector2 GetGroundedWispPos(){
		return wispGroundedPos;
	}

}
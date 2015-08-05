using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	//Refs
	[SerializeField] Transform entrance;





	public void Init(){
		foreach (Transform child in transform) {
			if (child.tag.Equals("Monster")){
				child.GetComponent<MonsterCommander>().Init ();
			}
		}
	}


	public Vector2 GetEntrancePosition()
	{
		return entrance.position;
	}
}

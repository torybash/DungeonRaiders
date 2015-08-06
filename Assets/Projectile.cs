using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	private UnitCommander ownerUnitCmd;

	public void Init(Vector2 velVec, Sprite sprite, UnitCommander unitCmd){
		GetComponent<SpriteRenderer>().sprite = sprite;
		GetComponent<Rigidbody2D>().velocity = velVec;
		ownerUnitCmd = unitCmd;
	}
}

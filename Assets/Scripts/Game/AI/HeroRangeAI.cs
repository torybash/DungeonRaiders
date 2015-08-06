using UnityEngine;
using System.Collections;

public class HeroRangeAI : AI {

	public override AIAction GFixedUpdate(UnitCommander unitCmd, Vector2 goalPos)
	{
		
		
		//Init
		Vector2 moveVec = Vector2.zero;
		Vector2 pos = unitCmd.transform.position;
		unitCmd.GetComponent<Rigidbody2D>().gravityScale = 1;
		state = AIState.MOVING;
		if (goalPos.x != pos.x) unitCmd.turnedRight = goalPos.x > pos.x;
		
		
		//Check for enemies //TODO DO BETTER
		float directionX = unitCmd.turnedRight ? 1f : -1f;
		Vector2 inFrontUpperLeft = new Vector2(pos.x + directionX * 0.5f, pos.y + 0.5f);
		Vector2 inFrontLowerRight = new Vector2(pos.x + directionX * 4.0f, pos.y - 0.5f);
		Collider2D[] inFrontColliders = Physics2D.OverlapAreaAll(inFrontUpperLeft, inFrontLowerRight);
		foreach (Collider2D coll in inFrontColliders) {
			if (coll.tag.Equals("Monster")){
				state = AIState.ATTACKING;
			}
		}
		
		
		
		switch (state) {
		case AIState.MOVING:
			moveVec = Movement(unitCmd, goalPos);
			break;
		case AIState.ATTACKING:
			
//			moveVec = AttackMove(unitCmd, inFrontColliders);
			break;
			
		default:
			break;
		}
		
		Debug.DrawLine(inFrontUpperLeft, inFrontLowerRight, Color.green);
		
		return new AIAction(moveVec, state == AIState.ATTACKING);
	}
}

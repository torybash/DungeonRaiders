using UnityEngine;
using System.Collections;

public class AI {

	protected AIState state = AIState.MOVING;

	public virtual AIAction GFixedUpdate(UnitCommander unitCmd, Vector2 goalPos){return new AIAction(Vector2.zero, false);}




	protected Vector2 Movement(UnitCommander unitCmd, Vector2 goalPos)
	{
		Vector2 moveVec = Vector2.zero;
		Vector2 pos = unitCmd.transform.position;
		bool isOnStairs = false;
		Vector2 upperLeft = new Vector2(pos.x - 0.1f, pos.y + 0.5f);
		Vector2 lowerRight = new Vector2(pos.x + 0.1f, pos.y - 0.5f);
		Collider2D[] colliders = Physics2D.OverlapAreaAll(upperLeft, lowerRight);
		
		float totXForce = 0;
		foreach (Collider2D coll in colliders) {
			
			//Is on stairs?
			if (coll.tag.Equals("Stairs"))
			{
				isOnStairs = true;
				unitCmd.GetComponent<Rigidbody2D>().gravityScale = 0;
				unitCmd.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				break;
			}//Move away from other heroes
			else if (coll.tag.Equals("Hero"))
			{
				float xDiff = coll.transform.position.x - pos.x;
				float xForce = xDiff == 0 ? 0 : 0.1f/xDiff;
				totXForce -= xForce;
			}
		}
		
		
		//Move in x or y direction / Can move in y direction?
		if (isOnStairs && CanContinueOnStairs(pos, goalPos)){ // &&  transform.position.y - 0.5f < goalPos.y || transform.position.y - 0.6f > goalPos.y){
			moveVec.y = Mathf.MoveTowards(pos.y, goalPos.y, 0.1f) - pos.y;
		}else{
			moveVec.x = Mathf.MoveTowards(pos.x, goalPos.x, 0.1f) - pos.x;
		}
		
		//Apply hero-distancer force
		if (totXForce > 0.1) totXForce = 0.1f;
		if (totXForce < -0.1) totXForce = -0.1f;
		if ((moveVec.x > 0 && totXForce < 0) || (moveVec.x < 0 && totXForce > 0)) moveVec.x += totXForce;
		
		return moveVec;
	}
	
	/// <summary>
	/// Move as close as possible to closest enemy
	/// </summary>
	/// <returns>The move.</returns>
	/// <param name="unitCmd">Unit cmd.</param>
	/// <param name="goalPos">Goal position.</param>
	protected Vector2 AttackMove(UnitCommander unitCmd, Collider2D[] inFrontColliders)
	{
		Vector2 moveVec = Vector2.zero;
		Vector2 pos = unitCmd.transform.position;
		
		//Get closest enemy
		float closestDist = float.MaxValue;
		Transform closestMonster = null;
		foreach (Collider2D coll in inFrontColliders) {
			if (coll.tag.Equals("Monster")){
				float dist = Vector2.Distance(unitCmd.transform.position, coll.transform.position);
				if (dist < closestDist){
					closestDist = dist;
					closestMonster = coll.transform;
				}
			}
		}
		
		//Turn towards closest enemy
		if (closestMonster.position.x > pos.x) unitCmd.turnedRight = true;
		else unitCmd.turnedRight = false;
		
		//Move towards enemy
		float goalPosX = closestMonster.position.x + (unitCmd.turnedRight ? -1 : 1) * 0.75f;
		moveVec.x = Mathf.MoveTowards(pos.x, goalPosX, 0.1f) - pos.x;
		
		//		Debug.Log("pos: " + pos + ", closestMonster.position: " + closestMonster.position + ", closestDist: " + closestDist + " , moveVec:  "+ moveVec);
		
		//keep distance to fellow heroes
		Vector2 upperLeft = new Vector2(pos.x - 0.1f, pos.y + 0.5f);
		Vector2 lowerRight = new Vector2(pos.x + 0.1f, pos.y - 0.5f);
		Collider2D[] colliders = Physics2D.OverlapAreaAll(upperLeft, lowerRight);
		
		float totXForce = 0;
		foreach (Collider2D coll in colliders) {
			
			if (coll.tag.Equals("Hero"))
			{
				float xDiff = coll.transform.position.x - pos.x;
				float xForce = xDiff == 0 ? 0 : 0.1f/xDiff;
				totXForce -= xForce;
			}
		}
		//Apply hero-distancer force
		if (totXForce > 0.1) totXForce = 0.1f;
		if (totXForce < -0.1) totXForce = -0.1f;
		if ((moveVec.x > 0 && totXForce < 0) || (moveVec.x < 0 && totXForce > 0)) moveVec.x += totXForce;
		
		return moveVec;
	}
	
	
	protected bool CanContinueOnStairs(Vector2 pos, Vector2 goalPos){
		if (goalPos.y > pos.y){
			Vector2 aboveUpperLeft = new Vector2(pos.x - 0.1f, pos.y - 0.4f);
			Vector2 aboveLowerRight = new Vector2(pos.x + 0.1f, pos.y - 0.5f);
			Collider2D[] aboveColliders = Physics2D.OverlapAreaAll( aboveUpperLeft, aboveLowerRight);
			foreach (Collider2D coll in aboveColliders) {
				if (coll.tag.Equals("Stairs")){
					return true;
				}
			}	
			//			Debug.DrawLine(new Vector3(aboveUpperLeft.x, aboveUpperLeft.y, 0), new Vector3(aboveLowerRight.x, aboveLowerRight.y, 0), Color.red);
			
		}else if (goalPos.y < pos.y){
			Vector2 belowUpperLeft = new Vector2(pos.x - 0.1f, pos.y - 0.55f);
			Vector2 belowLowerRight = new Vector2(pos.x + 0.1f, pos.y - 0.65f);
			Collider2D[] belowColliders = Physics2D.OverlapAreaAll( belowUpperLeft, belowLowerRight);
			foreach (Collider2D coll in belowColliders) {
				if (coll.tag.Equals("Stairs")){
					return true;
				}
			}	
			//			Debug.DrawLine(new Vector3(belowUpperLeft.x, belowUpperLeft.y, 0), new Vector3(belowLowerRight.x, belowLowerRight.y, 0), Color.red);
		}
		return false;
	}


}


public enum AIState{
	MOVING,
	ATTACKING
}

public class AIAction{
	public Vector2 moveVec;
	public bool attacking;

	public AIAction(Vector2 moveVec, bool attacking){
		this.moveVec = moveVec;
		this.attacking = attacking;
	}
}
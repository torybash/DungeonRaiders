using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroCommander : UnitCommander {
	




	void Awake () {
		Awoken ();
	}


	public Hero hero;

	public void Init(Hero hero)
	{
		this.hero = hero;
//		animSprites = new Dictionary<UnitAnimation, Sprite>();

//		animSprites = spLib.GetUnitAnimation(hero.heroClass);

		GetComponent<Animator>().runtimeAnimatorController = gameCtrl.spLib.GetHeroAnimator(hero.heroClass);

//		weaponContainer.gameObject.SetActive(false);

		attackCooldownDuration = 0.5f; //DEBUG TODO
		attackCooldownTimer = 0;

		maxHealth = hero.maxHP;
		health = hero.maxHP;

		//DEBUG TODO
		if (hero.heroClass == HeroClass.WARRIOR || hero.heroClass == HeroClass.BARBARIAN || hero.heroClass == HeroClass.ASSASSIN){
			unitAI = new HeroMeleeAI();
		}else if (hero.heroClass == HeroClass.RANGER){
			unitAI = new HeroRangeAI();
		}else if (hero.heroClass == HeroClass.WARRIOR || hero.heroClass == HeroClass.BARBARIAN){
			unitAI = new HeroCasterAI();
		}


		weaponContainer.GetComponent<Animator>().runtimeAnimatorController = hero.weapon.animatorController;


		PositionChanged();
	}


	public void PositionChanged()
	{
		GetComponent<SpriteRenderer>().sortingOrder = 10 + hero.position;
	}

	public void GFixedUpdate(HeroCommander inFrontHeroCmd)
	{
		Vector2 goalPos = Vector2.zero;
		goalPos = gameCtrl.wispCtrl.GetGroundedWispPos();
//		if (hero.position == 0){ //First hero
//			//Goal
//			goalPos = gameCtrl.wispCtrl.GetGroundedWispPos();
//			if (goalPos.x != transform.position.x) turnedRight = goalPos.x > transform.position.x;
//
//		}else{ //all other heroes
//			//Goal pos is behind last hero
//			goalPos = inFrontHeroCmd.transform.position + (inFrontHeroCmd.turnedRight ? -1f : 1f) * 0.25f * Vector3.right;
//			if (Mathf.Abs(goalPos.x - transform.position.x) < 0.05) turnedRight = inFrontHeroCmd.turnedRight;
//			else turnedRight = goalPos.x > transform.position.x;
//		}
//		if (goalPos.x != transform.position.x) turnedRight = goalPos.x > transform.position.x;


		AIAction aiAct = unitAI.GFixedUpdate(this, goalPos);



//		print ("AI Action - moveVec: " + aiAct.moveVec + ", attacking: " + aiAct.attacking);

		if (aiAct.attacking)
		{
			if (attackCooldownTimer <= 0)
			{ //FIRE WEAPON
//				weaponContainer.GetComponent<Animator>().ResetTrigger("useWeapon");
				//TODO make other stuff possible
				if (turnedRight){
					weaponContainer.GetComponent<Animator>().SetTrigger("useWeaponRight");
				}else{
					weaponContainer.GetComponent<Animator>().SetTrigger("useWeaponLeft");
				}

				if (hero.weapon.weaponType == WeaponType.RANGED){
					Transform projectileT = Instantiate(projectilePrefab);
					Vector2 velVec = turnedRight ? Vector2.right : -Vector2.right;
					projectileT.GetComponent<Projectile>().Init(velVec, hero.weapon.projectileSprite, this);

					projectileT.gameObject.layer = LayerMask.NameToLayer("HeroWeapons");
				}


				attackCooldownTimer = attackCooldownDuration;
			}else //Wait for cooldown
			{
				attackCooldownTimer -= Time.fixedDeltaTime;
			}
		}else
		{
//			if (aiAct.moveVec.x != 0) //only change direction if changed
//			{
//				turnedRight = aiAct.moveVec.x * 10 > 0;
//			}

			//Set directions //TODO  more elegant
			if (hero.position == 0){ //First hero
				if (goalPos.x != transform.position.x) turnedRight = goalPos.x > transform.position.x;
				
			}else{ //all other heroes
				if (Mathf.Abs(goalPos.x - transform.position.x) < 0.10) turnedRight = inFrontHeroCmd.turnedRight;
				else turnedRight = goalPos.x > transform.position.x;
			}
		}


		print ("Hero goal pos: "+ goalPos + ", hero pos: "+ transform.position + " turnedRight: " +  turnedRight + ", aiAct.attackin: " + aiAct.attacking);


		//Move -- cant move further than last hero (except for first)
		Vector2 actualMoveVec = Vector2.zero;
		if (hero.position == 0)
		{
			actualMoveVec = aiAct.moveVec;

		}else{
			Vector2 newPosition = transform.position;
			newPosition += aiAct.moveVec;

			if (inFrontHeroCmd.turnedRight){
				float maxXAllowed = inFrontHeroCmd.transform.position.x - 0.25f;
				if (newPosition.x > maxXAllowed) newPosition.x = maxXAllowed;
			}else{
				float minXAllowed = inFrontHeroCmd.transform.position.x + 0.25f;
				if (newPosition.x < minXAllowed) newPosition.x = minXAllowed;
			}
//			transform.position = Vector2.MoveTowards(transform.position, newPosition, 0.1f);
			actualMoveVec = Vector2.MoveTowards(transform.position, newPosition, 0.1f) - transform.position.v2();

		}
		transform.Translate(actualMoveVec);
//		Vector2 newPosition = transform.position;
//		newPosition.x = Mathf.MoveTowards(newPosition.x, goal
//		transform.Translate(aiAct.moveVec);

		//Animation

		GetComponent<Animator>().SetBool("turnedRight", turnedRight); 
		GetComponent<Animator>().SetFloat("moveSpeed", Mathf.Abs(actualMoveVec.x * 10));

		UpdateHealthBar();

//		Debug.Log("turnedRight: " +turnedRight + " moveVec: " + aiAct.moveVec + ", aiAct.attacking: " + aiAct.attacking + " --Time: " + Time.time);
	}
}




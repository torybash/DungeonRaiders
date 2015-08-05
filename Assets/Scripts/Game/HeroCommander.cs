using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroCommander : UnitCommander {
	




	void Awake () {
		Awoken ();
	}




	public void Init(Hero hero)
	{
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
	}




	public void GFixedUpdate()
	{
		Vector2 goalPos = gameCtrl.wispCtrl.GetGroundedWispPos();

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
		}


		//Move
		transform.Translate(aiAct.moveVec);

		//Animation

		GetComponent<Animator>().SetBool("turnedRight", turnedRight); 
		GetComponent<Animator>().SetFloat("moveSpeed", Mathf.Abs(aiAct.moveVec.x * 10));

		UpdateHealthBar();

//		Debug.Log("turnedRight: " +turnedRight + " moveVec: " + aiAct.moveVec + ", aiAct.attacking: " + aiAct.attacking + " --Time: " + Time.time);
	}
}




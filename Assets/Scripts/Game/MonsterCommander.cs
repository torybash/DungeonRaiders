using UnityEngine;
using System.Collections;

public class MonsterCommander : UnitCommander {


	[SerializeField] MonsterType monsterType;
	
	void Awake(){
		Awoken();
	}
	
	public void Init()
	{
		//		animSprites = new Dictionary<UnitAnimation, Sprite>();
		
		//		animSprites = spLib.GetUnitAnimation(hero.heroClass);
		
		GetComponent<Animator>().runtimeAnimatorController = gameCtrl.spLib.GetMonsterAnimator(monsterType);

		gameCtrl.unitCtrl.MonsterInitialised(this);


		health = 40;
		maxHealth = 40;

		//DEBUG
		unitAI = new SimpleMonsterAI();

	}


	public void GFixedUpdate()
	{
		//Get data from surroundings

		//Call monsterAI to figure out action

		//Use action

		UpdateHealthBar();
	}

	
	


	void OnTriggerEnter2D(Collider2D coll) {
		{
			if (coll.gameObject.layer == LayerMask.NameToLayer("HeroWeapons")){
				Debug.Log("OnTriggerEnter2D - Collision with " + coll);

				//Get weapon ownerer
				//DEBUG/TODO DO more elegantly
				Transform unitT = coll.transform.parent.parent;

				GotHit(unitT);
			}
		}
	}
}
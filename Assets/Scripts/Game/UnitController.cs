using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitController : MonoBehaviour {

	//Refs
	private List<HeroCommander> heroCommanders = new List<HeroCommander>();
	private List<MonsterCommander> monsterCommanders = new List<MonsterCommander>();


	//Prefabs
	[SerializeField] Transform heroPrefab;

	//Controller ref
	private GameController gameCtrl;


	public void SpawnHeroes(Vector2 pos)
	{
		int c = 0;
		foreach (Hero hero in GameManager.I.status.heroes) {
			//Create hero at position
			Transform heroT = (Transform) Instantiate(heroPrefab, pos + Vector2.right * c * 0.3f, Quaternion.identity);
			c++;

			//Initialiase
			heroT.GetComponent<HeroCommander>().Init(hero);

			//Add to list
			heroCommanders.Add(heroT.GetComponent<HeroCommander>());
		}
	}


	public void MonsterInitialised(MonsterCommander monsterCmd){
		monsterCommanders.Add (monsterCmd);
	}

	public void GFixedUpdate()
	{
		foreach (HeroCommander heroCmd in heroCommanders) {
			heroCmd.GFixedUpdate();
		}
		foreach (MonsterCommander monsterCmd in monsterCommanders) {
			monsterCmd.GFixedUpdate();
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitController : MonoBehaviour {

	//Refs
	private List<HeroCommander> heroCommanders = new List<HeroCommander>();
	private List<MonsterCommander> monsterCommanders = new List<MonsterCommander>();


	//Prefabs
	[SerializeField] Transform heroPrefab;

	//Controller ref
	private GameController gameCtrl;


	void Awake(){
		gameCtrl = GetComponent<GameController>();
	}

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
		//Update heroes, sort first by position
		List<HeroCommander> sortedHeroCommanders = heroCommanders.OrderBy(o=>o.hero.position).ToList();

		print ("Update unit controller");
		HeroCommander lastHeroCmd = null;
		foreach (HeroCommander heroCmd in sortedHeroCommanders) {
			heroCmd.GFixedUpdate(lastHeroCmd);
			lastHeroCmd = heroCmd;
			print ("Hero: " + heroCmd.hero.heroName + ", pos: "+ heroCmd.hero.position);

		}
		foreach (MonsterCommander monsterCmd in monsterCommanders) {
			monsterCmd.GFixedUpdate();
		}
	}

	public void MonsterKilled(MonsterCommander monsterCmd){
		monsterCommanders.Remove(monsterCmd);
	}



	public void HeroPositionChange(Hero hero, int change)
	{
		Debug.Log("HeroPositionUp -- BEFORE - hero.pos: " + hero.position);

		foreach (var item in GameManager.I.status.heroes) {
			print ("hero: " + item.heroName + " , pos: "+ item.position);
		}

		//Change position for other hero (who was in the new position)
		Hero otherHero = GameManager.I.status.heroes.Find(x => x.position == (hero.position + change));
		otherHero.position -= change;
		heroCommanders[otherHero.id].PositionChanged();
		
		//Change position for hero
		hero.position += change;
		heroCommanders[hero.id].PositionChanged();

		print ("AFTER - hero.pos: " + hero.position);
		foreach (var item in GameManager.I.status.heroes) {
			print ("hero: " + item.heroName + " , pos: "+ item.position);
		}

		//Update UI
		gameCtrl.gameUICtrl.UpdateGameHeroesPanel();
	}
	
}

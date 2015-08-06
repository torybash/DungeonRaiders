using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GameUIController : MonoBehaviour {

	//UI refs
	[SerializeField] Transform canvas;

	[SerializeField] Transform layoutGroupGameHeroes;


	//Prefabs
	[SerializeField] Transform healthBarPrefab;
	[SerializeField] Transform gameHeroPanelPrefab;

	//Refs
	private Dictionary<int, Transform> healthBarDict = new Dictionary<int, Transform>();

	private Dictionary<int, GameHeroPanel> gameHeroPanels = new Dictionary<int, GameHeroPanel>();


	public void UpdateHealthBar(UnitCommander unitCmd)
	{
		int instId = unitCmd.GetInstanceID();

		Transform healthBarT = null;
		if (!healthBarDict.ContainsKey(instId)){
			healthBarT = Instantiate(healthBarPrefab);
			healthBarT.SetParent(canvas);
			healthBarDict.Add(instId, healthBarT);
		}else{
			healthBarT = healthBarDict[instId];
		}

		Vector2 aboveHeadPosition = Camera.main.WorldToScreenPoint(unitCmd.transform.position);
		aboveHeadPosition.y += 25;

		healthBarT.position = aboveHeadPosition;

		float health = unitCmd.health / (float) unitCmd.maxHealth;
		healthBarT.GetComponent<HealthBar>().UpdateBar(health, HealthToColor(health));

	}


	private Color HealthToColor(float health){
		Color color = new Color();

		color.g = health;
		color.r = 1 - health;
		color.b = 0;
		color.a = 1;

		return color;
	}


	public void Init()
	{
		//Maybe sort hero list by position //TODO

		

		//Add a panel for each hero
		foreach (Hero hero in GameManager.I.status.heroes) {
			Transform gameHeroPanelT = Instantiate(gameHeroPanelPrefab);
			gameHeroPanelT.SetParent(layoutGroupGameHeroes);

			//Init panel
			gameHeroPanelT.GetComponent<GameHeroPanel>().Init(hero);

			gameHeroPanels.Add(hero.id, gameHeroPanelT.GetComponent<GameHeroPanel>());
		}

		foreach (Hero hero in GameManager.I.status.heroes) {
			gameHeroPanels[hero.id].GetComponent<RectTransform>().SetSiblingIndex(hero.position);
		}


		
	}

	public void UpdateGameHeroesPanel()
	{
		foreach (Hero hero in GameManager.I.status.heroes) {
			gameHeroPanels[hero.id].GetComponent<RectTransform>().SetSiblingIndex(hero.position);
			gameHeroPanels[hero.id].UpdateUI();
		}
	}


	public void GFixedUpdate(){
//		UpdateGameHeroesPanel();
	}
}



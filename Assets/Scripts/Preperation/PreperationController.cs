using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PreperationController : MonoBehaviour {

	//GUI references
	[SerializeField] RectTransform canvas;

	[SerializeField] Transform inactive;

	

	[SerializeField] Text textGold;
	[SerializeField] Text textHeroSouls;
	[SerializeField] RectTransform layoutGroupHeroes;



	[SerializeField] RectTransform panelHeroCreation;
	[SerializeField] RectTransform panelHeroInfo;
	[SerializeField] RectTransform panelGreyout;
	[SerializeField] RectTransform panelWorldMap;
	[SerializeField] RectTransform panelMapLevelInfo;

	//Prefabs
	[SerializeField] Transform panelHeroPrefab;
	
	//Variables
	List<RectTransform> heroTList = new List<RectTransform>();

	//Controlle refs
	WorldMapController worldMapCtrl;

	void Awake(){
		worldMapCtrl = GetComponent<WorldMapController>();
	}

	void Start () 
	{
		//Set GUI values
		textGold.text = "Gold: " + GameManager.I.status.gold;
		textHeroSouls.text = "Hero Souls: " + GameManager.I.status.heroSouls;

		//Create a single hero panel
		RectTransform heroT = (RectTransform) Instantiate(panelHeroPrefab);
		heroT.SetParent(layoutGroupHeroes);
		heroTList.Add(heroT);
	}
	



	public void AcceptNewHero(Hero hero)
	{
		//Destroy greyout
		DisablePanel(panelGreyout);

		//Destroy creator panel
		DisablePanel(panelHeroCreation);

		//Activate and init existing hero panel with new hero
		heroTList[heroTList.Count - 1].GetComponent<HeroPanelController>().Init(hero);

		//Make new hero panel and add to group
		RectTransform heroT = (RectTransform) Instantiate(panelHeroPrefab);
		heroT.SetParent(layoutGroupHeroes);
		heroTList.Add(heroT);

		//Add hero to gameStatus
		GameManager.I.status.heroes.Add(hero);
	}
	
	public void AddHero()
	{
		//Disable background / grey out //TODO set sibling index (layer)
		EnablePanel (panelGreyout);

		//Open hero creator panel
		EnablePanel (panelHeroCreation);

		//Init creator panel
		panelHeroCreation.GetComponent<HeroCreationPanelController>().InitPanel();

	}


	private void EnablePanel(RectTransform panel)
	{
		panel.SetParent(canvas);
	}

	private void DisablePanel(RectTransform panel)
	{
		panel.SetParent(inactive);
	}

	
	/***FROM UI***/
	public void OpenShop(){
	
	}

	public void OpenMap()
	{
		//If map is already open, close map instead
		if (panelWorldMap.parent == canvas.transform) {
			DisablePanel(panelWorldMap);
			DisablePanel(panelMapLevelInfo);
			return;
		}

		//Close/hide other windows


		//Open map window
		EnablePanel(panelWorldMap);
		
		//Open map level info
		EnablePanel(panelMapLevelInfo);

		//Init map indicators
		worldMapCtrl.InitMap();

//		GameManager.I.StartNextRound();

	}

}

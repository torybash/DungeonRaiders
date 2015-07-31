using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PreperationController : MonoBehaviour {

	//GUI references
	[SerializeField] RectTransform canvas;



	[SerializeField] Text textGold;
	[SerializeField] Text textHeroSouls;
	[SerializeField] RectTransform layoutGroupHeroes;


	//Prefabs
	[SerializeField] Transform panelHero;
	[SerializeField] Transform panelHeroCreation;
	[SerializeField] Transform panelHeroInfo;
	[SerializeField] Transform panelGreyout;


	//Variables
	List<Transform> heroTList;
	RectTransform heroCreationT;
	RectTransform heroInfoT;
	RectTransform greyoutT;

	void Start () 
	{
		//Set GUI values
		textGold.text = "Gold: " + GameManager.I.status.gold;
		textHeroSouls.text = "Hero Souls: " + GameManager.I.status.heroSouls;


	}
	


/***From UI***/

	public void AddHero()
	{
		//Disable background / grey out
		greyoutT = (RectTransform) Instantiate(panelGreyout);
		greyoutT.parent = canvas;
		greyoutT.localPosition = Vector3.zero;
		greyoutT.offsetMin = new Vector2(0, 0);
		greyoutT.offsetMax = new Vector2(0, 0);
//		greyoutT.SetSiblingIndex(5);

		//Open hero creator panel
		heroCreationT = (RectTransform) Instantiate(panelHeroCreation);
		heroCreationT.parent = canvas;
		heroCreationT.localPosition = Vector3.zero;

		//Init creator panel
		heroCreationT.GetComponent<HeroCreationController>().InitPanel();

	}


}

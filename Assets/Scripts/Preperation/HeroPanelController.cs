using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroPanelController : MonoBehaviour {

	//UI refs
	[SerializeField] RectTransform activePanel;
	[SerializeField] RectTransform passivePanel;

	[SerializeField] Text textName;
	[SerializeField] Text textClass;
	[SerializeField] Text textLevel;
	[SerializeField] Text textHealth;
	[SerializeField] Text textMana;
	[SerializeField] Text textDPS;


	//Controller ref
	private PreperationController prepCtrl;

	void Awake()
	{
		prepCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<PreperationController>();
	}


	public void Init(Hero hero)
	{
		//Hide "passive" panel and show "active panel
		passivePanel.gameObject.SetActive(false);
		activePanel.gameObject.SetActive(true);

		//Set UI values
		textName.text = hero.heroName;
		textClass.text = Strings.heroClassDict[hero.heroClass];
		textLevel.text = "Level: " + hero.level;
		textHealth.text = "HP: " + hero.maxHP;
		textMana.text = "Mana: " + hero.maxMana;
		textDPS.text = "DPS: " + new HeroHelper().CalculateHeroDPS(hero);

	}

	/***FROM UI***/
	public void AddHero()
	{
		prepCtrl.AddHero();
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroCreationPanelController : MonoBehaviour {

	//GUI refs
	[SerializeField] Text textHeroName;
	[SerializeField] Text textHeroClass;
	[SerializeField] Text textStrength;
	[SerializeField] Text textDexterity;
	[SerializeField] Text textIntelligence;
	[SerializeField] Text textVitality;

	//Variables
	private HeroClass currHeroClass;
	private Hero currHero;

	//Controller ref
	private PreperationController prepCtrl;


	void Awake()
	{
		prepCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<PreperationController>();
	}

	public void InitPanel()
	{
		currHeroClass = HeroClass.WARRIOR;

		UpdateWithNewClass();
	}


	public void UpdateWithNewClass()
	{
		//Make new random hero with class
		currHero = new HeroHelper().CreateHero(currHeroClass);
	

		//Update UI
		textHeroName.text = currHero.heroName;
		textHeroClass.text = Strings.heroClassDict[currHero.heroClass];
		textStrength.text = "Strength: " + currHero.strength;
		textDexterity.text = "Dexterity: " + currHero.dexterity;
		textIntelligence.text = "Intelligence: " + currHero.intelligence;
		textVitality.text = "Vitality: " + currHero.vitality;
	}


	/***FROM UI***/
	public void PreviousHeroClass()
	{
		int heroClassId = (int)currHeroClass - 1;
		if (heroClassId < 0) heroClassId = (int) HeroClass.LAST;
		currHeroClass = (HeroClass)heroClassId;
		UpdateWithNewClass();
	}

	public void NextHeroClass()
	{
		int heroClassId = (int)currHeroClass + 1;
		if (heroClassId > (int)HeroClass.LAST) heroClassId = 0;
		currHeroClass = (HeroClass)heroClassId;
		UpdateWithNewClass();
	}

	public void AcceptHero()
	{
		prepCtrl.AcceptNewHero(currHero);
	}
}

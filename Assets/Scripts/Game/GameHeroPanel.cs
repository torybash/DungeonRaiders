using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHeroPanel : MonoBehaviour {

	[SerializeField] Text textHeroName;
	[SerializeField] Text textHeroPosition;

	[SerializeField] Button buttonMoveUp;
	[SerializeField] Button buttonMoveDown;

	private GameController gameCtrl;

	private Hero hero;

	void Awake(){
		gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	public void Init(Hero hero)
	{
		this.hero = hero;

		UpdateUI();
	}


	public void UpdateUI(){
		textHeroName.text = hero.heroName;
		textHeroPosition.text = (hero.position + 1) + ".";

		buttonMoveDown.gameObject.SetActive(true);
		buttonMoveUp.gameObject.SetActive(true);
		if (hero.position == 0) buttonMoveUp.gameObject.SetActive(false);
		if (hero.position == GameManager.I.status.heroes.Count - 1) buttonMoveDown.gameObject.SetActive(false);
	}

	/***FROM UI***/
	public void MoveUp()
	{
		gameCtrl.unitCtrl.HeroPositionChange(hero, -1);

	}

	public void MoveDown()
	{
		gameCtrl.unitCtrl.HeroPositionChange(hero, 1);
	}
}

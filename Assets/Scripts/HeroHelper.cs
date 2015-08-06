using UnityEngine;
using System.Collections;

public class HeroHelper  {

	public Hero CreateHero(HeroClass heroClass)
	{
		Hero hero = new Hero();
		hero.heroName = Strings.RandomNameForClass(heroClass);
		hero.heroClass = heroClass;

		switch (heroClass) {
		case HeroClass.WARRIOR:
			hero.strength = 10;
			hero.dexterity = 5;
			hero.intelligence = 3;
			hero.vitality = 8;

			hero.weapon = GameManager.I.weaponLib.GetWeapon("Sword_Rusty");
			break;
		case HeroClass.BARBARIAN:
			hero.strength = 12;
			hero.dexterity = 4;
			hero.intelligence = 2;
			hero.vitality = 8;

			hero.weapon = GameManager.I.weaponLib.GetWeapon("Hammer_Rusty");
			break;
		case HeroClass.ASSASSIN:
			hero.strength = 6;
			hero.dexterity = 9;
			hero.intelligence = 3;
			hero.vitality = 5;
			break;
		case HeroClass.RANGER:
			hero.strength = 2;
			hero.dexterity = 12;
			hero.intelligence = 4;
			hero.vitality = 2;
			break;
		case HeroClass.WIZARD:
			hero.strength = 1;
			hero.dexterity = 6;
			hero.intelligence = 12;
			hero.vitality = 3;
			break;
		case HeroClass.PRIEST:
			hero.strength = 5;
			hero.dexterity = 2;
			hero.intelligence = 10;
			hero.vitality = 4;
			break;
		default:
			break;
		}

		hero.level = 1;
		hero.maxMana = CalculateMaxMana(hero.intelligence);
		hero.maxHP = CalculateMaxHP(hero.vitality);

		hero.position = GameManager.I.status.heroes.Count;
		hero.id = GameManager.I.status.heroes.Count;

		return hero;
	}



	public int CalculateHeroDPS(Hero hero)
	{
		//TODO: DO THIS
		return (hero.strength / 2 + hero.dexterity / 2 + hero.intelligence / 2)/2;
	}

	
	public int CalculateMaxHP(int vitality)
	{
		return 10 + vitality * 5;
	}

	public int CalculateMaxMana(int intelligence)
	{
		return 0 + intelligence * 4;
	}
}

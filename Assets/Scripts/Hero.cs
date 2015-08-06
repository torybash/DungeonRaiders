using UnityEngine;
using System.Collections;

[System.Serializable]
public class Hero  {

	public string heroName;
	public HeroClass heroClass;
	public int id;

	public int strength;
	public int dexterity;
	public int intelligence;
	public int vitality;

	public int level;
	public int maxHP;
	public int maxMana;

	public Weapon weapon;


	public int position;


	public void SetPosition(int position){
		this.position = position;
	}

}

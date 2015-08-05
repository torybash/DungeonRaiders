using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitComponents {


}


public enum HeroClass{
	WARRIOR = 0,
	BARBARIAN = 1,
	ASSASSIN = 2,
	RANGER = 3,
	WIZARD = 4,
	PRIEST = 5,
	LAST = 5
	

}

public enum MonsterType{
	SKELETON = 0,
	DEVIL = 1
}


public static class Strings{
	public static Dictionary<HeroClass, string> heroClassDict;
	public static Dictionary<HeroClass, List<string>> heroNamesDict;

	public static void Init()
	{
		heroClassDict = new Dictionary<HeroClass, string>();
		heroClassDict.Add(HeroClass.WARRIOR, "Warrior");
		heroClassDict.Add(HeroClass.BARBARIAN, "Barbarian");
		heroClassDict.Add(HeroClass.ASSASSIN, "Assassin");
		heroClassDict.Add(HeroClass.RANGER, "Ranger");
		heroClassDict.Add(HeroClass.WIZARD, "Wizard");
		heroClassDict.Add(HeroClass.PRIEST, "Priest");

		heroNamesDict = new Dictionary<HeroClass, List<string>>();
		heroNamesDict.Add(HeroClass.WARRIOR, new List<string>());
		heroNamesDict.Add(HeroClass.BARBARIAN, new List<string>());
		heroNamesDict.Add(HeroClass.ASSASSIN, new List<string>());
		heroNamesDict.Add(HeroClass.RANGER, new List<string>());
		heroNamesDict.Add(HeroClass.WIZARD, new List<string>());
		heroNamesDict.Add(HeroClass.PRIEST, new List<string>());

//		heroNamesDict[HeroClass
		heroNamesDict[HeroClass.WARRIOR].Add("Salvador");
		heroNamesDict[HeroClass.WARRIOR].Add("Guldur");
		heroNamesDict[HeroClass.WARRIOR].Add("Aron");
		heroNamesDict[HeroClass.BARBARIAN].Add("Groth");
		heroNamesDict[HeroClass.ASSASSIN].Add("Finch");
		heroNamesDict[HeroClass.RANGER].Add("Loopa");
		heroNamesDict[HeroClass.WIZARD].Add("Monford");
		heroNamesDict[HeroClass.PRIEST].Add("Erryka");
	}


	public static string RandomNameForClass(HeroClass heroClass)
	{

		int rndNameId = Random.Range(0, heroNamesDict[heroClass].Count);
		return heroNamesDict[heroClass][rndNameId];
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteLibrary : MonoBehaviour {

	[SerializeField] List<HeroAnimator> heroAnimators;
	private Dictionary<HeroClass, AnimatorOverrideController> heroAnimatorsDict;

	[SerializeField] List<MonsterAnimator> monsterAnimators;
	private Dictionary<MonsterType, AnimatorOverrideController> monsterAnimatorsDict;
	


	void Awake(){

		heroAnimatorsDict = new Dictionary<HeroClass, AnimatorOverrideController>();
		foreach (HeroAnimator heroAnim in heroAnimators) {
			heroAnimatorsDict.Add(heroAnim.heroClass, heroAnim.overrideController);
		}

		monsterAnimatorsDict = new Dictionary<MonsterType, AnimatorOverrideController>();
		foreach (MonsterAnimator monsterAnim in monsterAnimators) {
			monsterAnimatorsDict.Add(monsterAnim.monsterType, monsterAnim.overrideController);
		}
	}
	

	public AnimatorOverrideController GetHeroAnimator(HeroClass heroClass)
	{
		return heroAnimatorsDict[heroClass];
	}


	public AnimatorOverrideController GetMonsterAnimator(MonsterType monsterType)
	{
		return monsterAnimatorsDict[monsterType];
	}

}




[System.Serializable]
public class HeroAnimator {
	public HeroClass heroClass;
	public AnimatorOverrideController overrideController;
}

[System.Serializable]
public class MonsterAnimator {
	public MonsterType monsterType;
	public AnimatorOverrideController overrideController;
}

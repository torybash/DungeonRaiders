using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameStatus {

	public int mapId; //TODO: more advanced

	public int gold;
	public int heroSouls;

	public List<Hero> heroes;

	public List<int> levelsCompleted;


	public GameStatus(){
		mapId = -1;
		gold = 0;
		heroSouls = 2;

		heroes = new List<Hero>();

		levelsCompleted = new List<int>();
	}
}

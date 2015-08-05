using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapController : MonoBehaviour {

	//Map refs
	[SerializeField] List<MapDefinition> mapDefintions;
	private Dictionary<int, MapDefinition> mapDefintionsDict;

	//Controller ref
	private GameController gameCtrl;

	//Variables
	private Map currentMap;

	List<Transform> heroTs;


	//Prefabs
	[SerializeField] Transform heroPrefab;

	void Awake(){
		heroTs = new List<Transform>();
		mapDefintionsDict = new Dictionary<int, MapDefinition>();
		foreach (MapDefinition mapDef in mapDefintions) {
			mapDefintionsDict.Add(mapDef.mapId, mapDef);
		}

		gameCtrl = GetComponent<GameController>();
	}

	public void StartMap(int mapId)
	{
		//Find map by Id
		MapDefinition mapDef = mapDefintionsDict[mapId];

		//Initialise map (Wake up monsters)
		mapDef.map.Init();



		//Spawn heroes at start pos 
		gameCtrl.unitCtrl.SpawnHeroes(mapDef.map.GetEntrancePosition());

		//Ref
		currentMap = mapDef.map;
	}



}

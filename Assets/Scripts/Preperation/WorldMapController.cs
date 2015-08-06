using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldMapController : MonoBehaviour {


	[SerializeField] List<WorldLevel> worldLevels;
	private Dictionary<int, WorldLevel> worldLevelsDict;
	private Dictionary<MapIndicator, WorldLevel> worldLevelUsingIndicatorDict;

	private MapIndicator hoveredIndicator = null;




	private float minX, maxX, minY, maxY;

	private PreperationController prepCtrl;

	void Awake(){
		worldLevelsDict = new Dictionary<int, WorldLevel>();
		worldLevelUsingIndicatorDict = new Dictionary<MapIndicator, WorldLevel>();
		foreach (WorldLevel item in worldLevels) {
			worldLevelsDict.Add (item.levelID, item);
			worldLevelUsingIndicatorDict.Add (item.indicator, item);
		}

		prepCtrl = GetComponent<PreperationController>();

		enabled = false;
	}

	public void InitMap()
	{


//		worldMapPanel.gameObject.SetActive(true);


//		worldMapCamera.enabled = true;
//		worldMapCamera.GetComponent<AudioListener>().enabled = true;

//		Camera.main.GetComponent<AudioListener>().enabled = false;
//		Camera.main.enabled = false;


		


		List<int> levelsCompleted = GameManager.I.status.levelsCompleted;

		foreach (WorldLevel worldLevel in worldLevels) {
			worldLevel.indicator.Init(this);
		}

		//Open first level
		worldLevelsDict[0].indicator.SetOpen();

		//Show complted levels and paths
		foreach (int levelID in levelsCompleted) {
			worldLevelsDict[levelID].indicator.SetCompleted();
		}

		//Enable Update 
		enabled = true;
	}



	void Update()
	{
//		MouseInput();
//		CameraMovement();
	}

//	private void MouseInput()
//	{
//		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
//		
//		if (hoveredIndicator != null) hoveredIndicator.ResetHover();
//		
//		if (hit.collider != null)
//		{
//			hoveredIndicator = hit.collider.GetComponent<MapIndicator>();
//			hoveredIndicator.MouseHover();
//		}
//
//	}

//	private void CameraMovement(){
//		Vector2 input = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
//
//
//	}



	public void IndicatorClicked(MapIndicator indicator)
	{
		prepCtrl.ShowLevelInfo(worldLevelUsingIndicatorDict[indicator]);
	}

}

[System.Serializable]
public class WorldLevel{
	public int levelID;
	public string levelName;
	public string levelDescription;
	public MapIndicator indicator;
}